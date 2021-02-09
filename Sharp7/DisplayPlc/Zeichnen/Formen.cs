using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DisplayPlc.Zeichnen
{
    public partial class Formen
    {
        internal static void PlcRechteckZeichnen(int x, int xSpan, int y, int ySpan, Brush farbe, Grid grid)
        {
            var hintergrund = new Rectangle
            {
                Fill = farbe
            };

            Grid.SetColumn(hintergrund, x);
            Grid.SetColumnSpan(hintergrund, xSpan);
            Grid.SetRow(hintergrund, y);
            Grid.SetRowSpan(hintergrund, ySpan);
            grid.Children.Add(hintergrund);
        }

 


        internal static void PlcBorderZeichnen(int x, int xSpan, int y, int ySpan, int left, int top, int right, int bottom, Brush farbe, Grid grid)
        {
            var border = new Border
            {
                BorderBrush = farbe,
                BorderThickness = new Thickness(left, top, right, bottom)
            };

            Grid.SetColumn(border, x);
            Grid.SetColumnSpan(border, xSpan);
            Grid.SetRow(border, y);
            Grid.SetRowSpan(border, ySpan);
            grid.Children.Add(border);
        }


    }
}
