namespace Tiefgarage
{
    public partial class MainWindow
    {
        public void AnzeigeAktualisieren()
        {
            this.Dispatcher.Invoke(() =>
                       {
                           if (FensterAktiv)
                           {

                               if (AnzahlFahrzeuge_Alt != AnzahlFahrzeuge)
                               {
                                   AnzahlFahrzeuge_Alt = AnzahlFahrzeuge;
                                   lbl_FahrzeugZaehler.Content = "Autos in der Garage: " + AnzahlFahrzeuge.ToString();
                               }

                               if (AnzahlPersonen_Alt != AnzahlPersonen)
                               {
                                   AnzahlPersonen_Alt = AnzahlPersonen;
                                   lbl_PersonenZaehler.Content = "Personen in der Garage: " + AnzahlPersonen.ToString();
                               }

                               foreach (AlleFahrzeugePersonen fp in gAlleFahrzeugePersonen) fp.updatePosition();

                               if (Pegel_B1_Alt != Pegel_B1)
                               {
                                   Pegel_B1_Alt = Pegel_B1;
                                   if (Pegel_B1) circ_Lichtschranke_draussen_rechts.Fill = System.Windows.Media.Brushes.Red; else circ_Lichtschranke_draussen_rechts.Fill = System.Windows.Media.Brushes.LawnGreen;
                               }
                               if (Pegel_B2_Alt != Pegel_B2)
                               {
                                   Pegel_B2_Alt = Pegel_B2;
                                   if (Pegel_B2) circ_Lichtschranke_drinnen_rechts.Fill = System.Windows.Media.Brushes.Red; else circ_Lichtschranke_drinnen_rechts.Fill = System.Windows.Media.Brushes.LawnGreen;

                               }
                           }
                       });
        }

        public void alleBtnAktivieren()
        {
            foreach (AlleFahrzeugePersonen fp in gAlleFahrzeugePersonen)
            {
                this.Dispatcher.Invoke(() =>
                {
                    fp.btnAktivieren();
                });
            }
        }
        public void alleBtnDeaktivieren()
        {
            foreach (AlleFahrzeugePersonen fp in gAlleFahrzeugePersonen)
            {
                this.Dispatcher.Invoke(() =>
                {
                    fp.btnDeaktivieren();
                });
            }

        }
    }
}
