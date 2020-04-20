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
        private const double durchmesserZapfen = 5;

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
                mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(pos + 0 * hoeheKettenglied, Brushes.Black));
                mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(pos + 1 * hoeheKettenglied, Brushes.Red));
                mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(pos + 2 * hoeheKettenglied, Brushes.Cyan));

                mainWindow.ZeichenFlaeche.Children.Add(KettengliedZeichnen(pos + 0 * hoeheKettenglied, hoeheKettenglied, Brushes.Black));
                mainWindow.ZeichenFlaeche.Children.Add(KettengliedZeichnen(pos + 1 * hoeheKettenglied, hoeheKettenglied, Brushes.Red));
                mainWindow.ZeichenFlaeche.Children.Add(KettengliedZeichnen(pos + 2 * hoeheKettenglied, hoeheKettenglied, Brushes.Cyan));

                mainWindow.ZeichenFlaeche.Children.Add(LagerkisteZeichnen(pos + 1 * hoeheKettenglied, Brushes.Red));

                mainWindow.ZeichenFlaeche.Children.Add(BeschriftungZeichnen(pos + 1 * hoeheKettenglied, id));
            }
        }
               
        internal Ellipse ZapfenZeichnen(double offset, SolidColorBrush farbe)
        {
            var zapfen = new Ellipse
            {
                Width = durchmesserZapfen,
                Height = durchmesserZapfen,
                Fill = farbe
            };

            var (x, y) = Model.PositionBestimmen.ZapfenPositionBerechnen(position + offset, durchmesserZapfen);
            Canvas.SetLeft(zapfen, x);
            Canvas.SetTop(zapfen, y);

            return zapfen;
        }

        internal Rectangle KettengliedZeichnen(double offset, double abstand, SolidColorBrush farbe)
        {
            double posOffset = (breiteKettenglied - durchmesserZapfen) / 2;

            var (x, y, phi) = Model.PositionBestimmen.KettengliedPositionBerechnen(position + offset, position + offset + abstand, posOffset, durchmesserZapfen, breiteKettenglied, hoeheKettenglied);

            var kettenglied = new Rectangle
            {
                Width = breiteKettenglied,
                Height = hoeheKettenglied,
                Stroke = farbe,
                StrokeThickness = 1
            };

            kettenglied.RenderTransformOrigin = new Point(0.5, 0.14);
            kettenglied.RenderTransform = new RotateTransform(phi);
            Canvas.SetLeft(kettenglied, x);
            Canvas.SetTop(kettenglied, y);

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

            var (x, y) = Model.PositionBestimmen.ZapfenPositionBerechnen(position + offset, durchmesserZapfen);
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

            var (x, y) = Model.PositionBestimmen.ZapfenPositionBerechnen(position + offset, durchmesserZapfen);
            Canvas.SetLeft(beschriftung, x + breitelagerkisted / 2);
            Canvas.SetTop(beschriftung, y);

            return beschriftung;
        }

        internal void SetGeschwindigkeit(double geschwindigkeit) => position += geschwindigkeit;
        internal double GetGesamtLaenge() =>  Model.PositionBestimmen.GetGesamtLaenge(durchmesserZapfen);      
    }
}