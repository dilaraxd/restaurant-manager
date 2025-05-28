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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace GörselProg
{
    public partial class Mutfak : Form
    {
        Dictionary<Control, Rectangle> originalRects = new Dictionary<Control, Rectangle>();
        Size originalFormSize;
        private List<string> secilenler = new List<string>(); // boş liste

        // 1️⃣ Girişten gelen çağrı için (Form1 → giriş sonrası açılır)
        public Mutfak()
        {
            InitializeComponent();
            this.Size = new Size(1350, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;

            this.Load += new EventHandler(Form2_Load);
            this.Resize += new EventHandler(Form2_Resize);
        }

        // 2️⃣ Seçilenleri Form3’e göndermek için
        public Mutfak(List<string> secilenler)
        {
            this.secilenler = secilenler;

            InitializeComponent();
            this.Size = new Size(1350, 820);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;

            this.Load += new EventHandler(Form2_Load);
            this.Resize += new EventHandler(Form2_Resize);

            foreach (string item in secilenler)
            {
                checkedListBox2.Items.Add(item, true);
            }
        }
        public List<string> GetSecilenler()
        {
            List<string> secilenler = new List<string>();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                secilenler.Add(item.ToString());
            }
            return secilenler;
        }

        private void Form2_Resize(object sender, EventArgs e)
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
        private void Form2_Load(object sender, EventArgs e)
        {
            SiparisleriYukleVeDagit();

            originalFormSize = this.Size;            // ✅ 1. Form boyutu kaydediliyor
            SaveControlBounds(this);
        }
        private void HazirlaniyorButonu_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null || btn.Tag == null) return;

            string masaNo = btn.Tag.ToString(); // Örn: "MASA 7"
            string sayiKismi = new string(masaNo.Where(char.IsDigit).ToArray());
            int masaIndex = int.Parse(sayiKismi) + 1;

            // CheckedListBox adını oluştur
            string clbName = "checkedListBox" + masaIndex;

            // İlgili CheckedListBox'ı bul
            CheckedListBox clb = this.Controls.Find(clbName, true).FirstOrDefault() as CheckedListBox;
            if (clb == null) return;

            // Seçilen ürünleri topla
            List<string> secilenler = new List<string>();
            for (int i = 0; i < clb.Items.Count; i++)
            {
                if (clb.GetItemChecked(i))
                {
                    string mevcut = clb.Items[i].ToString();
                    if (!mevcut.Contains("⏳"))
                    {
                        string guncel = "⏳ " + mevcut;
                        secilenler.Add(guncel);
                        clb.Items[i] = guncel;

                        // CSV'ye yaz
                        string csvSatir = $"{masaNo},{guncel},{DateTime.Now}";
                        File.AppendAllText("hazırlananlar.csv", csvSatir + Environment.NewLine);
                    }
                }
            }

            if (secilenler.Count == 0)
            {
                MessageBox.Show("Lütfen en az bir ürün seçin.");
                return;
            }

           
        }


        private void HazirButonu_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null || btn.Tag == null) return;

            string masaNo = btn.Tag.ToString(); // Örn: "MASA 7"
            string sayiKismi = new string(masaNo.Where(char.IsDigit).ToArray());
            int masaIndex = int.Parse(sayiKismi) + 1;

            string clbName = "checkedListBox" + masaIndex;
            CheckedListBox clb = this.Controls.Find(clbName, true).FirstOrDefault() as CheckedListBox;
            if (clb == null) return;

            List<string> secilenler = new List<string>();
            List<object> silinecekler = new List<object>();

            foreach (var item in clb.CheckedItems)
            {
                secilenler.Add(item.ToString());
                silinecekler.Add(item);
            }

            if (secilenler.Count == 0)
            {
                MessageBox.Show("Lütfen en az bir ürün seçin.");
                return;
            }

            // CheckedListBox'tan seçilenleri sil
            foreach (var item in silinecekler)
            {
                clb.Items.Remove(item);
            }

            // 📁 CSV güncelleme: ⏳'lu satırları ✅ olarak değiştir
            string csvPath = "hazırlananlar.csv";
            if (File.Exists(csvPath))
            {
                var satirlar = File.ReadAllLines(csvPath).ToList();
                for (int i = 0; i < satirlar.Count; i++)
                {
                    foreach (string secilen in secilenler)
                    {
                        string temizSecilen = secilen.Replace("⏳ ", "").Trim();
                        if (satirlar[i].Contains(masaNo) && satirlar[i].Contains(temizSecilen))
                        {
                            satirlar[i] = satirlar[i].Replace("⏳", "✅");
                        }
                    }
                }
                File.WriteAllLines(csvPath, satirlar);
            }

            // Garson ekranına bilgi gönder
          

            
        }



        private void SiparisleriYukleVeDagit()
        {
            string dosyaYolu = "tumSiparisler.csv";

            if (!File.Exists(dosyaYolu))
            {
                MessageBox.Show("Henüz sipariş eklenmedi.");
                return;
            }

            var masaSiparisleri = new Dictionary<string, List<string>>();

            foreach (var satir in File.ReadAllLines(dosyaYolu).Skip(1)) // Başlığı atla
            {
                string[] parcalar = satir.Split(',');
                if (parcalar.Length < 4) continue;

                string masa = parcalar[0];
                string urun = parcalar[1];
                string adet = parcalar[2];

                string gosterim = $"{urun} x{adet}";

                if (!masaSiparisleri.ContainsKey(masa))
                    masaSiparisleri[masa] = new List<string>();

                masaSiparisleri[masa].Add(gosterim);
            }

            // CheckedListBox'lara dağıt
            foreach (var masa in masaSiparisleri)
            {
                switch (masa.Key)
                {
                    case "MASA 1": foreach (var item in masa.Value) checkedListBox2.Items.Add(item); break;
                    case "MASA 2": foreach (var item in masa.Value) checkedListBox3.Items.Add(item); break;
                    case "MASA 3": foreach (var item in masa.Value) checkedListBox4.Items.Add(item); break;
                    case "MASA 4": foreach (var item in masa.Value) checkedListBox5.Items.Add(item); break;
                    case "MASA 5": foreach (var item in masa.Value) checkedListBox6.Items.Add(item); break;
                    case "MASA 6": foreach (var item in masa.Value) checkedListBox7.Items.Add(item); break;
                    case "MASA 7": foreach (var item in masa.Value) checkedListBox8.Items.Add(item); break;
                    case "MASA 8": foreach (var item in masa.Value) checkedListBox9.Items.Add(item); break;
                    case "MASA 9": foreach (var item in masa.Value) checkedListBox10.Items.Add(item); break;
                    case "MASA 10": foreach (var item in masa.Value) checkedListBox11.Items.Add(item); break;
                    case "MASA 11": foreach (var item in masa.Value) checkedListBox12.Items.Add(item); break;
                    case "MASA 12": foreach (var item in masa.Value) checkedListBox13.Items.Add(item); break;
                }
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            List<string> secilenler = new List<string>();

            foreach (var item in checkedListBox1.CheckedItems)
            {
                secilenler.Add(item.ToString());
            }

            // CSV dosyasına yaz
            string dosyaYolu = "secilenler.csv"; // masaüstü istiyorsan: Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "secilenler.csv")

            try
            {
                using (StreamWriter writer = new StreamWriter(dosyaYolu, false, Encoding.UTF8))
                {
                    foreach (string secilen in secilenler)
                    {
                        writer.WriteLine(secilen);
                    }
                }

                MessageBox.Show("Seçilenler yönetici ekranına iletildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

            // Form3’e geç
           
        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {
            Application.OpenForms["Form1"]?.Show();
            this.Close(); // veya this.Hide();

        }
    }
}
