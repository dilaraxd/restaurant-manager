using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            this.Size = new Size(1350, 800);
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
            originalFormSize = this.Size;            // ✅ 1. Form boyutu kaydediliyor
            SaveControlBounds(this);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            List<string> secilenler = new List<string>();

            foreach (var item in checkedListBox1.CheckedItems)
            {
                secilenler.Add(item.ToString());
            }

            Form3 form3 = new Form3(secilenler);
           
            this.Hide(); // İstersen sadece gizle
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
