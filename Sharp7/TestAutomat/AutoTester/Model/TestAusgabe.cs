using PlcDatenTypen;
using TestAutomat.AutoTester.Config;

namespace TestAutomat.AutoTester.Model
{
    public class TestAusgabe
    {
        public int Nr { get; set; }
        public  string Zeit { get; set; }
        public TestErgebnis Ergebnis { get; set; }
        public TestBefehle Befehle { get; set; }
        public string DigInput { get; set; }
        public string DigOutput { get; set; }
        public string Kommentar { get; set; }

        public TestAusgabe(int nr, string zeit, TestErgebnis ergebnis, TestBefehle befehle, PlcUint digInput, PlcUint digOutput, string kommentar)
        {
            Nr = nr;
            Zeit = zeit;
            Ergebnis = ergebnis;
            Befehle = befehle;
            DigInput = digInput.GetBin16Bit();
            DigOutput = digOutput.GetBin16Bit();
            Kommentar = kommentar;
        }
    }
}