using System;
using System.Drawing;
using System.Windows.Forms;


namespace reversi
{
    public partial class REVERSI : Form
    {
        /// aparenta butoane "can t move" 
        private void Button1_Paint(object sender, PaintEventArgs e)
        {
            Button buton = (Button)sender;
            ControlPaint.DrawBorder(e.Graphics, buton.ClientRectangle,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset);
        }

        public REVERSI()
        {
            InitializeComponent();
        }

        int mutare;
        int mutare_in_plus;
        int score1, score2; //scor1 - negru ;  scor2 - alb
        string castig;
        int[,] mat = new int[9, 9];
        int scortot1 = 0, scortot2 = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
            label2.ForeColor = Color.Black;
            for (int i = 1; i <= 8; i++)
                for (int j = 1; j <= 8; j++)
                {
                    mat[i, j] = 0;
                    Butonas(i, j).BackColor = Color.DarkOliveGreen;
                }

            button45.Enabled = false;
            button44.Enabled = false;
            button55.Enabled = false;
            button54.Enabled = false;
            mat[4, 4] = 1;
            button44.BackColor = Color.White;
            mat[5, 5] = 1;
            button55.BackColor = Color.White;
            mat[4, 5] = -1;
            button45.BackColor = Color.Black;
            mat[5, 4] = -1;
            button54.BackColor = Color.Black;

            mutare = 1;
            mutare_in_plus = 0;
            score1 = 2;
            score2 = 2;

            button1.Enabled = true;
            button2.Enabled = false;
        }
        private void Button65_Click(object sender, EventArgs e)
        {

            mutare++;
            mutare_in_plus++;

            if (mutare % 2 == 1)
            {
                label1.ForeColor = Color.Red;
                label2.ForeColor = Color.Black;
                button1.Enabled = true;
                button2.Enabled = false;
            }
            else if (mutare % 2 == 0)
            {
                label2.ForeColor = Color.Red;
                label1.ForeColor = Color.Black;
                button2.Enabled = true;
                button1.Enabled = false;
            }
        }

        /// conditie de a putea muta + mutare + modificarile aferente
        private int Corect1(int l, int c)
        {

            //prima linie + colturi jos
            if (l == 1 && c == 1)
            {
                if (mat[l, c + 1] == 0 && mat[l + 1, c] == 0 && mat[l + 1, c + 1] == 0)
                    return 0;
            }
            else if (l == 1 && c == 8)
            {
                if (mat[l, c - 1] == 0 && mat[l + 1, c] == 0 && mat[l + 1, c - 1] == 0)
                    return 0;
            }
            else if (l == 1 && c > 1 && c < 8)
            {
                if (mat[l, c - 1] == 0 && mat[l, c + 1] == 0 && mat[l + 1, c - 1] == 0 && mat[l + 1, c] == 0 && mat[l + 1, c + 1] == 0)
                    return 0;
            }

            //ultima linie +colturi sus
            else if (l == 8 && c == 1)
            {
                if (mat[l, c + 1] == 0 && mat[l - 1, c] == 0 && mat[l - 1, c + 1] == 0)
                    return 0;
            }
            else if (l == 8 && c == 8)
            {
                if (mat[l, c - 1] == 0 && mat[l - 1, c] == 0 && mat[l - 1, c - 1] == 0)
                    return 0;
            }
            else if (l == 8 && c > 1 && c < 8)
            {
                if (mat[l, c - 1] == 0 && mat[l, c + 1] == 0 && mat[l - 1, c - 1] == 0 && mat[l - 1, c] == 0 && mat[l - 1, c + 1] == 0)
                    return 0;
            }

            //prima coloana
            else if (c == 1 && l > 1 && l < 8)
            {
                if (mat[l - 1, c] == 0 && mat[l - 1, c + 1] == 0 && mat[l, c + 1] == 0 && mat[l + 1, c] == 0 && mat[l + 1, c + 1] == 0)
                    return 0;
            }

            //ultima coloana
            else if (c == 8 && l > 1 && l < 8)
            {
                if (mat[l - 1, c] == 0 && mat[l - 1, c - 1] == 0 && mat[l, c - 1] == 0 && mat[l + 1, c] == 0 && mat[l + 1, c - 1] == 0)
                    return 0;
            }

            //interior matrice
            else if (c > 1 && c < 8 && l > 1 && l < 8)
            {
                if (mat[l - 1, c - 1] == 0 && mat[l - 1, c] == 0 && mat[l - 1, c + 1] == 0 && mat[l, c - 1] == 0 && mat[l, c + 1] == 0 && mat[l + 1, c - 1] == 0 && mat[l + 1, c] == 0 && mat[l + 1, c + 1] == 0)
                    return 0; //nu e ok
            }

            return 1; //e ok 
        }

