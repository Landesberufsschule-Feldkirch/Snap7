using ManualMode.Model;
using ManualMode.ViewModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ManualMode
{
    public partial class DiFenster
    {
        public bool DatenTypenBit { get; set; }
        public bool DatenTypenByte { get; set; }
        public bool DatenTypenWord { get; set; }

        public DiFenster(DiConfig diConfig, ManualViewModel mvm)
        {
            DatenTypenBit = false;
            DatenTypenByte = false;
            DatenTypenWord = false;

            InitializeComponent();
            DataContext = mvm;

            var anzahlZeilenConfig = DiDatenLesen(diConfig, mvm);
            DiCreateGrid(anzahlZeilenConfig);
        }
        private static int DiDatenLesen(DiConfig diConfig, ManualViewModel manViewModel)
        {
            var anzahlZeilenConfig = 0;
            var aktuellesBit = 0;
            var aktuellesByte = 0;

            foreach (var config in diConfig.DigitaleEingaenge)
            {
                if (config.LaufendeNr != anzahlZeilenConfig)
                {
                    throw new InvalidDataException($"{nameof(DiFenster)} invalid {config.LaufendeNr} ");
                }
                else
                {
                    switch (config.Type)
                    {
                        case PlcEinUndAusgaengeTypen.Default:
                            {
                                if (config.AnzahlBit == 1)
                                {
                                    if (config.StartByte == aktuellesByte && config.StartBit == aktuellesBit)
                                    {
                                        aktuellesBit++;
                                        if (aktuellesBit > 7)
                                        {
                                            aktuellesBit = 0;
                                            aktuellesByte++;
                                        }
                                    }
                                    else throw new InvalidDataException("Byte und Bit müssen schön gefüllt sein!");
                                }
                                break;
                            }
                        case PlcEinUndAusgaengeTypen.Ascii:
                            break;
                        case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent:
                            break;
                        case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille:
                            break;
                        case PlcEinUndAusgaengeTypen.BitmusterByte:
                            break;
                        case PlcEinUndAusgaengeTypen.Schieberegler:
                            break;
                        default:
                            throw new InvalidDataException(config.Type.ToString() + config.AnzahlBit);
                    }


                    manViewModel.ManVisuAnzeigen.VisibilityDi[config.LaufendeNr] = Visibility.Visible;
                    manViewModel.ManVisuAnzeigen.BezeichnungDi[config.LaufendeNr] = config.Bezeichnung;
                    manViewModel.ManVisuAnzeigen.KommentarDi[config.LaufendeNr] = config.Kommentar;

                    anzahlZeilenConfig++;
                }
            }

            return anzahlZeilenConfig;
        }
        private void DiCreateGrid(int anzahlZeilenConfig)
        {

            var diGrid = new Grid
            {
                Name = "DiGrid"
            };

            Content = diGrid;

            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                diGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(45) });
                diGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10) });
            }

            DiHintergrundRechteckZeichnen(0, 0, 7, Brushes.Yellow, diGrid);
            DiTextZeichnen("Wert", HorizontalAlignment.Center, 0, 0, diGrid);
            DiTextZeichnen("Bezeichnung", HorizontalAlignment.Center, 2, 0, diGrid);
            DiTextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, diGrid);

            for (var vbyte = 0; vbyte < 10; vbyte++)
            {
                for (var vBit = 0; vBit < 8; vBit++)
                {
                    if (8 * vbyte + vBit > anzahlZeilenConfig) continue;

                    DiHintergrundRechteckZeichnen(0, 2 + vbyte * 16 + 2 * vBit, 5, Brushes.YellowGreen, diGrid);
                    DiButtonZeichnen(vbyte, vBit, 0, 2 + vbyte * 16 + 2 * vBit, diGrid);
                    DiBezeichnungZeichnen(vbyte, vBit, 2, 2 + vbyte * 16 + 2 * vBit, diGrid);
                    DiKommentarZeichnen(vbyte, vBit, 4, 2 + vbyte * 16 + 2 * vBit, diGrid);
                }
            }
        }

        private static void DiHintergrundRechteckZeichnen(int x, int y, int span, Brush farbe, Panel panel)
        {
            var hintergrund = new Rectangle
            {
                Margin = new Thickness(-4, -4, 0, -4),
                Fill = farbe
            };

            Grid.SetColumn(hintergrund, x);
            Grid.SetColumnSpan(hintergrund, span);
            Grid.SetRow(hintergrund, y);
            panel.Children.Add(hintergrund);
        }
        private static void DiButtonZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
        {
            var parameterNummer = 8 * vbyte + vbit;

            var buttonTasten = new Button
            {
                Content = vbyte + "." + vbit,
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(3, 3, 3, 3)
            };

            buttonTasten.SetBinding(BackgroundProperty, new Binding("ManVisuAnzeigen.FarbeDi[" + parameterNummer + "]"));
            buttonTasten.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDi[" + parameterNummer + "]"));

            Grid.SetColumn(buttonTasten, x);
            Grid.SetRow(buttonTasten, y);

            panel.Children.Add(buttonTasten);
        }
        private static void DiBezeichnungZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
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
        private static void DiKommentarZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
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
        private static void DiTextZeichnen(string beschriftung, HorizontalAlignment alignment, int x, int y, Panel panel)
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