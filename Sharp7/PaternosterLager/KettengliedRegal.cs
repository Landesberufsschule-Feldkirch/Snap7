namespace PaternosterLager.ViewModel
{
    using System.Windows;
    using System.Windows.Media;

    public class KettengliedRegal
    {
        public GeometryGroup KettengliedMitAllem { get; set; }

        public KettengliedRegal(double x, double y)
        {
            RectangleGeometry kettenglied = new RectangleGeometry(new Rect(x + 0, y + 20, 30, 80));
            RectangleGeometry lagerkiste = new RectangleGeometry(new Rect(x + 100, y + 120, 130, 180));
            EllipseGeometry zapfenkettenglied = new EllipseGeometry(new Point(x + 10, y + 7), 20, 20);
            EllipseGeometry zapfenlagerkiste = new EllipseGeometry(new Point(x + 100, y + 75), 20, 20);

            KettengliedMitAllem = new GeometryGroup();

            KettengliedMitAllem.Children.Add(kettenglied);
            KettengliedMitAllem.Children.Add(lagerkiste);
            KettengliedMitAllem.Children.Add(zapfenkettenglied);
            KettengliedMitAllem.Children.Add(zapfenlagerkiste);
        }

        public Geometry GetKettengliedRegal() { return KettengliedMitAllem; }
    }
}