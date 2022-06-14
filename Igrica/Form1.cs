using Igrica.Model;
using System.Media;

namespace Igrica
{
    public partial class Form1 : Form
    {
        private int brSmecaUKanti;

        private Dictionary<string, Rectangle> tipoviKantiZaSmeceIPozicije = new();

        private PromenljiveBase? promenljive;  //Zato sto ce svaka promenljiva naslediti abstraktnu klasu 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // var velicinaEkrana = Screen.PrimaryScreen.WorkingArea.Size;   //pazi ako zelis da vidis velicinu ekrana, stavi u neku drugu f-ju

            RasiriProzorPrekoCelogEkrana();  

            InicijalizujTipoveKantiZaSmeceINjihovePozicije();

            PostaviDefaultVrednostLabela();

            panelIzaberiIgru.Dock = DockStyle.Fill;

        }
        private Dictionary<PictureBox, PictureBox> DictionarySmeceISmeceUPanelu()
        {
            return new Dictionary<PictureBox, PictureBox>()
            {
                {pictureBox1, pictureBox1Panel },
                {pictureBox2, pictureBox2Panel },
                {pictureBox3, pictureBox3Panel },
                {pictureBox4, pictureBox4Panel },
                {pictureBox5, pictureBox5Panel },
            };
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

               if(brSmecaUKanti == promenljive.BrSmeca)
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

        private async Task LogikaSmeceUKanti(PictureBox pictureBox, string tipSmeca)
        {
            var pozicijaSmeca = new Rectangle(pictureBox.Location, pictureBox.Size);

            foreach(var kanta in tipoviKantiZaSmeceIPozicije)
            {
                if (pozicijaSmeca.IntersectsWith(kanta.Value))
                {
                    if (tipSmeca == kanta.Key)
                    {
                        promenljive.SakriSmece(pictureBox);
                        brSmecaUKanti++;
                        label1.Text = brSmecaUKanti.ToString();
                        label2.Text = $"БРАВО! {brSmecaUKanti}/{promenljive.BrSmeca}";
                        await Task.Delay(1000);
                        label2.Text = "";
                    }
                    else
                    {
                        pictureBox.Location = promenljive.DefaultPozicijeSmeca[pictureBox.Name];
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
            ResetujSveNaEkranu();
        }

        private void ResetujSveNaEkranu()
        {
            brSmecaUKanti = 0;
            label1.Text = brSmecaUKanti.ToString();
            label2.Text = "";

            promenljive.ResetSmece();
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
            label1.Text = brSmecaUKanti.ToString();
            label2.Text = "";
            label3.Text = "";
        }

        private void StartIgruSoba_Click(object sender, EventArgs e)
        {
            panelIzaberiIgru.Hide();
            promenljive = new PromenljiveSoba(DictionarySmeceISmeceUPanelu(), this,
                                              @"C:\Users\PC\source\repos\ProjekatZaFakultet\Igrica\slike\bg1.jpg");
        }

        private void StartIgruSuma_Click(object sender, EventArgs e)
        {
            panelIzaberiIgru.Hide();
            promenljive = new PromenljiveSuma(DictionarySmeceISmeceUPanelu(), this,
                                             @"C:\Users\PC\source\repos\ProjekatZaFakultet\Igrica\slike\bg2.jpg");
        }

        private void StartIgruUcionica_Click(object sender, EventArgs e)
        {
            panelIzaberiIgru.Hide();
            promenljive = new PromenljiveUcionica(DictionarySmeceISmeceUPanelu(), this,
                                             @"C:\Users\PC\source\repos\ProjekatZaFakultet\Igrica\slike\bg3.jpg");
        }

        private void Back_Click(object sender, EventArgs e)
        {
            panelIzaberiIgru.Show();
            panelIzaberiIgru.BringToFront();
            ResetujSveNaEkranu();
        }
    }
}