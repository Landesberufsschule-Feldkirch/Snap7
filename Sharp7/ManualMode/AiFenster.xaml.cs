using System;
using ManualMode.ViewModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using ManualMode.Model;

namespace ManualMode
{
    public partial class AiFenster
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

        private readonly FensterFunktionen _fensterFunktionen = new FensterFunktionen();

        public AiFenster(Model.AiConfig aiConfig, ManualViewModel mvm)
        {
            DatenTypenBit = false;
            DatenTypenByte = false;
            DatenTypenWord = false;

            InitializeComponent();
            DataContext = mvm;

            var anzahlZeilenConfig = AiDatenLesen(aiConfig, mvm);
            if (DatenTypenBit) AiCreateGridBit(anzahlZeilenConfig, aiConfig);
            if (DatenTypenByte) AiCreateGridByte(anzahlZeilenConfig, aiConfig);
            if (DatenTypenWord) AiCreateGridWord(anzahlZeilenConfig, aiConfig);
            if (DatenTypenLong) AiCreateGridLong(anzahlZeilenConfig, aiConfig);
        }

        private static int AiDatenLesen(Model.AiConfig aiConfig, ManualViewModel manViewModel)
        {
            var anzahlZeilenConfig = 0;

            foreach (var config in aiConfig.AnalogeEingaenge)
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

                    manViewModel.ManVisuAnzeigen.VisibilityAi[config.LaufendeNr] = Visibility.Visible;
                    manViewModel.ManVisuAnzeigen.BezeichnungAi[config.LaufendeNr] = config.Bezeichnung;
                    manViewModel.ManVisuAnzeigen.KommentarAi[config.LaufendeNr] = config.Kommentar;

                    anzahlZeilenConfig++;
                }
                else throw new InvalidDataException($"{nameof(DiFenster)} invalid {config.LaufendeNr} ");
            }
            return anzahlZeilenConfig;
        }
        private void AiCreateGridBit(int anzahlZeilenConfig, AiConfig config)
        {
            var aiGgrid = new Grid { Name = "AiGrid" };
            Content = aiGgrid;

            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenWert) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenBezeichnung) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                aiGgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ZeilenHoehe) });
                aiGgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ZeilenAbstand) });
            }

            _fensterFunktionen.HintergrundRechteckZeichnen(0, 0, 5, Brushes.Yellow, aiGgrid);
            _fensterFunktionen.TextZeichnen("Wert", HorizontalAlignment.Left, 0, 0, aiGgrid);
            _fensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Left, 2, 0, aiGgrid);
            _fensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, aiGgrid);

            for (var vbyte = 0; vbyte < 10; vbyte++)
            {
                for (var vBit = 0; vBit < 8; vBit++)
                {
                    if (8 * vbyte + vBit >= anzahlZeilenConfig) continue;

                    _fensterFunktionen.HintergrundRechteckZeichnen(0, 2 + vbyte * 16 + 2 * vBit, 5, Brushes.YellowGreen, aiGgrid);
                    AiWertZeichnen(vbyte, vBit, 0, 2 + vbyte * 16 + 2 * vBit, aiGgrid);
                    AiBezeichnungZeichnen(vbyte, vBit, 2, 2 + vbyte * 16 + 2 * vBit, aiGgrid);
                    _fensterFunktionen.KommentarZeichnen(4, 2 + vbyte * 16 + 2 * vBit, vbyte, "Ai", VisibilityProperty, aiGgrid);
                }
            }
        }
        private void AiCreateGridByte(int anzahlZeilenConfig, AiConfig config)
        {

            var aiGgrid = new Grid { Name = "AiGrid" };
            Content = aiGgrid;

            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenWert) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenBezeichnung) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                aiGgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ZeilenHoehe) });
                aiGgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ZeilenAbstand) });
            }

            _fensterFunktionen.HintergrundRechteckZeichnen(0, 0, 5, Brushes.Yellow, aiGgrid);
            _fensterFunktionen.TextZeichnen("Wert", HorizontalAlignment.Left, 0, 0, aiGgrid);
            _fensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Left, 2, 0, aiGgrid);
            _fensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, aiGgrid);

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                _fensterFunktionen.HintergrundRechteckZeichnen(0, 2 + i, 5, Brushes.YellowGreen, aiGgrid);
                AiWertZeichnen(i, 0, 0, 2 + i, aiGgrid);
                AiBezeichnungZeichnen(i, 0, 2, 2 + i, aiGgrid);
                _fensterFunktionen.KommentarZeichnen(4, 2 + i, i, "Ai", VisibilityProperty, aiGgrid);

            }
        }
        private void AiCreateGridWord(int anzahlZeilenConfig, AiConfig config)
        {
            throw new NotImplementedException();
        }
        private void AiCreateGridLong(int anzahlZeilenConfig, AiConfig config)
        {
            throw new NotImplementedException();
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

    }
}