        private int Corect2(int l, int c, int culoare, int culoare2)
        {
            //culoare==1 alb   culoare==-1 negru
            if (culoare == 1) culoare2 = -1;
            else if (culoare == -1) culoare2 = 1;

            int fr = 0;

            for (int i = l - 1; i >= 1; i--)
            {
                if (mat[i, c] == 0) break;
                else
                {
                    if (mat[i, c] == culoare2) fr++;

                    if (mat[i, c] == culoare && fr != 0) return 1; // e ok
                    if (mat[i, c] == culoare && fr == 0) break;
                }
            }
            fr = 0;

            for (int i = l + 1; i <= 8; i++)
            {
                if (mat[i, c] == 0) break;
                else
                {
                    if (mat[i, c] == culoare2) fr++;

                    if (mat[i, c] == culoare && fr != 0) return 1; // e ok
                    if (mat[i, c] == culoare && fr == 0) break;
                }
            }
            fr = 0;

            for (int i = c - 1; i >= 1; i--)
            {
                if (mat[l, i] == 0) break;
                else
                {
                    if (mat[l, i] == culoare2) fr++;

                    if (mat[l, i] == culoare && fr != 0) return 1; // e ok
                    if (mat[l, i] == culoare && fr == 0) break;
                }
            }
            fr = 0;

            for (int i = c + 1; i <= 8; i++)
            {
                if (mat[l, i] == 0) break;
                else
                {
                    if (mat[l, i] == culoare2) fr++;

                    if (mat[l, i] == culoare && fr != 0) return 1; // e ok
                    if (mat[l, i] == culoare && fr == 0) break;
                }
            }
            fr = 0;
            int c1 = c;

            for (int i = l - 1; i >= 1 && c1 > 1; i--)
            {
                c1 = c1 - 1;
                if (mat[i, c1] == 0) break;
                else
                {
                    if (mat[i, c1] == culoare2) fr++;

                    if (mat[i, c1] == culoare && fr != 0) return 1; // e ok
                    if (mat[i, c1] == culoare && fr == 0) break;
                }
            }
            fr = 0;
            c1 = c;

            for (int i = l - 1; i >= 1 && c1 < 8; i--)
            {
                c1 = c1 + 1;
                if (mat[i, c1] == 0) break;
                else
                {
                    if (mat[i, c1] == culoare2) fr++;

                    if (mat[i, c1] == culoare && fr != 0) return 1; // e ok
                    if (mat[i, c1] == culoare && fr == 0) break;
                }
            }
            fr = 0;
            c1 = c;

            for (int i = l + 1; i <= 8 && c1 > 1; i++)
            {
                c1 = c1 - 1;
                if (mat[i, c1] == 0) break;
                else
                {
                    if (mat[i, c1] == culoare2) fr++;

                    if (mat[i, c1] == culoare && fr != 0) return 1; // e ok
                    if (mat[i, c1] == culoare && fr == 0) break;
                }


            }
            fr = 0;
            c1 = c;

            for (int i = l + 1; i <= 8 && c1 < 8; i++)
            {
                c1 = c1 + 1;
                if (mat[i, c1] == 0) break;
                else
                {
                    if (mat[i, c1] == culoare2) fr++;

                    if (mat[i, c1] == culoare && fr != 0) return 1; // e ok
                    if (mat[i, c1] == culoare && fr == 0) break;
                }
            }
            return 0; //nu e ok
        }

