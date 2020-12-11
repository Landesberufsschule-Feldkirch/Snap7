namespace _AlleManConfigTesten.Model
{
    public class AaDaten
    {
        public string Dateiname { get; set; }
        public int LaufendeNr { get; set; }
        public int StartByte { get; set; }
        public int StartBit { get; set; }
        public int AnzahlBit { get; set; }
        public double MinimalWert { get; set; }
        public double MaximalWert { get; set; }
        public double Schrittweite { get; set; }
        public ManConfigTesten.PlcEinUndAusgaengeTypen Type { get; set; }
        public string Bezeichnung { get; set; }
        public string Kommentar { get; set; }

        public AaDaten(string datei, int laufendeNr, int startByte, int startBit, int anzahlBit, double minimalWert, double maximalWert, double schrittweite, ManConfigTesten.PlcEinUndAusgaengeTypen type, string bezeichnung, string kommentar)
        {
            Dateiname = datei;
            LaufendeNr = laufendeNr;
            StartByte = startByte;
            StartBit = startBit;
            AnzahlBit = anzahlBit;
            MinimalWert = minimalWert;
            MaximalWert = maximalWert;
            Schrittweite = schrittweite;
            Type = type;
            Bezeichnung = bezeichnung;
            Kommentar = kommentar;
        }
    }
}
