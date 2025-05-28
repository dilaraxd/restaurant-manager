namespace GÖRSEL
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            btn = new Button();
            button8 = new Button();
            button9 = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            dgvSiparisler = new DataGridView();
            urun = new DataGridViewTextBoxColumn();
            adet = new DataGridViewTextBoxColumn();
            fiyat = new DataGridViewTextBoxColumn();
            toplam = new DataGridViewTextBoxColumn();
            azalt = new DataGridViewButtonColumn();
            Teslim = new DataGridViewCheckBoxColumn();
            panel1 = new Panel();
            lblMutfakBildirimi = new Label();
            lblToplamTutar = new Label();
            btnSiparisEkle = new Button();
            nudAdet = new NumericUpDown();
            lblSecilenMasa = new Label();
            cblMenu = new CheckedListBox();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSiparisler).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudAdet).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(346, 3);
            button1.Name = "button1";
            button1.Size = new Size(165, 150);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Masa_Click;
            // 
            // button2
            // 
            button2.Location = new Point(175, 159);
            button2.Name = "button2";
            button2.Size = new Size(165, 155);
            button2.TabIndex = 1;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Masa_Click;
            // 
            // button3
            // 
            button3.Location = new Point(346, 159);
            button3.Name = "button3";
            button3.Size = new Size(165, 155);
            button3.TabIndex = 2;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Masa_Click;
            // 
            // button4
            // 
            button4.Location = new Point(3, 159);
            button4.Name = "button4";
            button4.Size = new Size(166, 155);
            button4.TabIndex = 3;
            button4.Text = "mASA3";
            button4.UseVisualStyleBackColor = true;
            button4.Click += Masa_Click;
            // 
            // button5
            // 
            button5.Location = new Point(343, 320);
            button5.Name = "button5";
            button5.Size = new Size(165, 155);
            button5.TabIndex = 4;
            button5.Text = "button5";
            button5.UseVisualStyleBackColor = true;
            button5.Click += Masa_Click;
            // 
            // button6
            // 
            button6.Location = new Point(175, 3);
            button6.Name = "button6";
            button6.Size = new Size(165, 149);
            button6.TabIndex = 5;
            button6.Text = "MASA2";
            button6.UseVisualStyleBackColor = true;
            button6.Click += Masa_Click;
            // 
            // btn
            // 
            btn.Location = new Point(3, 3);
            btn.Name = "btn";
            btn.Size = new Size(166, 150);
            btn.TabIndex = 6;
            btn.Text = "MASA 1";
            btn.UseVisualStyleBackColor = true;
            btn.Click += Masa_Click;
            // 
            // button8
            // 
            button8.Location = new Point(3, 320);
            button8.Name = "button8";
            button8.Size = new Size(166, 155);
            button8.TabIndex = 7;
            button8.Text = "button8";
            button8.UseVisualStyleBackColor = true;
            button8.Click += Masa_Click;
            // 
            // button9
            // 
            button9.Location = new Point(175, 320);
            button9.Name = "button9";
            button9.Size = new Size(162, 155);
            button9.TabIndex = 8;
            button9.Text = "button9";
            button9.UseVisualStyleBackColor = true;
            button9.Click += Masa_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(btn);
            flowLayoutPanel1.Controls.Add(button6);
            flowLayoutPanel1.Controls.Add(button1);
            flowLayoutPanel1.Controls.Add(button4);
            flowLayoutPanel1.Controls.Add(button2);
            flowLayoutPanel1.Controls.Add(button3);
            flowLayoutPanel1.Controls.Add(button8);
            flowLayoutPanel1.Controls.Add(button9);
            flowLayoutPanel1.Controls.Add(button5);
            flowLayoutPanel1.Location = new Point(1, 4);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(514, 485);
            flowLayoutPanel1.TabIndex = 9;
            // 
            // dgvSiparisler
            // 
            dgvSiparisler.AllowUserToAddRows = false;
            dgvSiparisler.AllowUserToDeleteRows = false;
            dgvSiparisler.AllowUserToResizeColumns = false;
            dgvSiparisler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSiparisler.BackgroundColor = SystemColors.Menu;
            dgvSiparisler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSiparisler.Columns.AddRange(new DataGridViewColumn[] { urun, adet, fiyat, toplam, azalt, Teslim });
            dgvSiparisler.GridColor = Color.MistyRose;
            dgvSiparisler.Location = new Point(1, 648);
            dgvSiparisler.MultiSelect = false;
            dgvSiparisler.Name = "dgvSiparisler";
            dgvSiparisler.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.MistyRose;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = Color.MistyRose;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvSiparisler.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvSiparisler.RowHeadersVisible = false;
            dgvSiparisler.RowHeadersWidth = 51;
            dgvSiparisler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSiparisler.Size = new Size(511, 283);
            dgvSiparisler.TabIndex = 3;
            dgvSiparisler.Visible = false;
            dgvSiparisler.CellContentClick += dgvSiparisler_CellContentClick;
            dgvSiparisler.Click += btnSiparisEkle_Click;
            // 
            // urun
            // 
            urun.HeaderText = "Ürün";
            urun.MinimumWidth = 6;
            urun.Name = "urun";
            urun.ReadOnly = true;
            // 
            // adet
            // 
            adet.HeaderText = "adet";
            adet.MinimumWidth = 6;
            adet.Name = "adet";
            adet.ReadOnly = true;
            // 
            // fiyat
            // 
            fiyat.HeaderText = "fiyat";
            fiyat.MinimumWidth = 6;
            fiyat.Name = "fiyat";
            fiyat.ReadOnly = true;
            // 
            // toplam
            // 
            toplam.HeaderText = "Toplam";
            toplam.MinimumWidth = 6;
            toplam.Name = "toplam";
            toplam.ReadOnly = true;
            // 
            // azalt
            // 
            azalt.HeaderText = "Azalt";
            azalt.MinimumWidth = 6;
            azalt.Name = "azalt";
            azalt.ReadOnly = true;
            // 
            // Teslim
            // 
            Teslim.HeaderText = "Teslim Bilgisi";
            Teslim.MinimumWidth = 6;
            Teslim.Name = "Teslim";
            Teslim.ReadOnly = true;
            Teslim.Resizable = DataGridViewTriState.True;
            Teslim.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Snow;
            panel1.Controls.Add(lblMutfakBildirimi);
            panel1.Controls.Add(lblToplamTutar);
            panel1.Controls.Add(btnSiparisEkle);
            panel1.Controls.Add(nudAdet);
            panel1.Controls.Add(lblSecilenMasa);
            panel1.Controls.Add(cblMenu);
            panel1.Location = new Point(514, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(537, 927);
            panel1.TabIndex = 10;
            panel1.Paint += panel1_Paint;
            // 
            // lblMutfakBildirimi
            // 
            lblMutfakBildirimi.AutoSize = true;
            lblMutfakBildirimi.Location = new Point(257, 697);
            lblMutfakBildirimi.Name = "lblMutfakBildirimi";
            lblMutfakBildirimi.Size = new Size(50, 20);
            lblMutfakBildirimi.TabIndex = 7;
            lblMutfakBildirimi.Text = "label1";
            lblMutfakBildirimi.Visible = false;
            lblMutfakBildirimi.Click += label1_Click;
            // 
            // lblToplamTutar
            // 
            lblToplamTutar.AutoSize = true;
            lblToplamTutar.BackColor = Color.WhiteSmoke;
            lblToplamTutar.ForeColor = Color.Black;
            lblToplamTutar.Location = new Point(17, 663);
            lblToplamTutar.Name = "lblToplamTutar";
            lblToplamTutar.Size = new Size(50, 20);
            lblToplamTutar.TabIndex = 6;
            lblToplamTutar.Text = "label2";
            lblToplamTutar.Visible = false;
            lblToplamTutar.Click += lblToplamTutar_Click;
            // 
            // btnSiparisEkle
            // 
            btnSiparisEkle.BackColor = SystemColors.MenuBar;
            btnSiparisEkle.Location = new Point(385, 74);
            btnSiparisEkle.Name = "btnSiparisEkle";
            btnSiparisEkle.Size = new Size(126, 40);
            btnSiparisEkle.TabIndex = 4;
            btnSiparisEkle.Text = "Sipariş Ekle";
            btnSiparisEkle.UseVisualStyleBackColor = false;
            btnSiparisEkle.Click += btnSiparisEkle_Click;
            // 
            // nudAdet
            // 
            nudAdet.BackColor = Color.Snow;
            nudAdet.Location = new Point(285, 41);
            nudAdet.Name = "nudAdet";
            nudAdet.Size = new Size(150, 27);
            nudAdet.TabIndex = 2;
            nudAdet.ValueChanged += nudAdet_ValueChanged;
            // 
            // lblSecilenMasa
            // 
            lblSecilenMasa.AutoSize = true;
            lblSecilenMasa.BackColor = Color.Snow;
            lblSecilenMasa.Location = new Point(285, 18);
            lblSecilenMasa.Name = "lblSecilenMasa";
            lblSecilenMasa.Size = new Size(102, 20);
            lblSecilenMasa.TabIndex = 1;
            lblSecilenMasa.Text = "Seçilen Masa :";
            lblSecilenMasa.Click += lblSecilenMasa_Click;
            // 
            // cblMenu
            // 
            cblMenu.BackColor = SystemColors.Menu;
            cblMenu.FormattingEnabled = true;
            cblMenu.Items.AddRange(new object[] { "Pesto Soslu Makarna", "Bolognese Soslu Makarna", "Alfredo Makarna", "Cheese Burger", "Klasik Burger", "Trüflü Cheddar Burger", "Klasik Pizza", "Margarita Pizza", "4 Peynirli Pizza", "Etli Pizza", "Funghi Pizza", "Tavuklu Barbekülü Pizza", "Kremalı Mantar Soslu Tavuk", "Sweet Chili Soslu Tavuk", "Izgara Tavuk Pirzola", "Bonfile Lokum", "Et Fajita", "Tavuklu Sezar Salata", "Biftek Salata", "Akdeniz Salatası", "Tavuklu Wrap", "Etli Wrap", "Mantı" });
            cblMenu.Location = new Point(7, 3);
            cblMenu.Name = "cblMenu";
            cblMenu.Size = new Size(244, 510);
            cblMenu.TabIndex = 0;
            cblMenu.SelectedIndexChanged += cblMenu_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Snow;
            ClientSize = new Size(1057, 933);
            Controls.Add(panel1);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(dgvSiparisler);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSiparisler).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudAdet).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button btn;
        private Button button8;
        private Button button9;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panel1;
        private DataGridView dgvSiparisler;
        private NumericUpDown nudAdet;
        private Label lblSecilenMasa;
        private CheckedListBox cblMenu;
        private Button btnSiparisEkle;
        private Label lblToplamTutar;
        private DataGridViewTextBoxColumn urun;
        private DataGridViewTextBoxColumn adet;
        private DataGridViewTextBoxColumn fiyat;
        private DataGridViewTextBoxColumn toplam;
        private DataGridViewButtonColumn azalt;
        private DataGridViewCheckBoxColumn Teslim;
        private Label lblMutfakBildirimi;
    }
}
