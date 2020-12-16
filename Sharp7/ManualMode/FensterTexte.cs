using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ManualMode
{
    public static partial class FensterFunktionen
    {
        internal static void TextZeichnen(string beschriftung, HorizontalAlignment alignment, int x, int y, Grid grid)
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
        internal static void KommentarZeichnen(int x, int y, int par, string bez, DependencyProperty visibilityProperty, Grid grid)
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

            kommentar.SetBinding(TextBlock.TextProperty, new Binding($"ManVisuAnzeigen.Kommentar{bez} [{ par }]"));
            kommentar.SetBinding(visibilityProperty, new Binding($"ManVisuAnzeigen.Visibility{bez} [{ par }]"));

            Grid.SetColumn(kommentar, x);
            Grid.SetRow(kommentar, y);
            grid.Children.Add(kommentar);
        }
        internal static void BezeichnungZeichnen(int x, int y, int par, string bez, HorizontalAlignment horizontal, DependencyProperty visibilityProperty, Grid grid)
        {
            var bezeichnung = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = horizontal
            };

            bezeichnung.SetBinding(TextBlock.TextProperty, new Binding($"ManVisuAnzeigen.Bezeichnung{bez} [{ par }]"));
            bezeichnung.SetBinding(visibilityProperty, new Binding($"ManVisuAnzeigen.Visibility{bez} [{ par }]"));

            Grid.SetColumn(bezeichnung, x);
            Grid.SetRow(bezeichnung, y);
            grid.Children.Add(bezeichnung);
        }
        internal static void TextboxByteZeichnen(int x, int y, int par, string bez, DependencyProperty visibilityProperty, ManualMode manualMode, Grid grid)
        {
            var textBox = new TextBox
            {
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Text = "",
                Name = $"{bez}_{par}",
                Tag = manualMode
            };


            textBox.TextChanged += TextBoxChanged;

            Grid.SetColumn(textBox, x);
            Grid.SetRow(textBox, y);

            grid.Children.Add(textBox);
        }
        private static void TextBoxChanged(object s, TextChangedEventArgs e)
        {
            if (s is not TextBox) return;
            var localSender = (TextBox) s;

            var manMode = localSender.Tag as ManualMode;
            var textBoxNamensTeile = localSender.Name.Split("_");

            var id = short.Parse(textBoxNamensTeile[1]);
            var eingabe = localSender.Text.Length > 0 ? byte.Parse(localSender.Text) : (byte) 0;

            switch (textBoxNamensTeile[0])
            {
                case "Da":
                    if (manMode != null) manMode.Datenstruktur.DigOutput[id] = eingabe;
                    break;
            }
        }
    }
}