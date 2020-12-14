using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ManualMode
{

    public class FensterFunktionen
    {
        internal void HintergrundRechteckZeichnen(int x, int y, int span, Brush farbe, Grid grid)
        {
            var hintergrund = new Rectangle
            {
                Margin = new Thickness(-4, -4, 0, -4),
                Fill = farbe
            };

            Grid.SetColumn(hintergrund, x);
            Grid.SetColumnSpan(hintergrund, span);
            Grid.SetRow(hintergrund, y);
            grid.Children.Add(hintergrund);
        }



        internal void TextZeichnen(string beschriftung, HorizontalAlignment alignment, int x, int y, Grid grid)
        {
            var text = new TextBlock
            {
                Text = beschriftung,
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = alignment
            };

            Grid.SetColumn(text, x);
            Grid.SetRow(text, y);
            grid.Children.Add(text);
        }


        internal void KommentarZeichnen(int x, int y, int par, string bezKommentar, string bezVisibility, DependencyProperty visibilityProperty, Grid grid)
        {
            var kommentar = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                Padding = new Thickness(10, 5, 5, 5),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            kommentar.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.KommentarAa[" + par + "]"));
            kommentar.SetBinding(visibilityProperty, new Binding("ManVisuAnzeigen.VisibilityAa[" + par + "]"));

            Grid.SetColumn(kommentar, x);
            Grid.SetRow(kommentar, y);
            grid.Children.Add(kommentar);
        }
    }
}