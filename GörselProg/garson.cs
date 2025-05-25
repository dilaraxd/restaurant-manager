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
    public partial class garson : Form
    {
        public garson()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            List<string> secilenler = new List<string>();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                secilenler.Add(item.ToString());
            }

         Mutfak frm2 = new Mutfak(secilenler); // Listeyi gönder
            
        

    }
}
}
