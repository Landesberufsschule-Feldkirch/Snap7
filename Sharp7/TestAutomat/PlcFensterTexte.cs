using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestAutomat
{
    public partial class PlcFensterFormen
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

        internal static void PlcBezeichnungZeichnen(int x, int y, string bez, Grid grid)
        {
            var bezeichnung = new TextBlock
            {
                Text = bez,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Black),
                Padding = new Thickness(10, 5, 5, 5)
            };
            Grid.SetColumn(bezeichnung, x);
            Grid.SetRow(bezeichnung, y);
            grid.Children.Add(bezeichnung);
        }


    }
}
