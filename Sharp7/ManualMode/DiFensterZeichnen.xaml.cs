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