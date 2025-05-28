using System;
using System.Collections;
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
    public partial class Form1 : Form
    {
        Dictionary<Control, Rectangle> originalRects = new Dictionary<Control, Rectangle>();
        Size originalFormSize;
        private Hashtable personelVeritabani = new Hashtable();
        public Form1()
        {
    

            InitializeComponent();
            this.Size = new Size(1150, 700); // Geniş başlangıç boyutu
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;

            this.Load += new EventHandler(Form1_Load);
            this.Resize += new EventHandler(Form1_Resize);
            //kayıtlı personel girişi
            personelVeritabani.Add("Garson", "grsnID80");

            personelVeritabani.Add("MutfakPersoneli", "mtfkID27");
            personelVeritabani.Add("Yönetici", "yntciID46");
            personelVeritabani.Add("Kasiyer", "ksyrID16");

            button1.Click += button1_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi =textBox1.Text.Trim();
            string sifre = textBox2.Text;
            if (personelVeritabani.ContainsKey(kullaniciAdi))
            {
                if (personelVeritabani[kullaniciAdi].ToString() == sifre)
                {
                    if (kullaniciAdi == "Garson" && sifre == "grsnID80")
                    {
                        Form acikForm = Application.OpenForms["Garson"];
                        if (acikForm == null)
                        {
                           garson grsn = new garson();
                            grsn.Show();
                        }
                        else
                        {
                            acikForm.BringToFront();
                        }
                        this.Hide();
                    }
                    else if (kullaniciAdi == "MutfakPersoneli" && sifre=="mtfkID27")
                    {
                  
                            Form acikForm = Application.OpenForms["Mutfak"];
                            if (acikForm == null)
                            {
                                Mutfak mtfk = new Mutfak();
                                mtfk.Show();
                            }
                            else
                            {
                                acikForm.BringToFront();
                            }
                            this.Hide();
                        
                    }
                    else if (kullaniciAdi == "Yönetici" && sifre == "yntciID46")
                    {
                        Form acikForm = Application.OpenForms["Form3"];
                        if (acikForm == null)
                        {
                            Mutfak mutfak = new Mutfak();
                            List<string> secilenler = mutfak.GetSecilenler();
                            Form3 frm3 = new Form3(secilenler);
                            frm3.Show();
                        }
                        else
                        {
                            acikForm.BringToFront();
                        }
                        this.Hide();
                    }
                    else if (kullaniciAdi == "Kasiyer" && sifre == "ksyrID16")
                    {
                        Form acikForm = Application.OpenForms["Kasiyer"];
                        if (acikForm == null)
                        {
                            Kasiyer ksyr = new Kasiyer();
                           
                            
                            ksyr.Show();
                        }
                        else
                        {
                            acikForm.BringToFront();
                        }
                        this.Hide();
                    }


                }
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            originalFormSize = this.Size;           
            SaveControlBounds(this);
        }
    }
}
