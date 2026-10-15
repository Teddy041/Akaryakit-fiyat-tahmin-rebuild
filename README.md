# Akaryakit fiyat tahmin rebuild

İstanbul Beşiktaş pompa fiyatından benzin ve dizel tahmini yapan bir Windows Forms uygulaması. Sağdaki kart, Kadıköy için güncel benzin ve dizel listesini gösterir.

## Ekran

Tarih ve ürünü seçin. Büyük rakam o günün tahmini, alttaki çizgi seçilen yılın her günü. Turuncu nokta seçtiğiniz gün. Yeşil hücre, o sütundaki en düşük güncel fiyat.

Seri 1 Ocak 2011 – 15 Ekim 2026. Bu aralığın dışındaki tarihler eğitimde yok.

## Veri

Eğitim dosyası `FuelPricePrediction/data/fuelPrice.xlsx`. Sütunlar `Date`, `Benzin`, `Dizel`. Fiyatlara KDV dahil değildir.

Benzin ve dizel ayrı eğitilir. Dosya değişmediyse kayıtlı model açılır. Son %20 gün, sonraki dönemin kronolojik testidir. Ekrandaki tahmin serinin tamamını kullanır.

Tahmin yalnızca geçmiş fiyatlardaki takvim düzenine bakar. Savaş, ülkeler arası yaptırım, ani vergi kararı veya kur şoku gibi olağan dışı olayları ayrıca ele almaz. Bu tür bir kırılma olduğunda rakam, piyasanın gideceği yerden sapabilir.

## Çalıştırma

.NET Framework 4.7.2 ve Visual Studio gerekir. Çözümü açınca NuGet paketlerini geri yükleyin, sonra derleyin.

Canlı fiyat için proje klasörüne `apikey.local.txt` koyun. Bu dosya depoya girmez:

```
apikey ANAHTARINIZ
```

Güncel liste CollectAPI üzerinden gelir: `turkeyGasoline` ve `turkeyDiesel`, ilçe Kadıköy.
