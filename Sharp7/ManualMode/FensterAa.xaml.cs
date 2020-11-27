using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using ManualMode.Model;
using ManualMode.ViewModel;

namespace ManualMode
{
    public partial class FensterAa
    {
        public FensterAa(ConfigAa configAa, ManualViewModel mvm)
        {
            var manualViewModel = mvm;
            InitializeComponent();
            DataContext = manualViewModel;

            var anzahlByte = DatenAaLesen(configAa, manualViewModel);
            CreateGridAa(anzahlByte);
        }
        private static int DatenAaLesen(ConfigAa configAa, ManualViewModel manualViewModel)
        {
            var anzahlByte = 0;

            foreach (var config in configAa.AnalogeAusgaenge)
            {
                if (config.LaufendeNr == anzahlByte)
                {
                    manualViewModel.ManVisuAnzeigen.VisibilityAa[config.LaufendeNr] = Visibility.Visible;
                    manualViewModel.ManVisuAnzeigen.BezeichnungAa[config.LaufendeNr] = config.Bezeichnung;
                    manualViewModel.ManVisuAnzeigen.KommentarAa[config.LaufendeNr] = config.Kommentar;

                    anzahlByte++;
                }
                else
                {
                    throw new InvalidDataException($"{nameof(FensterDi)} invalid {config.LaufendeNr} ");
                }
            }
            return anzahlByte + 1;
        }
        private void CreateGridAa(int anzahlByte)
        {
            var gridAa = new Grid
            {
                Name = "GridAa",
                Width = 600
            };

            Content = gridAa;

            gridAa.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });
            gridAa.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            gridAa.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });

            var rowDefCollection = new ObservableCollection<RowDefinition>();
            for (var i = 0; i < anzahlByte; i++)
            {
                rowDefCollection.Add(new RowDefinition { Height = new GridLength(45) });
                gridAa.RowDefinitions.Add(rowDefCollection[i]);
            }

            TextAaZeichnen("Wert", HorizontalAlignment.Left, 0, 0, gridAa);
            TextAaZeichnen("Bezeichnung", HorizontalAlignment.Left, 1, 0, gridAa);
            TextAaZeichnen("Kommentar", HorizontalAlignment.Left, 2, 0, gridAa);

            for (var vbyte = 0; vbyte < anzahlByte; vbyte++)
            {

                BezeichnungAaZeichnen(vbyte, 1, 1 + vbyte, gridAa);
                KommentarAaZeichnen(vbyte, 2, 1 + vbyte, gridAa);
            }
        }
        private static void BezeichnungAaZeichnen(int vbyte, int x, int y, Panel panel)
        {
            var parameterNummer = vbyte;

            var bezeichnung = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            bezeichnung.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.BezeichnungAa[" + parameterNummer + "]"));
            bezeichnung.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityAa[" + parameterNummer + "]"));

            Grid.SetColumn(bezeichnung, x);
            Grid.SetRow(bezeichnung, y);
            panel.Children.Add(bezeichnung);
        }
        private static void KommentarAaZeichnen(int vbyte, int x, int y, Panel panel)
        {
            var parameterNummer = vbyte;

            var kommentar = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                Padding = new Thickness(10, 5, 5, 5),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            kommentar.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.KommentarAa[" + parameterNummer + "]"));
            kommentar.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityAa[" + parameterNummer + "]"));

            Grid.SetColumn(kommentar, x);
            Grid.SetRow(kommentar, y);
            panel.Children.Add(kommentar);
        }
        private static void TextAaZeichnen(string beschriftung, HorizontalAlignment alignment, int x, int y, Panel panel)
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
            panel.Children.Add(text);
        }
    }
}
