using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GörselProg
{
    public partial class Kasiyer : Form
    {
        public class Siparis
        {
            public bool Secili { get; set; }
            public string UrunAdi { get; set; }
            public int Adet { get; set; }
            public double Fiyat { get; set; }
            public bool OdendiMi { get; set; }
        }

        Dictionary<string, List<Siparis>> masaSiparisleri = new Dictionary<string, List<Siparis>>();
        List<Siparis> odemeYapilanlar = new List<Siparis>();

        string siparisDosyaYolu = "siparisler.csv";
        string odenenDosyaYolu = "odenenler.csv";

        public Kasiyer()
        {
            InitializeComponent();

            panellogo.Visible = true;
            panelodeme.Visible = false;

            this.Load += Kasiyer_Load;
        }

        private void Kasiyer_Load(object sender, EventArgs e)
        {
            panelodeme.Visible = false;
            panellogo.Visible = true;
            masaLabel.Font = new Font("Segoe UI", 40, FontStyle.Bold);
            labelToplam.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            masaLabel.ForeColor = Color.DarkBlue;
            GridAyarla();
            SiparisleriCsvdenYukle(siparisDosyaYolu);
        }

        private void GridAyarla()
        {
            dataGridSiparis.AutoGenerateColumns = false;
            dataGridSiparis.Columns.Clear();

            var secCol = new DataGridViewCheckBoxColumn();
            secCol.HeaderText = "Seç";
            secCol.DataPropertyName = "Secili";
            dataGridSiparis.Columns.Add(secCol);

            dataGridSiparis.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Ürün Adı",
                DataPropertyName = "UrunAdi"
            });

            dataGridSiparis.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Adet",
                DataPropertyName = "Adet"
            });

            dataGridSiparis.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Fiyat",
                DataPropertyName = "Fiyat"
            });

            dataGridSiparis.CellValueChanged += dataGridSiparis_CellValueChanged;
            dataGridSiparis.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dataGridSiparis.IsCurrentCellDirty)
                    dataGridSiparis.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            dataGridSiparis.CellFormatting += dataGridSiparis_CellFormatting;
        }

        private void SiparisleriCsvdenYukle(string dosyaYolu)
        {
            if (!File.Exists(dosyaYolu)) return;

            using (var reader = new StreamReader(dosyaYolu))
            {
                reader.ReadLine(); // başlık
                while (!reader.EndOfStream)
                {
                    string[] satir = reader.ReadLine().Split(',');
                    if (satir.Length < 4) return; // Satır eksikse atla

                    string masa = satir[0].Trim();
                    string urun = satir[1].Trim();
                    int adet = int.Parse(satir[2]);
                    double fiyat = double.Parse(satir[3]);


                    if (!masaSiparisleri.ContainsKey(masa))
                        masaSiparisleri[masa] = new List<Siparis>();

                    masaSiparisleri[masa].Add(new Siparis
                    {
                        UrunAdi = urun,
                        Adet = adet,
                        Fiyat = fiyat,
                        Secili = false,
                        OdendiMi = false
                    });
                }
            }
        }

        private void dataGridSiparis_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double toplam = 0;
            foreach (DataGridViewRow row in dataGridSiparis.Rows)
            {
                bool secili = Convert.ToBoolean(row.Cells[0].Value);
                if (secili)
                {
                    int adet = Convert.ToInt32(row.Cells[2].Value);
                    double fiyat = Convert.ToDouble(row.Cells[3].Value);
                    toplam += adet * fiyat;
                }
            }
            labelToplam.Text = $"Toplam Tutar: {toplam} ₺";
        }

        private void dataGridSiparis_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridSiparis.Rows[e.RowIndex].DataBoundItem is Siparis siparis)
            {
                if (siparis.OdendiMi)
                {
                    e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Strikeout);
                    e.CellStyle.ForeColor = Color.Gray;
                }
                else
                {
                    e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void Masa_Click(string masaNo)
        {
            panellogo.Visible = false;
            panelodeme.Visible = true;
            masaLabel.Text = masaNo;

            if (masaSiparisleri.ContainsKey(masaNo))
            {
                dataGridSiparis.DataSource = null;
                dataGridSiparis.DataSource = masaSiparisleri[masaNo];
            }
        }

        private void masa1_Click(object sender, EventArgs e) => Masa_Click("MASA 1");
        private void masa2_Click(object sender, EventArgs e) => Masa_Click("MASA 2");
        private void masa3_Click(object sender, EventArgs e) => Masa_Click("MASA 3");
        private void masa4_Click(object sender, EventArgs e) => Masa_Click("MASA 4");
        private void masa5_Click(object sender, EventArgs e) => Masa_Click("MASA 5");
        private void masa6_Click(object sender, EventArgs e) => Masa_Click("MASA 6");
        private void masa7_Click(object sender, EventArgs e) => Masa_Click("MASA 7");
        private void masa8_Click(object sender, EventArgs e) => Masa_Click("MASA 8");
        private void masa9_Click(object sender, EventArgs e) => Masa_Click("MASA 9");
        private void masa10_Click(object sender, EventArgs e) => Masa_Click("MASA 10");
        private void masa11_Click(object sender, EventArgs e) => Masa_Click("MASA 11");
        private void masa12_Click(object sender, EventArgs e) => Masa_Click("MASA 12");

        private void buttonOdemeYap_Click(object sender, EventArgs e)
        {
            string aktifMasa = masaLabel.Text;
            if (!masaSiparisleri.ContainsKey(aktifMasa)) return;

            var mevcutListe = masaSiparisleri[aktifMasa];
            List<Siparis> yeniListe = new List<Siparis>();

            foreach (var siparis in mevcutListe)
            {
                if (siparis.Secili && !siparis.OdendiMi)
                {
                    for (int i = 0; i < siparis.Adet; i++)
                    {
                        Siparis yeni = new Siparis
                        {
                            UrunAdi = siparis.UrunAdi,
                            Adet = 1,
                            Fiyat = siparis.Fiyat,
                            Secili = false,
                            OdendiMi = true
                        };
                        odemeYapilanlar.Add(yeni);
                    }
                    // Ödenen satır listeye eklenmez
                }
                else
                {
                    yeniListe.Add(siparis); // ödenmemişler kalır
                }
            }

            // Yeni liste boşsa masa tamamen temizlenmiş demektir
            if (yeniListe.Count == 0)
            {
                masaSiparisleri.Remove(aktifMasa);
                dataGridSiparis.DataSource = null; // grid boşaltılır
            }
            else
            {
                masaSiparisleri[aktifMasa] = yeniListe;
                dataGridSiparis.DataSource = null;
                dataGridSiparis.DataSource = yeniListe;
            }

            dataGridSiparis.Refresh();
            labelToplam.Text = "Toplam Tutar: 0 ₺";

            // ✅ 1. CSV'de kalan siparişleri güncelle
            var kalanlar = new List<string> { "Masa,UrunAdi,Adet,Fiyat" };
            foreach (var masa in masaSiparisleri)
            {
                foreach (var s in masa.Value)
                {
                    kalanlar.Add($"{masa.Key},{s.UrunAdi},{s.Adet},{s.Fiyat}");
                }
            }
            File.WriteAllLines(siparisDosyaYolu, kalanlar);

            // ✅ 2. Ödenenleri odenenler.csv'ye ekle
            if (!File.Exists(odenenDosyaYolu))
                File.AppendAllText(odenenDosyaYolu, "Masa,UrunAdi,Adet,Fiyat\n");

            foreach (var s in odemeYapilanlar)
            {
                File.AppendAllText(odenenDosyaYolu, $"{aktifMasa},{s.UrunAdi},{s.Adet},{s.Fiyat}\n");
            }

            odemeYapilanlar.Clear();
        }


        private void buttonYoneticiGoster_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (File.Exists(odenenDosyaYolu))
            {
                var satirlar = File.ReadAllLines(odenenDosyaYolu);
                foreach (var satir in satirlar.Skip(1))
                    sb.AppendLine(satir);
            }
            MessageBox.Show(sb.ToString(), "Yöneticiye Giden Siparişler");
        }
    }
}
