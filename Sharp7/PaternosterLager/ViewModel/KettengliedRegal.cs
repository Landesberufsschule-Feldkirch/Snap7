namespace PaternosterLager.ViewModel
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class KettengliedRegal
    {
        private double position;
        private readonly int id;

        private const double abstand = 99.2;
        private const double hoehelagerkiste = 30;
        private const double breitelagerkisted = 60;
        private const double hoeheKettenglied = abstand / 3;
        private const double breiteKettenglied = 10;
        private const double durchmesserBolzen = 6;

        public KettengliedRegal(int id)
        {
            this.id = id;
            position = id * abstand;

            SetGeschwindigkeit(0);
        }

        internal void Zeichnen(MainWindow mainWindow, double pos)
        {
            if (mainWindow.FensterAktiv)
            {
                if ((id % 2) == 0)
                {
                    mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(pos + 0 * hoeheKettenglied, Brushes.Black));
                    mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(pos + 1 * hoeheKettenglied, Brushes.Red));
                    mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(pos + 2 * hoeheKettenglied, Brushes.Cyan));
                   
                                      mainWindow.ZeichenFlaeche.Children.Add(KettengliedZeichnen(pos + 0 * hoeheKettenglied, hoeheKettenglied, 1, Brushes.Black));
                                      mainWindow.ZeichenFlaeche.Children.Add(KettengliedZeichnen(pos + 1 * hoeheKettenglied, hoeheKettenglied, 2, Brushes.Red));
                                      mainWindow.ZeichenFlaeche.Children.Add(KettengliedZeichnen(pos + 2 * hoeheKettenglied, hoeheKettenglied, 3, Brushes.Cyan));

                                      mainWindow.ZeichenFlaeche.Children.Add(LagerkisteZeichnen(pos + 1 * hoeheKettenglied, Brushes.Red));
                          /*                */
                    mainWindow.ZeichenFlaeche.Children.Add(BeschriftungZeichnen(pos + 1 * hoeheKettenglied, id));
                }

            }
        }

        internal Ellipse ZapfenZeichnen(double offset, SolidColorBrush farbe)
        {
            var bolzen = new Ellipse
            {
                Width = durchmesserBolzen,
                Height = durchmesserBolzen,
                Fill = farbe
            };

            var (x, y, _) = Model.PositionBestimmen.ZapfenPositionBerechnen(position + offset, durchmesserBolzen);
            Canvas.SetLeft(bolzen, x - durchmesserBolzen / 2);
            Canvas.SetTop(bolzen, y - durchmesserBolzen / 2);

            return bolzen;
        }

        internal Rectangle KettengliedZeichnen(double offset, double abstand, double staerke, SolidColorBrush farbe)
        {
            double posOffset = (breiteKettenglied - durchmesserBolzen) / 2;

            var (x, y, phi, rechtsOben) = Model.PositionBestimmen.KettengliedPositionBerechnen(position + offset, position + offset + abstand, durchmesserBolzen, breiteKettenglied, hoeheKettenglied);

            var kettenglied = new Rectangle
            {
                Width = breiteKettenglied,
                Height = hoeheKettenglied,
                Stroke = farbe,
                StrokeThickness = staerke
            };

            if (rechtsOben)
            {
                kettenglied.RenderTransformOrigin = new Point(0.5, 0.14);
                kettenglied.RenderTransform = new RotateTransform(phi);
                Canvas.SetLeft(kettenglied, x - breiteKettenglied / 2);
                Canvas.SetTop(kettenglied, y - breiteKettenglied / 2);
            }
            else
            {
                kettenglied.RenderTransformOrigin = new Point(0.5, 0.14);
                kettenglied.RenderTransform = new RotateTransform(phi);
                Canvas.SetLeft(kettenglied, x - breiteKettenglied / 2);
                Canvas.SetTop(kettenglied, y - hoeheKettenglied + 4 * posOffset);
            }



            return kettenglied;
        }

        internal Rectangle LagerkisteZeichnen(double offset, SolidColorBrush farbe)
        {
            var lagerKiste = new Rectangle
            {
                Width = breitelagerkisted,
                Height = hoehelagerkiste,
                Stroke = farbe,
                StrokeThickness = 1
            };

            var (x, y, rechtsOben) = Model.PositionBestimmen.ZapfenPositionBerechnen(position + offset, durchmesserBolzen);
            Canvas.SetLeft(lagerKiste, x - breitelagerkisted / 2);
            Canvas.SetTop(lagerKiste, y);

            return lagerKiste;
        }

        internal Label BeschriftungZeichnen(double offset, int id)
        {
            var beschriftung = new Label
            {
                Content = id,
                FontSize = 14
            };

            var (x, y, rechtsOben) = Model.PositionBestimmen.ZapfenPositionBerechnen(position + offset, durchmesserBolzen);
            Canvas.SetLeft(beschriftung, x + breitelagerkisted / 2);
            Canvas.SetTop(beschriftung, y);

            return beschriftung;
        }

        internal void SetGeschwindigkeit(double geschwindigkeit) => position += geschwindigkeit;
        internal double GetGesamtLaenge() => Model.PositionBestimmen.GetGesamtLaenge(durchmesserBolzen);
    }
}