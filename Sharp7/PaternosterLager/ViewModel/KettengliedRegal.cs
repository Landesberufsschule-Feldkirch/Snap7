namespace PaternosterLager.ViewModel
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class KettengliedRegal
    {
        private double _position;
        private readonly int _id;

        private readonly double _hoeheKettenglied;

        private const double Hoehelagerkiste = 40;
        private const double Breitelagerkisted = 80;

        private const double BreiteKettenglied = 14;
        private const double DurchmesserBolzen = 8;

        public KettengliedRegal(int id, double anzahlKisten)
        {
            this._id = id;

            var abstandLagerkisten = Model.PositionBestimmen.GetGesamtLaenge(DurchmesserBolzen) / anzahlKisten;
            _hoeheKettenglied = abstandLagerkisten / 3;

            _position = id * abstandLagerkisten;
            SetGeschwindigkeit(0);
        }

        internal void Zeichnen(MainWindow mainWindow, double pos)
        {
            if (mainWindow.FensterAktiv)
            {
                mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(pos + 0 * _hoeheKettenglied, Brushes.Black));
                mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(pos + 1 * _hoeheKettenglied, Brushes.Red));
                mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(pos + 2 * _hoeheKettenglied, Brushes.Cyan));

                mainWindow.ZeichenFlaeche.Children.Add(KettengliedZeichnen(pos + 0 * _hoeheKettenglied, 1, Brushes.Black));
                mainWindow.ZeichenFlaeche.Children.Add(KettengliedZeichnen(pos + 1 * _hoeheKettenglied, 1, Brushes.Red));
                mainWindow.ZeichenFlaeche.Children.Add(KettengliedZeichnen(pos + 2 * _hoeheKettenglied, 1, Brushes.Cyan));

                mainWindow.ZeichenFlaeche.Children.Add(LagerkisteZeichnen(pos + 1 * _hoeheKettenglied, Brushes.Red));

                mainWindow.ZeichenFlaeche.Children.Add(BeschriftungZeichnen(pos + 1 * _hoeheKettenglied, _id));
            }
        }

        internal Ellipse ZapfenZeichnen(double offset, SolidColorBrush farbe)
        {
            var bolzen = new Ellipse
            {
                Width = DurchmesserBolzen,
                Height = DurchmesserBolzen,
                Fill = farbe
            };

            var (x, y, _) = Model.PositionBestimmen.ZapfenPositionBerechnen(_position + offset, DurchmesserBolzen);
            Canvas.SetLeft(bolzen, x - DurchmesserBolzen / 2);
            Canvas.SetTop(bolzen, y - DurchmesserBolzen / 2);

            return bolzen;
        }

        internal Rectangle KettengliedZeichnen(double offset, double staerke, SolidColorBrush farbe)
        {
            var kettenglied = new Rectangle
            {
                Width = BreiteKettenglied,
                Height = _hoeheKettenglied,
                Stroke = farbe,
                StrokeThickness = staerke
            };

            var (x, y, phi, positionUndRichtung) = Model.PositionBestimmen.KettengliedPositionBerechnen(_position + offset, DurchmesserBolzen, BreiteKettenglied, _hoeheKettenglied);

            switch (positionUndRichtung)
            {
                case Model.Zeichenbereich.Links:
                    kettenglied.RenderTransformOrigin = new Point(0.5, 0.14);
                    kettenglied.RenderTransform = new RotateTransform(phi);
                    Canvas.SetLeft(kettenglied, x - BreiteKettenglied / 2);
                    Canvas.SetTop(kettenglied, y + BreiteKettenglied / 2 - _hoeheKettenglied);
                    return kettenglied;

                default:
                    kettenglied.RenderTransformOrigin = new Point(0.5, 0.14);
                    kettenglied.RenderTransform = new RotateTransform(phi);
                    Canvas.SetLeft(kettenglied, x - BreiteKettenglied / 2);
                    Canvas.SetTop(kettenglied, y - BreiteKettenglied / 2);
                    return kettenglied;
            }
        }

        internal Rectangle LagerkisteZeichnen(double offset, SolidColorBrush farbe)
        {
            var lagerKiste = new Rectangle
            {
                Width = Breitelagerkisted,
                Height = Hoehelagerkiste,
                Stroke = farbe,
                StrokeThickness = 3
            };

            var (x, y, _) = Model.PositionBestimmen.ZapfenPositionBerechnen(_position + offset, DurchmesserBolzen);
            Canvas.SetLeft(lagerKiste, x - Breitelagerkisted / 2);
            Canvas.SetTop(lagerKiste, y - DurchmesserBolzen / 2);

            return lagerKiste;
        }

        internal Label BeschriftungZeichnen(double offset, int inhalt)
        {
            var beschriftung = new Label
            {
                Content = inhalt,
                FontSize = 14
            };

            var (x, y, _) = Model.PositionBestimmen.ZapfenPositionBerechnen(_position + offset, DurchmesserBolzen);
            Canvas.SetLeft(beschriftung, x + Breitelagerkisted / 2);
            Canvas.SetTop(beschriftung, y);

            return beschriftung;
        }

        internal void SetGeschwindigkeit(double geschwindigkeit) => _position += geschwindigkeit;

        internal double GetGesamtLaenge() => Model.PositionBestimmen.GetGesamtLaenge(DurchmesserBolzen);
    }
}