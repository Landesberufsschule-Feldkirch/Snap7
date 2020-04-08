namespace PaternosterLager.ViewModel
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class KettengliedRegal
    {
        public GeometryGroup KettengliedMitAllem { get; set; }
        private readonly int id;
        private readonly double posX;
        private readonly double posY;
        private readonly double bewegung;

        private const double abstandY = 75;
        private const double hoehelagerkiste = 30;
        private const double breitelagerkisted = 50;
        private const double hoeheKettenglied = 25;
        private const double breiteKettenglied = 10;
        private const double radiusZapfen = 5;

        public KettengliedRegal(int i)
        {
            id = i;
            posX = 100;
            posY = id * abstandY;

            var lagerkiste = new RectangleGeometry(new Rect(-breitelagerkisted / 2, 1 * hoeheKettenglied - radiusZapfen, breitelagerkisted, hoehelagerkiste));

            var kettenglied1 = new RectangleGeometry(new Rect(-breiteKettenglied / 2, 0 * hoeheKettenglied, breiteKettenglied, hoeheKettenglied));
            var kettenglied2 = new RectangleGeometry(new Rect(-breiteKettenglied / 2, 1 * hoeheKettenglied, breiteKettenglied, hoeheKettenglied));
            var kettenglied3 = new RectangleGeometry(new Rect(-breiteKettenglied / 2, 2 * hoeheKettenglied, breiteKettenglied, hoeheKettenglied));

            var zapfen1 = new EllipseGeometry(new Point(0, 0 * hoeheKettenglied), radiusZapfen, radiusZapfen);
            var zapfen2 = new EllipseGeometry(new Point(0,  1 * hoeheKettenglied), radiusZapfen, radiusZapfen);
            var zapfen3 = new EllipseGeometry(new Point(0,  2 * hoeheKettenglied), radiusZapfen, radiusZapfen);

            KettengliedMitAllem = new GeometryGroup();

            KettengliedMitAllem.Children.Add(lagerkiste);
            KettengliedMitAllem.Children.Add(kettenglied1);
            KettengliedMitAllem.Children.Add(kettenglied2);
            KettengliedMitAllem.Children.Add(kettenglied3);
            KettengliedMitAllem.Children.Add(zapfen1);
            KettengliedMitAllem.Children.Add(zapfen2);
            KettengliedMitAllem.Children.Add(zapfen3);
        }

        internal void setGeschwindigkeit(double geschwindigkeit)
        {
            //throw new NotImplementedException();
        }

        public Geometry GetKettengliedRegal() => KettengliedMitAllem;
        public double getPosX() => posX;
        public double getPosY() => posY;
    }
}