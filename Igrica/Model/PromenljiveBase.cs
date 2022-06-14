using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igrica.Model
{
    public abstract class PromenljiveBase 
    {

        protected Dictionary<PictureBox, PictureBox> _listaSmecaSaSmecemUPanelu;

        public PromenljiveBase(Dictionary<PictureBox, PictureBox> listaSmecaSaSmecemUPanelu,
                               Form form1, string imgRoute)
        {
            _listaSmecaSaSmecemUPanelu = listaSmecaSaSmecemUPanelu;
            BrSmeca = _listaSmecaSaSmecemUPanelu.Count;

            form1.BackgroundImage = new Bitmap(imgRoute);                  
            PostaviSlikeSmeca();
            PostaviDefaultPozicijeSmeca();
        }

        public int BrSmeca { get; }
        public Dictionary<string, Point> DefaultPozicijeSmeca { get; private set; } = new Dictionary<string, Point>();

        public void ResetSmece()
        {
            foreach (var pBox in _listaSmecaSaSmecemUPanelu)
            {
                pBox.Key.Show();
                pBox.Value.Show();
                pBox.Key.Location = DefaultPozicijeSmeca[pBox.Key.Name];  //Zato sto pamti pocetnu poziciju 
            }

            DefaultPozicijeSmeca = new Dictionary<string, Point>();
            PostaviDefaultPozicijeSmeca();
        }
        public void SakriSmece(PictureBox pictureBox)
        {
            pictureBox.Hide();
            _listaSmecaSaSmecemUPanelu[pictureBox].Hide();
        }
        public void PrikaziSmece(PictureBox pictureBox)
        {
            pictureBox.Show();
            _listaSmecaSaSmecemUPanelu[pictureBox].Show();
        }

        protected abstract void PostaviSlikeSmeca();
        protected abstract void PostaviDefaultPozicijeSmeca();  //OBAVEZNO POSTAVI POCETNE POZICIJE ZA NOVE MAPE

    }
}
