using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FuelPricePrediction.Model
{
    internal sealed class AkaryakitFiyati
    {
        public float Year { get; set; }
        public float Month { get; set; }
        public float Day { get; set; }
        public float Price { get; set; }
    }

    internal sealed class TahminSonucu
    {
        [ColumnName("Score")]
        public float Tahmin { get; set; }
    }

    internal sealed class ChronologicalReport
    {
        public long DataStampTicks { get; set; }
        public int TotalRows { get; set; }
        public int TrainRows { get; set; }
        public int TestRows { get; set; }
        public DateTime SeriesStart { get; set; }
        public DateTime SeriesEnd { get; set; }
        public DateTime TrainEnd { get; set; }
        public DateTime TestStart { get; set; }
        public DateTime TestEnd { get; set; }
        public double MeanAbsoluteError { get; set; }
        public double RootMeanSquaredError { get; set; }

        public string ToDisplayString()
        {
            var culture = CultureInfo.GetCultureInfo("tr-TR");
            return string.Format(
                culture,
                "Kronolojik test (son %20, {0:dd.MM.yyyy} – {1:dd.MM.yyyy}): MAE {2:0.00} TL, RMSE {3:0.00} TL."
                + " Tahmin modeli tüm seriyle eğitildi ({4:dd.MM.yyyy} – {5:dd.MM.yyyy}, {6} gün)."
                + " Test, {7:dd.MM.yyyy} tarihine kadar olan günlerle eğitilip sonraki günlerde ölçüldü.",
                TestStart,
                TestEnd,
                MeanAbsoluteError,
                RootMeanSquaredError,
                SeriesStart,
                SeriesEnd,
                TotalRows,
                TrainEnd);
        }
    }

    internal sealed class FuelSeriesTrainer
    {
        public const int Seed = 1;
        public const double TestFraction = 0.2;
        public const string BenzinProduct = "Benzin";
        public const string DizelProduct = "Dizel";

        public ITransformer BenzinModel { get; private set; }
        public ITransformer DizelModel { get; private set; }
        public ChronologicalReport BenzinReport { get; private set; }
        public ChronologicalReport DizelReport { get; private set; }

        public ITransformer ModelFor(string product)
        {
            return product == DizelProduct ? DizelModel : BenzinModel;
        }

        public ChronologicalReport ReportFor(string product)
        {
            return product == DizelProduct ? DizelReport : BenzinReport;
        }

        public void LoadOrTrain(MLContext mlContext)
        {
            var dataPath = Path.Combine(AppContext.BaseDirectory, "data", "fuelPrice.xlsx");
            var benzinPath = Path.Combine(AppContext.BaseDirectory, "model-benzin.zip");
            var dizelPath = Path.Combine(AppContext.BaseDirectory, "model-dizel.zip");
            var metaPath = Path.Combine(AppContext.BaseDirectory, "model.meta.json");

            if (!File.Exists(dataPath))
            {
                throw new FileNotFoundException(
                    "Fiyat serisi bulunamadı. fuelPrice.xlsx, uygulamanın data klasöründe olmalı: " + dataPath,
                    dataPath);
            }

            var stamp = File.GetLastWriteTimeUtc(dataPath).Ticks;
            if (TryLoad(mlContext, benzinPath, dizelPath, metaPath, stamp))
                return;

            var rows = LoadRows(dataPath);
            var pipeline = CreatePipeline(mlContext);
            BenzinModel = TrainProduct(mlContext, pipeline, rows, BenzinProduct, row => row.Benzin, stamp, out var benzinReport);
            DizelModel = TrainProduct(mlContext, pipeline, rows, DizelProduct, row => row.Dizel, stamp, out var dizelReport);
            BenzinReport = benzinReport;
            DizelReport = dizelReport;

            mlContext.Model.Save(BenzinModel, mlContext.Data.LoadFromEnumerable(Project(rows, row => row.Benzin)).Schema, benzinPath);
            mlContext.Model.Save(DizelModel, mlContext.Data.LoadFromEnumerable(Project(rows, row => row.Dizel)).Schema, dizelPath);
            File.WriteAllText(metaPath, JsonConvert.SerializeObject(new ModelBundle
            {
                DataStampTicks = stamp,
                Benzin = BenzinReport,
                Dizel = DizelReport
            }, Formatting.Indented));
        }

        private bool TryLoad(MLContext mlContext, string benzinPath, string dizelPath, string metaPath, long stamp)
        {
            if (!File.Exists(benzinPath) || !File.Exists(dizelPath) || !File.Exists(metaPath))
                return false;

            ModelBundle bundle;
            try
            {
                bundle = JsonConvert.DeserializeObject<ModelBundle>(File.ReadAllText(metaPath));
            }
            catch (JsonException)
            {
                return false;
            }

            if (bundle == null || bundle.DataStampTicks != stamp || bundle.Benzin == null || bundle.Dizel == null)
                return false;

            BenzinModel = mlContext.Model.Load(benzinPath, out _);
            DizelModel = mlContext.Model.Load(dizelPath, out _);
            BenzinReport = bundle.Benzin;
            DizelReport = bundle.Dizel;
            return true;
        }

        private static ITransformer TrainProduct(
            MLContext mlContext,
            IEstimator<ITransformer> pipeline,
            List<DailyPrice> rows,
            string product,
            Func<DailyPrice, float?> price,
            long stamp,
            out ChronologicalReport report)
        {
            var series = Project(rows, price);
            if (series.Count < 2)
                throw new InvalidOperationException(product + " serisinde kronolojik bölme için en az 2 gün gerekli.");

            var testCount = Math.Max(1, (int)Math.Floor(series.Count * TestFraction));
            if (testCount >= series.Count)
                testCount = series.Count - 1;

            var trainRows = series.Take(series.Count - testCount).ToList();
            var testRows = series.Skip(series.Count - testCount).ToList();
            var evaluationModel = pipeline.Fit(mlContext.Data.LoadFromEnumerable(trainRows));
            var scored = evaluationModel.Transform(mlContext.Data.LoadFromEnumerable(testRows));
            var metrics = mlContext.Regression.Evaluate(scored);
            var fullData = mlContext.Data.LoadFromEnumerable(series);
            var model = pipeline.Fit(fullData);

            report = new ChronologicalReport
            {
                DataStampTicks = stamp,
                TotalRows = series.Count,
                TrainRows = trainRows.Count,
                TestRows = testRows.Count,
                SeriesStart = ToDate(series[0]),
                SeriesEnd = ToDate(series[series.Count - 1]),
                TrainEnd = ToDate(trainRows[trainRows.Count - 1]),
                TestStart = ToDate(testRows[0]),
                TestEnd = ToDate(testRows[testRows.Count - 1]),
                MeanAbsoluteError = metrics.MeanAbsoluteError,
                RootMeanSquaredError = metrics.RootMeanSquaredError
            };
            return model;
        }

        private static List<AkaryakitFiyati> Project(List<DailyPrice> rows, Func<DailyPrice, float?> price)
        {
            return rows
                .Where(row => price(row).HasValue)
                .Select(row => new AkaryakitFiyati
                {
                    Year = row.Date.Year,
                    Month = row.Date.Month,
                    Day = row.Date.Day,
                    Price = price(row).Value
                })
                .ToList();
        }

        private static IEstimator<ITransformer> CreatePipeline(MLContext mlContext)
        {
            return mlContext.Transforms.CopyColumns("Label", nameof(AkaryakitFiyati.Price))
                .Append(mlContext.Transforms.Concatenate(
                    "Features",
                    nameof(AkaryakitFiyati.Year),
                    nameof(AkaryakitFiyati.Month),
                    nameof(AkaryakitFiyati.Day)))
                .Append(mlContext.Regression.Trainers.FastTree(new FastTreeRegressionTrainer.Options
                {
                    Seed = Seed,
                    FeatureSelectionSeed = Seed,
                    NumberOfThreads = 1,
                    FeatureFraction = 1,
                    FeatureFractionPerSplit = 1,
                    BaggingSize = 0
                }));
        }

        private static List<DailyPrice> LoadRows(string path)
        {
            var byDate = new SortedDictionary<DateTime, DailyPrice>();

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var workbook = new XSSFWorkbook(fs);
                var sheet = workbook.GetSheetAt(0);
                var header = sheet.GetRow(sheet.FirstRowNum);
                if (header == null)
                    throw new InvalidOperationException("Fiyat dosyasında başlık satırı yok.");

                var columns = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (var c = header.FirstCellNum; c < header.LastCellNum; c++)
                {
                    var name = header.GetCell(c)?.ToString()?.Trim();
                    if (!string.IsNullOrEmpty(name))
                        columns[name] = c;
                }

                if (!columns.ContainsKey("Date") || !columns.ContainsKey("Benzin") || !columns.ContainsKey("Dizel"))
                    throw new InvalidOperationException("Fiyat dosyasında Date, Benzin ve Dizel sütunları olmalı.");

                for (var i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    if (row == null)
                        continue;

                    var dateCell = row.GetCell(columns["Date"]);
                    if (dateCell == null || !TryReadDate(dateCell, out var date))
                        continue;

                    byDate[date] = new DailyPrice
                    {
                        Date = date,
                        Benzin = TryReadPrice(row.GetCell(columns["Benzin"]), out var benzin) ? benzin : (float?)null,
                        Dizel = TryReadPrice(row.GetCell(columns["Dizel"]), out var dizel) ? dizel : (float?)null
                    };
                }
            }

            return byDate.Values.ToList();
        }

        private static bool TryReadDate(ICell cell, out DateTime date)
        {
            date = default;
            if (cell.CellType == CellType.Numeric)
            {
                date = DateUtil.IsCellDateFormatted(cell)
                    ? cell.DateCellValue.GetValueOrDefault().Date
                    : DateTime.FromOADate(cell.NumericCellValue).Date;
                return date.Year > 1900;
            }

            var text = cell.ToString().Trim();
            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var serial))
            {
                date = DateTime.FromOADate(serial).Date;
                return date.Year > 1900;
            }

            var styles = DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal;
            if (DateTime.TryParse(text, CultureInfo.GetCultureInfo("tr-TR"), styles, out date)
                || DateTime.TryParse(text, CultureInfo.InvariantCulture, styles, out date))
            {
                date = date.Date;
                return true;
            }

            return false;
        }

        private static bool TryReadPrice(ICell cell, out float price)
        {
            price = 0;
            if (cell == null || cell.CellType == CellType.Blank)
                return false;
            if (cell.CellType == CellType.Numeric)
            {
                price = (float)cell.NumericCellValue;
                return price > 0;
            }

            var text = cell.ToString().Trim();
            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var invariant)
                || double.TryParse(text, NumberStyles.Float, CultureInfo.GetCultureInfo("tr-TR"), out invariant))
            {
                price = (float)invariant;
                return price > 0;
            }

            return false;
        }

        private static DateTime ToDate(AkaryakitFiyati row)
        {
            return new DateTime((int)row.Year, (int)row.Month, (int)row.Day);
        }

        private sealed class DailyPrice
        {
            public DateTime Date { get; set; }
            public float? Benzin { get; set; }
            public float? Dizel { get; set; }
        }

        private sealed class ModelBundle
        {
            public long DataStampTicks { get; set; }
            public ChronologicalReport Benzin { get; set; }
            public ChronologicalReport Dizel { get; set; }
        }
    }
}
