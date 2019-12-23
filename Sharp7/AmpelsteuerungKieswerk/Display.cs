using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow
    {
        public void Display_Task()
        {
            DatenRangieren.AmpelChangedEvent += DatenRangieren_AmpelChangedEvent;

            DatenRangieren_AmpelChangedEvent(null, new AmpelZustandEventArgs(AmpelZustand.Rot, AmpelZustand.Rot));

            while (FensterAktiv)
            {
                Dispatcher.Invoke(() =>
                        {
                            if (FensterAktiv)
                            {
                                AnzeigeAktualisieren();
                                lock (lockit)
                                {
                                    foreach (Button btn in gAlleButton)
                                    {
                                        var lkw = btn.Tag as LKW;
                                        lkw?.LastwagenAnzeigen(FensterAktiv, btn);
                                    }
                                }
                            }
                        });
                Thread.Sleep(10);
            }
        }

        private void DatenRangieren_AmpelChangedEvent(object sender, AmpelZustandEventArgs e)
        {
            KreisFarbeUmschalten(circ_Ampel_links_rot, Colors.White);
            KreisFarbeUmschalten(circ_Ampel_links_gelb, Colors.White);
            KreisFarbeUmschalten(circ_Ampel_links_gruen, Colors.White);

            KreisFarbeUmschalten(circ_Ampel_rechts_rot, Colors.White);
            KreisFarbeUmschalten(circ_Ampel_rechts_gelb, Colors.White);
            KreisFarbeUmschalten(circ_Ampel_rechts_gruen, Colors.White);

            switch (e.AmpelZustandLinks)
            {
                case AmpelZustand.Rot:
                    KreisFarbeUmschalten(circ_Ampel_links_rot, Colors.Red);
                    break;
                case AmpelZustand.Gelb:
                    KreisFarbeUmschalten(circ_Ampel_links_gelb, Colors.Yellow);
                    break;
                case AmpelZustand.Grün:
                    KreisFarbeUmschalten(circ_Ampel_links_gruen, Colors.LawnGreen);
                    break;
                default:
                    break;
            }

            switch (e.AmpelZustandLinks)
            {
                case AmpelZustand.Rot:
                    KreisFarbeUmschalten(circ_Ampel_rechts_rot, Colors.Red);
                    break;
                case AmpelZustand.Gelb:
                    KreisFarbeUmschalten(circ_Ampel_rechts_gelb, Colors.Yellow);
                    break;
                case AmpelZustand.Grün:
                    KreisFarbeUmschalten(circ_Ampel_rechts_gruen, Colors.LawnGreen);
                    break;
                default:
                    break;
            }
        }
        public void AnzeigeAktualisieren()
        {
            KreisFarbeUmschalten(B1, circ_Lichtschranke_draussen_links, Colors.Red, Colors.LightGray);
            KreisFarbeUmschalten(B2, circ_Lichtschranke_drinnen_links, Colors.Red, Colors.LightGray);
            KreisFarbeUmschalten(B3, circ_Lichtschranke_drinnen_rechts, Colors.Red, Colors.LightGray);
            KreisFarbeUmschalten(B4, circ_Lichtschranke_draussen_rechts, Colors.Red, Colors.LightGray);
        }
        void KreisFarbeUmschalten(bool Wert, Ellipse ellipse, Color FarbeEin, Color FarbeAus)
        {
            Dispatcher.Invoke(() =>
            {
                if (Wert) ellipse.Fill = new SolidColorBrush(FarbeEin);
                else ellipse.Fill = new SolidColorBrush(FarbeAus);
            });
        }
        void KreisFarbeUmschalten(Ellipse ellipse, Color farbe)
        {
            Dispatcher.Invoke(() =>
            {
                ellipse.Fill = new SolidColorBrush(farbe);
            });

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
