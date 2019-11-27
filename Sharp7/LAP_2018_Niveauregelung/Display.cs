using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LAP_2018_Niveauregelung
{
    public partial class MainWindow
    {
        double HoeheFuellBalken = 315;
        public void AnzeigeAktualisieren()
        {


    



            this.Dispatcher.Invoke(() =>
                       {
                           if (FensterAktiv)
                           {
                               imgSichbarkeitUmschalten(B1, imgB1Ein, imgB1Aus);
                               imgSichbarkeitUmschalten(B2, imgB2Ein, imgB2Aus);
                               imgSichbarkeitUmschalten(B3, imgB3Ein, imgB3Aus);

                               rctFarbenUmschalten(M1, ZuleitungLinksWaagrecht, Brushes.Blue, Brushes.LightBlue);
                               rctFarbenUmschalten(M1, ZuleitungLinksSenkrecht, Brushes.Blue, Brushes.LightBlue);

                               rctFarbenUmschalten(M2, ZuleitungRechtsWaagrecht, Brushes.Blue, Brushes.LightBlue);
                               rctFarbenUmschalten(M2, ZuleitungRechtsSenkrecht, Brushes.Blue, Brushes.LightBlue);

                               imgSichbarkeitUmschalten(M1, M1_Ein, M1_Aus);
                               imgSichbarkeitUmschalten(M2, M2_Ein, M2_Aus);

                               if (F1) btnF1.Background = System.Windows.Media.Brushes.LawnGreen; else btnF1.Background = System.Windows.Media.Brushes.Red;
                               if (F2) btnF2.Background = System.Windows.Media.Brushes.LawnGreen; else btnF2.Background = System.Windows.Media.Brushes.Red;

                               btnSichbarkeitUmschalten(Y1, btnVentilEin, btnVentilAus);

                               BehaelterVoll.Margin = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - Pegel), 0, 0);

                               rctFarbenUmschalten((Pegel > 0.01), Ableitung, Brushes.Blue, Brushes.LightBlue);
                               rctFarbenUmschalten(Y1 && (Pegel > 0.01), AbleitungUnten, Brushes.Blue, Brushes.LightBlue);

                               circFarbenUmschalten(P1, circP1, Brushes.Green, Brushes.White);
                               circFarbenUmschalten(P2, circP2, Brushes.Red, Brushes.White);
                               circFarbenUmschalten(P3, circP3, Brushes.OrangeRed, Brushes.White);

                           }
                       });
        }

        public void btnSichbarkeitUmschalten(bool Bedingung, Button btnEin, Button btnAus)
        {
            if (Bedingung) btnEin.Visibility = System.Windows.Visibility.Visible; else btnEin.Visibility = System.Windows.Visibility.Hidden;
            if (Bedingung) btnAus.Visibility = System.Windows.Visibility.Hidden; else btnAus.Visibility = System.Windows.Visibility.Visible;
        }
        public void imgSichbarkeitUmschalten(bool Bedingung, Image imgEin, Image imgAus)
        {
            if (Bedingung) imgEin.Visibility = System.Windows.Visibility.Visible; else imgEin.Visibility = System.Windows.Visibility.Hidden;
            if (Bedingung) imgAus.Visibility = System.Windows.Visibility.Hidden; else imgAus.Visibility = System.Windows.Visibility.Visible;
        }

        public void rctFarbenUmschalten(bool Bedingung, Rectangle Rechteck, System.Windows.Media.Brush brushEin, System.Windows.Media.Brush brushAus)
        {
            if (Bedingung) Rechteck.Fill = brushEin;
            else Rechteck.Fill = brushAus;
        }

        public void circFarbenUmschalten(bool Bedingung, Ellipse circ, System.Windows.Media.Brush brushEin, System.Windows.Media.Brush brushAus)
        {
            if (Bedingung) circ.Fill = brushEin;
            else circ.Fill = brushAus;
        }



    }
}
