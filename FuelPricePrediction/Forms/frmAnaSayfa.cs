using FuelPricePrediction.Model;
using Microsoft.ML;
using Microsoft.ML.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FuelPricePrediction.Forms
{
    public partial class frmAnaSayfa : Form
    {
        private static readonly Color Ink = Color.FromArgb(32, 42, 51);
        private static readonly Color Muted = Color.FromArgb(96, 108, 120);
        private static readonly Color Line = Color.FromArgb(27, 94, 122);
        private static readonly Color Mark = Color.FromArgb(184, 92, 46);
        private static readonly Color Warning = Color.FromArgb(140, 84, 24);
        private static readonly Color Cheapest = Color.FromArgb(226, 240, 230);

        private readonly string apiKey = LoadApiKey();

        private static string LoadApiKey()
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                var candidate = Path.Combine(dir.FullName, "apikey.local.txt");
                if (File.Exists(candidate))
                {
                    var text = File.ReadAllText(candidate).Trim();
                    if (text.Length > 0)
                        return text;
                }

                dir = dir.Parent;
            }

            return string.Empty;
        }

        private readonly MLContext mlContext;
        private readonly FuelSeriesTrainer trainer = new FuelSeriesTrainer();
        private readonly CultureInfo culture = CultureInfo.GetCultureInfo("tr-TR");
        private string loadError;

        public frmAnaSayfa()
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            InitializeComponent();
            DoubleBuffered = true;
            comboBoxCities.SelectedIndex = 0;
            StyleGrid();
            PrepareChart();

            mlContext = new MLContext(seed: FuelSeriesTrainer.Seed);
            try
            {
                trainer.LoadOrTrain(mlContext);
            }
            catch (Exception ex)
            {
                loadError = ex.Message;
            }

            if (cmbProduct.SelectedIndex < 0)
                cmbProduct.SelectedIndex = 0;
            cmbProduct.SelectedIndexChanged += cmbProduct_SelectedIndexChanged;
            Load += frmAnaSayfa_Load;
        }

        private string SelectedProduct
        {
            get { return (cmbProduct.SelectedItem as string) == FuelSeriesTrainer.DizelProduct ? FuelSeriesTrainer.DizelProduct : FuelSeriesTrainer.BenzinProduct; }
        }

        private void frmAnaSayfa_Load(object sender, EventArgs e)
        {
            if (loadError != null)
            {
                lblMetrics.Text = loadError;
                lblResult.Text = "Seri yüklenemedi. Tahmin kullanılamaz.";
                btnPredict.Enabled = false;
                MessageBox.Show(loadError, "Veri yüklenemedi");
                return;
            }

            ShowMetrics();
            ShowPrediction();
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadError != null || trainer.BenzinModel == null)
                return;

            ShowMetrics();
            ShowPrediction();
        }

        private void ShowMetrics()
        {
            var report = trainer.ReportFor(SelectedProduct);
            lblMetrics.Text = string.Format(
                culture,
                "{0}, İstanbul Beşiktaş, KDV dahil. Seri {1:dd.MM.yyyy} – {2:dd.MM.yyyy}, {3} gün.\r\nSonraki dönem testi ({4:dd.MM.yyyy} – {5:dd.MM.yyyy}): ortalama hata {6:0.00} TL, sapma {7:0.00} TL.",
                SelectedProduct,
                report.SeriesStart,
                report.SeriesEnd,
                report.TotalRows,
                report.TestStart,
                report.TestEnd,
                report.MeanAbsoluteError,
                report.RootMeanSquaredError);
        }

        private void btnPredict_Click(object sender, EventArgs e)
        {
            if (trainer.ModelFor(SelectedProduct) == null)
            {
                MessageBox.Show(loadError ?? "Model hazır değil.");
                return;
            }

            ShowPrediction();
        }

        private void ShowPrediction()
        {
            var model = trainer.ModelFor(SelectedProduct);
            if (model == null)
                return;

            var userDate = dtpDate.Value.Date;
            var predictor = mlContext.Model.CreatePredictionEngine<AkaryakitFiyati, TahminSonucu>(model);
            var prediction = predictor.Predict(new AkaryakitFiyati
            {
                Year = userDate.Year,
                Month = userDate.Month,
                Day = userDate.Day
            });

            lblPriceValue.Text = string.Format(culture, "{0:0.00} TL", prediction.Tahmin);
            lblResult.Text = string.Format(culture, "{0:dd MMMM yyyy} {1} tahmini", userDate, SelectedProduct.ToLower(culture));

            var report = trainer.ReportFor(SelectedProduct);
            if (userDate < report.SeriesStart)
            {
                lblRangeNote.ForeColor = Warning;
                lblRangeNote.Text = string.Format(culture, "Seri {0:dd.MM.yyyy} tarihinde başlıyor. Bu günden öncesi eğitimde yok.", report.SeriesStart);
            }
            else if (userDate > report.SeriesEnd)
            {
                lblRangeNote.ForeColor = Warning;
                lblRangeNote.Text = string.Format(culture, "Seri {0:dd.MM.yyyy} tarihinde bitiyor. Bu günden sonrası eğitimde yok.", report.SeriesEnd);
            }
            else
            {
                lblRangeNote.ForeColor = Muted;
                lblRangeNote.Text = "Seçilen gün grafikte turuncu nokta ile işaretli.";
            }

            ShowYearlyPredictions(userDate, predictor);
        }

        private void ShowYearlyPredictions(DateTime selected, PredictionEngine<AkaryakitFiyati, TahminSonucu> predictor)
        {
            var predictions = new List<KeyValuePair<DateTime, float>>();
            for (int month = 1; month <= 12; month++)
            {
                for (int day = 1; day <= DateTime.DaysInMonth(selected.Year, month); day++)
                {
                    var date = new DateTime(selected.Year, month, day);
                    var prediction = predictor.Predict(new AkaryakitFiyati { Year = date.Year, Month = date.Month, Day = date.Day });
                    predictions.Add(new KeyValuePair<DateTime, float>(date, prediction.Tahmin));
                }
            }

            chartPredictions.Series.Clear();

            var series = new Series("Günlük tahmin")
            {
                ChartType = SeriesChartType.Line,
                XValueType = ChartValueType.Date,
                Color = Line,
                BorderWidth = 2,
                ToolTip = "#VALX{dd.MM.yyyy}: #VALY{0.00} TL"
            };

            foreach (var prediction in predictions)
                series.Points.AddXY(prediction.Key, prediction.Value);

            var mark = new Series("Seçilen gün")
            {
                ChartType = SeriesChartType.Point,
                XValueType = ChartValueType.Date,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 10,
                MarkerColor = Mark,
                Color = Mark,
                ToolTip = "#VALX{dd.MM.yyyy}: #VALY{0.00} TL"
            };
            var selectedPoint = predictions.First(pair => pair.Key.Date == selected.Date);
            mark.Points.AddXY(selectedPoint.Key, selectedPoint.Value);

            chartPredictions.Series.Add(series);
            chartPredictions.Series.Add(mark);

            var area = chartPredictions.ChartAreas[0];
            double min = predictions.Min(pair => pair.Value);
            double max = predictions.Max(pair => pair.Value);
            FitPriceAxis(area.AxisY, min, max);
            area.AxisX.Minimum = new DateTime(selected.Year, 1, 1).ToOADate();
            area.AxisX.Maximum = new DateTime(selected.Year, 12, 31).ToOADate();
            area.AxisY.Title = "TL / litre";

            chartPredictions.Invalidate();
        }

        private static void FitPriceAxis(Axis axis, double min, double max)
        {
            double span = Math.Max(max - min, 0.4);
            double low = Math.Max(0, min - span * 0.35);
            double high = max + span * 0.35;
            double range = Math.Max(0.4, high - low);
            double step = range <= 1.2 ? 0.2 : range <= 3 ? 0.5 : range <= 8 ? 1 : 2;
            axis.Minimum = Math.Floor(low / step) * step;
            axis.Maximum = Math.Ceiling(high / step) * step;
            if (axis.Maximum <= axis.Minimum)
                axis.Maximum = axis.Minimum + step;
            axis.Interval = step;
            axis.LabelStyle.Format = step < 1 ? "0.00" : "0";
        }

        private void PrepareChart()
        {
            chartPredictions.AntiAliasing = AntiAliasingStyles.All;
            chartPredictions.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            chartPredictions.BackColor = Color.White;
            chartPredictions.Palette = ChartColorPalette.None;

            var area = new ChartArea("Fiyat");
            area.BackColor = Color.White;
            area.AxisX.Title = "Ay";
            area.AxisX.TitleFont = new Font("Segoe UI", 9F);
            area.AxisX.TitleForeColor = Muted;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8.5F);
            area.AxisX.LabelStyle.ForeColor = Ink;
            area.AxisX.LabelStyle.Format = "MMM";
            area.AxisX.IntervalType = DateTimeIntervalType.Months;
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.LineColor = Color.FromArgb(210, 216, 222);
            area.AxisX.MajorTickMark.LineColor = Color.FromArgb(210, 216, 222);

            area.AxisY.Title = "TL / litre";
            area.AxisY.TitleFont = new Font("Segoe UI", 9F);
            area.AxisY.TitleForeColor = Muted;
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8.5F);
            area.AxisY.LabelStyle.ForeColor = Ink;
            area.AxisY.LabelStyle.Format = "0.00";
            area.AxisY.LabelAutoFitStyle = LabelAutoFitStyles.None;
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(226, 232, 236);
            area.AxisY.LineColor = Color.FromArgb(210, 216, 222);
            area.AxisY.MajorTickMark.LineColor = Color.FromArgb(210, 216, 222);
            area.AxisY.IsStartedFromZero = false;

            chartPredictions.ChartAreas.Add(area);

            var legend = new Legend
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Near,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Ink,
                LegendStyle = LegendStyle.Row
            };
            chartPredictions.Legends.Add(legend);
        }

        private void StyleGrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 250);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Ink;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dataGridView1.DefaultCellStyle.ForeColor = Ink;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(226, 236, 242);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Ink;
            dataGridView1.DefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async void btnGetir_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    MessageBox.Show("Canlı fiyat için apikey.local.txt dosyasına CollectAPI anahtarını yazın.");
                    return;
                }

                string selectedCity = (comboBoxCities.SelectedItem as string ?? string.Empty).ToLower(culture);
                if (string.IsNullOrEmpty(selectedCity))
                {
                    MessageBox.Show("Lütfen bir il seçin.");
                    return;
                }

                btnGetir.Enabled = false;
                lblLiveEmpty.Text = "Fiyatlar getiriliyor…";
                lblLiveEmpty.Visible = true;

                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
                var dieselUrl = $"https://api.collectapi.com/gasPrice/turkeyDiesel?district=kadikoy&city={selectedCity}";
                var gasolineUrl = $"https://api.collectapi.com/gasPrice/turkeyGasoline?district=kadikoy&city={selectedCity}";
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(20);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("authorization", apiKey);
                    var dieselTask = client.GetAsync(dieselUrl);
                    var gasolineTask = client.GetAsync(gasolineUrl);
                    await Task.WhenAll(dieselTask, gasolineTask);

                    var dieselResponse = dieselTask.Result;
                    var gasolineResponse = gasolineTask.Result;
                    var dieselBody = await dieselResponse.Content.ReadAsStringAsync();
                    var gasolineBody = await gasolineResponse.Content.ReadAsStringAsync();
                    if (!dieselResponse.IsSuccessStatusCode && !gasolineResponse.IsSuccessStatusCode)
                    {
                        lblLiveEmpty.Text = "Fiyatlar alınamadı.\r\nDizel " + (int)dieselResponse.StatusCode
                            + ", benzin " + (int)gasolineResponse.StatusCode;
                        MessageBox.Show("API'den benzin ve dizel alınamadı.");
                        return;
                    }

                    var byBrand = new Dictionary<string, PumpPrice>(StringComparer.Create(culture, true));
                    string updatedOn = null;
                    var notes = new List<string>();
                    if (dieselResponse.IsSuccessStatusCode)
                        MergeFuel(byBrand, dieselBody, "dizel", (row, price) => row.Dizel = price, ref updatedOn);
                    else
                        notes.Add("Dizel listesi alınamadı.");

                    if (gasolineResponse.IsSuccessStatusCode)
                        MergeFuel(byBrand, gasolineBody, "benzin", (row, price) => row.Benzin = price, ref updatedOn);
                    else
                        notes.Add("Benzin listesi alınamadı.");

                    var prices = byBrand.Values
                        .OrderBy(row => row.Marka, StringComparer.Create(culture, false))
                        .ToList();
                    if (prices.Count == 0)
                    {
                        lblLiveEmpty.Text = "Bu sorgu için pompa fiyatı dönmedi.";
                        MessageBox.Show("API yanıtında benzin veya dizel fiyatı yok.");
                        return;
                    }

                    dataGridView1.DataSource = prices;
                    ApplyLiveColumns();
                    HighlightCheapest("Benzin");
                    HighlightCheapest("Dizel");
                    dataGridView1.ClearSelection();
                    lblLiveEmpty.Visible = false;
                    var hint = string.IsNullOrWhiteSpace(updatedOn)
                        ? "Yeşil hücre, o sütundaki en düşük fiyat."
                        : "Son güncelleme " + updatedOn + ". Yeşil hücre, o sütundaki en düşük fiyat.";
                    if (notes.Count > 0)
                        hint = string.Join(" ", notes.ToArray()) + " " + hint;
                    lblLiveHint.Text = hint;
                }
            }
            catch (Exception ex)
            {
                lblLiveEmpty.Text = "Fiyatlar alınamadı.";
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                btnGetir.Enabled = true;
            }
        }

        private void ApplyLiveColumns()
        {
            if (dataGridView1.Columns.Contains("Marka"))
            {
                dataGridView1.Columns["Marka"].HeaderText = "Marka";
                dataGridView1.Columns["Marka"].DisplayIndex = 0;
                dataGridView1.Columns["Marka"].FillWeight = 150;
            }

            if (dataGridView1.Columns.Contains("Benzin"))
            {
                dataGridView1.Columns["Benzin"].HeaderText = "Benzin";
                dataGridView1.Columns["Benzin"].DisplayIndex = 1;
                dataGridView1.Columns["Benzin"].FillWeight = 70;
                dataGridView1.Columns["Benzin"].DefaultCellStyle.Format = "N2";
                dataGridView1.Columns["Benzin"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dataGridView1.Columns.Contains("Dizel"))
            {
                dataGridView1.Columns["Dizel"].HeaderText = "Dizel";
                dataGridView1.Columns["Dizel"].DisplayIndex = 2;
                dataGridView1.Columns["Dizel"].FillWeight = 70;
                dataGridView1.Columns["Dizel"].DefaultCellStyle.Format = "N2";
                dataGridView1.Columns["Dizel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private static void MergeFuel(
            IDictionary<string, PumpPrice> byBrand,
            string json,
            string priceField,
            Action<PumpPrice, decimal> assign,
            ref string updatedOn)
        {
            var root = JObject.Parse(json);
            if (root.Value<bool?>("success") != true)
                return;

            var stamp = root.Value<string>("lastupdate");
            if (!string.IsNullOrWhiteSpace(stamp))
                updatedOn = stamp;

            foreach (var item in root["result"] ?? Enumerable.Empty<JToken>())
            {
                var price = ReadPrice(item[priceField]);
                if (price <= 0)
                    continue;

                var name = (item.Value<string>("marka") ?? string.Empty).Trim();
                if (name.Length == 0 || IsStaleBrand(name))
                    continue;

                if (!byBrand.TryGetValue(name, out var row))
                {
                    row = new PumpPrice { Marka = name };
                    byBrand[name] = row;
                }

                assign(row, price);
            }
        }

        private static bool IsStaleBrand(string name)
        {
            var compact = name.Replace(" ", string.Empty).Replace("İ", "i").Replace("ı", "i").ToLowerInvariant();
            return compact == "7kita";
        }

        private static decimal ReadPrice(JToken token)
        {
            if (token == null || token.Type == JTokenType.Null)
                return 0;
            if (token.Type == JTokenType.Integer || token.Type == JTokenType.Float)
                return token.Value<decimal>();

            var text = token.ToString().Trim();
            if (text.Length == 0 || text == "-")
                return 0;
            if (decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                return value;
            if (decimal.TryParse(text, NumberStyles.Float, CultureInfo.GetCultureInfo("tr-TR"), out value))
                return value;
            return 0;
        }

        private void HighlightCheapest(string columnName)
        {
            if (!dataGridView1.Columns.Contains(columnName))
                return;

            int best = -1;
            decimal bestPrice = decimal.MaxValue;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (!(dataGridView1.Rows[i].Cells[columnName].Value is decimal price) || price <= 0)
                    continue;

                if (price < bestPrice)
                {
                    bestPrice = price;
                    best = i;
                }
            }

            if (best < 0)
                return;

            var cell = dataGridView1.Rows[best].Cells[columnName];
            cell.Style.BackColor = Cheapest;
            cell.Style.SelectionBackColor = Color.FromArgb(210, 230, 214);
        }

        private sealed class PumpPrice
        {
            public string Marka { get; set; }
            public decimal? Benzin { get; set; }
            public decimal? Dizel { get; set; }
        }
    }
}
