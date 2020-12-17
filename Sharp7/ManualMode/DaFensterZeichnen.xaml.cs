using ManualMode.Model;
using ManualMode.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManualMode
{
    public partial class DaFenster
    {
        private void DaCreateGridBit(int anzahlZeilenConfig, DaConfig config, ManualViewModel manualViewModel)
        {
            const int spaltenAbstand = 10;
            const int spaltenWert = 80;
            const int spaltenBezeichnung = 120;
            const int spaltenKommentar = 300;

            const int zeilenAbstand = 10;
            const int zeilenHoehe = 45;

            var daGrid = new Grid { Name = "DaGrid" };
            Content = daGrid;

            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenWert) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenWert) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenBezeichnung) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                daGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenHoehe) });
                daGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenAbstand) });
            }

            FensterFunktionen.HintergrundRechteckZeichnen(0, 0, 7, Brushes.Yellow, daGrid);

            FensterFunktionen.TextZeichnen("Tasten", HorizontalAlignment.Center, 0, 0, daGrid);
            FensterFunktionen.TextZeichnen("Toggeln", HorizontalAlignment.Center, 2, 0, daGrid);
            FensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Center, 4, 0, daGrid);
            FensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 6, 0, daGrid);

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                FensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * i, 7, Brushes.YellowGreen, daGrid);
                FensterFunktionen.ButtonTastenZeichnen(0, 2 + 2 * i, config.DigitaleAusgaenge[i].StartByte, config.DigitaleAusgaenge[i].StartBit, i, "Da", BackgroundProperty, VisibilityProperty, daGrid, manualViewModel);
                FensterFunktionen.ButtonToggelnZeichnen(2, 2 + 2 * i, config.DigitaleAusgaenge[i].StartByte, config.DigitaleAusgaenge[i].StartBit, i, "Da", BackgroundProperty, VisibilityProperty, daGrid, manualViewModel);
                FensterFunktionen.BezeichnungZeichnen(4, 2 + 2 * i, i, "Da", HorizontalAlignment.Center, VisibilityProperty, daGrid);
                FensterFunktionen.KommentarZeichnen(6, 2 + 2 * i, i, "Da", VisibilityProperty, daGrid);
            }
        }
        private void DaCreateGridByte(int anzahlZeilenConfig, ManualViewModel manualViewModel)
        {
            const int spaltenAbstand = 10;
            const int spaltenWert = 80;
            const int spaltenBezeichnung = 120;
            const int spaltenKommentar = 300;

            const int zeilenAbstand = 10;
            const int zeilenHoehe = 45;
            
            var daGrid = new Grid { Name = "DaGrid" };
            Content = daGrid;

            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenWert) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenWert) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenBezeichnung) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                daGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenHoehe) });
                daGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenAbstand) });
            }

            FensterFunktionen.HintergrundRechteckZeichnen(0, 0, 7, Brushes.Yellow, daGrid);

            FensterFunktionen.TextZeichnen("Eingabe", HorizontalAlignment.Center, 0, 0, daGrid);
            FensterFunktionen.TextZeichnen("Aktuell", HorizontalAlignment.Center, 2, 0, daGrid);
            FensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Left, 4, 0, daGrid);
            FensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 6, 0, daGrid);

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                FensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * i, 7, Brushes.YellowGreen, daGrid);
                FensterFunktionen.TextboxByteZeichnen(0, 2 + 2 * i, i, "Da", VisibilityProperty, manualViewModel.ManualMode, daGrid);
                FensterFunktionen.BezeichnungZeichnen(2, 2 + 2 * i, i, "Da", HorizontalAlignment.Center, VisibilityProperty, daGrid);
                FensterFunktionen.KommentarZeichnen(6, 2 + 2 * i, i, "Da", VisibilityProperty, daGrid);
            }
        }
        private static void DaCreateGridWord() => throw new NotImplementedException();
        private static void DaCreateGridLong() => throw new NotImplementedException();
    }
}