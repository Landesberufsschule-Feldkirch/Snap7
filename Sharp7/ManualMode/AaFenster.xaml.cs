using ManualMode.Model;
using ManualMode.ViewModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ManualMode
{
    public partial class AaFenster
    {
        public AaFenster(AaConfig aaConfig, ManualViewModel mvm)
        {
            var manualViewModel = mvm;
            InitializeComponent();
            DataContext = manualViewModel;

            var anzahlZeilenConfig = AaDatenLesen(aaConfig, manualViewModel);
            AaCreateGrid(anzahlZeilenConfig);
        }
        private static int AaDatenLesen(AaConfig aaConfig, ManualViewModel manualViewModel)
        {
            var anzahlZeilenConfig = 0;

            foreach (var config in aaConfig.AnalogeAusgaenge)
            {
                if (config.LaufendeNr == anzahlZeilenConfig)
                {
                    manualViewModel.ManVisuAnzeigen.VisibilityAa[config.LaufendeNr] = Visibility.Visible;
                    manualViewModel.ManVisuAnzeigen.BezeichnungAa[config.LaufendeNr] = config.Bezeichnung;
                    manualViewModel.ManVisuAnzeigen.KommentarAa[config.LaufendeNr] = config.Kommentar;

                    anzahlZeilenConfig++;
                }
                else
                {
                    throw new InvalidDataException($"{nameof(DiFenster)} invalid {config.LaufendeNr} ");
                }
            }
            return anzahlZeilenConfig + 1;
        }
        private void AaCreateGrid(int anzahlZeilenConfig)
        {
            var aaGrid = new Grid
            {
                Name = "AaGrid",
                Width = 600
            };

            Content = aaGrid;

            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                aaGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(45) });
                aaGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10) });
            }

            AaTextZeichnen("Wert", HorizontalAlignment.Left, 0, 0, aaGrid);
            AaTextZeichnen("Bezeichnung", HorizontalAlignment.Left, 2, 0, aaGrid);
            AaTextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, aaGrid);

            for (var vbyte = 0; vbyte < anzahlZeilenConfig; vbyte++)
            {
                AaBezeichnungZeichnen(vbyte, 2, 2 + 2 * vbyte, aaGrid);
                AaKommentarZeichnen(vbyte, 4, 2 + 2 * vbyte, aaGrid);
            }
        }
        private static void AaBezeichnungZeichnen(int vbyte, int x, int y, Panel panel)
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
        private static void AaKommentarZeichnen(int vbyte, int x, int y, Panel panel)
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
        private static void AaTextZeichnen(string beschriftung, HorizontalAlignment alignment, int x, int y, Panel panel)
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
