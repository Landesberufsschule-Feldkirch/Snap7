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

        internal static void PlcZeichnen(Grid grid, ConfigPlc.Plc configPlc, DependencyProperty backgroundProperty)
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


            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            for (var i = 0; i < 25; i++) grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(spaltenBreite) });

            foreach (var row in new[] { reiheObererRand, reiheKommentar, reiheBezeichnung, reiheHoehe, reiheLabel, reiheLabel, reiheHoehe, reiheHoehe, reiheHoehe, reiheLabel, reiheLabel, reiheHoehe, reiheBezeichnung, reiheKommentar })
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(row) });

            Formen.PlcRechteckZeichnen(1, 22, 3, 9, Brushes.LightGray, grid);

            Formen.PlcBorderZeichnen(3, 8, 3, 3, 3, 0, 3, 3, Brushes.White, grid);

            ///////////////////////////////////////////////////////////////////
            //  obere Hälfte zeichnen
            ///////////////////////////////////////////////////////////////////

            Formen.LabelZeichnen(3, 1, 5, 1, Brushes.White, schriftGross, "DI", 0, "-", VisibilityProperty, grid);
            Formen.LabelZeichnen(4, 1, 5, 1, Brushes.White, schriftKlein, "a", 0, "-", VisibilityProperty, grid);
            for (var i = 0; i < 8; i++)
            {
                Formen.LabelZeichnen(3 + i, 1, 4, 1, Brushes.White, schriftKlein, $".{i}", i, "Di", VisibilityProperty, grid);
                Formen.PlcButtonZeichnen(3 + i, 3, i, "FarbeDi", backgroundProperty, grid);

                var (anzeigen, bezeichnung, kommentar) = DiGetBezeichnung(configPlc, i);
                if (!anzeigen) continue;
                Formen.BezeichnungZeichnen(3 + i, 2, schriftGross, VerticalAlignment.Bottom, bezeichnung, i, "Di", VisibilityProperty, grid);
                Formen.KommentarZeichnen(3 + i, 1, schriftKlein, VerticalAlignment.Bottom, kommentar, i, "Di", VisibilityProperty, grid);
            }

            Formen.PlcBorderZeichnen(13, 8, 3, 3, 3, 0, 3, 3, Brushes.White, grid);
            Formen.LabelZeichnen(13, 1, 5, 1, Brushes.White, schriftGross, "DI", 0, "-", VisibilityProperty, grid);
            Formen.LabelZeichnen(14, 1, 5, 1, Brushes.White, schriftKlein, "b", 0, "-", VisibilityProperty, grid);
            for (var i = 0; i < 8; i++)
            {
                Formen.LabelZeichnen(13 + i, 1, 4, 1, Brushes.White, schriftKlein, $".{i}", 8 + i, "Di", VisibilityProperty, grid);
                Formen.PlcButtonZeichnen(13 + i, 3, 8 + i, "FarbeDi", backgroundProperty, grid);

                var (anzeigen, bezeichnung, kommentar) = DiGetBezeichnung(configPlc, 8 + i);
                if (!anzeigen) continue;
                Formen.BezeichnungZeichnen(13 + i, 2, schriftGross, VerticalAlignment.Bottom, bezeichnung, 8 + i, "Di", VisibilityProperty, grid);
                Formen.KommentarZeichnen(13 + i, 1, schriftKlein, VerticalAlignment.Bottom, kommentar, 8 + i, "Di", VisibilityProperty, grid);
            }

            ///////////////////////////////////////////////////////////////////
            //  untere Hälfte zeichnen
            ///////////////////////////////////////////////////////////////////

            Formen.PlcBorderZeichnen(3, 8, 9, 3, 3, 3, 3, 0, Brushes.White, grid);
            Formen.LabelZeichnen(3, 1, 9, 1, Brushes.White, schriftGross, "DQ", 0, "-", VisibilityProperty, grid);
            Formen.LabelZeichnen(4, 1, 9, 1, Brushes.White, schriftKlein, "a", 0, "-", VisibilityProperty, grid);
            for (var i = 0; i < 8; i++)
            {
                Formen.LabelZeichnen(3 + i, 1, 10, 1, Brushes.White, schriftGross, $".{i}", i, "Da", VisibilityProperty, grid);
                Formen.PlcButtonZeichnen(3 + i, 11, i, "FarbeDa", backgroundProperty, grid);

                var (anzeigen, bezeichnung, kommentar) = DaGetBezeichnung(configPlc, i);
                if (!anzeigen) continue;
                Formen.BezeichnungZeichnen(3 + i, 12, schriftGross, VerticalAlignment.Top, bezeichnung, i, "Da", VisibilityProperty, grid);
                Formen.KommentarZeichnen(3 + i, 13, schriftKlein, VerticalAlignment.Top, kommentar, i, "Da", VisibilityProperty, grid);
            }

            Formen.PlcBorderZeichnen(13, 8, 9, 3, 3, 3, 3, 0, Brushes.White, grid);
            Formen.LabelZeichnen(13, 1, 9, 1, Brushes.White, schriftGross, "DQ", 0, "-", VisibilityProperty, grid);
            Formen.LabelZeichnen(14, 1, 9, 1, Brushes.White, schriftKlein, "b", 0, "-", VisibilityProperty, grid);
            for (var i = 0; i < 8; i++)
            {
                Formen.LabelZeichnen(13 + i, 1, 10, 1, Brushes.White, schriftKlein, $".{i}", 8 + i, "Da", VisibilityProperty, grid);
                Formen.PlcButtonZeichnen(13 + i, 11, 10 + i, "FarbeDa", backgroundProperty, grid);

                var (anzeigen, bezeichnung, kommentar) = DaGetBezeichnung(configPlc, 8 + i);
                if (!anzeigen) continue;
                Formen.BezeichnungZeichnen(13 + i, 12, schriftGross, VerticalAlignment.Top, bezeichnung, 8 + i, "Da", VisibilityProperty, grid);
                Formen.KommentarZeichnen(13 + i, 13, schriftKlein, VerticalAlignment.Top, kommentar, 8 + i, "Da", VisibilityProperty, grid);
            }

            Formen.LabelZeichnen(3, 20, 6, 3, Brushes.White, schriftGanzGross, "S7-1214 DC/DC/DC", 0, "-", VisibilityProperty, grid);
        }
        private static (bool anzeigen, string bezeichnung, string kommentar) DiGetBezeichnung(ConfigPlc.Plc configPlc, int i)
        {
            return i + 1 > configPlc.GetConfigPlc.DiConfig.DigitaleEingaenge.Count ? (false, "", "") : (true, configPlc.GetConfigPlc.DiConfig.DigitaleEingaenge[i].Bezeichnung, configPlc.GetConfigPlc.DiConfig.DigitaleEingaenge[i].Kommentar);
        }
        private static (bool anzeigen, string bezeichnung, string kommentar) DaGetBezeichnung(ConfigPlc.Plc configPlc, int i)
        {
            return i + 1 > configPlc.GetConfigPlc.DaConfig.DigitaleAusgaenge.Count ? (false, "", "") : (true, configPlc.GetConfigPlc.DaConfig.DigitaleAusgaenge[i].Bezeichnung, configPlc.GetConfigPlc.DaConfig.DigitaleAusgaenge[i].Kommentar);
        }
    }
}