using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DisplayPlc.Zeichnen
{
    public partial class Formen
    {
        internal static void PlcLabelZeichnen(int x, int y, Brush farbe, double fontSize, string text, Grid grid)
        {
            var label = new Label
            {
                Content = text,
                FontSize = fontSize,
                Foreground = farbe,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            Grid.SetColumn(label, x);
            Grid.SetRow(label, y);
            grid.Children.Add(label);
        }
        internal static void PlcBezeichnungZeichnen(int x, int y, string bez, int schriftGroesse, VerticalAlignment vertical, Grid grid)
        {
            var bezeichnung = new TextBlock
            {
                Text = bez,
                FontSize = schriftGroesse,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Black),
                Padding = new Thickness(10, 5, 5, 5),
                Background = new SolidColorBrush(Colors.AliceBlue),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = vertical,
                RenderTransformOrigin = new Point(0.5, 0.5),
                LayoutTransform = new RotateTransform { Angle = 270 }
            };
            Grid.SetColumn(bezeichnung, x);
            Grid.SetRow(bezeichnung, y);
            grid.Children.Add(bezeichnung);
        }
        internal static void PlcKommentarZeichnen(int x, int y, string komment, int schriftGroesse, VerticalAlignment vertical, Grid grid)
        {
            var kommentar = new TextBlock
            {
                Text = komment,
                FontSize = schriftGroesse,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Black),
                Padding = new Thickness(10, 5, 5, 5),
                Background = new SolidColorBrush(Colors.LightGoldenrodYellow),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = vertical,
                RenderTransformOrigin = new Point(0.5, 0.5),
                LayoutTransform = new RotateTransform { Angle = 270 }
            };
            Grid.SetColumn(kommentar, x);
            Grid.SetRow(kommentar, y);
            grid.Children.Add(kommentar);
        }
    }
}