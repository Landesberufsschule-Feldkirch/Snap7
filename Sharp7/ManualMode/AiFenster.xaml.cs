using ManualMode.ViewModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ManualMode
{
    public partial class AiFenster
    {
        public AiFenster(Model.AiConfig aiConfig, ManualViewModel mvm)
        {
            var manViewModel = mvm;
            InitializeComponent();
            DataContext = manViewModel;

            var anzahlZeilenConfig = AiDatenLesen(aiConfig, manViewModel);
            AiCreateGrid(anzahlZeilenConfig);
        }
        private static int AiDatenLesen(Model.AiConfig aiConfig, ManualViewModel manViewModel)
        {
            var anzahlZeilenConfig = 0;

            foreach (var config in aiConfig.AnalogeEingaenge)
            {
                if (config.LaufendeNr == anzahlZeilenConfig)
                {
                    manViewModel.ManVisuAnzeigen.VisibilityAi[config.LaufendeNr] = Visibility.Visible;
                    manViewModel.ManVisuAnzeigen.BezeichnungAi[config.LaufendeNr] = config.Bezeichnung;
                    manViewModel.ManVisuAnzeigen.KommentarAi[config.LaufendeNr] = config.Kommentar;

                    anzahlZeilenConfig++;
                }
                else
                {
                    throw new InvalidDataException($"{nameof(DiFenster)} invalid {config.LaufendeNr} ");
                }
            }
            return anzahlZeilenConfig + 1;
        }
        private void AiCreateGrid(in int anzahlZeilenConfig)
        {

            var aiGgrid = new Grid
            {
                Name = "AiGrid",
                Width = 600
            };

            Content = aiGgrid;

            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                aiGgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(45) });
                aiGgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10) });
            }

            AiTextZeichnen("Wert", HorizontalAlignment.Left, 0, 0, aiGgrid);
            AiTextZeichnen("Bezeichnung", HorizontalAlignment.Left, 2, 0, aiGgrid);
            AiTextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, aiGgrid);

            for (var vbyte = 0; vbyte < 10; vbyte++)
            {
                for (var vBit = 0; vBit < 8; vBit++)
                {
                    if (8 * vbyte + vBit >= anzahlZeilenConfig) continue;

                    AiWertZeichnen(vbyte, vBit, 0, 2 + vbyte * 16 + 2 * vBit, aiGgrid);
                    AiBezeichnungZeichnen(vbyte, vBit, 2, 2 + vbyte * 16 + 2 * vBit, aiGgrid);
                    AiKommentarZeichnen(vbyte, vBit, 4, 2 + vbyte * 16 + 2 * vBit, aiGgrid);
                }
            }
        }
        private static void AiWertZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
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
        private static void AiBezeichnungZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
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
        private static void AiKommentarZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
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
        private static void AiTextZeichnen(string beschriftung, HorizontalAlignment alignment, int x, int y, Panel panel)
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