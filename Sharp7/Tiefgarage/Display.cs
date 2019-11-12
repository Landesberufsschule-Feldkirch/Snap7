using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tiefgarage
{
    public partial class MainWindow
    {

        public void AnzeigeAktualisieren()
        {
            this.Dispatcher.Invoke(() =>
            {
                lbl_FahrzeugZaehler.Content = "Autos in der Garage: " + DigOutput[0].ToString();
                lbl_PersonenZaehler.Content = "Personen in der Garage: " + DigOutput[1].ToString();

                foreach (AlleFahrzeugePersonen fp in gAlleFahrzeugePersonen)
                {
                    fp.updatePosition();
                }

                if (Pegel_B1) circ_Lichtschranke_draussen_rechts.Fill = System.Windows.Media.Brushes.Red; else circ_Lichtschranke_draussen_rechts.Fill = System.Windows.Media.Brushes.LawnGreen;
                if (Pegel_B2) circ_Lichtschranke_drinnen_rechts.Fill = System.Windows.Media.Brushes.Red; else circ_Lichtschranke_drinnen_rechts.Fill = System.Windows.Media.Brushes.LawnGreen;

            });
        }
    }
}
