using System.Collections.ObjectModel;

namespace _AlleManConfigTesten.Model
{
    public class AiConfig
    {
        public ObservableCollection<AiEinstellungen> AnalogeEingaenge { get; set; } = new();
    }

    public class AiEinstellungen
    {
        public int LaufendeNr { get; set; }
        public int StartByte { get; set; }
        public int StartBit { get; set; }
        public int AnzahlBit { get; set; }
        public ManConfigTesten.PlcEinUndAusgaengeTypen Type { get; set; }
        public string Bezeichnung { get; set; }
        public string Kommentar { get; set; }

        public AiEinstellungen()
        {
            LaufendeNr = 0;
            StartByte = 0;
            StartBit = 0;
            AnzahlBit = 0;
            Type = ManConfigTesten.PlcEinUndAusgaengeTypen.Default;
            Bezeichnung = "";
            Kommentar = "";
        }
    }
}