        private void Modificari(int l, int c, int culoare, int culoare2)
        {
            //culoare==1 alb   culoare==-1 negru
            int fr = 0;

            for (int i = l - 1; i >= 1; i--)
            {
                if (mat[i, c] == 0) break;
                else
                {
                    if (mat[i, c] == culoare2) fr++;

                    if (mat[i, c] == culoare && fr != 0)
                    {
                        for (int j = l - 1; j > i; j--)
                            if (culoare == -1 && mat[j, c] != -1)
                            {
                                Butonas(j, c).BackColor = Color.Black;
                                mat[j, c] = -1;
                                score1++;
                                score2--;

                            }
                            else if (culoare == 1 && mat[j, c] != 1)
                            {
                                Butonas(j, c).BackColor = Color.White;
                                mat[j, c] = 1;
                                score1--;
                                score2++;
                            }
                        break;
                    }
                    if (mat[i, c] == culoare && fr == 0) break;
                }
            }
            fr = 0;

            for (int i = l + 1; i <= 8; i++)
            {
                if (mat[i, c] == 0) break;
                else
                {
                    if (mat[i, c] == culoare2) fr++;

                    if (mat[i, c] == culoare && fr != 0)
                    {
                        for (int j = l + 1; j < i; j++)
                            if (culoare == -1 && mat[j, c] != -1)
                            {
                                Butonas(j, c).BackColor = Color.Black;
                                mat[j, c] = -1;
                                score1++;
                                score2--;

                            }
                            else if (culoare == 1 && mat[j, c] != 1)
                            {
                                Butonas(j, c).BackColor = Color.White;
                                mat[j, c] = 1;
                                score1--;
                                score2++;
                            }
                        break;
                    }
                    if (mat[i, c] == culoare && fr == 0) break;
                }
            }
            fr = 0;

            for (int i = c - 1; i >= 1; i--)
            {
                if (mat[l, i] == 0) break;
                else
                {
                    if (mat[l, i] == culoare2) fr++;

                    if (mat[l, i] == culoare && fr != 0)
                    {
                        for (int j = c - 1; j > i; j--)
                            if (culoare == -1 && mat[l, j] != -1)
                            {
                                Butonas(l, j).BackColor = Color.Black;
                                mat[l, j] = -1;
                                score1++;
                                score2--;

                            }
                            else if (culoare == 1 && mat[l, j] != 1)
                            {
                                Butonas(l, j).BackColor = Color.White;
                                mat[l, j] = 1;
                                score1--;
                                score2++;
                            }
                        break;
                    }
                    if (mat[l, i] == culoare && fr == 0) break;
                }
            }
            fr = 0;

            for (int i = c + 1; i <= 8; i++)
            {
                if (mat[l, i] == 0) break;
                else
                {
                    if (mat[l, i] == culoare2) fr++;

                    if (mat[l, i] == culoare && fr != 0)
                    {
                        for (int j = c + 1; j < i; j++)
                            if (culoare == -1 && mat[l, j] != -1)
                            {
                                Butonas(l, j).BackColor = Color.Black;
                                mat[l, j] = -1;
                                score1++;
                                score2--;

                            }
                            else if (culoare == 1 && mat[l, j] != 1)
                            {
                                Butonas(l, j).BackColor = Color.White;
                                mat[l, j] = 1;
                                score1--;
                                score2++;
                            }
                        break;
                    }
                    if (mat[l, i] == culoare && fr == 0) break;
                }
            }
            fr = 0;
            int c1 = c;

            for (int i = l - 1; i >= 1 && c1 > 1; i--)
            {
                c1 = c1 - 1;
                if (mat[i, c1] == 0) break;
                else
                {
                    if (mat[i, c1] == culoare2) fr++;

                    if (mat[i, c1] == culoare && fr != 0)
                    {
                        int c2 = c;
                        for (int j = l - 1; j > i && c2 > c1; j--)
                        {
                            c2 = c2 - 1;
                            if (culoare == -1 && mat[j, c2] != -1)
                            {
                                Butonas(j, c2).BackColor = Color.Black;
                                mat[j, c2] = -1;
                                score1++;
                                score2--;

                            }
                            else if (culoare == 1 && mat[j, c2] != 1)
                            {
                                Butonas(j, c2).BackColor = Color.White;
                                mat[j, c2] = 1;
                                score1--;
                                score2++;
                            }
                        }
                        break;
                    }
                    if (mat[i, c1] == culoare && fr == 0) break;
                }
            }
            fr = 0;
            c1 = c;

            for (int i = l - 1; i >= 1 && c1 < 8; i--)
            {
                c1 = c1 + 1;
                if (mat[i, c1] == 0) break;
                else
                {
                    if (mat[i, c1] == culoare2) fr++;

                    if (mat[i, c1] == culoare && fr != 0)
                    {
                        int c2 = c;
                        for (int j = l - 1; j > i && c2 < c1; j--)
                        {
                            c2 = c2 + 1;
                            if (culoare == -1 && mat[j, c2] != -1)
                            {
                                Butonas(j, c2).BackColor = Color.Black;
                                mat[j, c2] = -1;
                                score1++;
                                score2--;

                            }
                            else if (culoare == 1 && mat[j, c2] != 1)
                            {
                                Butonas(j, c2).BackColor = Color.White;
                                mat[j, c2] = 1;
                                score1--;
                                score2++;
                            }
                        }
                        break;
                    }
                    if (mat[i, c1] == culoare && fr == 0) break;
                }
            }
            fr = 0;
            c1 = c;

            for (int i = l + 1; i <= 8 && c1 > 1; i++)
            {
                c1 = c1 - 1;
                if (mat[i, c1] == 0) break;
                if (mat[i, c1] == culoare2) fr++;
                if (mat[i, c1] == culoare && fr != 0)
                {
                    int c2 = c;
                    for (int j = l + 1; j < i && c2 > c1; j++)
                    {
                        c2 = c2 - 1;
                        if (culoare == -1 && mat[j, c2] != -1)
                        {
                            Butonas(j, c2).BackColor = Color.Black;
                            mat[j, c2] = -1;
                            score1++;
                            score2--;

                        }
                        else if (culoare == 1 && mat[j, c2] != 1)
                        {
                            Butonas(j, c2).BackColor = Color.White;
                            mat[j, c2] = 1;
                            score1--;
                            score2++;
                        }
                    }
                    break;
                }
                if (mat[i, c1] == culoare && fr == 0) break;

            }
            fr = 0;
            c1 = c;

            for (int i = l + 1; i <= 8 && c1 < 8; i++)
            {
                c1 = c1 + 1;
                if (mat[i, c1] == 0) break;
                else
                {
                    if (mat[i, c1] == culoare2) fr++;

                    if (mat[i, c1] == culoare && fr != 0)
                    {
                        int c2 = c;
                        for (int j = l + 1; j < i && c2 < c1; j++)
                        {
                            c2 = c2 + 1;
                            if (culoare == -1 && mat[j, c2] != -1)
                            {
                                Butonas(j, c2).BackColor = Color.Black;
                                mat[j, c2] = -1;
                                score1++;
                                score2--;

                            }
                            else if (culoare == 1 && mat[j, c2] != 1)
                            {
                                Butonas(j, c2).BackColor = Color.White;
                                mat[j, c2] = 1;
                                score1--;
                                score2++;
                            }
                        }
                        break;
                    }
                    if (mat[i, c1] == culoare && fr == 0) break;
                }
            }
        }

