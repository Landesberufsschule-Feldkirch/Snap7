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

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                _fensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * i, 5, Brushes.YellowGreen, aaGrid);
                _fensterFunktionen.BezeichnungZeichnen(2, 2 + 2 * i, i, "Aa", HorizontalAlignment.Center, VisibilityProperty, aaGrid);
                _fensterFunktionen.KommentarZeichnen(4, 2 + 2 * i, i, "Aa", VisibilityProperty, aaGrid);
            }
        }
        private static void AaCreateGridByte() => throw new NotImplementedException();
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

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                _fensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * i, 5, Brushes.YellowGreen, aaGrid);
                //AaBezeichnungZeichnen(i, 2, 2 + 2 * i, aaGrid);
                _fensterFunktionen.BezeichnungZeichnen(2, 2 + 2 * i, i, "Aa", HorizontalAlignment.Center, VisibilityProperty, aaGrid);
                _fensterFunktionen.KommentarZeichnen(4, 2 + 2 * i, i, "Aa", VisibilityProperty, aaGrid);
            }
        }
        private static void AaCreateGridLong() => throw new NotImplementedException();
    }
}