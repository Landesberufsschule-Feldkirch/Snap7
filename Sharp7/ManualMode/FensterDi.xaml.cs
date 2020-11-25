using System.Collections.ObjectModel;
using ManualMode.ViewModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using ManualMode.Model;

namespace ManualMode
{
    public partial class FensterDi
    {
        public FensterDi(Model.ConfigDi configDi, ManualViewModel mvm)
        {
            var manViewModel = mvm;
            InitializeComponent();
            DataContext = manViewModel;

            var anzahlBit = DigitaleEingaengeDatenLesen(configDi, manViewModel);
            CreateGridDi(anzahlBit);
        }



        private static int DigitaleEingaengeDatenLesen(ConfigDi configDi, ManualViewModel manViewModel)
        {
            var anzahlBit = 0;

            foreach (var config in configDi.DigitaleEingaenge)
            {
                if (config.LaufendeNr == anzahlBit)
                {
                    manViewModel.ManVisuAnzeigen.VisibilityDi[config.LaufendeNr] = Visibility.Visible;
                    manViewModel.ManVisuAnzeigen.BezeichnungDi[config.LaufendeNr] = config.Bezeichnung;
                    manViewModel.ManVisuAnzeigen.KommentarDi[config.LaufendeNr] = config.Kommentar;

                    anzahlBit++;
                }
                else
                {
                    throw new InvalidDataException($"{nameof(FensterDi)} invalid {config.LaufendeNr} ");
                }
            }

            return anzahlBit + 1;
        }


        private void CreateGridDi(int anzahlBit)
        {

            var gridDi = new Grid
            {
                Name = "GridDi",
                Width = 600
            };

            Content = gridDi;

            gridDi.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
            gridDi.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            gridDi.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });

            var rowDefCollection = new ObservableCollection<RowDefinition>();
            for (var i = 0; i < anzahlBit; i++)
            {
                rowDefCollection.Add(new RowDefinition { Height = new GridLength(45) });
                gridDi.RowDefinitions.Add(rowDefCollection[i]);
            }

            TextZeichnen("Wert", HorizontalAlignment.Center, 0, 0, gridDi);
            TextZeichnen("Bezeichnung", HorizontalAlignment.Center, 1, 0, gridDi);
            TextZeichnen("Kommentar", HorizontalAlignment.Left, 2, 0, gridDi);

            for (var vbyte = 0; vbyte < 10; vbyte++)
            {
                for (var vBit = 0; vBit < 8; vBit++)
                {
                    if (8 * vbyte + vBit >= anzahlBit) continue;
                    ButtonZeichnen(vbyte, vBit, 0, 1 + vbyte * 8 + vBit, gridDi);
                    BezeichnungZeichnen(vbyte, vBit, 1, 1 + vbyte * 8 + vBit, gridDi);
                    KommentarZeichnen(vbyte, vBit, 2, 1 + vbyte * 8 + vBit, gridDi);

                }
            }
        }

        private static void ButtonZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
        {
            var parameterNummer = 8 * vbyte + vbit;

            var buttonTasten = new Button
            {
                Content = vbyte + "." + vbit,
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black)
            };

            buttonTasten.SetBinding(BackgroundProperty, new Binding("ManVisuAnzeigen.FarbeDi[" + parameterNummer + "]"));
            buttonTasten.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDi[" + parameterNummer + "]"));

            Grid.SetColumn(buttonTasten, x);
            Grid.SetRow(buttonTasten, y);

            panel.Children.Add(buttonTasten);
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
                HorizontalAlignment = HorizontalAlignment.Center
            };

            bezeichnung.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.BezeichnungDi[" + parameterNummer + "]"));
            bezeichnung.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDi[" + parameterNummer + "]"));

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

            kommentar.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.KommentarDi[" + parameterNummer + "]"));
            kommentar.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDi[" + parameterNummer + "]"));

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