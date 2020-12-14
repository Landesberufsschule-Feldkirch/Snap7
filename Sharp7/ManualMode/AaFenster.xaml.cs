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
    public partial class AaFenster
    {
        public static bool DatenTypenBit { get; set; }
        public static bool DatenTypenByte { get; set; }
        public static bool DatenTypenWord { get; set; }
        public static bool DatenTypenLong { get; set; }

        private const int SpaltenAbstand = 10;
        private const int SpaltenWert = 300;
        private const int SpaltenBezeichnung = 120;
        private const int SpaltenKommentar = 300;

        private const int ZeilenAbstand = 10;
        private const int ZeilenHoehe = 45;

        private FensterFunktionen _fensterFunktionen = new FensterFunktionen();

        public AaFenster(AaConfig aaConfig, ManualViewModel mvm)
        {
            DatenTypenBit = false;
            DatenTypenByte = false;
            DatenTypenWord = false;

            var manualViewModel = mvm;
            InitializeComponent();
            DataContext = manualViewModel;

            var anzahlZeilenConfig = AaDatenLesen(aaConfig, manualViewModel);

            if (DatenTypenBit) AaCreateGridBit(anzahlZeilenConfig);
            if (DatenTypenByte) AaCreateGridByte(anzahlZeilenConfig);
            if (DatenTypenWord) AaCreateGridWord(anzahlZeilenConfig);
            if (DatenTypenLong) AaCreateGridLong(anzahlZeilenConfig);
        }


        private static int AaDatenLesen(AaConfig aaConfig, ManualViewModel manualViewModel)
        {
            var anzahlZeilenConfig = 0;

            foreach (var config in aaConfig.AnalogeAusgaenge)
            {
                if (config.LaufendeNr == anzahlZeilenConfig)
                {
                    switch (config.AnzahlBit)
                    {
                        case 1:
                            DatenTypenBit = true;
                            if (DatenTypenByte || DatenTypenWord || DatenTypenLong) throw new ArgumentOutOfRangeException(nameof(config.AnzahlBit));
                            break;
                        case 8:
                            DatenTypenByte = true;
                            if (DatenTypenBit || DatenTypenWord || DatenTypenLong) throw new ArgumentOutOfRangeException(nameof(config.AnzahlBit));
                            break;
                        case 16:
                            DatenTypenWord = true;
                            if (DatenTypenBit || DatenTypenByte || DatenTypenLong) throw new ArgumentOutOfRangeException(nameof(config.AnzahlBit));
                            break;
                        case 32:
                            DatenTypenLong = true;
                            if (DatenTypenBit || DatenTypenByte || DatenTypenWord) throw new ArgumentOutOfRangeException(nameof(config.AnzahlBit));
                            break;
                        default: throw new ArgumentOutOfRangeException(nameof(config.AnzahlBit));
                    }

                    manualViewModel.ManVisuAnzeigen.VisibilityAa[config.LaufendeNr] = Visibility.Visible;
                    manualViewModel.ManVisuAnzeigen.BezeichnungAa[config.LaufendeNr] = config.Bezeichnung;
                    manualViewModel.ManVisuAnzeigen.KommentarAa[config.LaufendeNr] = config.Kommentar;

                    anzahlZeilenConfig++;
                }
                else throw new InvalidDataException($"{nameof(DiFenster)} invalid {config.LaufendeNr} ");
            }
            return anzahlZeilenConfig;
        }
        private void AaCreateGridBit(int anzahlZeilenConfig)
        {
            var aaGrid = new Grid { Name = "AaGrid" };
            Content = aaGrid;

            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenWert) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenBezeichnung) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                aaGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ZeilenHoehe) });
                aaGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ZeilenAbstand) });
            }

            _fensterFunktionen.HintergrundRechteckZeichnen(0, 0, 5, Brushes.Yellow, aaGrid);
            _fensterFunktionen.TextZeichnen("Wert", HorizontalAlignment.Left, 0, 0, aaGrid);
            _fensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Left, 2, 0, aaGrid);
            _fensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, aaGrid);

            for (var vbyte = 0; vbyte < anzahlZeilenConfig; vbyte++)
            {
                _fensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * vbyte, 5, Brushes.YellowGreen, aaGrid);
                AaBezeichnungZeichnen(vbyte, 2, 2 + 2 * vbyte, aaGrid);
                  _fensterFunktionen.KommentarZeichnen(4,2 + 2 * vbyte,vbyte, "KommentarAa", "VisibilityAa",VisibilityProperty, aaGrid);
            }
        }
        private void AaCreateGridByte(in int anzahlZeilenConfig)
        {
            throw new NotImplementedException();
        }
        private void AaCreateGridWord(in int anzahlZeilenConfig)
        {
            var aaGrid = new Grid { Name = "AaGrid" };
            Content = aaGrid;

            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenWert) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenBezeichnung) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                aaGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ZeilenHoehe) });
                aaGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ZeilenAbstand) });
            }

            _fensterFunktionen.HintergrundRechteckZeichnen(0, 0, 5, Brushes.Yellow, aaGrid);
            _fensterFunktionen.TextZeichnen("Wert", HorizontalAlignment.Left, 0, 0, aaGrid);
            _fensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Left, 2, 0, aaGrid);
            _fensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, aaGrid);

            for (var vbyte = 0; vbyte < anzahlZeilenConfig; vbyte++)
            {
                _fensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * vbyte, 5, Brushes.YellowGreen, aaGrid);
                AaBezeichnungZeichnen(vbyte, 2, 2 + 2 * vbyte, aaGrid);
                 _fensterFunktionen.KommentarZeichnen(4,2 + 2 * vbyte,vbyte, "KommentarAa", "VisibilityAa",VisibilityProperty, aaGrid);
            }
        }
        private void AaCreateGridLong(in int anzahlZeilenConfig)
        {
            throw new NotImplementedException();
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

    }
}
