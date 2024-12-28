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
    public partial class Form1 : Form
    {
        static Random r = new Random();
        Label[,] kartyak;
        int valasz1 = 6;

        public Form1()
        {
            InitializeComponent();
            GenerateMemoria();
            this.Text = "Memória Játék";

            int meret = valasz1 * 65;
            this.ClientSize = new Size(meret, meret);
        }

        public void GenerateMemoria()
        {
            kartyak = new Label[valasz1, valasz1];
            int meret = 65;

            for (int i = 0; i < valasz1; i++)
            {
                for (int j = valasz1-1;  j >= 0; j--)
                {
                    kartyak[i, j] = new Label()
                    {
                        Width = meret,
                        Height = meret,
                        Left = j * meret,
                        Top = i * meret,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Arial", 14),
                        BackColor = Color.DarkCyan,
                    };

                    kartyak[i, j].Click += Label_Click;
                    this.Controls.Add(kartyak[i, j]);

                }
            }

            Keveres();
        }


        private void Label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            for (int i = 0; i < valasz1; i++){
                for (int j = valasz1 - 1; j >= 0; j--){
                    if (kartyak[i, j] == clickedLabel){
                        //valami valami
                        return;
                    }
                }
            }
        }

        public void Keveres()
        {
            //valami valami 2
        }
    }
}
