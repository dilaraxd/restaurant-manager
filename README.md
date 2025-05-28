# 🧾 Restoran Sipariş Yönetim Sistemi

## 📌 Proje Tanımı

Bu masaüstü uygulaması; restoran ortamında sipariş alma, hazırlama, teslim etme ve ödeme işlemlerinin tümünü tek bir sistem üzerinden yönetmek amacıyla geliştirilmiştir. Kullanıcı rollerine göre (Garson, Mutfak Görevlisi, Kasiyer, Yönetici) farklı arayüzler sunularak iş akışı hızlandırılmış, hata oranı azaltılmış ve müşteri memnuniyeti artırılmıştır.

## 🎯 Temel Özellikler

- 4 farklı personel rolüne göre giriş ve ekran yönlendirme
- Masa bazlı sipariş yönetimi
- Sipariş iptal ve güncelleme işlemleri
- Mutfak ile anlık sipariş takibi
- Ödeme türü seçimi (Nakit/Kredi Kartı)
- Eksik ürün bildirimi ve yönetimi
- Günlük ciro raporlama

## 💡 Kullanılan Teknolojiler

- C# Windows Forms (.NET Framework)
- Visual Studio IDE
- CSV dosya okuma/yazma (veri tabanı yerine)
- Panel, DataGridView, CheckedListBox, NumericUpDown gibi WinForms bileşenleri

## 🧰 Kurulum

1. **Projeyi Klonlayın:**
   ```bash
   git clone https://github.com/dilaraxd/restaurant-manager.git
   ```

2. **Visual Studio ile Açın:**
   - `restaurant-manager.sln` dosyasını çift tıklayarak açın.

3. **Gerekli CSV Dosyaları:**
   - `siparisler.csv`, `odenenler.csv`, `ciro.csv` dosyalarının `bin/Debug` klasöründe olduğundan emin olun.
   - Örnek `siparisler.csv` içeriği:
     ```
     Masa,UrunAdi,Adet,Fiyat
     MASA 8,Kola,1,30
     MASA 11,Pizza,2,80
     ```

4. **Projeyi Derleyip Çalıştırın.**

## 🖥️ Ekran Görüntüleri

- **Garson Arayüzü**: Masa seçimi, sipariş girişi
- **Mutfak Paneli**: Anlık sipariş görüntüleme ve "hazır" bildirimi
- **Kasiyer Paneli**: Sipariş ödemesi alma ve ödendi olarak işaretleme
- **Yönetici Paneli**: Ciro görüntüleme, eksik ürün yönetimi

## 👩‍💻 Görev Dağılımı

| Ekip Üyesi       | Sorumluluklar                                                                                   |
|------------------|-------------------------------------------------------------------------------------------------|
| **Berra Akman**  |  Kasiyer arayüzü, ödeme alma, ödeme bilgilerini iletme ve günlük ciro hesaplama, CSV işlemleri       |
| **Beyza Nur Postlu** | Garson arayüzü, sipariş hazırlama ve bildirim sistemleri , CSV işlemleri    |
| **Dilara Cömert**| Yönetici ve Mutfak Görevlisi arayüzü,Personel giriş ekranı, eksik malzeme ve sipariş hazırlama bildirim sistemleri, CSV işlemleri    |

## 📂 Klasör Yapısı

```
restaurant-manager/
├── bin/Debug/
│   ├── siparisler.csv
│   ├── odenenler.csv
│   └── ciro.csv
├── GörselProg/
│   ├── Garson.cs
│   ├── Kasiyer.cs
│   ├── Mutfak.cs
│   ├── Yonetici.cs
│   └── Ortak sınıflar.cs
├── .vs/
├── .gitignore
└── README.md
```

## 🔁 İş Akışı (Özet)

1. **Garson** siparişi alır, sistem üzerinden girer.
2. **Mutfak görevlisi** siparişi hazırlar ve sisteme "hazır" olarak girer.
3. **Garson** siparişi teslim eder ve işaretler.
4. **Kasiyer** ödeme alır ve yöneticiye raporlar.
5. **Yönetici** eksik ürün ve ciro yönetimini yapar.



## 🤝 Katkı Sağlamak

> Bu proje bir üniversite ödevi kapsamında geliştirilmiştir. Ancak öneri ve geri bildirimlere açıktır. 

## 🏫 Akademik Bilgiler

- **Üniversite**: Bursa Uludağ Üniversitesi
- **Bölüm**: Bilgisayar Mühendisliği
- **Ders**: Görsel Programlama
- **Dönem**: 2024-2025 Bahar
- **Grup**: 3
