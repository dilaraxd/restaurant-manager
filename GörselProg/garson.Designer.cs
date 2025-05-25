namespace GörselProg
{
    partial class garson
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Makarna ",
            "Domates",
            "Pesto ",
            "Soğan",
            "Salatalık",
            "Tuz",
            "Karabiber",
            "Salça",
            "Marul",
            "Sirke",
            "Turşu",
            "Un",
            "Şeker",
            "Kola",
            "Soda",
            "Meyve Suyu",
            "Çilek",
            "Muz",
            "Kabartma Tozu",
            "Maya",
            "Vanilya",
            "Krema",
            "Ispanak",
            "Pırasa",
            "Lahana",
            "Kahve",
            "Pirinç",
            "Bulgur",
            "Mantı",
            "Peynir",
            "Yufka",
            "Ananas",
            "Süt",
            "Yoğurt",
            "Su"});
            this.checkedListBox1.Location = new System.Drawing.Point(224, 85);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(1048, 760);
            this.checkedListBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(521, 909);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(439, 90);
            this.button1.TabIndex = 1;
            this.button1.Text = "gönder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // garson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1575, 1127);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListBox1);
            this.Name = "garson";
            this.Text = "garson";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button button1;
    }
}