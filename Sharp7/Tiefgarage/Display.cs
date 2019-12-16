using System.Threading;
using System.Windows.Controls;

namespace Tiefgarage
{
    public partial class MainWindow
    {
        public void Display_Task()
        {
            while (FensterAktiv)
            {
                this.Dispatcher.Invoke(() =>
                {
                    if (FensterAktiv)
                    {
                        AnzeigeAktualisieren();
                    }
                });
                Thread.Sleep(10);
            }
        }

        public void AnzeigeAktualisieren()
        {
            lbl_FahrzeugZaehler.Content = "Autos in der Garage: " + AnzahlFahrzeuge.ToString();
            lbl_PersonenZaehler.Content = "Personen in der Garage: " + AnzahlPersonen.ToString();

            foreach (Button btn in gAlleButtons)
            {
                var fp = btn.Tag as FahrzeugPerson;
                fp.UpdatePosition();
            }

            if (Pegel_B1) circ_Lichtschranke_draussen_rechts.Fill = System.Windows.Media.Brushes.Red; else circ_Lichtschranke_draussen_rechts.Fill = System.Windows.Media.Brushes.LawnGreen;
            if (Pegel_B2) circ_Lichtschranke_drinnen_rechts.Fill = System.Windows.Media.Brushes.Red; else circ_Lichtschranke_drinnen_rechts.Fill = System.Windows.Media.Brushes.LawnGreen;
        }

        public void AlleBtnAktivieren()
        {
            this.Dispatcher.Invoke(() =>
            {
                foreach (Button btn in gAlleButtons)
                {
                    btn.IsEnabled = true;
                }
            });
        }
        public void AlleBtnDeaktivieren()
        {
            this.Dispatcher.Invoke(() =>
            {
                foreach (Button btn in gAlleButtons)
                {
                    btn.IsEnabled = false;
                }
            });
        }

    }
}
