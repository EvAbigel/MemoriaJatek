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

        Button verifyButton;

        Label[] kartyak;
        int db = 6;
        int[] randomszamok;

        TextBox editBox = new TextBox();

        public Form1()
        {
            InitializeComponent();

            editBox.Visible = false; 
            editBox.Leave += EditBox_Leave;
            editBox.KeyDown += EditBox_KeyDown;
            this.Controls.Add(editBox);

            Kezdes();
            GenerateMemoria(db);
            timer1.Start();
            this.Text = "Memória Játék";

            verifyButton = new Button()
            {
                Text = "Verify",
                Width = 100,
                Height = 40,
                Left = 10,
                Top = 150
            };
            verifyButton.Click += VerifyButton_Click; 
            this.Controls.Add(verifyButton);
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

        public void GenerateMemoria(int szam)
        {
            kartyak = new Label[db];
            int width = 85;
            int height = 85;
            int padding = 15;

            int offsetX = width + padding;
            int offsetY = height + padding;
            int sorok = 3;

            for (int i = 0; i < db; i++)
            {
                int oszlop = i / sorok;
                int sor = i % sorok;

                kartyak[i] = new Label()
                {
                    Width = width,
                    Height = height,
                    Left = sor * offsetX + padding,
                    Top = oszlop * offsetY + padding,
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 14),
                    BackColor = Color.DarkCyan,
                    Name = randomszamok[i].ToString(),
                    Text = randomszamok[i].ToString(),
                };

                kartyak[i].Click += Label_Click;
                this.Controls.Add(kartyak[i]);
            }

            int oszlopSzam = (int)Math.Ceiling((double)db / sorok); 
            this.ClientSize = new Size(sorok * offsetX + padding, oszlopSzam * offsetY + padding);
        }

        private void Label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                editBox.Text = clickedLabel.Text;
                editBox.Location = clickedLabel.Location;
                editBox.Size = clickedLabel.Size;
                editBox.Visible = true;
                editBox.Focus();
                editBox.Tag = clickedLabel; // Store the clicked label in the Tag property
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
            HashSet<int> list = new HashSet<int>();
            while (list.Count != randomszamok.Length)
            {   
                list.Add( r.Next(mini, maxi));
            }

            for (int i = 0; i < list.Count; i++)
            {
                randomszamok[i] = (list.ToArray())[i];
            }
        }

        private void EditBox_Leave(object sender, EventArgs e)
        {
            Label label = editBox.Tag as Label;
            if (label != null)
            {
                label.Text = editBox.Text;
                editBox.Visible = false;
            }
        }

        private void EditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditBox_Leave(sender, e);
            }
        }

        private void VerifyButton_Click(object sender, EventArgs e)
        {
            bool allCorrect = true;

            for (int i = 0; i < db; i++)
            {
                if (kartyak[i].Text != kartyak[i].Name)
                {
                    allCorrect = false;
                    kartyak[i].BackColor = Color.Red;
                }
                else
                {
                    kartyak[i].BackColor = Color.Green;
                }
            }

            if (allCorrect)
            {
                MessageBox.Show("All labels are correct!");
            }
            else
            {
                MessageBox.Show("Some labels are incorrect. Try again!");
            }
        }


        //teendő: labelbe való írás -> verifikálás, valami gomb ami indítja a verifikálást
        //ötlet: label név: válasz (már meg van), ha input = válasz, akkor jó
        //+ kéne még a próbálkozás counter, ha 0 akkor egyből indítja a verifikációt
    }
}
