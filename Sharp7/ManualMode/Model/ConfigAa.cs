using System.Collections.ObjectModel;

namespace ManualMode.Model
{
    public class ConfigAa
    {
        public ObservableCollection<AaEinstellungen> AnalogeAusgaenge { get; set; } = new ObservableCollection<AaEinstellungen>();
    }

    public class AaEinstellungen
    {
        public AaEinstellungen()
        {
            StartByte = 0;
            StartBit = 0;
            AnzahlBit = 0;
            Bezeichnung = "";
            Kommentar = "";
        }

        public int StartByte { get; set; }
        public int StartBit { get; set; }
        public int AnzahlBit { get; set; }
        public string Bezeichnung { get; set; }
        public string Kommentar { get; set; }

    }
}
