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