        private Button Butonas(int l, int c)
        {
            string nume = "button" + l + c;
            if (button11.Name == nume) return button11;
            if (button12.Name == nume) return button12;
            if (button13.Name == nume) return button13;
            if (button14.Name == nume) return button14;
            if (button15.Name == nume) return button15;
            if (button16.Name == nume) return button16;
            if (button17.Name == nume) return button17;
            if (button18.Name == nume) return button18;
            if (button21.Name == nume) return button21;
            if (button22.Name == nume) return button22;
            if (button23.Name == nume) return button23;
            if (button24.Name == nume) return button24;
            if (button25.Name == nume) return button25;
            if (button26.Name == nume) return button26;
            if (button27.Name == nume) return button27;
            if (button28.Name == nume) return button28;
            if (button31.Name == nume) return button31;
            if (button32.Name == nume) return button32;
            if (button33.Name == nume) return button33;
            if (button34.Name == nume) return button34;
            if (button35.Name == nume) return button35;
            if (button36.Name == nume) return button36;
            if (button37.Name == nume) return button37;
            if (button38.Name == nume) return button38;
            if (button41.Name == nume) return button41;
            if (button42.Name == nume) return button42;
            if (button43.Name == nume) return button43;
            if (button44.Name == nume) return button44;
            if (button45.Name == nume) return button45;
            if (button46.Name == nume) return button46;
            if (button47.Name == nume) return button47;
            if (button48.Name == nume) return button48;
            if (button51.Name == nume) return button51;
            if (button52.Name == nume) return button52;
            if (button53.Name == nume) return button53;
            if (button54.Name == nume) return button54;
            if (button55.Name == nume) return button55;
            if (button56.Name == nume) return button56;
            if (button57.Name == nume) return button57;
            if (button58.Name == nume) return button58;
            if (button61.Name == nume) return button61;
            if (button62.Name == nume) return button62;
            if (button63.Name == nume) return button63;
            if (button64.Name == nume) return button64;
            if (button65.Name == nume) return button65;
            if (button66.Name == nume) return button66;
            if (button67.Name == nume) return button67;
            if (button68.Name == nume) return button68;
            if (button71.Name == nume) return button71;
            if (button72.Name == nume) return button72;
            if (button73.Name == nume) return button73;
            if (button74.Name == nume) return button74;
            if (button75.Name == nume) return button75;
            if (button76.Name == nume) return button76;
            if (button77.Name == nume) return button77;
            if (button78.Name == nume) return button78;
            if (button81.Name == nume) return button81;
            if (button82.Name == nume) return button82;
            if (button83.Name == nume) return button83;
            if (button84.Name == nume) return button84;
            if (button85.Name == nume) return button85;
            if (button86.Name == nume) return button86;
            if (button87.Name == nume) return button87;
            if (button88.Name == nume) return button88;

            return null;
        }
        private void afisare()
        {
            MessageBoxButtons buton = MessageBoxButtons.YesNo;
            string title = "WOOOW";
            DialogResult result = MessageBox.Show("WANT TO PLAY A NEW GAME?", title, buton, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                label1.ForeColor = Color.Red;
                label2.ForeColor = Color.Black;
                for (int i = 1; i <= 8; i++)
                    for (int j = 1; j <= 8; j++)
                    {
                        mat[i, j] = 0;
                        Butonas(i, j).Enabled = true;
                        Butonas(i, j).BackColor = Color.DarkOliveGreen;
                    }

                button45.Enabled = false;
                button44.Enabled = false;
                button55.Enabled = false;
                button54.Enabled = false;
                mat[4, 4] = 1;
                button44.BackColor = Color.White;
                mat[5, 5] = 1;
                button55.BackColor = Color.White;
                mat[4, 5] = -1;
                button45.BackColor = Color.Black;
                mat[5, 4] = -1;
                button54.BackColor = Color.Black;

                mutare = 1;
                mutare_in_plus = 0;
                score1 = 2;
                score2 = 2;

                button1.Enabled = true;
                button2.Enabled = false;
            }
            else
            {
                string bstplyr;
                if (scortot1 > scortot2) bstplyr = "PLAYER1 😁🤑😫";
                else if (scortot1 < scortot2) bstplyr = "PLAYER2 😛✌😮";
                else bstplyr = "PLAYER1 && PLAYER2 💚";
                MessageBox.Show("BEST PLAYER FROM ALL ROUNDS IS: " + bstplyr);
                this.Close();
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // creeare matrice in functie de culoare (-1 = negru, 1 = alb, 0 = fara culoare)
            Button button = (Button)sender;

            // gasim linia si coloana butonului in matrice
            string nume = button.Name;
            char linie = nume[nume.Length - 2];
            char coloana = nume[nume.Length - 1];
            int l = linie - '0';
            int c = coloana - '0';

            /// modificare scor in functie de pozitionare
            if (button.Enabled && Corect1(l, c) == 1)
            {

                if (mutare % 2 == 1)
                {
                    if (Corect2(l, c, -1, 0) == 1)
                    {
                        mat[l, c] = -1;
                        score1++;
                        Modificari(l, c, -1, 1);
                        button.BackColor = Color.Black;

                        label4.ResetText();
                        label4.Text = "SCORE = " + score1;
                        label5.ResetText();
                        label5.Text = "SCORE = " + score2;

                        button.Enabled = false;
                        mutare++;
                    }
                    else
                    {
                        MessageBox.Show("NOT ABLE TO MOVE THERE");
                    }
                }
                else if (mutare % 2 == 0)
                {
                    if (Corect2(l, c, 1, 0) == 1)
                    {
                        mat[l, c] = 1;
                        score2++;
                        Modificari(l, c, 1, -1);
                        button.BackColor = Color.White;

                        label4.ResetText();
                        label4.Text = "SCORE = " + score1;
                        label5.ResetText();
                        label5.Text = "SCORE = " + score2;

                        button.Enabled = false;
                        mutare++;
                    }
                    else
                    {
                        MessageBox.Show("NOT ABLE TO MOVE THERE");
                    }
                }

            }
            else MessageBox.Show("NOT ABLE TO MOVE THERE");

            if (mutare % 2 == 1)
            {
                label1.ForeColor = Color.Red;
                label2.ForeColor = Color.Black;
                button1.Enabled = true;
                button2.Enabled = false;
            }
            else if (mutare % 2 == 0)
            {
                label2.ForeColor = Color.Red;
                label1.ForeColor = Color.Black;
                button2.Enabled = true;
                button1.Enabled = false;
            }

            // afisare rezultat
            if (score1 == 0)
            {
                scortot2++;
                MessageBox.Show("CONGRATULATION PLAYER2 YOU WON !!!");
                afisare();
            }
            else if (score2 == 0)
            {
                scortot1++;
                MessageBox.Show("CONGRATULATION PLAYER1 YOU WON !!!");
                afisare();
            }

            if (mutare - mutare_in_plus - 1 == 60)
            {
                if (score1 > score2)
                {
                    scortot1++;
                    castig = label1.Text;
                    MessageBox.Show("CONGRATULATION " + castig + " YOU WON !!!");
                    afisare();
                }
                else if (score2 > score1)
                {
                    scortot2++;
                    castig = label2.Text;
                    MessageBox.Show("CONGRATULATION " + castig + " YOU WON !!!");
                    afisare();
                }
                else
                {
                    MessageBox.Show(" DRAW :)");
                    afisare();
                }
            }
        }
    }
}
