using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LAP_2018_Abfuellanlage
{
    public partial class MainWindow
    {
        private readonly double HoeheFuellBalken = 9 * 35;

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
            foreach (Flaschen flasche in gAlleFlaschen) { flasche.AnzeigeAktualisieren(FensterAktiv); }

            BildSichbarkeitUmschalten(K1, imgVentilFuellenEin, imgVentilFuellenAus);
            BildSichbarkeitUmschalten(K2, imgVentilVereinzelnEin, imgVentilVereinzelnAus);
            BildSichbarkeitUmschalten(B1, imgEndschalter_Geschlossen, imgEndschalter_Offen);

            KnopfHintergrundfarbeAendern(F5, btnF5, Brushes.LightGray, Brushes.Red); // Thermorelais --> Öffner!!

            KreisHintergrundfarbeAendern(M1, cirRolleLinksEin, Brushes.Green, Brushes.White);
            KreisHintergrundfarbeAendern(P1, circP1, Brushes.Green, Brushes.White);
            KreisHintergrundfarbeAendern(P2, circP2, Brushes.Red, Brushes.White);

            rctBehaelterVoll.Margin = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - Pegel), 0, 0);
            RechteckHintergrundfarbeAendern((Pegel > 0.01), rctFuellenOben, Brushes.Blue, Brushes.LightBlue);
            RechteckSichbarkeitUmschalten((K1 && (Pegel > 0.01)), rctFuellenUnten);
        }

        private void KnopfHintergrundfarbeAendern(bool Bedingung, Button Knopf, System.Windows.Media.Brush brushEin, System.Windows.Media.Brush brushAus)
        {
            if (Bedingung) Knopf.Background = brushEin; else Knopf.Background = brushAus;
        }

        private void RechteckHintergrundfarbeAendern(bool Bedingung, Rectangle rechteck, System.Windows.Media.Brush brushEin, System.Windows.Media.Brush brushAus)
        {
            if (Bedingung) rechteck.Fill = brushEin; else rechteck.Fill = brushAus;
        }

        private void KreisHintergrundfarbeAendern(bool Bedingung, Ellipse kreis, System.Windows.Media.Brush brushEin, System.Windows.Media.Brush brushAus)
        {
            if (Bedingung) kreis.Fill = brushEin; else kreis.Fill = brushAus;
        }

        public static void BildSichbarkeitUmschalten(bool Bedingung, Image imgEin, Image imgAus)
        {
            if (Bedingung) imgEin.Visibility = System.Windows.Visibility.Visible; else imgEin.Visibility = System.Windows.Visibility.Hidden;
            if (Bedingung) imgAus.Visibility = System.Windows.Visibility.Hidden; else imgAus.Visibility = System.Windows.Visibility.Visible;
        }

        public static void RechteckSichbarkeitUmschalten(bool Bedingung, Rectangle rechteck)
        {
            if (Bedingung) rechteck.Visibility = System.Windows.Visibility.Visible; else rechteck.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}