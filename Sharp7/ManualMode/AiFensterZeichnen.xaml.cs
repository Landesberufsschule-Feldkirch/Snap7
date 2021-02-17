using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using static System.Windows.Controls.Grid;

namespace ManualMode
{
    public partial class AiFenster
    {
        private void AiCreateGridBit(int anzahlZeilenConfig)
        {
            const int spaltenAbstand = 10;
            const int spaltenWert = 300;
            const int spaltenBezeichnung = 120;
            const int spaltenKommentar = 500;

            const int zeilenAbstand = 10;
            const int zeilenHoehe = 45;

            var aiGgrid = new Grid { Name = "AiGrid" };
            Content = aiGgrid;

            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenWert) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenBezeichnung) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                aiGgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenHoehe) });
                aiGgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenAbstand) });
            }

            FensterFunktionen.HintergrundRechteckZeichnen(0, 0, 5, Brushes.Yellow, aiGgrid);
            FensterFunktionen.TextZeichnen("Wert", HorizontalAlignment.Left, 0, 0, aiGgrid);
            FensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Left, 2, 0, aiGgrid);
            FensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, aiGgrid);

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                FensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * i, 5, Brushes.YellowGreen, aiGgrid);
                AiWertZeichnen(0, 2 + 2 * i, i, aiGgrid);
                FensterFunktionen.BezeichnungZeichnen(2, 2 + 2 * i, i, "Ai", HorizontalAlignment.Center, VisibilityProperty, aiGgrid);
                FensterFunktionen.KommentarZeichnen(4, 2 + 2 * i, i, "Ai", VisibilityProperty, aiGgrid);
            }
        }
        private void AiCreateGridByte(int anzahlZeilenConfig)
        {
            const int spaltenAbstand = 10;
            const int spaltenWert = 300;
            const int spaltenBezeichnung = 120;
            const int spaltenKommentar = 500;

            const int zeilenAbstand = 10;
            const int zeilenHoehe = 45;

            var aiGgrid = new Grid { Name = "AiGrid" };
            Content = aiGgrid;

            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenWert) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenBezeichnung) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                aiGgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenHoehe) });
                aiGgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenAbstand) });
            }

            FensterFunktionen.HintergrundRechteckZeichnen(0, 0, 5, Brushes.Yellow, aiGgrid);
            FensterFunktionen.TextZeichnen("Wert", HorizontalAlignment.Left, 0, 0, aiGgrid);
            FensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Left, 2, 0, aiGgrid);
            FensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, aiGgrid);

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                FensterFunktionen.HintergrundRechteckZeichnen(0, 2 + i, 5, Brushes.YellowGreen, aiGgrid);
                AiWertZeichnen(0, 2 + 2 * i, i, aiGgrid);
                FensterFunktionen.BezeichnungZeichnen(2, 2 + 2 * i, i, "Ai", HorizontalAlignment.Center, VisibilityProperty, aiGgrid);
                FensterFunktionen.KommentarZeichnen(4, 2 + i, i, "Ai", VisibilityProperty, aiGgrid);
            }
        }
        private void AiCreateGridWord(int anzahlZeilenConfig)
        {
            const int spaltenAbstand = 10;
            const int spaltenWert = 300;
            const int spaltenBezeichnung = 120;
            const int spaltenKommentar = 500;

            const int zeilenAbstand = 10;
            const int zeilenHoehe = 45;

            var aiGgrid = new Grid { Name = "AiGrid" };
            Content = aiGgrid;

            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenWert) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenBezeichnung) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenAbstand) });
            aiGgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                aiGgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenHoehe) });
                aiGgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(zeilenAbstand) });
            }

            FensterFunktionen.HintergrundRechteckZeichnen(0, 0, 5, Brushes.Yellow, aiGgrid);
            FensterFunktionen.TextZeichnen("Wert", HorizontalAlignment.Left, 0, 0, aiGgrid);
            FensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Left, 2, 0, aiGgrid);
            FensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, aiGgrid);

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                FensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2*i, 5, Brushes.YellowGreen, aiGgrid);
                AiWertZeichnen(0, 2 + 2 * i, i, aiGgrid);
                FensterFunktionen.BezeichnungZeichnen(2, 2 + 2 * i, i, "Ai", HorizontalAlignment.Center, VisibilityProperty, aiGgrid);
                FensterFunktionen.KommentarZeichnen(4, 2 + 2 * i, i, "Ai", VisibilityProperty, aiGgrid);
            }
        }
        private static void AiCreateGridLong() => throw new NotImplementedException();
        private static void AiWertZeichnen(int x, int y, int par, Panel grid)
        {
            var bezeichnung = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(10, 0, 0, 0)
            };

            bezeichnung.SetBinding(TextBlock.TextProperty, new Binding($"ManVisuAnzeigen.ContentAi[{par}]"));
            bezeichnung.SetBinding(VisibilityProperty, new Binding($"ManVisuAnzeigen.VisibilityAi[{par}]"));

            SetColumn(bezeichnung, x);
            SetRow(bezeichnung, y);
            grid.Children.Add(bezeichnung);
        }
    }
}