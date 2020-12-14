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
        public static bool DatenTypenBit { get; set; }
        public static bool DatenTypenByte { get; set; }
        public static bool DatenTypenWord { get; set; }
        public static bool DatenTypenLong { get; set; }


        private const int SpaltenAbstand = 10;
        private const int SpaltenWert = 80;
        private const int SpaltenBezeichnung = 120;
        private const int SpaltenKommentar = 300;

        private const int ZeilenAbstand = 10;
        private const int ZeilenHoehe = 45;

        private readonly FensterFunktionen _fensterFunktionen = new FensterFunktionen();

        public DaFenster(DaConfig daConfig, ManualViewModel mvm)
        {
            DatenTypenBit = false;
            DatenTypenByte = false;
            DatenTypenWord = false;

            InitializeComponent();
            DataContext = mvm;

            var anzahlZeilenConfig = DaDatenLesen(daConfig, mvm);
            if (DatenTypenBit) DaCreateGridBit(anzahlZeilenConfig, daConfig, mvm);
            if (DatenTypenByte) DaCreateGridByte(anzahlZeilenConfig, daConfig, mvm);
            if (DatenTypenWord) DaCreateGridWord(anzahlZeilenConfig, daConfig, mvm);
            if (DatenTypenLong) DaCreateGridLong(anzahlZeilenConfig, daConfig, mvm);
        }




        private static int DaDatenLesen(DaConfig daConfig, ManualViewModel manualViewModel)
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
                            if (DatenTypenByte || DatenTypenWord || DatenTypenLong) throw new ArgumentOutOfRangeException();
                            break;
                        case 8:
                            DatenTypenByte = true;
                            if (DatenTypenBit || DatenTypenWord || DatenTypenLong) throw new ArgumentOutOfRangeException();
                            break;
                        case 16:
                            DatenTypenWord = true;
                            if (DatenTypenBit || DatenTypenByte || DatenTypenLong) throw new ArgumentOutOfRangeException();
                            break;
                        case 32:
                            DatenTypenLong = true;
                            if (DatenTypenBit || DatenTypenByte || DatenTypenWord) throw new ArgumentOutOfRangeException();
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

        private void DaCreateGridBit(int anzahlZeilenConfig, DaConfig config, ManualViewModel manualViewModel)
        {
            var daGrid = new Grid { Name = "DaGrid" };
            Content = daGrid;

            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenWert) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenWert) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenBezeichnung) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                daGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ZeilenHoehe) });
                daGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ZeilenAbstand) });
            }

            _fensterFunktionen.HintergrundRechteckZeichnen(0, 0, 7, Brushes.Yellow, daGrid);

            _fensterFunktionen.TextZeichnen("Tasten", HorizontalAlignment.Center, 0, 0, daGrid);
            _fensterFunktionen.TextZeichnen("Toggeln", HorizontalAlignment.Center, 2, 0, daGrid);
            _fensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Center, 4, 0, daGrid);
            _fensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 6, 0, daGrid);

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                _fensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * i, 7, Brushes.YellowGreen, daGrid);

                DaButtonTastenZeichnen(config.DigitaleAusgaenge[i].StartByte, config.DigitaleAusgaenge[i].StartBit, 0,
                    2 + 2 * i, daGrid, manualViewModel);
                DaButtonToggelnZeichnen(config.DigitaleAusgaenge[i].StartByte, config.DigitaleAusgaenge[i].StartBit, 2,
                    2 + 2 * i, daGrid, manualViewModel);
                DaBezeichnungBitZeichnen(config.DigitaleAusgaenge[i].StartByte, config.DigitaleAusgaenge[i].StartBit, 4,
                    2 + 2 * i, daGrid);
                DaKommentarBitZeichnen(config.DigitaleAusgaenge[i].StartByte, config.DigitaleAusgaenge[i].StartBit, 6,
                    2 + 2 * i, daGrid);
            }
        }
        private void DaCreateGridByte(int anzahlZeilenConfig, DaConfig config, ManualViewModel manualViewModel)
        {
            var daGrid = new Grid { Name = "DaGrid" };
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

            _fensterFunktionen.HintergrundRechteckZeichnen(0, 0, 7, Brushes.Yellow, daGrid);

            _fensterFunktionen.TextZeichnen("Eingabe", HorizontalAlignment.Center, 0, 0, daGrid);
            _fensterFunktionen.TextZeichnen("Aktuell", HorizontalAlignment.Center, 2, 0, daGrid);
            _fensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Left, 4, 0, daGrid);
            _fensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 6, 0, daGrid);

            for (var vbyte = 0; vbyte <= anzahlZeilenConfig; vbyte++)
            {
                _fensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * vbyte, 7, Brushes.YellowGreen, daGrid);
                DaTextboxByteZeichnen(vbyte, 0, 2 + 2 * vbyte, daGrid);
                DaBezeichnungByteZeichnen(vbyte, 4, 2 + 2 * vbyte, daGrid);
                DaKommentarByteZeichnen(vbyte, 6, 2 + 2 * vbyte, daGrid);
            }
        }
        private void DaCreateGridWord(int anzahlZeilenConfig, DaConfig config, ManualViewModel manualViewModel)
        {
            throw new NotImplementedException();
        }
        private void DaCreateGridLong(int anzahlZeilenConfig, DaConfig config, ManualViewModel manualViewModel)
        {
            throw new NotImplementedException();
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
    }
}