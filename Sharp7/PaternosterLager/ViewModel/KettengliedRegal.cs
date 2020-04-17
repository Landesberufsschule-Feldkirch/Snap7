namespace PaternosterLager.ViewModel
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class KettengliedRegal
    {
        public GeometryGroup KettengliedMitAllem { get; set; }

        private double position;


        private const double abstand = 99.2;
        private const double hoehelagerkiste = 30;
        private const double breitelagerkisted = 50;
        private const double hoeheKettenglied = abstand / 3;
        private const double breiteKettenglied = 10;
        private const double durchmesserZapfen = 5;

        public KettengliedRegal(int id)
        {
            position = id * abstand;

            SetGeschwindigkeit(0);

            var lagerkiste = new RectangleGeometry(new Rect(-breitelagerkisted / 2, 1 * hoeheKettenglied - durchmesserZapfen, breitelagerkisted, hoehelagerkiste));

            var kettenglied1 = new RectangleGeometry(new Rect(-breiteKettenglied / 2, 0 * hoeheKettenglied, breiteKettenglied, hoeheKettenglied));
            var kettenglied2 = new RectangleGeometry(new Rect(-breiteKettenglied / 2, 1 * hoeheKettenglied, breiteKettenglied, hoeheKettenglied));
            var kettenglied3 = new RectangleGeometry(new Rect(-breiteKettenglied / 2, 2 * hoeheKettenglied, breiteKettenglied, hoeheKettenglied));

            var zapfen1 = new EllipseGeometry(new Point(0, 0 * hoeheKettenglied), durchmesserZapfen, durchmesserZapfen);
            var zapfen2 = new EllipseGeometry(new Point(0, 1 * hoeheKettenglied), durchmesserZapfen, durchmesserZapfen);
            var zapfen3 = new EllipseGeometry(new Point(0, 2 * hoeheKettenglied), durchmesserZapfen, durchmesserZapfen);

            var text = new FormattedText(id.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 12, Brushes.Black, 2);
            var textgeometry = text.BuildGeometry(new Point(breitelagerkisted / 2 + 5, hoeheKettenglied));


            KettengliedMitAllem = new GeometryGroup();

            KettengliedMitAllem.Children.Add(lagerkiste);
            KettengliedMitAllem.Children.Add(kettenglied1);
            KettengliedMitAllem.Children.Add(kettenglied2);
            KettengliedMitAllem.Children.Add(kettenglied3);
            KettengliedMitAllem.Children.Add(zapfen1);
            KettengliedMitAllem.Children.Add(zapfen2);
            KettengliedMitAllem.Children.Add(zapfen3);
            KettengliedMitAllem.Children.Add(textgeometry);
        }

        internal void Zeichnen(MainWindow mainWindow)
        {
            if (mainWindow.FensterAktiv)
            {
                mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(0 * hoeheKettenglied, Brushes.Black));
                mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(1 * hoeheKettenglied, Brushes.Red));
                mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(2 * hoeheKettenglied, Brushes.Cyan));

                mainWindow.ZeichenFlaeche.Children.Add(KettengliedZeichnen(0 * hoeheKettenglied, hoeheKettenglied, Brushes.Black));
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

            kettenglied.LayoutTransform = new RotateTransform(phi, 0, 0);

            Canvas.SetLeft(kettenglied, x);
            Canvas.SetTop(kettenglied, y);

            return kettenglied;
        }


        internal void SetGeschwindigkeit(double geschwindigkeit) => position += geschwindigkeit;
        public Geometry GetKettengliedRegal() => KettengliedMitAllem;
    }
}