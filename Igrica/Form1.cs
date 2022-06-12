
using System.Media;

namespace Igrica
{
    public partial class Form1 : Form
    {
        private int brSmecaUKanti, brSmeca;

        private Dictionary<PictureBox, PictureBox> listaSmecaSaSmecemUPanelu = new();
        private Dictionary<string, Rectangle> tipoviKantiZaSmeceIPozicije = new();
        private Dictionary<string, Point> defaultPozicijeSmeca = new();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var velicinaEkrana = Screen.PrimaryScreen.WorkingArea.Size;

            RasiriProzorPrekoCelogEkrana();  //Expand the screen

            InicijalizujListuSmecaSaSmecemUPanelu();
            InicijalizujTipoveKantiZaSmeceINjihovePozicije();

            PostaviDefaultPozicijeSmeca();
            PostaviDefaultVrednostLabela();
        }

        private async void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            await Logika(pictureBox1, e, Smece.Staklo.ToString());
        }

        private async void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
           await Logika(pictureBox2 , e, Smece.Papir.ToString());
        }

        private async void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
           await Logika(pictureBox3 , e, Smece.Papir.ToString());
        }
        private async void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            await Logika(pictureBox4, e, Smece.Plastika.ToString());
        }
        private async void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
           await Logika(pictureBox5, e, Smece.Plastika.ToString());
        }

        private async Task Logika(PictureBox pictureBox, MouseEventArgs e, string tipSmeca)
        {
            int misPozicijaX = 25, misPozicijaY = 25;

            if (e.Button == 0)
            {
                misPozicijaX = e.X;     
                misPozicijaY = e.Y;

               await LogikaSmeceUKanti(pictureBox, tipSmeca); //funkcija koja proverava da li je smece u kanti 

               if(brSmecaUKanti == brSmeca)
               {
                    SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\PC\source\repos\ProjekatZaFakultet\Igrica\wavfile\Win.wav");
                    simpleSound.Play();
                    label2.Text = "УСПЕЛИ СТЕ!";
                }
            }
            else                     
            {
                pictureBox.BringToFront();
                pictureBox.Left += (e.X - misPozicijaX);
                pictureBox.Top += (e.Y - misPozicijaY);
            }
        }
        private void PostaviDefaultPozicijeSmeca()
        {
            Random rnd = new();
            int num;

            List<Point> listaSvihLokacijaPredmeta = new();

            foreach(var pBox in listaSmecaSaSmecemUPanelu)
            {
                listaSvihLokacijaPredmeta.Add(pBox.Key.Location);
            }
            
            foreach(var pBox in listaSmecaSaSmecemUPanelu)
            {
                num = rnd.Next(0, listaSvihLokacijaPredmeta.Count);

                pBox.Key.Location = listaSvihLokacijaPredmeta[num];
                defaultPozicijeSmeca.Add(pBox.Key.Name, pBox.Key.Location);

                listaSvihLokacijaPredmeta.RemoveAt(num);
            }
        }

        private async Task LogikaSmeceUKanti(PictureBox pictureBox, string tipSmeca)
        {
            var pozicijaSmeca = new Rectangle(pictureBox.Location, pictureBox.Size);

            foreach(var kanta in tipoviKantiZaSmeceIPozicije)
            {
                if (pozicijaSmeca.IntersectsWith(kanta.Value))
                {
                    if (tipSmeca == kanta.Key)
                    {
                        pictureBox.Hide();
                        listaSmecaSaSmecemUPanelu[pictureBox].Hide();
                        brSmecaUKanti++;
                        label1.Text = brSmecaUKanti.ToString();
                        label2.Text = $"БРАВО! {brSmecaUKanti}/{brSmeca}";
                        await Task.Delay(1000);
                        label2.Text = "";
                    }
                    else
                    {
                        pictureBox.Location = defaultPozicijeSmeca[pictureBox.Name];
                        label3.Text = "ПОГРЕШНА КАНТА!";
                        await Task.Delay(1000);
                        label3.Text = "";
                    }
                }
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetAll_Click(object sender, EventArgs e)
        {
            brSmecaUKanti = 0;
            label1.Text = brSmecaUKanti.ToString();
            label2.Text = "";

            foreach(var pBox in listaSmecaSaSmecemUPanelu)
            {
                pBox.Key.Show();
                pBox.Value.Show();
                pBox.Key.Location = defaultPozicijeSmeca[pBox.Key.Name];  //Zato sto pamti pocetnu poziciju 
            }

            defaultPozicijeSmeca = new Dictionary<string, Point>();
            PostaviDefaultPozicijeSmeca();
        }

        private void InicijalizujListuSmecaSaSmecemUPanelu()
        {
            listaSmecaSaSmecemUPanelu.Add(pictureBox1, pictureBox1Panel);
            listaSmecaSaSmecemUPanelu.Add(pictureBox2, pictureBox2Panel);
            listaSmecaSaSmecemUPanelu.Add(pictureBox3, pictureBox3Panel);
            listaSmecaSaSmecemUPanelu.Add(pictureBox4, pictureBox4Panel);
            listaSmecaSaSmecemUPanelu.Add(pictureBox5, pictureBox5Panel);
        }
        private void InicijalizujTipoveKantiZaSmeceINjihovePozicije()
        { 
            tipoviKantiZaSmeceIPozicije.Add(Smece.Plastika.ToString(), new Rectangle(KantaPlastika.Location, KantaPlastika.Size));
            tipoviKantiZaSmeceIPozicije.Add(Smece.Papir.ToString(), new Rectangle(KantaPapir.Location, KantaPapir.Size));
            tipoviKantiZaSmeceIPozicije.Add(Smece.Staklo.ToString(), new Rectangle(KantaStaklo.Location, KantaStaklo.Size));

        }
        private void RasiriProzorPrekoCelogEkrana()
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }
        private void PostaviDefaultVrednostLabela()
        {
            brSmeca = listaSmecaSaSmecemUPanelu.Count;
            label1.Text = brSmecaUKanti.ToString();
            label2.Text = "";
            label3.Text = "";
        }

    }
}