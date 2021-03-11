using DisplayPlc.Config;
using DisplayPlc.Zeichnen;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DisplayPlc
{
    public partial class DisplayPlc
    {
        public GetPlcConfig GetPlcConfig { get; set; }

        internal static void PlcZeichnen(Grid plcGrid, DependencyProperty backgroundProperty, ConfigPlc.Plc configPlc)
        {
            const int spaltenBreite = 30;
            const int reiheHoehe = 35;
            const int reiheObererRand = 10;
            const int reiheKommentar = 200;
            const int reiheBezeichnung = 50;
            const int reiheLabel = 25;

            const int schriftGanzGross = 35;
            const int schriftGross = 14;
            const int schriftKlein = 12;


            plcGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            for (var i = 0; i < 25; i++) plcGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenBreite) });

            foreach (var row in new[] { reiheObererRand, reiheKommentar, reiheBezeichnung, reiheHoehe, reiheLabel, reiheLabel, reiheHoehe, reiheHoehe, reiheHoehe, reiheLabel, reiheLabel, reiheHoehe, reiheBezeichnung, reiheKommentar })
                plcGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(row) });

            Formen.PlcRechteckZeichnen(1, 22, 3, 9, Brushes.LightGray, plcGrid);

            Formen.PlcBorderZeichnen(3, 8, 3, 3, 3, 0, 3, 3, Brushes.White, plcGrid);

            Formen.PlcLabelZeichnen(3, 1, 5, 1, Brushes.White, schriftGross, "DI", 0, "-", VisibilityProperty, plcGrid);
            Formen.PlcLabelZeichnen(4, 1, 5, 1, Brushes.White, schriftKlein, "a", 0, "-", VisibilityProperty, plcGrid);
            for (var i = 0; i < 8; i++)
            {
                Formen.PlcLabelZeichnen(3 + i, 1, 4, 1, Brushes.White, schriftKlein, $".{i}", i, "Di", VisibilityProperty, plcGrid);
                Formen.PlcButtonZeichnen(3 + i, 3, i, "FarbeDi", backgroundProperty, plcGrid);

                var (anzeigen, bezeichnung, kommentar) = DiGetBezeichnung(configPlc, i);
                if (!anzeigen) continue;
                Formen.PlcBezeichnungZeichnen(3 + i, 2, bezeichnung, schriftGross, VerticalAlignment.Bottom, plcGrid);
                Formen.PlcKommentarZeichnen(3 + i, 1, kommentar, schriftKlein, VerticalAlignment.Bottom, i, "Di", VisibilityProperty, plcGrid);
            }

            Formen.PlcBorderZeichnen(13, 8, 3, 3, 3, 0, 3, 3, Brushes.White, plcGrid);
            Formen.PlcLabelZeichnen(13, 1, 5, 1, Brushes.White, schriftGross, "DI", 0, "-", VisibilityProperty, plcGrid);
            Formen.PlcLabelZeichnen(14, 1, 5, 1, Brushes.White, schriftKlein, "b", 0, "-", VisibilityProperty, plcGrid);
            for (var i = 0; i < 8; i++)
            {
                Formen.PlcLabelZeichnen(13 + i, 1, 4, 1, Brushes.White, schriftKlein, $".{i}", 8 + i, "Di", VisibilityProperty, plcGrid);
                Formen.PlcButtonZeichnen(13 + i, 3, 8 + i, "FarbeDi", backgroundProperty, plcGrid);

                var (anzeigen, bezeichnung, kommentar) = DiGetBezeichnung(configPlc, 8 + i);
                if (!anzeigen) continue;
                Formen.PlcBezeichnungZeichnen(13 + i, 2, bezeichnung, schriftGross, VerticalAlignment.Bottom, plcGrid);
                Formen.PlcKommentarZeichnen(13 + i, 1, kommentar, schriftKlein, VerticalAlignment.Bottom, 8 + i, "Di", VisibilityProperty, plcGrid);
            }

            Formen.PlcBorderZeichnen(3, 8, 9, 3, 3, 3, 3, 0, Brushes.White, plcGrid);
            Formen.PlcLabelZeichnen(3, 1, 9, 1, Brushes.White, schriftGross, "DQ", 0, "-", VisibilityProperty, plcGrid);
            Formen.PlcLabelZeichnen(4, 1, 9, 1, Brushes.White, schriftKlein, "a", 0, "-", VisibilityProperty, plcGrid);
            for (var i = 0; i < 8; i++)
            {
                Formen.PlcLabelZeichnen(3 + i, 1, 10, 1, Brushes.White, schriftGross, $".{i}", i, "Da", VisibilityProperty, plcGrid);
                Formen.PlcButtonZeichnen(3 + i, 11, i, "FarbeDa", backgroundProperty, plcGrid);

                var (anzeigen, bezeichnung, kommentar) = DaGetBezeichnung(configPlc, i);
                if (!anzeigen) continue;
                Formen.PlcBezeichnungZeichnen(3 + i, 12, bezeichnung, schriftGross, VerticalAlignment.Top, plcGrid);
                Formen.PlcKommentarZeichnen(3 + i, 13, kommentar, schriftKlein, VerticalAlignment.Top, i, "Da", VisibilityProperty, plcGrid);
            }

            Formen.PlcBorderZeichnen(13, 8, 9, 3, 3, 3, 3, 0, Brushes.White, plcGrid);
            Formen.PlcLabelZeichnen(13, 1, 9, 1, Brushes.White, schriftGross, "DQ", 0, "-", VisibilityProperty, plcGrid);
            Formen.PlcLabelZeichnen(14, 1, 9, 1, Brushes.White, schriftKlein, "b", 0, "-", VisibilityProperty, plcGrid);
            for (var i = 0; i < 8; i++)
            {
                Formen.PlcLabelZeichnen(13 + i, 1, 10, 1, Brushes.White, schriftKlein, $".{i}", 8 + i, "Da", VisibilityProperty, plcGrid);
                Formen.PlcButtonZeichnen(13 + i, 11, 10 + i, "FarbeDa", backgroundProperty, plcGrid);

                var (anzeigen, bezeichnung, kommentar) = DaGetBezeichnung(configPlc, 8 + i);
                if (!anzeigen) continue;
                Formen.PlcBezeichnungZeichnen(13 + i, 12, bezeichnung, schriftGross, VerticalAlignment.Top, plcGrid);
                Formen.PlcKommentarZeichnen(13 + i, 13, kommentar, schriftKlein, VerticalAlignment.Top, 8 + i, "Da", VisibilityProperty, plcGrid);
            }

            Formen.PlcLabelZeichnen(3, 20, 6, 3, Brushes.White, schriftGanzGross, "S7-1214 DC/DC/DC", 0, "-", VisibilityProperty, plcGrid);
        }

   

        private static (bool anzeigen, string bezeichnung, string kommentar) DiGetBezeichnung(ConfigPlc.Plc configPlc, int i)
        {
            return i + 1 > configPlc.GetConfig.DiConfig.DigitaleEingaenge.Count ? (false, "", "") : (true, configPlc.GetConfig.DiConfig.DigitaleEingaenge[i].Bezeichnung, configPlc.GetConfig.DiConfig.DigitaleEingaenge[i].Kommentar);
        }
        private static (bool anzeigen, string bezeichnung, string kommentar) DaGetBezeichnung(ConfigPlc.Plc configPlc, int i)
        {
            return i + 1 > configPlc.GetConfig.DaConfig.DigitaleAusgaenge.Count ? (false, "", "") : (true, configPlc.GetConfig.DaConfig.DigitaleAusgaenge[i].Bezeichnung, configPlc.GetConfig.DaConfig.DigitaleAusgaenge[i].Kommentar);
        }


    }
}