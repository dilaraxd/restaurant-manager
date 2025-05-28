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
    public partial class Form3 : Form
    {
        Dictionary<Control, Rectangle> originalRects = new Dictionary<Control, Rectangle>();
        Size originalFormSize;
        public Form3(List<string> secilenler)
        {
            InitializeComponent();
            label2.Text = "\n" + string.Join(Environment.NewLine, secilenler);
            this.Size = new Size(1350, 690);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;

            this.Load += new EventHandler(Form3_Load);
            this.Resize += new EventHandler(Form3_Resize);
        }

        private void Form3_Resize(object sender, EventArgs e)
        {
            float xRatio = (float)this.Width / originalFormSize.Width;
            float yRatio = (float)this.Height / originalFormSize.Height;

            foreach (Control ctrl in originalRects.Keys)
            {
                // panel içindeki kontrolleri tamamen atla
                if (panel1.Contains(ctrl) || panel2.Contains(ctrl)) continue;

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
        private void Form3_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = true;
            panel2.Visible = false;
            originalFormSize = this.Size;
            SaveControlBounds(this);
                
            // 🔽 secilenler.csv dosyasını oku ve label1'e yaz
            string dosyaYolu = "secilenler.csv"; // Gerekirse tam yolu ver: Desktop için → Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "secilenler.csv")
            if (System.IO.File.Exists(dosyaYolu))
            {
                try
                {
                    string[] satirlar = System.IO.File.ReadAllLines(dosyaYolu, Encoding.UTF8);
                    label1.Text = string.Join(Environment.NewLine, satirlar);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dosya okuma hatası: " + ex.Message);
                }
            }
            else
            {
                label1.Text = "CSV dosyası bulunamadı.";
            }
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = !panel3.Visible;
            panel1.Visible = !panel1.Visible;
            button1.Parent = this;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = !panel3.Visible;
            panel2.Visible = !panel2.Visible;
            button3.Parent = this;
            string csvYolu = Path.Combine(Application.StartupPath, "odenenler.csv");
            CiroVerileriniYukle("odenenler.csv");
        }
        private void CiroVerileriniYukle(string dosyaYolu)
        {
            if (!File.Exists(dosyaYolu))
            {
                MessageBox.Show("odenenler.csv bulunamadı.");
                return;
            }

            dataGridViewCiro.Rows.Clear();

            using (StreamReader sr = new StreamReader(dosyaYolu))
            {
                sr.ReadLine(); // Başlık atla

                while (!sr.EndOfStream)
                {
                    var satir = sr.ReadLine().Split(',');

                    if (satir.Length >= 4)
                    {
                        // Sıralama: Masa, UrunAdi, Adet, Fiyat
                        dataGridViewCiro.Rows.Add(
                            satir[0].Trim(),
                            satir[1].Trim(),
                            satir[2].Trim(),
                            satir[3].Trim()
                        );
                    }
                }
            }
        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.OpenForms["Form1"]?.Show();
            this.Close(); // veya this.Hide();

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            panel3.Parent = this;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            double toplamCiro = 0;

            foreach (DataGridViewRow row in dataGridViewCiro.Rows)
            {
                if (row.Cells["Adet"].Value != null && row.Cells["Fiyat"].Value != null)
                {
                    if (int.TryParse(row.Cells["Adet"].Value.ToString(), out int adet) &&
                        double.TryParse(row.Cells["Fiyat"].Value.ToString(), out double fiyat))
                    {
                        toplamCiro += adet * fiyat;
                    }
                }
            }

            MessageBox.Show("Toplam Ciro: " + toplamCiro.ToString("C2"), "Ciro Bilgisi");
        }
    }
    }

