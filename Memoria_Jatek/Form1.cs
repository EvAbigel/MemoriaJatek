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
        int probalkozasok;
        Label probalkozasLabel;

        int elapsedSeconds = 0; 
        int elapsedMinutes = 0;

        public Form1()
        {
            InitializeComponent();
            EditBoxBeallit();
            Kezdes();
            GenerateMemoria(db);
            timer1.Start();
            this.Text = "Memória Játék";

        }

        public void EditBoxBeallit()
        {
            editBox.Visible = false;
            editBox.Enabled = true;
            editBox.ReadOnly = true;
            editBox.Leave += EditBox_Leave;
            editBox.KeyDown += EditBox_KeyDown;
            this.Controls.Add(editBox);
        }

        public void Kerdesek()
        {
            int sec = 0;

            QuestionDialog hany = new QuestionDialog();
            QuestionDialog2 ido = new QuestionDialog2();
            QuestionDialog3 szamjegy = new QuestionDialog3();

            if(hany.ShowDialog() == DialogResult.Yes)
            {
                probalkozasok = 6;
                
                db = 6;
                randomszamok = new int[db];
                ido.SzovegBeallit(db);
                ido.ShowDialog();
                sec = ido.IdoMeghat(db);
                timer1.Interval = sec * 1000;
            }
            else
            {
                probalkozasok = 9;
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
            verifyButton = new Button()
            {
                Text = "Ellenőrzés",
                Width = 100,
                Height = 40,
                Left = 10,
                Top = oszlopSzam * offsetY + padding,
                Enabled = false,
            };
            verifyButton.Click += VerifyButton_Click;
            this.Controls.Add(verifyButton);

            this.ClientSize = new Size(sorok * offsetX + padding, verifyButton.Top + verifyButton.Height + padding);


            probalkozasLabel = new Label()
            {
                Text = $"Hátralévő próbálkozások száma: {probalkozasok}",
                AutoSize = true,
                Left = verifyButton.Left + verifyButton.Width + 10,
                Top = verifyButton.Top
            };

            this.Controls.Add(probalkozasLabel);
        }

        private void Label_Click(object sender, EventArgs e)
        {
            if (editBox.ReadOnly == true) return;

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
            verifyButton.Enabled = true;
            editBox.ReadOnly = false;
            
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
            Bevezeto_szoveg kezdes = new Bevezeto_szoveg();
            if(kezdes.ShowDialog() == DialogResult.Yes)
            {
                MessageBox.Show("Akkor kezdjük csak elöbb állítsuk be a nehézségi szintet.", "Beállítások");
                Kerdesek();
                MessageBox.Show("Megfognak jelenni kártyák előtted amin lesznek számok az általad beállított ideig láthatod őket utána a számok eltűnnek és a kártyákra rákattinthatsz és beléjük írhatsz számokat amiket enter lenyomással okézol le.");
                timer2.Start();
            }
            else
            {
                this.Close();
            }
            
            
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
                probalkozasok--;
                probalkozasLabel.Text = $"Hátralévő próbálkozások száma: {probalkozasok}"; 
                e.SuppressKeyPress = true;
                EditBox_Leave(sender, e);
                if(probalkozasok == 0)
                {
                    MessageBox.Show("Elfogytak a probálkozásaid!");
                    VerifyButton_Click(sender, e);
                }
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
                MessageBox.Show($"Minden tökéletes gratulálok! Megoldásra forditott idő:{elapsedMinutes} perc és {elapsedSeconds} másodperc");
                timer2.Stop();
                this.Close();
            }
            else
            {
                MessageBox.Show("Van hibás szám!");
                DialogResult result = MessageBox.Show("Tudni szeretnéd a jó megoldást?(igen) Vagy újra probálod(nem)?", "Mi legyen?", MessageBoxButtons.YesNo);
                if(result == DialogResult.No)
                {
                    ResetLabels();
                    probalkozasok = db;
                    probalkozasLabel.Text = $"Hátralévő próbálkozások száma: {probalkozasok}";
                }
                else
                {
                    for (int i = 0; i < db; i++) {
                        if (kartyak[i].BackColor == Color.Red) {
                            kartyak[i].Text = randomszamok[i].ToString();
                        }
                    }
                }
                editBox.ReadOnly = true;
                verifyButton.Enabled = false;
            }
        }

        public void ResetLabels() {
            for (int i = 0; i < db; i++) {
                kartyak[i].Text = randomszamok[i].ToString();
                kartyak[i].BackColor = Color.DarkCyan; 
            } 
            editBox.ReadOnly = true; 
            verifyButton.Enabled = false; 
            timer1.Start(); 
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            elapsedSeconds++;
            if (elapsedSeconds == 60) {
                elapsedMinutes++;
                elapsedSeconds = 0; 
            }
        }
    }
}
