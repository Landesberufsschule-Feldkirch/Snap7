using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LAP_2018_Niveauregelung
{    public partial class MainWindow
    {
        private readonly double HoeheFuellBalken = 315;

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
            ImgSichbarkeitUmschalten(B1, imgB1Ein, imgB1Aus);
            ImgSichbarkeitUmschalten(B2, imgB2Ein, imgB2Aus);
            ImgSichbarkeitUmschalten(B3, imgB3Ein, imgB3Aus);

            RctFarbenUmschalten(M1, ZuleitungLinksWaagrecht, Brushes.Blue, Brushes.LightBlue);
            RctFarbenUmschalten(M1, ZuleitungLinksSenkrecht, Brushes.Blue, Brushes.LightBlue);

            RctFarbenUmschalten(M2, ZuleitungRechtsWaagrecht, Brushes.Blue, Brushes.LightBlue);
            RctFarbenUmschalten(M2, ZuleitungRechtsSenkrecht, Brushes.Blue, Brushes.LightBlue);

            ImgSichbarkeitUmschalten(M1, M1_Ein, M1_Aus);
            ImgSichbarkeitUmschalten(M2, M2_Ein, M2_Aus);

            if (F1) btnF1.Background = System.Windows.Media.Brushes.LawnGreen; else btnF1.Background = System.Windows.Media.Brushes.Red;
            if (F2) btnF2.Background = System.Windows.Media.Brushes.LawnGreen; else btnF2.Background = System.Windows.Media.Brushes.Red;

            BtnSichbarkeitUmschalten(Y1, btnVentilEin, btnVentilAus);

            BehaelterVoll.Margin = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - Pegel), 0, 0);

            RctFarbenUmschalten((Pegel > 0.01), Ableitung, Brushes.Blue, Brushes.LightBlue);
            RctFarbenUmschalten(Y1 && (Pegel > 0.01), AbleitungUnten, Brushes.Blue, Brushes.LightBlue);

            CircFarbenUmschalten(P1, circP1, Brushes.Green, Brushes.White);
            CircFarbenUmschalten(P2, circP2, Brushes.Red, Brushes.White);
            CircFarbenUmschalten(P3, circP3, Brushes.OrangeRed, Brushes.White);
        }

        public void BtnSichbarkeitUmschalten(bool Bedingung, Button btnEin, Button btnAus)
        {
            if (Bedingung) btnEin.Visibility = System.Windows.Visibility.Visible; else btnEin.Visibility = System.Windows.Visibility.Hidden;
            if (Bedingung) btnAus.Visibility = System.Windows.Visibility.Hidden; else btnAus.Visibility = System.Windows.Visibility.Visible;
        }
        public void ImgSichbarkeitUmschalten(bool Bedingung, Image imgEin, Image imgAus)
        {
            if (Bedingung) imgEin.Visibility = System.Windows.Visibility.Visible; else imgEin.Visibility = System.Windows.Visibility.Hidden;
            if (Bedingung) imgAus.Visibility = System.Windows.Visibility.Hidden; else imgAus.Visibility = System.Windows.Visibility.Visible;
        }
        public void RctFarbenUmschalten(bool Bedingung, Rectangle Rechteck, System.Windows.Media.Brush brushEin, System.Windows.Media.Brush brushAus)
        {
            if (Bedingung) Rechteck.Fill = brushEin;
            else Rechteck.Fill = brushAus;
        }
        public void CircFarbenUmschalten(bool Bedingung, Ellipse circ, System.Windows.Media.Brush brushEin, System.Windows.Media.Brush brushAus)
        {
            if (Bedingung) circ.Fill = brushEin;
            else circ.Fill = brushAus;
        }
    }
}
