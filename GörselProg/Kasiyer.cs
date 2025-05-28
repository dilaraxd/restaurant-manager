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
        Dictionary<Control, Rectangle> originalRects = new Dictionary<Control, Rectangle>();
        Size originalFormSize;

        string siparisDosyaYolu = "siparisler.csv";
        string odenenDosyaYolu = "odenenler.csv";

        public Kasiyer()
        {
            InitializeComponent();
            this.Size = new Size(1350, 700); // Geniş başlangıç boyutu
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;

            this.Load += new EventHandler(Kasiyer_Load);
            this.Resize += new EventHandler(Kasiyer_Resize);
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
            originalFormSize = this.Size;
            SaveControlBounds(this);
        }
        private void Kasiyer_Resize(object sender, EventArgs e)
        {
            float xRatio = (float)this.Width / originalFormSize.Width;
            float yRatio = (float)this.Height / originalFormSize.Height;

            foreach (Control ctrl in originalRects.Keys)
            {
                Rectangle original = originalRects[ctrl];
                ctrl.Left = (int)(original.Left * xRatio);
                ctrl.Top = (int)(original.Top * yRatio);
                ctrl.Width = (int)(original.Width * xRatio);
                ctrl.Height = (int)(original.Height * yRatio);
            }
        }
        private void SaveControlBounds(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                originalRects[ctrl] = ctrl.Bounds;
                if (ctrl.HasChildren)
                    SaveControlBounds(ctrl);
            }
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
                reader.ReadLine(); // başlık satırını atla

                while (!reader.EndOfStream)
                {
                    string[] satir = reader.ReadLine().Split(',');
                    if (satir.Length < 5) continue; // "Durum" dahil en az 5 alan olmalı

                    string masa = satir[0].Trim();
                    string urun = satir[1].Trim();
                    int adet = int.Parse(satir[2]);
                    double fiyat = double.Parse(satir[3]);
                    string durum = satir[4].Trim();

                    if (!masaSiparisleri.ContainsKey(masa))
                        masaSiparisleri[masa] = new List<Siparis>();

                    masaSiparisleri[masa].Add(new Siparis
                    {
                        UrunAdi = urun,
                        Adet = adet,
                        Fiyat = fiyat,
                        Secili = false,
                        OdendiMi = (durum == "Teslim Edildi")
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
            panellogo.Visible = !panellogo.Visible;
            panelodeme.Visible = true;
            masaLabel.Text = masaNo;

            string dosyaYolu = "siparisler.csv";
            if (!File.Exists(dosyaYolu))
            {
                MessageBox.Show("Sipariş dosyası bulunamadı.");
                return;
            }

            string aktifMasa = masaLabel.Text.Trim(); // Örn: "MASA 5"
            List<Siparis> teslimEdilenSiparisler = new List<Siparis>();

            using (var reader = new StreamReader(dosyaYolu))
            {
                reader.ReadLine(); // Başlık satırını atla

                while (!reader.EndOfStream)
                {
                    string[] satir = reader.ReadLine().Split(',');
                    if (satir.Length < 5) continue;

                    string masa = satir[0].Trim();
                    string urun = satir[1].Trim();
                    int adet = int.Parse(satir[2]);
                    double fiyat = double.Parse(satir[3]);
                    string durum = satir[4].Trim();

                    if (masa == aktifMasa && durum == "Teslim Edildi")
                    {
                        teslimEdilenSiparisler.Add(new Siparis
                        {
                            UrunAdi = urun,
                            Adet = adet,
                            Fiyat = fiyat,
                            Secili = false,
                            OdendiMi = true
                        });
                    }
                }
            }


            dataGridSiparis.DataSource = null;
            dataGridSiparis.DataSource = teslimEdilenSiparisler;
            labelToplam.Text = $"{aktifMasa} için teslim edilen siparişler yüklendi.";
        
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
                }
                else
                {
                    yeniListe.Add(siparis); // ödenmemişler kalır
                }
            }

            if (yeniListe.Count == 0)
            {
                masaSiparisleri.Remove(aktifMasa);
                dataGridSiparis.DataSource = null;
            }
            else
            {
                masaSiparisleri[aktifMasa] = yeniListe;
                dataGridSiparis.DataSource = null;
                dataGridSiparis.DataSource = yeniListe;
            }

            dataGridSiparis.Refresh();
            labelToplam.Text = "Toplam Tutar: 0 ₺";

            // ✅ 1. Tüm siparişleri başlık dahil csv'ye yazacağız
            var tumSatirlar = new List<string> { "Masa,UrunAdi,Adet,Fiyat,Durum" };

            // ➕ Hazırlanıyor durumundakileri yaz
            foreach (var masa in masaSiparisleri)
            {
                foreach (var s in masa.Value)
                {
                    tumSatirlar.Add($"{masa.Key},{s.UrunAdi},{s.Adet},{s.Fiyat},Hazırlanıyor");
                }
            }

            // ➕ Yeni ödenenleri yaz (teslim edildi)
            foreach (var s in odemeYapilanlar)
            {
                tumSatirlar.Add($"{aktifMasa},{s.UrunAdi},{s.Adet},{s.Fiyat},Teslim Edildi");
            }

            // ✍️ siparisler.csv dosyasını güncelle
            File.WriteAllLines(siparisDosyaYolu, tumSatirlar);

            // ✅ 2. odenenler.csv güncelle (ekleme şeklinde)
            if (!File.Exists("odenenler.csv"))
                File.AppendAllText("odenenler.csv", "Masa,UrunAdi,Adet,Fiyat\n");

            foreach (var s in odemeYapilanlar)
            {
                File.AppendAllText("odenenler.csv", $"{aktifMasa},{s.UrunAdi},{s.Adet},{s.Fiyat}\n");
            }

            odemeYapilanlar.Clear();
        }



        private void buttonYoneticiGoster_Click(object sender, EventArgs e)
        {
            string aktifMasa = masaLabel.Text.Trim();
            StringBuilder sb = new StringBuilder();

            if (File.Exists(odenenDosyaYolu))
            {
                var satirlar = File.ReadAllLines(odenenDosyaYolu);
                foreach (var satir in satirlar.Skip(1)) // başlığı atla
                {
                    string[] parcalar = satir.Split(',');
                    if (parcalar.Length >= 4 && parcalar[0].Trim() == aktifMasa)
                    {
                        sb.AppendLine(satir);
                    }
                }
            }

            if (sb.Length == 0)
                MessageBox.Show($"{aktifMasa} için yöneticiye gönderilmiş sipariş bulunamadı.", "Bilgi");
            else
                MessageBox.Show(sb.ToString(), $"{aktifMasa} - Yöneticiye Giden Siparişler");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Application.OpenForms["Form1"]?.Show();
            this.Close(); // veya this.Hide();

        }
    }
}
