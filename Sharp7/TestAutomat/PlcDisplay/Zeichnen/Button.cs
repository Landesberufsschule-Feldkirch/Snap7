using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using static System.Windows.Controls.Grid;

namespace TestAutomat.PlcDisplay.Zeichnen
{
    public static partial class Formen
    {
        internal static void PlcButtonZeichnen(int x, int y, int par, string bez, DependencyProperty backgroundProperty, Grid grid)
        {
            var buttonTasten = new Button
            {   Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(3, 3, 3, 3)
            };

            buttonTasten.SetBinding(backgroundProperty, new Binding($"PlcDisplayAnzeige.{bez} [{ par }]"));

            SetColumn(buttonTasten, x);
            SetRow(buttonTasten, y);

            grid.Children.Add(buttonTasten);
        }
    }
}