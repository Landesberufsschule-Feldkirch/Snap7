using ManualMode.Model;
using ManualMode.ViewModel;
using System;
using System.Windows.Shapes;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace ManualMode
{
    public partial class DaFenster
    {
        public bool DatenTypenBit { get; set; }
        public bool DatenTypenByte { get; set; }
        public bool DatenTypenWord { get; set; }


        public DaFenster(DaConfig daConfig, ManualViewModel mvm)
        {
            DatenTypenBit = false;
            DatenTypenByte = false;
            DatenTypenWord = false;

            InitializeComponent();
            DataContext = mvm;

            var anzahlZeilenConfig = DaDatenLesen(daConfig, mvm);
            DaCreateGrid(anzahlZeilenConfig, mvm);
        }
        private int DaDatenLesen(DaConfig daConfig, ManualViewModel manualViewModel)
        {
            var anzahlZeilenConfig = 0;

            foreach (var config in daConfig.DigitaleAusgaenge)
            {
                if (config.LaufendeNr == anzahlZeilenConfig)
                {

                    switch (config.AnzahlBit)
                    {
                        case 1:
                            DatenTypenBit = true;
                            if (DatenTypenByte || DatenTypenWord) throw new ArgumentOutOfRangeException();
                            break;
                        case 8:
                            DatenTypenByte = true;
                            if (DatenTypenBit || DatenTypenWord) throw new ArgumentOutOfRangeException();
                            break;
                        case 16:
                            DatenTypenWord = true;
                            if (DatenTypenBit || DatenTypenByte) throw new ArgumentOutOfRangeException();
                            break;
                        default: throw new ArgumentOutOfRangeException();
                    }

                    switch (config.Type)
                    {
                        case PlcEinUndAusgaengeTypen.BitmusterByte:
                        case PlcEinUndAusgaengeTypen.Default:
                            manualViewModel.ManVisuAnzeigen.VisibilityDa[config.LaufendeNr] = Visibility.Visible;
                            manualViewModel.ManVisuAnzeigen.BezeichnungDa[config.LaufendeNr] = config.Bezeichnung;
                            manualViewModel.ManVisuAnzeigen.KommentarDa[config.LaufendeNr] = config.Kommentar;
                            break;
                        case PlcEinUndAusgaengeTypen.Ascii:
                            break;
                        case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent:
                            break;
                        case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille:
                            break;
                        case PlcEinUndAusgaengeTypen.Schieberegler:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    anzahlZeilenConfig++;
                }
                else throw new InvalidDataException($"{nameof(DiFenster)} invalid {config.LaufendeNr} ");

            }

            return anzahlZeilenConfig;
        }
        private void DaCreateGrid(int anzahlZeilenConfig, ManualViewModel manualViewModel)
        {
            var daGrid = new Grid
            {
                Name = "DaGrid"
            };

            Content = daGrid;

            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                daGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(45) });
                daGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10) });
            }

            DaHintergrundRechteckZeichnen(0, 0, 7, Brushes.Yellow, daGrid);

            if (DatenTypenByte)
            {
                DaTextZeichnen("Eingabe", HorizontalAlignment.Center, 0, 0, daGrid);
                DaTextZeichnen("Aktuell", HorizontalAlignment.Center, 2, 0, daGrid);
                DaTextZeichnen("Bezeichnung", HorizontalAlignment.Left, 4, 0, daGrid);
                DaTextZeichnen("Kommentar", HorizontalAlignment.Left, 6, 0, daGrid);

                for (var vbyte = 0; vbyte <= anzahlZeilenConfig; vbyte++)
                {
                    DaHintergrundRechteckZeichnen(0, 2 + 2 * vbyte, 7, Brushes.YellowGreen, daGrid);
                    DaTextboxByteZeichnen(vbyte, 0, 2 + 2 * vbyte, daGrid);
                    DaBezeichnungByteZeichnen(vbyte, 4, 2 + 2 * vbyte, daGrid);
                    DaKommentarByteZeichnen(vbyte, 6, 2 + 2 * vbyte, daGrid);
                }
            }
            else
            {
                DaTextZeichnen("Tasten", HorizontalAlignment.Center, 0, 0, daGrid);
                DaTextZeichnen("Toggeln", HorizontalAlignment.Center, 2, 0, daGrid);
                DaTextZeichnen("Bezeichnung", HorizontalAlignment.Center, 4, 0, daGrid);
                DaTextZeichnen("Kommentar", HorizontalAlignment.Left, 6, 0, daGrid);

                for (var vbyte = 0; vbyte < 10; vbyte++)
                {
                    for (var vBit = 0; vBit < 8; vBit++)
                    {
                        if (8 * vbyte + vBit >= anzahlZeilenConfig) continue;

                        DaHintergrundRechteckZeichnen(0, 2 + vbyte * 16 + 2 * vBit, 7, Brushes.YellowGreen, daGrid);
                        DaButtonTastenZeichnen(vbyte, vBit, 0, 2 + vbyte * 16 + 2 * vBit, daGrid, manualViewModel);
                        DaButtonToggelnZeichnen(vbyte, vBit, 2, 2 + vbyte * 16 + 2 * vBit, daGrid, manualViewModel);
                        DaBezeichnungBitZeichnen(vbyte, vBit, 4, 2 + vbyte * 16 + 2 * vBit, daGrid);
                        DaKommentarBitZeichnen(vbyte, vBit, 6, 2 + vbyte * 16 + 2 * vBit, daGrid);
                    }
                }
            }
        }

        private static void DaHintergrundRechteckZeichnen(int x, int y, int span, Brush farbe, Panel panel)
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

        private static void DaButtonTastenZeichnen(int vbyte, int vbit, int x, int y, Panel panel, ManualViewModel manualViewModel)
        {
            var parameterNummer = 8 * vbyte + vbit;

            var buttonTasten = new Button
            {
                Content = vbyte + "." + vbit,
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Command = manualViewModel.BtnTasten,
                CommandParameter = parameterNummer.ToString(),
                Margin = new Thickness(3, 3, 3, 3)
            };

            buttonTasten.SetBinding(BackgroundProperty, new Binding("ManVisuAnzeigen.FarbeTastenToggelnDa[" + parameterNummer + "]"));
            buttonTasten.SetBinding(ButtonBase.ClickModeProperty, new Binding("ManVisuAnzeigen.ClickModeTasten[" + parameterNummer + "]"));
            buttonTasten.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(buttonTasten, x);
            Grid.SetRow(buttonTasten, y);

            panel.Children.Add(buttonTasten);
        }
        private static void DaButtonToggelnZeichnen(int vbyte, int vbit, int x, int y, Panel panel, ManualViewModel manualViewModel)
        {
            var parameterNummer = 8 * vbyte + vbit;

            var buttonToggeln = new Button
            {
                Content = vbyte + "." + vbit,
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Command = manualViewModel.BtnToggeln,
                CommandParameter = parameterNummer.ToString(),
                Margin = new Thickness(3, 3, 3, 3)
            };

            buttonToggeln.SetBinding(BackgroundProperty, new Binding("ManVisuAnzeigen.FarbeTastenToggelnDa[" + parameterNummer + "]"));
            buttonToggeln.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(buttonToggeln, x);
            Grid.SetRow(buttonToggeln, y);

            panel.Children.Add(buttonToggeln);
        }
        private static void DaBezeichnungBitZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
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

            bezeichnung.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.BezeichnungDa[" + parameterNummer + "]"));
            bezeichnung.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(bezeichnung, x);
            Grid.SetRow(bezeichnung, y);
            panel.Children.Add(bezeichnung);
        }
        private static void DaKommentarBitZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
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

            kommentar.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.KommentarDa[" + parameterNummer + "]"));
            kommentar.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(kommentar, x);
            Grid.SetRow(kommentar, y);
            panel.Children.Add(kommentar);
        }
        private static void DaTextboxByteZeichnen(int vbyte, int x, int y, Panel panel)
        {
            var parameterNummer = vbyte;

            var textBox = new TextBox
            {
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black)
            };



            //textBox.SetBinding(TextBox.TextProperty, new Binding( "ManVisuAnzeigen.WertDa[" + parameterNummer + "], Mode=TwoWay, UpdateSourceTrigger=PropertyChanged"  ) );
            //textBox.SetBinding(TextBox.TextProperty, new Binding("ManVisuAnzeigen.WertDa[" + parameterNummer + "], UpdateSourceTrigger=PropertyChanged"));
            //textBox.SetBinding(TextBox.TextProperty, new Binding("ManVisuAnzeigen.WertDa[" + parameterNummer + "]"));


            var textBinding = new Binding
            {
                Path = new PropertyPath("ManVisuAnzeigen.WertDa[" + parameterNummer + "]"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.LostFocus
            };

            textBox.SetBinding(TextBox.TextProperty, textBinding);
            textBox.IsEnabled = true;


            textBox.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(textBox, x);
            Grid.SetRow(textBox, y);

            panel.Children.Add(textBox);
        }
        private static void DaBezeichnungByteZeichnen(int vbyte, int x, int y, Panel panel)
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

            bezeichnung.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.BezeichnungDa[" + parameterNummer + "]"));
            bezeichnung.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(bezeichnung, x);
            Grid.SetRow(bezeichnung, y);
            panel.Children.Add(bezeichnung);
        }
        private static void DaKommentarByteZeichnen(int vbyte, int x, int y, Panel panel)
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

            kommentar.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.KommentarDa[" + parameterNummer + "]"));
            kommentar.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(kommentar, x);
            Grid.SetRow(kommentar, y);
            panel.Children.Add(kommentar);
        }
        private static void DaTextZeichnen(string beschriftung, HorizontalAlignment alignment, int x, int y, Panel panel)
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