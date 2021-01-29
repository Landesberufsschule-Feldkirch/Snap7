using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestAutomat.PlcDisplay.Zeichnen;

namespace TestAutomat.PlcDisplay
{
    public partial class PlcWindow
    {
        internal static void PlcZeichnen(Grid plcGrid, DependencyProperty backgroundProperty, ManualMode.ManualMode manualMode, AutoTesterWindow autoTester)
        {
            const int spaltenBreite = 30;
            const int reiheHoehe = 35;
            const int reiheObererRand = 10;
            const int reiheKommentar = 200;
            const int reiheBezeichnung = 50;
            const int reiheLabel = 25;

            const int schriftGross = 14;
            const int schriftKlein = 12;

            plcGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            for (var i = 0; i < 40; i++) plcGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenBreite) });

            foreach (var row in new[] { reiheObererRand, reiheKommentar, reiheBezeichnung, reiheHoehe, reiheLabel, reiheLabel, reiheHoehe, reiheHoehe, reiheHoehe, reiheLabel, reiheLabel, reiheHoehe, reiheBezeichnung, reiheKommentar })
                plcGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(row) });

            Formen.PlcRechteckZeichnen(1, 22, 3, 9, Brushes.LightGray, plcGrid);

            Formen.PlcBorderZeichnen(3, 8, 3, 3, 3, 0, 3, 3, Brushes.White, plcGrid);
            Formen.PlcLabelZeichnen(3, 5, Brushes.White, schriftGross, "DI", plcGrid);
            Formen.PlcLabelZeichnen(4, 5, Brushes.White, schriftKlein, "a", plcGrid);
            for (var i = 0; i < 8; i++)
            {
                Formen.PlcLabelZeichnen(3 + i, 4, Brushes.White, schriftKlein, $".{i}", plcGrid);
                Formen.PlcButtonZeichnen(3 + i, 3, i, "FarbeDi", backgroundProperty, plcGrid);

                var (anzeigen, bezeichnung, kommentar) = DiGetBezeichnung(manualMode, autoTester, i);
                if (!anzeigen) continue;
                Formen.PlcBezeichnungZeichnen(3 + i, 2, bezeichnung, schriftGross, VerticalAlignment.Bottom, plcGrid);
                Formen.PlcKommentarZeichnen(3 + i, 1, kommentar,schriftKlein, VerticalAlignment.Bottom, plcGrid);
            }

            Formen.PlcBorderZeichnen(13, 8, 3, 3, 3, 0, 3, 3, Brushes.White, plcGrid);
            Formen.PlcLabelZeichnen(13, 5, Brushes.White, schriftGross, "DI", plcGrid);
            Formen.PlcLabelZeichnen(14, 5, Brushes.White, schriftKlein, "b", plcGrid);
            for (var i = 0; i < 8; i++)
            {
                Formen.PlcLabelZeichnen(13 + i, 4, Brushes.White, schriftKlein, $".{i}", plcGrid);
                Formen.PlcButtonZeichnen(13 + i, 3, 10 + i, "FarbeDi", backgroundProperty, plcGrid);
                var (anzeigen, bezeichnung, kommentar) = DiGetBezeichnung(manualMode, autoTester, 8 + i);
                if (!anzeigen) continue;
                Formen.PlcBezeichnungZeichnen(13 + i, 2, bezeichnung,schriftGross, VerticalAlignment.Bottom, plcGrid);
                Formen.PlcKommentarZeichnen(13 + i, 1, kommentar, schriftKlein,VerticalAlignment.Bottom, plcGrid);
            }

            Formen.PlcBorderZeichnen(3, 8, 9, 3, 3, 3, 3, 0, Brushes.White, plcGrid);
            Formen.PlcLabelZeichnen(3, 9, Brushes.White, schriftGross, "DQ", plcGrid);
            Formen.PlcLabelZeichnen(4, 9, Brushes.White, schriftKlein, "a", plcGrid);
            for (var i = 0; i < 8; i++)
            {
                Formen.PlcLabelZeichnen(3 + i, 10, Brushes.White, schriftGross, $".{i}", plcGrid);
                Formen.PlcButtonZeichnen(3 + i, 11, i, "FarbeDa", backgroundProperty, plcGrid);

                var (anzeigen, bezeichnung, kommentar) = DaGetBezeichnung(manualMode, autoTester, i);
                if (!anzeigen) continue;
                Formen.PlcBezeichnungZeichnen(3 + i, 12, bezeichnung,schriftGross, VerticalAlignment.Top, plcGrid);
                Formen.PlcKommentarZeichnen(3 + i, 13, kommentar,schriftKlein, VerticalAlignment.Top, plcGrid);
            }

            Formen.PlcBorderZeichnen(13, 8, 9, 3, 3, 3, 3, 0, Brushes.White, plcGrid);
            Formen.PlcLabelZeichnen(13, 9, Brushes.White, schriftGross, "DQ", plcGrid);
            Formen.PlcLabelZeichnen(14, 9, Brushes.White, schriftKlein, "b", plcGrid);
            for (var i = 0; i < 8; i++)
            {
                Formen.PlcLabelZeichnen(13 + i, 10, Brushes.White, schriftKlein, $".{i}", plcGrid);
                Formen.PlcButtonZeichnen(13 + i, 11, 10 + i, "FarbeDa", backgroundProperty, plcGrid);

                var (anzeigen, bezeichnung, kommentar) = DaGetBezeichnung(manualMode, autoTester, 8 + i);
                if (!anzeigen) continue;
                Formen.PlcBezeichnungZeichnen(13 + i, 12, bezeichnung,schriftGross, VerticalAlignment.Top, plcGrid);
                Formen.PlcKommentarZeichnen(13 + i, 13, kommentar,schriftKlein, VerticalAlignment.Top, plcGrid);
            }
        }

        private static (bool anzeigen, string bezeichnung, string kommentar) DiGetBezeichnung(ManualMode.ManualMode manualMode, AutoTesterWindow autoTester, int i)
        {
            if (!autoTester.MyAutoTester.GetPlcConfig.PlcBelegung.GetEingangAktiv(i)) return (false, "", "");
            return i + 1 > manualMode.GetConfig.DiConfig.DigitaleEingaenge.Count ? (false, "", "") : (true, manualMode.GetConfig.DiConfig.DigitaleEingaenge[i].Bezeichnung, manualMode.GetConfig.DiConfig.DigitaleEingaenge[i].Kommentar);
        }

        private static (bool anzeigen, string bezeichnung, string kommentar) DaGetBezeichnung(ManualMode.ManualMode manualMode, AutoTesterWindow autoTester, int i)
        {
            if (!autoTester.MyAutoTester.GetPlcConfig.PlcBelegung.GetAusgangAktiv(i)) return (false, "", "");
            return i + 1 > manualMode.GetConfig.DaConfig.DigitaleAusgaenge.Count ? (false, "", "") : (true, manualMode.GetConfig.DaConfig.DigitaleAusgaenge[i].Bezeichnung, manualMode.GetConfig.DaConfig.DigitaleAusgaenge[i].Kommentar);
        }
    }
}