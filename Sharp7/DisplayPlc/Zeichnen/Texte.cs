using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using static System.Windows.Controls.Grid;

namespace DisplayPlc.Zeichnen;

public static partial class Formen
{
    internal static void BezeichnungZeichnen(int x, int y, double fontSize, VerticalAlignment vertical, string text, int par, string bez, DependencyProperty visibilityProperty, Grid grid)
    {
        var bezeichnung = new TextBlock
        {
            FontSize = fontSize,
            FontWeight = FontWeights.Bold,
            Foreground = new SolidColorBrush(Colors.Black),
            Padding = new Thickness(10, 5, 5, 5),
            Background = new SolidColorBrush(Colors.AliceBlue),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = vertical,
            RenderTransformOrigin = new Point(0.5, 0.5),
            LayoutTransform = new RotateTransform { Angle = 270 }
        };

        if (bez == "-")
        {
            bezeichnung.Text = text;

        }
        else
        {
            bezeichnung.SetBinding(TextBlock.TextProperty, new Binding($"DisplayPlcAnzeige.Bezeichnung{bez} [{par}]"));
            bezeichnung.SetBinding(visibilityProperty, new Binding($"DisplayPlcAnzeige.Visibility{bez} [{par}]"));
        }

        SetColumn(bezeichnung, x);
        SetRow(bezeichnung, y);
        grid.Children.Add(bezeichnung);
    }
    internal static void LabelZeichnen(int x, int xSpan, int y, int ySpan, Brush farbe, double fontSize, string text, int par, string bez, DependencyProperty visibilityProperty, Grid grid)
    {
        var label = new Label
        {
            FontSize = fontSize,
            Foreground = farbe,
            VerticalAlignment = VerticalAlignment.Bottom
        };

        if (bez == "-")
        {
            label.Content = text;

        }
        else
        {
            label.SetBinding(TextBlock.TextProperty, new Binding($"DisplayPlcAnzeige.Label{bez} [{ par }]"));
            label.SetBinding(visibilityProperty, new Binding($"DisplayPlcAnzeige.Visibility{bez} [{ par }]"));
        }

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
    internal static void KommentarZeichnen(int x, int y, int schriftGroesse, VerticalAlignment vertical, string text, int par, string bez, DependencyProperty visibilityProperty, Grid grid)
    {
        var kommentar = new TextBlock
        {
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

        if (bez == "-")
        {
            kommentar.Text = text;

        }
        else
        {
            kommentar.SetBinding(TextBlock.TextProperty, new Binding($"DisplayPlcAnzeige.Kommentar{bez} [{par}]"));
            kommentar.SetBinding(visibilityProperty, new Binding($"DisplayPlcAnzeige.Visibility{bez} [{par}]"));
        }

        SetColumn(kommentar, x);
        SetRow(kommentar, y);

        if (bez != "-") kommentar.SetBinding(visibilityProperty, new Binding($"DisplayPlcAnzeige.Visibility{bez} [{ par }]"));

        grid.Children.Add(kommentar);
    }
}