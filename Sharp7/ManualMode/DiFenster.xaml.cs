using System;
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

        public DiFenster(DiConfig diConfig, ManualViewModel mvm)
        {
            DatenTypenBit = false;
            DatenTypenByte = false;
            DatenTypenWord = false;

            InitializeComponent();
            DataContext = mvm;

            var anzahlZeilenConfig = DiDatenLesen(diConfig, mvm);

            if (DatenTypenBit) DiCreateGridBit(anzahlZeilenConfig, diConfig);
            if (DatenTypenByte) DiCreateGridByte(anzahlZeilenConfig, diConfig);
            if (DatenTypenWord) DiCreateGridWord(anzahlZeilenConfig, diConfig);
            if (DatenTypenLong) DiCreateGridLong(anzahlZeilenConfig, diConfig);
        }

        private static int DiDatenLesen(DiConfig diConfig, ManualViewModel manViewModel)
        {
            var anzahlZeilenConfig = 0;
            var aktuellesBit = 0;
            var aktuellesByte = 0;

            foreach (var config in diConfig.DigitaleEingaenge)
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
                else
                {
                    throw new InvalidDataException($"{nameof(DiFenster)} invalid {config.LaufendeNr} ");
                }
            }

            return anzahlZeilenConfig;
        }

        private void DiCreateGridBit(int anzahlZeilenConfig, DiConfig config)
        {
            var diGrid = new Grid { Name = "DiGrid" };
            Content = diGrid;

            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenWert) });
            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenBezeichnung) });
            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                diGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ZeilenHoehe) });
                diGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ZeilenAbstand) });
            }

            _fensterFunktionen.HintergrundRechteckZeichnen(0, 0, 7, Brushes.Yellow, diGrid);
            _fensterFunktionen.TextZeichnen("Wert", HorizontalAlignment.Center, 0, 0, diGrid);
            _fensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Center, 2, 0, diGrid);
            _fensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, diGrid);

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {

                _fensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * i, 5, Brushes.YellowGreen, diGrid);
                DiButtonZeichnen(config.DigitaleEingaenge[i].StartByte, config.DigitaleEingaenge[i].StartBit, 0, 2 + config.DigitaleEingaenge[i].StartByte * 16 + 2 * config.DigitaleEingaenge[i].StartBit, diGrid);
                DiBezeichnungZeichnen(config.DigitaleEingaenge[i].StartByte, config.DigitaleEingaenge[i].StartBit, 2, 2 + config.DigitaleEingaenge[i].StartByte * 16 + 2 * config.DigitaleEingaenge[i].StartBit, diGrid);
                _fensterFunktionen.KommentarZeichnen(4, 2 + 2 * i, i, "Di", VisibilityProperty, diGrid);

            }
        }
        private void DiCreateGridByte(int anzahlZeilenConfig, DiConfig config)
        {
            throw new NotImplementedException();
        }
        private void DiCreateGridWord(int anzahlZeilenConfig, DiConfig config)
        {
            throw new NotImplementedException();
        }
        private void DiCreateGridLong(int anzahlZeilenConfig, DiConfig config)
        {
            throw new NotImplementedException();
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
    }
}