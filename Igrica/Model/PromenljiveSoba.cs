using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igrica.Model
{
    public class PromenljiveSoba : PromenljiveBase
    {
        public PromenljiveSoba(Dictionary<PictureBox, PictureBox> listaSmecaSaSmecemUPanelu,
                               Form form1, string imgRoute) 
                               : base(listaSmecaSaSmecemUPanelu, form1, imgRoute)
        {
        }


        protected override void PostaviSlikeSmeca()
        {
        //    throw new NotImplementedException();
        }

        protected override void PostaviDefaultPozicijeSmeca()
        {
            Random rnd = new();
            int num;

            List<Point> listaSvihLokacijaPredmeta = new();

            foreach (var pBox in _listaSmecaSaSmecemUPanelu)
            {
                listaSvihLokacijaPredmeta.Add(pBox.Key.Location);
            }

            foreach (var pBox in _listaSmecaSaSmecemUPanelu)
            {
                num = rnd.Next(0, listaSvihLokacijaPredmeta.Count);

                pBox.Key.Location = listaSvihLokacijaPredmeta[num];
                this.DefaultPozicijeSmeca.Add(pBox.Key.Name, pBox.Key.Location);

                listaSvihLokacijaPredmeta.RemoveAt(num);
            }
        }
    }
}
