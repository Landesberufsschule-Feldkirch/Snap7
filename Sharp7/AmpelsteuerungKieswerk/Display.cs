using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow
    {
        public void Display_Task()
        {
            this.Dispatcher.Invoke(() =>
                        {
                            if (FensterAktiv)
                            {
                                //GridRasterEinblenden();
                                AnzeigeAktualisieren();

                                foreach (Button btn in gAlleButton)
                                {
                                    var lkw = btn.Tag as LKW;
                                    lkw.LastwagenAnzeigen(FensterAktiv, btn);
                                }
                            }
                        });
        }

        public void AnzeigeAktualisieren()
        {
            KreisFarbeUmschalten(P1_links_gruen, circ_Ampel_links_gruen, Colors.LawnGreen, Colors.White);
            KreisFarbeUmschalten(P2_links_gelb, circ_Ampel_links_gelb, Colors.Yellow, Colors.White);
            KreisFarbeUmschalten(P3_links_rot, circ_Ampel_links_rot, Colors.Red, Colors.White);

            KreisFarbeUmschalten(P4_rechts_gruen, circ_Ampel_rechts_gruen, Colors.LawnGreen, Colors.White);
            KreisFarbeUmschalten(P5_rechts_gelb, circ_Ampel_rechts_gelb, Colors.Yellow, Colors.White);
            KreisFarbeUmschalten(P6_rechts_rot, circ_Ampel_rechts_rot, Colors.Red, Colors.White);

            KreisFarbeUmschalten(B1, circ_Lichtschranke_draussen_links, Colors.Red, Colors.LightGray);
            KreisFarbeUmschalten(B2, circ_Lichtschranke_drinnen_links, Colors.Red, Colors.LightGray);
            KreisFarbeUmschalten(B3, circ_Lichtschranke_drinnen_rechts, Colors.Red, Colors.LightGray);
            KreisFarbeUmschalten(B4, circ_Lichtschranke_draussen_rechts, Colors.Red, Colors.LightGray);
        }

        void KreisFarbeUmschalten(bool Wert, Ellipse ellipse, Color FarbeEin, Color FarbeAus)
        {
            if (Wert) ellipse.Fill = new SolidColorBrush(FarbeEin);
            else ellipse.Fill = new SolidColorBrush(FarbeAus);
        }

        void GridRasterEinblenden()
        {
            for (var i = 0; i < 1000; i += 50)
            {
                var line = new Line
                {
                    Stroke = Brushes.Cyan,
                    X1 = 10,
                    X2 = 2000,
                    Y1 = i,
                    Y2 = i
                };
                if ((i % 100) == 0) line.StrokeThickness = 5;
                else line.StrokeThickness = 2;
                myCanvas.Children.Add(line);
            }

            for (var i = 0; i < 2000; i += 50)
            {
                var line = new Line
                {
                    Stroke = Brushes.Violet,
                    X1 = i,
                    X2 = i,
                    Y1 = 0,
                    Y2 = 1000
                };
                if ((i % 100) == 0) line.StrokeThickness = 5;
                else line.StrokeThickness = 2;
                myCanvas.Children.Add(line);
            }
        }
    }
}
