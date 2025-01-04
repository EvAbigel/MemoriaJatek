using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memoria_Jatek
{
    public partial class QuestionDialog2 : Form
    {
        public QuestionDialog2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }
        public int IdoMeghat(int db)
        {
            int ido = 0;
            if (db == 6)
            {
                button1.Text = $"5 sec";
                button2.Text = $"10 sec";
                if (DialogResult == DialogResult.Yes)
                {
                    ido = 5;
                }
                else
                {
                    ido = 10;
                }
                return ido;
            }
            if(db == 9)
            {
                button1.Text = $"10 sec";
                button2.Text = $"20 sec";
                if (DialogResult == DialogResult.Yes)
                {
                    ido = 10;
                }
                else
                {
                    ido = 20;
                }
                return ido;
            }
            return ido;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
