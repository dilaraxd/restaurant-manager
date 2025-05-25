using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            this.Size = new Size(1300, 700);
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
      
            panel2.Visible = false;
            originalFormSize = this.Size;
            SaveControlBounds(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = !panel1.Visible;
            button1.Parent = this;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = !panel2.Visible;
            button3.Parent = this;

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 gırıs = new Form1();
            gırıs.Show();
            this.Hide();
        }
    }
}
