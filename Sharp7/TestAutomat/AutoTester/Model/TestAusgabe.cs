using PlcDatenTypen;
using TestAutomat.AutoTester.Config;

namespace TestAutomat.AutoTester.Model
{
    public class TestAusgabe
    {
        public int Nr { get; set; }
        public TestBefehle Befehle { get; set; }
        public string DigInput { get; set; }
        public string DigOutput { get; set; }
        public string Kommentar { get; set; }

        public TestAusgabe(int nr, TestBefehle befehle, PlcUint digInput, PlcUint digOutput, string kommentar)
        {
            Nr = nr;
            Befehle = befehle;
            DigInput = digInput.GetBin16Bit();
            DigOutput = digOutput.GetBin16Bit();
            Kommentar = kommentar;
        }
    }
}
