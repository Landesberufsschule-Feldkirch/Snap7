using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ManualMode
{
    public partial class AiFenster
    {
        private void AiCreateGridBit(int anzahlZeilenConfig)
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
                _fensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * i, 5, Brushes.YellowGreen, aiGgrid);
                AiWertZeichnen(0, 2 + 2 * i, i,  aiGgrid);
                _fensterFunktionen.BezeichnungZeichnen(2, 2 + 2 * i, i, "Ai", HorizontalAlignment.Center, VisibilityProperty, aiGgrid);
                _fensterFunktionen.KommentarZeichnen(4, 2 + 2 * i, i, "Ai", VisibilityProperty, aiGgrid);
            }
        }
        private void AiCreateGridByte(int anzahlZeilenConfig)
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
                AiWertZeichnen(0, 2 + 2 * i, i,  aiGgrid);
                _fensterFunktionen.BezeichnungZeichnen(2, 2 + 2 * i, i, "Ai", HorizontalAlignment.Center, VisibilityProperty, aiGgrid);
                _fensterFunktionen.KommentarZeichnen(4, 2 + i, i, "Ai", VisibilityProperty, aiGgrid);
            }
        }
        private static void AiCreateGridWord() => throw new NotImplementedException();
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
                Margin = new Thickness(10,0,0,0)
            };

            bezeichnung.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.ContentAi[" + par + "]"));
            bezeichnung.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityAi[" + par + "]"));

            Grid.SetColumn(bezeichnung, x);
            Grid.SetRow(bezeichnung, y);
            grid.Children.Add(bezeichnung);
        }
    }
}