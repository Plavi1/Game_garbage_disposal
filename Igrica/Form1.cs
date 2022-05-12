
namespace Igrica
{
    public partial class Form1 : Form
    {
        private string[] tipoviSmeca = { "staklo", "papir", "plastika" };
        private int ubacenoSmecaUKante = 0;
        private int labelVrednost = 0;

        private Dictionary<string, Point> defaultPozicije = new ();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;   
            label1.Text = labelVrednost.ToString();
            label2.Text = "";
            PostaviDefaultPozicije();
        }

        private async void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            await Logika(pictureBox1, e, tipoviSmeca[0]);
        }

        private async void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
           await Logika(pictureBox2 , e, tipoviSmeca[1]);
        }

        private async void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
           await Logika(pictureBox3 , e, tipoviSmeca[1]);
        }
        private async void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            await Logika(pictureBox4, e, tipoviSmeca[2]);
        }
        private async void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
           await Logika(pictureBox5, e, tipoviSmeca[2]);
        }

        private async Task Logika(PictureBox pictureBox, MouseEventArgs e, string tipSmeca)
        {
            int misPozicijaX = 25, misPozicijaY = 25;

            if (e.Button == 0)
            {
                misPozicijaX = e.X;     
                misPozicijaY = e.Y;

               await LogikaSmeceUKanti(pictureBox, tipSmeca); //funkcija koja proverava da li je smece u kanti 

               if(labelVrednost == 5)
                {
                    label2.Text = "USPELI STE DA OCISTITE SMECE!";
                }
            }
            else                     
            {
                pictureBox.BringToFront();
                pictureBox.Left += (e.X - misPozicijaX);
                pictureBox.Top += (e.Y - misPozicijaY);
            }
        }
        private void PostaviDefaultPozicije()
        {
            var velicinaEkrana = Screen.PrimaryScreen.WorkingArea.Size; 

            // postavljamo default pozicije onako kako su na Form Designu stavljene
            defaultPozicije.Add(pictureBox1.Name, pictureBox1.Location);
            defaultPozicije.Add(pictureBox2.Name, pictureBox2.Location);
            defaultPozicije.Add(pictureBox3.Name, pictureBox3.Location);
            defaultPozicije.Add(pictureBox4.Name, pictureBox4.Location);
            defaultPozicije.Add(pictureBox5.Name, pictureBox5.Location);
        }
        private async Task LogikaSmeceUKanti(PictureBox pictureBox, string tipSmeca)
        {
            var pozicijaSmeca = new System.Drawing.Rectangle(pictureBox.Location, pictureBox.Size);

            var pozicijaKantePlastika = new System.Drawing.Rectangle(KantaPlastika.Location, KantaPlastika.Size);
            var pozicijaKantePapir = new System.Drawing.Rectangle(KantaPapir.Location, KantaPapir.Size);
            var pozicijaKanteStaklo = new System.Drawing.Rectangle(KantaStaklo.Location, KantaStaklo.Size);

            //Provera KantaStaklo
            if (pozicijaSmeca.IntersectsWith(pozicijaKanteStaklo))
            {
                if(tipSmeca == "staklo")
                {
                    pictureBox.Hide();
                    ubacenoSmecaUKante++;
                    labelVrednost++;
                    label1.Text = labelVrednost.ToString();
                    label2.Text = $"Bravo! {labelVrednost}/5";
                    await Task.Delay(3000);
                    label2.Text = "";
                }
                else
                {
                    pictureBox.Location = defaultPozicije[pictureBox.Name];
                    MessageBox.Show("Pogresna kanta!");
                }
            }

            //Provera KantaPlastika
            else if (pozicijaSmeca.IntersectsWith(pozicijaKantePlastika))
            {
                if (tipSmeca == "plastika")
                {
                    pictureBox.Hide();
                    ubacenoSmecaUKante++;
                    labelVrednost++;
                    label1.Text = labelVrednost.ToString();
                    label2.Text = $"Bravo! {labelVrednost}/5";
                    await Task.Delay(3000);
                    label2.Text = "";
                }
                else
                {
                    pictureBox.Location = defaultPozicije[pictureBox.Name];
                    MessageBox.Show("Pogresna kanta!");
                }
            }

            //Provera KantaPapir
            else if (pozicijaSmeca.IntersectsWith(pozicijaKantePapir))
            {
                if (tipSmeca == "papir")
                {
                    pictureBox.Hide();
                    ubacenoSmecaUKante++;
                    labelVrednost++;
                    label1.Text = labelVrednost.ToString();
                    label2.Text = $"Bravo! {labelVrednost}/5";
                    await Task.Delay(3000);
                    label2.Text = "";

                }
                else
                {
                    pictureBox.Location = defaultPozicije[pictureBox.Name];
                    MessageBox.Show("Pogresna kanta!");
                }
            }

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetAll_Click(object sender, EventArgs e)
        {
            ubacenoSmecaUKante = 0;
            labelVrednost = 0;
            label1.Text = labelVrednost.ToString();
            label2.Text = "";

            pictureBox1.Show();
            pictureBox2.Show();
            pictureBox3.Show();
            pictureBox4.Show();
            pictureBox5.Show();

            pictureBox1.Location = defaultPozicije[pictureBox1.Name];
            pictureBox2.Location = defaultPozicije[pictureBox2.Name];
            pictureBox3.Location = defaultPozicije[pictureBox3.Name];
            pictureBox4.Location = defaultPozicije[pictureBox4.Name];
            pictureBox5.Location = defaultPozicije[pictureBox5.Name];
        }
    }
}