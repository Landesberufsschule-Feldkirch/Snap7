using ManualMode.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManualMode
{
    public partial class AaFenster
    {
        private void AaCreateGridBit(int anzahlZeilenConfig)
        {
            const int spaltenAbstand = 10;
            const int spaltenWert = 300;
            const int spaltenBezeichnung = 120;
            const int spaltenKommentar = 500;

            const int zeilenAbstand = 10;
            const int zeilenHoehe = 45;

            var aaGrid = new Grid { Name = "AaGrid" };
            Content = aaGrid;

            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenWert) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenBezeichnung) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                aaGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenHoehe) });
                aaGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenAbstand) });
            }

            FensterFunktionen.HintergrundRechteckZeichnen(0, 0, 5, Brushes.Yellow, aaGrid);
            FensterFunktionen.TextZeichnen("Wert", HorizontalAlignment.Left, 0, 0, aaGrid);
            FensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Left, 2, 0, aaGrid);
            FensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, aaGrid);

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                FensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * i, 5, Brushes.YellowGreen, aaGrid);
                FensterFunktionen.BezeichnungZeichnen(2, 2 + 2 * i, i, "Aa", HorizontalAlignment.Center, VisibilityProperty, aaGrid);
                FensterFunktionen.KommentarZeichnen(4, 2 + 2 * i, i, "Aa", VisibilityProperty, aaGrid);
            }
        }
        private static void AaCreateGridByte() => throw new NotImplementedException();
        private void AaCreateGridWord(in int anzahlZeilenConfig, ManualMode manualMode)
        {
            const int spaltenAbstand = 10;
            const int spaltenWert = 300;
            const int spaltenBezeichnung = 120;
            const int spaltenKommentar = 500;

            const int zeilenAbstand = 10;
            const int zeilenHoehe = 45;

            var configAaConfig = manualMode.GetConfig.AaConfig;
            var aaGrid = new Grid { Name = "AaGrid" };
            Content = aaGrid;

            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenWert) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenBezeichnung) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            aaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                aaGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenHoehe) });
                aaGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenAbstand) });
            }

            FensterFunktionen.HintergrundRechteckZeichnen(0, 0, 5, Brushes.Yellow, aaGrid);
            FensterFunktionen.TextZeichnen("Wert", HorizontalAlignment.Left, 0, 0, aaGrid);
            FensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Left, 2, 0, aaGrid);
            FensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, aaGrid);

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                FensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * i, 5, Brushes.YellowGreen, aaGrid);

                switch (configAaConfig.AnalogeAusgaenge[i].Type)
                {
                    case PlcEinUndAusgaengeTypen.Ascii: break;
                    case PlcEinUndAusgaengeTypen.Default: break;
                    case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent: break;
                    case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille: break;
                    case PlcEinUndAusgaengeTypen.BitmusterByte: break;
                    case PlcEinUndAusgaengeTypen.SiemensAnalogwertSchieberegler:
                        FensterFunktionen.SliderZeichnen(0, 2 + 2 * i, i, "Aa", configAaConfig.AnalogeAusgaenge[i].MinimalWert, configAaConfig.AnalogeAusgaenge[i].MaximalWert, configAaConfig.AnalogeAusgaenge[i].Schrittweite, manualMode, aaGrid);
                        FensterFunktionen.SliderWertZeichnen(0, 2 + 2 * i, i, "Aa", VisibilityProperty, manualMode, aaGrid);
                        break;
                    default: throw new ArgumentOutOfRangeException();
                }

                FensterFunktionen.BezeichnungZeichnen(2, 2 + 2 * i, i, "Aa", HorizontalAlignment.Center, VisibilityProperty, aaGrid);
                FensterFunktionen.KommentarZeichnen(4, 2 + 2 * i, i, "Aa", VisibilityProperty, aaGrid);
            }
        }
        private static void AaCreateGridLong() => throw new NotImplementedException();
    }
}