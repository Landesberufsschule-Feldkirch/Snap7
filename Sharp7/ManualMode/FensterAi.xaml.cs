using System.Collections.ObjectModel;
using ManualMode.ViewModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ManualMode
{
    public partial class FensterAi
    {
        public FensterAi(Model.ConfigAi configAi, ManualViewModel mvm)
        {
            var manViewModel = mvm;
            InitializeComponent();
            DataContext = manViewModel;

            var anzahlByte = AnalogeEingangeDatenLesen(configAi, manViewModel);
            CreateGridAi(anzahlByte);
        }
        
        private static int AnalogeEingangeDatenLesen(Model.ConfigAi configAi, ManualViewModel manViewModel)
        {
            var anzahlByte = 0;

            foreach (var config in configAi.AnalogeEingaenge)
            {
                if (config.LaufendeNr == anzahlByte)
                {
                    manViewModel.ManVisuAnzeigen.VisibilityAi[config.LaufendeNr] = Visibility.Visible;
                    manViewModel.ManVisuAnzeigen.BezeichnungAi[config.LaufendeNr] = config.Bezeichnung;
                    manViewModel.ManVisuAnzeigen.KommentarAi[config.LaufendeNr] = config.Kommentar;

                    anzahlByte++;
                }
                else
                {
                    throw new InvalidDataException($"{nameof(FensterDi)} invalid {config.LaufendeNr} ");
                }
            }
            return anzahlByte + 1;
        }
        private void CreateGridAi(in int anzahlBit)
        {

            var gridAi = new Grid
            {
                Name = "GridAi",
                Width = 600
            };

            Content = gridAi;

            gridAi.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });
            gridAi.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            gridAi.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });

            var rowDefCollection = new ObservableCollection<RowDefinition>();
            for (var i = 0; i < anzahlBit; i++)
            {
                rowDefCollection.Add(new RowDefinition { Height = new GridLength(45) });
                gridAi.RowDefinitions.Add(rowDefCollection[i]);
            }

            TextZeichnen("Wert", HorizontalAlignment.Left, 0, 0, gridAi);
            TextZeichnen("Bezeichnung", HorizontalAlignment.Left, 1, 0, gridAi);
            TextZeichnen("Kommentar", HorizontalAlignment.Left, 2, 0, gridAi);

            for (var vbyte = 0; vbyte < 10; vbyte++)
            {
                for (var vBit = 0; vBit < 8; vBit++)
                {
                    if (8 * vbyte + vBit >= anzahlBit) continue;

                    WertZeichnen(vbyte, vBit, 0, 1 + vbyte * 8 + vBit, gridAi);
                    BezeichnungZeichnen(vbyte, vBit, 1, 1 + vbyte * 8 + vBit, gridAi);
                    KommentarZeichnen(vbyte, vBit, 2, 1 + vbyte * 8 + vBit, gridAi);
                }
            }
        }
        private static void WertZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
        {
            var parameterNummer = 8 * vbyte + vbit;

            var bezeichnung = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            bezeichnung.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.ContentAi[" + parameterNummer + "]"));
            bezeichnung.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityAi[" + parameterNummer + "]"));

            Grid.SetColumn(bezeichnung, x);
            Grid.SetRow(bezeichnung, y);
            panel.Children.Add(bezeichnung);
        }
        private static void BezeichnungZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
        {
            var parameterNummer = 8 * vbyte + vbit;

            var bezeichnung = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            bezeichnung.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.BezeichnungAi[" + parameterNummer + "]"));
            bezeichnung.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityAi[" + parameterNummer + "]"));

            Grid.SetColumn(bezeichnung, x);
            Grid.SetRow(bezeichnung, y);
            panel.Children.Add(bezeichnung);
        }
        private static void KommentarZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
        {
            var parameterNummer = 8 * vbyte + vbit;

            var kommentar = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                Padding = new Thickness(10, 5, 5, 5),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            kommentar.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.KommentarAi[" + parameterNummer + "]"));
            kommentar.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityAi[" + parameterNummer + "]"));

            Grid.SetColumn(kommentar, x);
            Grid.SetRow(kommentar, y);
            panel.Children.Add(kommentar);
        }
        private static void TextZeichnen(string beschriftung, HorizontalAlignment alignment, int x, int y, Panel panel)
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