using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;

namespace GÖRSEL
{
    public partial class Form1 : Form
    {
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


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {


            // DataGridView sütunları

            cblMenu.Visible = false;
            nudAdet.Visible = false;
            btnSiparisEkle.Visible = false;

            // Tüm masa butonlarına aynı click olayı ata
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is System.Windows.Forms.Button && ctrl.Name.StartsWith("button"))
                {
                    ctrl.Click += Masa_Click;
                }
            }
        }
        private void Masa_Click(object sender, EventArgs e)
        {
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
            SiparisleriYenile();

        }
        private void btnSiparisEkle_Click(object sender, EventArgs e)
        {
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblSecilenMasa_Click(object sender, EventArgs e)
        {

        }

        private void nudAdet_ValueChanged(object sender, EventArgs e)
        {

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

                // 🔔 Bildirim etiketi - MASA bilgisi ile
                lblMutfakBildirimi.Visible = true;
                lblMutfakBildirimi.Text = sip.TeslimEdildi
                    ? $"✅ {secilenMasa} - Hazırlandı: {sip.UrunAdi}"
                    : $"🕒 {secilenMasa} - Hazırlanıyor: {sip.UrunAdi}";

                lblMutfakBildirimi.ForeColor = sip.TeslimEdildi ? Color.Green : Color.Orange;

                SiparisleriYenile();
            }
        }



        private void lblToplamTutar_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
