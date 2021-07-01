namespace _AlleManConfigTesten.Model
{
    public class AiDaten
    {
        public string Dateiname { get; set; }
        public int LaufendeNr { get; set; }
        public int StartByte { get; set; }
        public int StartBit { get; set; }
        public int AnzahlBit { get; set; }
        public ManConfigTesten.PlcEinUndAusgaengeTypen Type { get; set; }
        public string Bezeichnung { get; set; }
        public string Kommentar { get; set; }

        public AiDaten(string datei, int laufendeNr, int startByte, int startBit, int anzahlBit, ManConfigTesten.PlcEinUndAusgaengeTypen type, string bezeichnung, string kommentar)
        {
            Dateiname = datei;
            LaufendeNr = laufendeNr;
            StartByte = startByte;
            StartBit = startBit;
            AnzahlBit = anzahlBit;
            Type = type;
            Bezeichnung = bezeichnung;
            Kommentar = kommentar;
        }
    }
}