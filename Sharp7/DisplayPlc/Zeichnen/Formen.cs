using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static System.Windows.Controls.Grid;

namespace DisplayPlc.Zeichnen
{
    public static partial class Formen
    {
        internal static void PlcRechteckZeichnen(int x, int xSpan, int y, int ySpan, Brush farbe, Grid grid)
        {
            var hintergrund = new Rectangle
            {
                Fill = farbe
            };

            SetColumn(hintergrund, x);
            SetColumnSpan(hintergrund, xSpan);
            SetRow(hintergrund, y);
            SetRowSpan(hintergrund, ySpan);
            grid.Children.Add(hintergrund);
        }
        internal static void PlcBorderZeichnen(int x, int xSpan, int y, int ySpan, int left, int top, int right, int bottom, Brush farbe, Grid grid)
        {
            var border = new Border
            {
                BorderBrush = farbe,
                BorderThickness = new Thickness(left, top, right, bottom)
            };

            SetColumn(border, x);
            SetColumnSpan(border, xSpan);
            SetRow(border, y);
            SetRowSpan(border, ySpan);
            grid.Children.Add(border);
        }
    }
}