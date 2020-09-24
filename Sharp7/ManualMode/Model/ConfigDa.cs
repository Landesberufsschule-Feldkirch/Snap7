using System.Collections.ObjectModel;

namespace ManualMode.Model
{
    public class ConfigDa
    {
        public ObservableCollection<DaEinstellungen> DigitaleAusaenge { get; set; } = new ObservableCollection<DaEinstellungen>();
    }

    public class DaEinstellungen
    {
        public DaEinstellungen()
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
