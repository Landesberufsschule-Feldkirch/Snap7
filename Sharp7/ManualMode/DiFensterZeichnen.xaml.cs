using ManualMode.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

            _fensterFunktionen.HintergrundRechteckZeichnen(0, 0, 7, Brushes.Yellow, diGrid);
            _fensterFunktionen.TextZeichnen("Wert", HorizontalAlignment.Center, 0, 0, diGrid);
            _fensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Center, 2, 0, diGrid);
            _fensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 4, 0, diGrid);

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {

                _fensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * i, 5, Brushes.YellowGreen, diGrid);
                DiButtonZeichnen(config.DigitaleEingaenge[i].StartByte, config.DigitaleEingaenge[i].StartBit, 0, 2 + config.DigitaleEingaenge[i].StartByte * 16 + 2 * config.DigitaleEingaenge[i].StartBit, diGrid);
                _fensterFunktionen.BezeichnungZeichnen(2, 2 + 2 * i, i, "Di",HorizontalAlignment.Center, VisibilityProperty, diGrid);
                _fensterFunktionen.KommentarZeichnen(4, 2 + 2 * i, i, "Di", VisibilityProperty, diGrid);

            }
        }
        private static void DiCreateGridByte() => throw new NotImplementedException();
        private static void DiCreateGridWord() => throw new NotImplementedException();
        private static void DiCreateGridLong() => throw new NotImplementedException();
        private static void DiButtonZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
        {
            var parameterNummer = 8 * vbyte + vbit;

            var buttonTasten = new Button
            {
                Content = vbyte + "." + vbit,
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(3, 3, 3, 3)
            };

            buttonTasten.SetBinding(BackgroundProperty, new Binding("ManVisuAnzeigen.FarbeDi[" + parameterNummer + "]"));
            buttonTasten.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDi[" + parameterNummer + "]"));

            Grid.SetColumn(buttonTasten, x);
            Grid.SetRow(buttonTasten, y);

            panel.Children.Add(buttonTasten);
        }
    }
}