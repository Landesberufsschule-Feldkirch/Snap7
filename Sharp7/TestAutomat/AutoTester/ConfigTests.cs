using System.Collections.ObjectModel;

namespace TestAutomat.AutoTester
{
    public class ConfigTests
    {
        // ReSharper disable once UnusedMember.Global
        public ObservableCollection<TestsEinstellungen> AutomatischeSoftwareTests { get; set; } = new();
    }

    public class TestsEinstellungen
    {
        public TestsEinstellungen()
        {
            LaufendeNr = 0;
            EingaengeBitmuster = "";
            EingaengeAnzahlBit = 0;
            AusgaengeBitmuster = "";
            AusgaengeBitmaske = "";
            AusgaengeAnzahlBit = 0;
            Befehl = "";
            BefehlZusatz1 = "";
            BefehlZusatz2 = "";
            Kommentar = "";
        }

        public int LaufendeNr { get; set; }
        public string EingaengeBitmuster { get; set; }
        public int EingaengeAnzahlBit { get; set; }
        public string AusgaengeBitmuster { get; set; }
        public string AusgaengeBitmaske { get; set; }
        public int AusgaengeAnzahlBit { get; set; }
        public string Befehl { get; set; }
        public string BefehlZusatz1 { get; set; }
        public string BefehlZusatz2 { get; set; }
        public string Kommentar { get; set; }
    }
}