namespace TestAutomat.AutoTester.Model
{
    public class TestAusgabe
    {
        public int Nr { get; set; }
        public  string Zeit { get; set; }
        public AutoTester.TestErgebnis Ergebnis { get; set; }
        public string Befehle { get; set; }
        public string DigInput { get; set; }
        public string DigOutput { get; set; }
        public string Kommentar { get; set; }

        public TestAusgabe(int nr, string zeit, AutoTester.TestErgebnis ergebnis, string befehle, string digInput, string digOutput, string kommentar)
        {
            Nr = nr;
            Zeit = zeit;
            Ergebnis = ergebnis;
            Befehle = befehle;
            DigInput = digInput;
            DigOutput = digOutput;
            Kommentar = kommentar;
        }
    }
}