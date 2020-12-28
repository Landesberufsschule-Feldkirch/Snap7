using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        internal static void PlcBezeichnungZeichnen(int x, int y, int par, string bez, DependencyProperty visibilityProperty, Grid grid)
        {
            var bezeichnung = new TextBlock
            {
                Text = $"{bez}{par}",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Black),
                Padding = new Thickness(10, 5, 5, 5)
            };

          //  bezeichnung.SetBinding(TextBlock.TextProperty, new Binding($"ManVisuAnzeigen.{bez} [{ par }]"));
           // bezeichnung.SetBinding(visibilityProperty, new Binding($"ManVisuAnzeigen.Visibility{bez} [{ par }]"));

            Grid.SetColumn(bezeichnung, x);
            Grid.SetRow(bezeichnung, y);
            grid.Children.Add(bezeichnung);
        }


    }
}
