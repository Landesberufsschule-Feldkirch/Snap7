using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static System.Windows.Controls.Grid;

namespace ManualMode
{
    public static partial class FensterFunktionen
    {
        internal static void HintergrundRechteckZeichnen(int x, int y, int span, Brush farbe, Grid grid)
        {
            var hintergrund = new Rectangle
            {
                Margin = new Thickness(-4, -4, 0, -4),
                Fill = farbe
            };

            SetColumn(hintergrund, x);
            SetColumnSpan(hintergrund, span);
            SetRow(hintergrund, y);
            grid.Children.Add(hintergrund);
        }
    }
}