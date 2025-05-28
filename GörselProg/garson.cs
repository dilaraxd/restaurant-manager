using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GörselProg
{
    public partial class garson : Form
    {
        public static Label MutfakBildirimiLabel;

        Dictionary<Control, Rectangle> originalRects = new Dictionary<Control, Rectangle>();
        Size originalFormSize;
        Dictionary<string, List<Siparis>> masaSiparisleri = new Dictionary<string, List<Siparis>>();
        Dictionary<string, double> urunFiyatlari = new Dictionary<string, double>()
        {
            {"Pesto Soslu Makarna",220},
{"Bolognese Soslu Makarna",210},
{"Alfredo Makarna",250},
{"Cheese Burger",300},
{"Klasik Burger",280},
{"Trüflü Cheddar Burger",320},
{"Klasik Pizza",300},
{"Margarita Pizza",280},
{"4 Peynirli Pizza",310},
{"Etli Pizza",350},
{"Funghi Pizza",330},
{"Tavuklu Barbekülü Pizza",320},
{"Kremalı Mantar Soslu Tavuk",320},
{"Sweet Chili Soslu Tavuk",340},
{"Izgara Tavuk Pirzola",300},
{"Bonfile Lokum",400},
{"Et Fajita",450},
{"Tavuklu Sezar Salata",250},
{"Biftek Salata",300},
{"Akdeniz Salatası",200},
{"Tavuklu Wrap",250},
{"Etli Wrap",270},
{"Mantı",420}


        };

        string secilenMasa = "";
        System.Windows.Forms.Button oncekiSecilenMasa = null;

        public class Siparis
        {
            public string UrunAdi { get; set; }
            public int Adet { get; set; }
            public double Fiyat { get; set; }
            public bool TeslimEdildi { get; set; }
        }
        public garson()
        {
            InitializeComponent();
            this.Size = new Size(1200, 800); // Geniş başlangıç boyutu
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;

            this.Load += new EventHandler(garson_Load);
            this.Resize += new EventHandler(garson_Resize);
        
        }
      
        private void garson_Resize(object sender, EventArgs e)
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
        private void SiparisleriCsvyeKaydet()
        {
            string dosyaYolu = "tumSiparisler.csv";
            using (StreamWriter sw = new StreamWriter(dosyaYolu, false, Encoding.UTF8))
            {
                // Başlık yaz
                sw.WriteLine("Masa,UrunAdi,Adet,Fiyat");

                foreach (var masa in masaSiparisleri)
                {
                    foreach (var siparis in masa.Value)
                    {
                        sw.WriteLine($"{masa.Key},{siparis.UrunAdi},{siparis.Adet},{siparis.Fiyat}");
                    }
                }
            }
        }
        private void HazirlanıyorDosyasiniGoster()
        {
            string dosyaYolu = "hazırlananlar.csv";

            if (File.Exists(dosyaYolu))
            {
                string[] satirlar = File.ReadAllLines(dosyaYolu);
                StringBuilder sb = new StringBuilder();
                foreach (string satir in satirlar)
                {
                    // Format: "MASA 6,Etli Pizza x2,28.05.2025 23:06:54"
                    string[] parcalar = satir.Split(',');

                    if (parcalar.Length >= 3)
                    {
                        string masa = parcalar[0];
                        string urun = parcalar[1];
                      

                        sb.AppendLine($"{masa}, Hazırlanıyor: {urun}");
                    }
                }


                lblMutfakBildirimi.Text = sb.ToString();
            }
            else
            {
                lblMutfakBildirimi.Text = "hazirlaniyor.csv dosyası bulunamadı.";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            List<string> secilenler = new List<string>();
            foreach (var item in cblMenu.CheckedItems)
            {
                secilenler.Add(item.ToString());
            }

         Mutfak frm2 = new Mutfak(secilenler); // Listeyi gönder
            
        

    }
        private void Masa_Click(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
            panel2.Visible = !panel2.Visible;
            panel3.Visible = !panel2.Visible;
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            if (btn == null) return;

            secilenMasa = btn.Text;

            // Önceki masa görünümünü sıfırla
            if (oncekiSecilenMasa != null)
            {
                oncekiSecilenMasa.BackColor = SystemColors.Control;
                oncekiSecilenMasa.ForeColor = Color.Black;
                oncekiSecilenMasa.FlatStyle = FlatStyle.Standard;
            }

            // Yeni masa işaretle
            btn.BackColor = Color.DarkGreen;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Popup;
            oncekiSecilenMasa = btn;

            panel1.Visible = true;
            cblMenu.Visible = true;
            lblSecilenMasa.Text = "Seçilen Masa: " + secilenMasa;
            dgvSiparisler.Visible = false;
            lblToplamTutar.Visible = false;
            btnSiparisEkle.Visible = true;
            SiparisleriYenile();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SiparisleriCsvyeKaydet();

            if (string.IsNullOrEmpty(secilenMasa))
            {
                MessageBox.Show("Lütfen önce masa seçiniz.");
                return;
            }

            if (cblMenu.CheckedItems.Count == 0)
            {
                // 👇 Sadece gerçekten kullanıcı bu butona basarak çağırdıysa göster
                if (ActiveControl == btnSiparisEkle)
                {
                    MessageBox.Show("Lütfen en az bir ürün seçiniz.");
                }
                return;
            }

            int adet = (int)nudAdet.Value;

            if (!masaSiparisleri.ContainsKey(secilenMasa))
                masaSiparisleri[secilenMasa] = new List<Siparis>();

            var liste = masaSiparisleri[secilenMasa];

            foreach (var item in cblMenu.CheckedItems)
            {
                string urun = item.ToString();
                double fiyat = urunFiyatlari.ContainsKey(urun) ? urunFiyatlari[urun] : 0;

                var mevcut = liste.FirstOrDefault(s => s.UrunAdi == urun);
                if (mevcut != null)
                    mevcut.Adet += adet;
                else
                    liste.Add(new Siparis { UrunAdi = urun, Adet = adet, Fiyat = fiyat });
            }

            cblMenu.ClearSelected();
            for (int i = 0; i < cblMenu.Items.Count; i++)
                cblMenu.SetItemChecked(i, false);

            nudAdet.Visible = false;
            btnSiparisEkle.Visible = false;
            dgvSiparisler.Visible = true;
            lblToplamTutar.Visible = true;
            btnSiparisEkle.Visible = true;
            SiparisleriYenile();

        }
        private void SiparisleriYenile()
        {
            dgvSiparisler.Rows.Clear();

            if (masaSiparisleri.ContainsKey(secilenMasa))
            {
                foreach (var siparis in masaSiparisleri[secilenMasa])
                {
                    dgvSiparisler.Rows.Add(
                        siparis.UrunAdi,
                        siparis.Adet,
                        (siparis.Adet * siparis.Fiyat).ToString("C2")
                    );
                }
            }
            double toplamTutar = 0;

            dgvSiparisler.Rows.Clear();

            if (masaSiparisleri.ContainsKey(secilenMasa))
            {
                foreach (var siparis in masaSiparisleri[secilenMasa])
                {
                    double satirToplam = siparis.Adet * siparis.Fiyat;
                    toplamTutar += satirToplam;

                    dgvSiparisler.Rows.Add(
                        siparis.UrunAdi,
                        siparis.Adet,
                        satirToplam.ToString("C2")
                    );
                }
            }

            // Toplam tutarı etikete yaz
            lblToplamTutar.Text = "Toplam: " + toplamTutar.ToString("C2");
            dgvSiparisler.Rows.Clear();

            if (masaSiparisleri.ContainsKey(secilenMasa))
            {
                foreach (var siparis in masaSiparisleri[secilenMasa])
                {
                    int rowIndex = dgvSiparisler.Rows.Add(
       siparis.UrunAdi,
       siparis.Adet,
       siparis.Fiyat.ToString("C2"),
       (siparis.Adet * siparis.Fiyat).ToString("C2"),
       null, // buton sütunu otomatik görünecek
       siparis.TeslimEdildi // checkbox sütununa denk geliyor
   );

                    // 🔸 Eğer teslim edildiyse satırı yeşile boya
                    if (siparis.TeslimEdildi)
                    {
                        dgvSiparisler.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
            }
        
    }

        private void cblMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cblMenu.SelectedItem != null)
            {
                nudAdet.Value = 1;
                nudAdet.Visible = true;
                btnSiparisEkle.Visible = true;

            }

        }

        private void dgvSiparisler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string urun = dgvSiparisler.Rows[e.RowIndex].Cells[0].Value?.ToString();
            if (!masaSiparisleri.ContainsKey(secilenMasa)) return;

            var liste = masaSiparisleri[secilenMasa];
            var sip = liste.FirstOrDefault(s => s.UrunAdi == urun);
            if (sip == null) return;

            // ➖ Azalt butonuna basıldıysa
            if (dgvSiparisler.Columns[e.ColumnIndex].Name == "azalt")
            {
                if (sip.Adet > 1)
                    sip.Adet--;
                else
                    liste.Remove(sip); // Adet 1 ise tamamen sil

                SiparisleriYenile();
            }

            // ✅ Teslim checkbox’ına tıklandıysa
            else if (dgvSiparisler.Columns[e.ColumnIndex].Name == "teslim")
            {
                sip.TeslimEdildi = !sip.TeslimEdildi;

                if (sip.TeslimEdildi)
                {
                    string csvYolu = "siparisler.csv";

                    // Başlık satırı eklensin (ilk kez oluşturuluyorsa)
                    if (!File.Exists(csvYolu))
                    {
                        File.AppendAllText(csvYolu, "Masa,UrunAdi,Adet,Fiyat,Durum,TarihSaat\n");
                    }

                    // Satır bilgisi CSV'ye yazılsın
                    File.AppendAllText(csvYolu, $"{secilenMasa},{sip.UrunAdi},{sip.Adet},{sip.Fiyat},Teslim Edildi,{DateTime.Now}\n");

                    // Liste ve DataGridView'den siparişi kaldır
                    if (masaSiparisleri.ContainsKey(secilenMasa))
                    {
                        masaSiparisleri[secilenMasa].Remove(sip); // veriden kaldır
                    }

                    dgvSiparisler.Rows.RemoveAt(e.RowIndex); // görselden kaldır (opsiyonel, SiparisleriYenile() varsa gerek yok)
                }

                // Yeniden yükle (güncel kalan siparişler)
                SiparisleriYenile();
            }

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void garson_Load(object sender, EventArgs e)
        {
            MutfakBildirimiLabel = lblMutfakBildirimi;
            HazirlanıyorDosyasiniGoster();
            panel1.Visible = false; panel2.Visible = false;
            panel3.Visible = true;
            originalFormSize = this.Size;
            SaveControlBounds(this);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Application.OpenForms["Form1"]?.Show();
            this.Close(); // veya this.Hide();

        }
    }
}
