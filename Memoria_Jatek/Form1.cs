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
        int minrandom;
        int maxrandom;

        Label[] kartyak;
        int db = 6;
        int[] randomszamok;

        public Form1()
        {
            InitializeComponent();
            Kezdes();
            GenerateMemoria();
            timer1.Start();
            this.Text = "Memória Játék";
        }

        public void Kerdesek()
        {
            int sec = 0;

            QuestionDialog hany = new QuestionDialog();
            QuestionDialog2 ido = new QuestionDialog2();
            QuestionDialog3 szamjegy = new QuestionDialog3();

            if(hany.ShowDialog() == DialogResult.Yes)
            {
                db = 6;
                randomszamok = new int[db];
                ido.SzovegBeallit(db);
                ido.ShowDialog();
                sec = ido.IdoMeghat(db);
                timer1.Interval = sec * 1000;
            }
            else
            {
                db = 9;
                randomszamok = new int[db];
                ido.SzovegBeallit(db);
                ido.ShowDialog();
                sec = ido.IdoMeghat(db);
                timer1.Interval = sec * 1000;
            }

            if(szamjegy.ShowDialog() == DialogResult.Yes)
            {
                minrandom = 0;
                maxrandom = 10;

                Tolt(minrandom, maxrandom);

            }
            else
            {
                minrandom = 10;
                maxrandom = 100;

                Tolt(minrandom, maxrandom);
            }
        }

        public void GenerateMemoria()
        {
            kartyak = new Label[db];
            int width = 85;
            int height = 150;
            int padding = 15;
            int offset = width + padding;

            for (int i = 0; i < db; i++)
            {
                kartyak[i] = new Label()
                {
                    Width = width,
                    Height = height,
                    Left = i * offset + padding,
                    Top = padding,
                    BorderStyle = BorderStyle.Fixed3D,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 14),
                    BackColor = Color.DarkCyan,
                    Name = randomszamok[i].ToString(),
                    Text = randomszamok[i].ToString(),
                };

                kartyak[i].Click += Label_Click;
                this.Controls.Add(kartyak[i]);
            }

            this.ClientSize = new Size(offset * db + padding, height + padding * 2);
        }

        private void Label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            for (int i = 0; i < db; i++){
                if (kartyak[i] == clickedLabel)
                {
                    //valami valami
                    return;
                }
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
                kartyak[i].Text = string.Empty;
            }
        }

        public void Kezdes()
        {
            MessageBox.Show("Hello! Üdvőzőlek a játéban ahol a memoriádat fogjuk tesztelni. Készenálsz, hogy belevágj ebbe a nehéz feladatba?" ,"Bevezető szöveg", MessageBoxButtons.OK);
            MessageBox.Show("Akkor kezdjük csak elöbb állítsuk be a nehézségi szintet.");
            Kerdesek();
        }



        public void Tolt(int mini, int maxi)
        {
            for (int i = 0; i < randomszamok.Length; i++)
            {
                randomszamok[i] = r.Next(mini, maxi);
            }
        }
    }
}
