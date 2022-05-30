
using System.Media;

namespace Igrica
{
    public partial class Form1 : Form
    {

        private int ubacenoSmecaUKante, label1Vrednost, brojPictureBoxova;

        private Dictionary<string, Rectangle> tipoviKantiZaSmeceIPozicije = new();
        private Dictionary<string, Point> defaultPozicije = new();

        private List<PictureBox> listaPictureBoxovaUPanelu = new();
        private List<PictureBox> listaPictureBoxova = new();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;  

            PanelPictureBoxToList();
            PictureBoxToList();
            InicijalizujTipoviKantiZaSmeceIPozicije();

            brojPictureBoxova = listaPictureBoxova.Count;
            label1.Text = label1Vrednost.ToString();
            label2.Text = "";
            label3.Text = "";
            PostaviDefaultPozicije();
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

               if(label1Vrednost == brojPictureBoxova)
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
        private void PostaviDefaultPozicije()
        {
            //var velicinaEkrana = Screen.PrimaryScreen.WorkingArea.Size; 

            Random rnd = new();
            int num;

            List<Point> listaSvihLokacijaPredmeta = new();

            foreach(PictureBox pBox in listaPictureBoxova)
            {
                listaSvihLokacijaPredmeta.Add(pBox.Location);
            }
            
            foreach(PictureBox pBox in listaPictureBoxova)
            {
                num = rnd.Next(0, listaSvihLokacijaPredmeta.Count);

                pBox.Location = listaSvihLokacijaPredmeta[num];
                defaultPozicije.Add(pBox.Name, pBox.Location);

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
                        ObrisiIzPanela(pictureBox.Name);
                        ubacenoSmecaUKante++;
                        label1Vrednost++;
                        label1.Text = label1Vrednost.ToString();
                        label2.Text = $"БРАВО! {label1Vrednost}/{brojPictureBoxova}";
                        await Task.Delay(1000);
                        label2.Text = "";
                    }
                    else
                    {
                        pictureBox.Location = defaultPozicije[pictureBox.Name];
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
            ubacenoSmecaUKante = 0;
            label1Vrednost = 0;
            label1.Text = label1Vrednost.ToString();
            label2.Text = "";

            foreach(PictureBox pBox in listaPictureBoxova)
            {
                pBox.Show();           
                pBox.Location = defaultPozicije[pBox.Name]; 
            }


            defaultPozicije = new Dictionary<string, Point>();
            PostaviDefaultPozicije();

            PanelPictureBoxToList();

            foreach(PictureBox pBoxPanel in listaPictureBoxovaUPanelu)
            {
                pBoxPanel.Show();
            }
        }

        private void ObrisiIzPanela(string pictureBoxName)
        {
            string pictureBoxNameForDelete = $"{pictureBoxName}Panel";

            foreach(PictureBox pictureBoxPanel in listaPictureBoxovaUPanelu)
            {
                if(pictureBoxPanel.Name == pictureBoxNameForDelete)
                {
                    pictureBoxPanel.Hide();
                    listaPictureBoxovaUPanelu.Remove(pictureBoxPanel);
                    return;
                }
            }
        }
        private void PanelPictureBoxToList()
        {
            listaPictureBoxovaUPanelu.Add(pictureBox1Panel);
            listaPictureBoxovaUPanelu.Add(pictureBox2Panel);
            listaPictureBoxovaUPanelu.Add(pictureBox3Panel);
            listaPictureBoxovaUPanelu.Add(pictureBox4Panel);
            listaPictureBoxovaUPanelu.Add(pictureBox5Panel);

        }
        private void PictureBoxToList()
        {
            listaPictureBoxova.Add(pictureBox1);
            listaPictureBoxova.Add(pictureBox2);
            listaPictureBoxova.Add(pictureBox3);
            listaPictureBoxova.Add(pictureBox4);
            listaPictureBoxova.Add(pictureBox5);
        }
        private void InicijalizujTipoviKantiZaSmeceIPozicije()
        { 
            tipoviKantiZaSmeceIPozicije.Add(Smece.Plastika.ToString(), new Rectangle(KantaPlastika.Location, KantaPlastika.Size));
            tipoviKantiZaSmeceIPozicije.Add(Smece.Papir.ToString(), new Rectangle(KantaPapir.Location, KantaPapir.Size));
            tipoviKantiZaSmeceIPozicije.Add(Smece.Staklo.ToString(), new Rectangle(KantaStaklo.Location, KantaStaklo.Size));

        }

    }
}