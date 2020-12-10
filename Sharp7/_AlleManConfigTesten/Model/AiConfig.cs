using System.Collections.ObjectModel;

namespace _AlleManConfigTesten.Model
{
    public class AiConfig
    {
        public ObservableCollection<AiEinstellungen> AnalogeEingaenge { get; set; } = new ObservableCollection<AiEinstellungen>();
    }

    public class AiEinstellungen
    {
        public AiEinstellungen()
        {
            LaufendeNr = 0;
            StartByte = 0;
            StartBit = 0;
            AnzahlBit = 0;
            Type = AlleManConfigTesten.PlcEinUndAusgaengeTypen.Default;
            Bezeichnung = "";
            Kommentar = "";
        }

        public int LaufendeNr { get; set; }
        public int StartByte { get; set; }
        public int StartBit { get; set; }
        public int AnzahlBit { get; set; }
        public AlleManConfigTesten.PlcEinUndAusgaengeTypen Type { get; set; }
        public string Bezeichnung { get; set; }
        public string Kommentar { get; set; }
    }
}