using System;
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
                    if (FensterAktiv) AnzeigeAktualisieren();
                });
                Thread.Sleep(10);
            }
        }

        public void AnzeigeAktualisieren()
        {
            /*
            Tuple<bool, bool> Lichtschranken;

            lbl_FahrzeugZaehler.Content = "Autos in der Garage: " + AnzahlFahrzeuge.ToString();
            lbl_PersonenZaehler.Content = "Personen in der Garage: " + AnzahlPersonen.ToString();

            B1 = false;
            B2 = false;

            foreach (Button btn in gAlleButtons)
            {
                var fp = btn.Tag as FahrzeugPerson;
                Lichtschranken = fp.Bewegen();

                B1 |= Lichtschranken.Item1;
                B2 |= Lichtschranken.Item2;

                btn.SetValue(Canvas.LeftProperty, fp.PosAktuell.X);
                btn.SetValue(Canvas.TopProperty, fp.PosAktuell.Y);
            }

            if (B1) circ_Lichtschranke_draussen_rechts.Fill = System.Windows.Media.Brushes.Red; else circ_Lichtschranke_draussen_rechts.Fill = System.Windows.Media.Brushes.LawnGreen;
            if (B2) circ_Lichtschranke_drinnen_rechts.Fill = System.Windows.Media.Brushes.Red; else circ_Lichtschranke_drinnen_rechts.Fill = System.Windows.Media.Brushes.LawnGreen;
            */
        }
    }
}