using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestAutomat.PlcDisplay.Zeichnen;

namespace TestAutomat.PlcDisplay
{
    public partial class PlcWindow
    {
        internal void PlcZeichnen(Grid plcGrid, DependencyProperty backgroundProperty  ,ManualMode.ManualMode manualMode, AutoTesterWindow autoTester)
        {
            plcGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            for (var i = 0; i < 40; i++) plcGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(45) });

            foreach (var row in new[] { 10, 200, 70, 45, 45, 45, 45, 45, 45, 45, 45, 45, 70, 200 })
                plcGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(row) });

            Formen.PlcRechteckZeichnen(1, 30, 3, 9, Brushes.LightGray, plcGrid);

            Formen.PlcBorderZeichnen(3, 8, 3, 3, 3, 0, 3, 3, Brushes.White, plcGrid);
            Formen.PlcLabelZeichnen(3, 5, Brushes.White, 18, "DI", plcGrid);
            Formen.PlcLabelZeichnen(4, 5, Brushes.White, 16, "a", plcGrid);
            for (var i = 0; i < 8; i++)
            {
                Formen.PlcLabelZeichnen(3 + i, 4, Brushes.White, 16, $".{i}", plcGrid);
                Formen.PlcButtonZeichnen(3 + i, 3, i, "FarbeDi", backgroundProperty, plcGrid);

                var (anzeigen, bezeichnung, kommentar) = DiGetBezeichnung(manualMode, autoTester, i);
                if (!anzeigen) continue;
                Formen.PlcBezeichnungZeichnen(3 + i, 2, bezeichnung, VerticalAlignment.Bottom, plcGrid);
                Formen.PlcKommentarZeichnen(3 + i, 1, kommentar, VerticalAlignment.Bottom, plcGrid);
            }

            Formen.PlcBorderZeichnen(13, 8, 3, 3, 3, 0, 3, 3, Brushes.White, plcGrid);
            Formen.PlcLabelZeichnen(13, 5, Brushes.White, 18, "DI", plcGrid);
            Formen.PlcLabelZeichnen(14, 5, Brushes.White, 16, "b", plcGrid);
            for (var i = 0; i < 8; i++)
            {

                Formen.PlcLabelZeichnen(13 + i, 4, Brushes.White, 16, $".{i}", plcGrid);
                Formen.PlcButtonZeichnen(13 + i, 3, 10 + i, "FarbeDi", backgroundProperty, plcGrid);
                var (anzeigen, bezeichnung, kommentar) = DiGetBezeichnung(manualMode, autoTester, 8 + i);
                if (!anzeigen) continue;
                Formen.PlcBezeichnungZeichnen(13 + i, 2, bezeichnung, VerticalAlignment.Bottom, plcGrid);
                Formen.PlcKommentarZeichnen(13 + i, 1, kommentar, VerticalAlignment.Bottom, plcGrid);
            }

            Formen.PlcBorderZeichnen(3, 8, 9, 3, 3, 3, 3, 0, Brushes.White, plcGrid);
            Formen.PlcLabelZeichnen(3, 9, Brushes.White, 18, "DQ", plcGrid);
            Formen.PlcLabelZeichnen(4, 9, Brushes.White, 16, "a", plcGrid);
            for (var i = 0; i < 8; i++)
            {
                Formen.PlcLabelZeichnen(3 + i, 10, Brushes.White, 16, $".{i}", plcGrid);
                Formen.PlcButtonZeichnen(3 + i, 11, i, "FarbeDa", backgroundProperty, plcGrid);

                var (anzeigen, bezeichnung, kommentar) = DaGetBezeichnung(manualMode, autoTester, i);
                if (!anzeigen) continue;
                Formen.PlcBezeichnungZeichnen(3 + i, 12, bezeichnung, VerticalAlignment.Top, plcGrid);
                Formen.PlcKommentarZeichnen(3 + i, 13, kommentar, VerticalAlignment.Top, plcGrid);
            }

            Formen.PlcBorderZeichnen(13, 8, 9, 3, 3, 3, 3, 0, Brushes.White, plcGrid);
            Formen.PlcLabelZeichnen(13, 9, Brushes.White, 18, "DQ", plcGrid);
            Formen.PlcLabelZeichnen(14, 9, Brushes.White, 16, "b", plcGrid);
            for (var i = 0; i < 8; i++)
            {
                Formen.PlcLabelZeichnen(13 + i, 10, Brushes.White, 16, $".{i}", plcGrid);
                Formen.PlcButtonZeichnen(13 + i, 11, 10 + i, "FarbeDa", backgroundProperty, plcGrid);

                var (anzeigen, bezeichnung, kommentar) = DaGetBezeichnung(manualMode, autoTester, 8 + i);
                if (!anzeigen) continue;
                Formen.PlcBezeichnungZeichnen(13 + i, 12, bezeichnung, VerticalAlignment.Top, plcGrid);
                Formen.PlcKommentarZeichnen(13 + i, 13, kommentar, VerticalAlignment.Top, plcGrid);
            }
        }

        private static (bool anzeigen, string bezeichnung, string kommentar) DiGetBezeichnung(ManualMode.ManualMode manualMode, AutoTesterWindow autoTester, int i)
        {
            if (!autoTester.AutoTester.GetPlcConfig.PlcBelegung.GetEingangAktiv(i)) return (false, "", "");
            return i + 1 > manualMode.GetConfig.DiConfig.DigitaleEingaenge.Count ? (false, "", "") : (true, manualMode.GetConfig.DiConfig.DigitaleEingaenge[i].Bezeichnung, manualMode.GetConfig.DiConfig.DigitaleEingaenge[i].Kommentar);
        }

        private static (bool anzeigen, string bezeichnung, string kommentar) DaGetBezeichnung(ManualMode.ManualMode manualMode, AutoTesterWindow autoTester, int i)
        {
            if (!autoTester.AutoTester.GetPlcConfig.PlcBelegung.GetAusgangAktiv(i)) return (false, "", "");
            return i + 1 > manualMode.GetConfig.DaConfig.DigitaleAusgaenge.Count ? (false, "", "") : (true, manualMode.GetConfig.DaConfig.DigitaleAusgaenge[i].Bezeichnung, manualMode.GetConfig.DaConfig.DigitaleAusgaenge[i].Kommentar);
        }

    }
}
