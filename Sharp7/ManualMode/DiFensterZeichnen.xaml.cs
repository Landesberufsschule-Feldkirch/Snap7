using ManualMode.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManualMode
{
    public partial class DiFenster
    {
        private void DiCreateGridBit(int anzahlZeilenConfig, DiConfig config)
        {
            const int spaltenAbstand = 10;
            const int spaltenWert = 80;
            const int spaltenBezeichnung = 120;
            const int spaltenKommentar = 300;

            const int zeilenAbstand = 10;
            const int zeilenHoehe = 45;

            var diGrid = new Grid { Name = "DiGrid" };
            Content = diGrid;

            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenWert) });
            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenBezeichnung) });
            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            diGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                diGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenHoehe) });
                diGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenAbstand) });
            }

            FensterFunktionen.HintergrundRechteckZeichnen(0, 0, 7, Brushes.Yellow, diGrid);
            FensterFunktionen.TextZeichnen("Wert", HorizontalAlignment.Center, 0, 0, diGrid);
            FensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Center, 2, 0, diGrid);
            FensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, diGrid);

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                FensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * i, 5, Brushes.YellowGreen, diGrid);
                FensterFunktionen.ButtonZeichnen(0, 2 + 2 * i, config.DigitaleEingaenge[i].StartByte, config.DigitaleEingaenge[i].StartBit, i, "Di", BackgroundProperty, VisibilityProperty, diGrid);
                FensterFunktionen.BezeichnungZeichnen(2, 2 + 2 * i, i, "Di", HorizontalAlignment.Center, VisibilityProperty, diGrid);
                FensterFunktionen.KommentarZeichnen(4, 2 + 2 * i, i, "Di", VisibilityProperty, diGrid);
            }
        }
        private static void DiCreateGridByte() => throw new NotImplementedException();
        private static void DiCreateGridWord() => throw new NotImplementedException();
        private static void DiCreateGridLong() => throw new NotImplementedException();
    }
}