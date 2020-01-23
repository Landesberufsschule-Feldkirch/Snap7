using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace LAP_2019_Foerderanlage
{
    public partial class MainWindow
    {
        private const int PosWagenMengeOffset_X = 12;
        private const int PosWagenLabelOffset_X = 75;
        private const int MaterialSiloHoehe = 8 * 35;

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

                if (DebugWindowAktiv)
                {
                    Q3_RL = setManualWindow.M1_RL;
                    Q4_LL = setManualWindow.M1_LL;
                    XFU = setManualWindow.M2;
                    Y1 = setManualWindow.Y1;
                }

                Thread.Sleep(10);
            }
        }

        public void AnzeigeAktualisieren()
        {
            rctMaterialsiloFuellstand.Margin = new System.Windows.Thickness(0, MaterialSiloHoehe * (1 - MaterialSiloFuellstand), 0, 0);

            polyWagen.SetValue(Canvas.LeftProperty, WagenPosition_X);
            rctMaterialwagenMenge.SetValue(Canvas.LeftProperty, WagenPosition_X + PosWagenMengeOffset_X);
            rctMaterialwagenMenge.SetValue(Canvas.TopProperty, 10 + 88 - WagenFuellstand);
            rctMaterialwagenMenge.SetValue(Canvas.HeightProperty, WagenFuellstand);
            lblMaterialWagen.SetValue(Canvas.LeftProperty, WagenPosition_X + PosWagenLabelOffset_X);

            ImgSichbarkeitUmschalten(S7, imgS7Oeffner, imgS7Schliesser);
            ImgSichbarkeitUmschalten(S8, imgS8Oeffner, imgS8Schliesser);

            ImgSichbarkeitUmschalten(Y1, imgVentilEin, imgVentilAus);

            circSichtbarkeitSchalten(XFU, cirSchneckeEin);                  // Schneckenförderer M1

            circSichtbarkeitSchalten((Q3_RL | Q4_LL), cirRolleLinksEin);    // Förderband M1

            PolySichbarkeitSchalten(Q3_RL, polyPfeilRechtslauf);
            PolySichbarkeitSchalten(Q4_LL, polyPfeilLinkslauf);

            RechteckHintergrundfarbeAendern(MaterialSiloFuellstand > 0.01 & Y1, rctFuellenUnten, Brushes.Firebrick, Brushes.LightGray);
            RechteckHintergrundfarbeAendern(MaterialSiloFuellstand > 0.01, rctFuellenOben, Brushes.Firebrick, Brushes.LightGray);

            if (AnimationGestartet)
            {
                var controller = ImageBehavior.GetAnimationController(imgSchneckenfoerderer);
                if (XFU) controller.Play(); else controller.Pause();
            }
        }

        public void PolySichbarkeitSchalten(bool Bedingung, Polygon poly)
        {
            if (Bedingung) poly.Visibility = System.Windows.Visibility.Visible; else poly.Visibility = System.Windows.Visibility.Hidden;
        }

        public void ImgSichbarkeitUmschalten(bool Bedingung, Image imgEin, Image imgAus)
        {
            if (Bedingung) imgEin.Visibility = System.Windows.Visibility.Visible; else imgEin.Visibility = System.Windows.Visibility.Hidden;
            if (Bedingung) imgAus.Visibility = System.Windows.Visibility.Hidden; else imgAus.Visibility = System.Windows.Visibility.Visible;
        }

        public void circSichtbarkeitSchalten(bool Bedingung, Ellipse circ)
        {
            if (Bedingung) circ.Visibility = System.Windows.Visibility.Visible; else circ.Visibility = System.Windows.Visibility.Hidden;
        }

        private void RechteckHintergrundfarbeAendern(bool Bedingung, Rectangle rechteck, System.Windows.Media.Brush brushEin, System.Windows.Media.Brush brushAus)
        {
            if (Bedingung) rechteck.Fill = brushEin; else rechteck.Fill = brushAus;
        }
    }
}