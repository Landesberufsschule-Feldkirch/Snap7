using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Windows.Controls.Grid;

namespace DisplayPlc.Zeichnen
{
    public static partial class Formen
    {
        internal static void PlcLabelZeichnen(int x, int xSpan, int y, int ySpan, Brush farbe, double fontSize, string text, Grid grid)
        {
            var label = new Label
            {
                Content = text,
                FontSize = fontSize,
                Foreground = farbe,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            SetColumn(label, x);
            if (xSpan > 1) SetColumnSpan(label, xSpan);
            SetRow(label, y);
            if (ySpan > 1)
            {
                SetRowSpan(label, ySpan);
                label.VerticalAlignment = VerticalAlignment.Center;
            }
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
            SetColumn(bezeichnung, x);
            SetRow(bezeichnung, y);
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
            SetColumn(kommentar, x);
            SetRow(kommentar, y);
            grid.Children.Add(kommentar);
        }
    }
}