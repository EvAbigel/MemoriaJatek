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
        int db = 6;
        int szamok = 1;

        public Form1()
        {
            InitializeComponent();
            Kezdes();
            GenerateMemoria();
            timer1.Start();
            this.Text = "Memória Játék";

            int meret = db * 65;
            this.ClientSize = new Size(meret, meret);
           
        }

        public void GenerateMemoria()
        {
            kartyak = new Label[db, db];
            int meret = 65;

            for (int i = 0; i < db; i++)
            {
                for (int j = db-1;  j >= 0; j--)
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
                        Text = szamok.ToString(),
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

            for (int i = 0; i < db; i++){
                for (int j = db - 1; j >= 0; j--){
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

        public void Kerdesek()
        {
            int sec = 0;
            int tizedesjgyek = 0;

            QuestionDialog hany = new QuestionDialog();
            QuestionDialog2 ido = new QuestionDialog2();
            QuestionDialog3 szamjegy = new QuestionDialog3();

            if(hany.ShowDialog() == DialogResult.Yes)
            {
                db = 6;
                ido.SzovegBeallit(db);
                ido.ShowDialog();
                sec = ido.IdoMeghat(db);
                timer1.Interval = sec * 1000;
            }
            else
            {
                db = 9;
                ido.SzovegBeallit(db);
                ido.ShowDialog();
                sec = ido.IdoMeghat(db);
                timer1.Interval = sec * 1000;
            }

            if(szamjegy.ShowDialog() == DialogResult.Yes)
            {
                tizedesjgyek = 1;
                
            }
            else
            {
                tizedesjgyek = 2;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            UpdateLabelText();
            
        }

        public void UpdateLabelText()
        {
            for (int i = 0; i < db; i++)
            {
                for (int j = db - 1; j >= 0; j--)
                {
                    kartyak[i, j].Text = string.Empty;
                }
            }
        }

        public void Kezdes()
        {
            MessageBox.Show("Hello! Üdvőzőlek a játéban ahol a memoriádat fogjuk tesztelni. Készenálsz, hogy belevágj ebbe a nehéz feladatba?" ,"Bevezető szöveg", MessageBoxButtons.OK);
            MessageBox.Show("Akkor kezdjük csak elöbb állítsuk be a nehézségi szintet.");
            Kerdesek();
            
        }
    }
}
