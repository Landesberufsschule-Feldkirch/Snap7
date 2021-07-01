using System.Collections.ObjectModel;

namespace _AlleManConfigTesten.Model
{
    public class AaConfig
    {
        public ObservableCollection<AaEinstellungen> AnalogeAusgaenge { get; set; } = new();
    }

    public class AaEinstellungen
    {
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

        public AaEinstellungen()
        {
            LaufendeNr = 0;
            StartByte = 0;
            StartBit = 0;
            AnzahlBit = 0;
            MinimalWert = 0;
            MaximalWert = 0;
            Schrittweite = 0;
            Type = ManConfigTesten.PlcEinUndAusgaengeTypen.Default;
            Bezeichnung = "";
            Kommentar = "";
        }
    }
}
