namespace TestAutomat.Model
{
    public class TestAusgabe
    {
        public int Nr { get; set; }
        public  string Zeit { get; set; }
        public AutoTester.TestErgebnis Ergebnis { get; set; }
        public string DigInput { get; set; }
        public string DigOutput { get; set; }
        public string Kommentar { get; set; }

        public TestAusgabe(int nr, string zeit, AutoTester.TestErgebnis ergebnis, string digInput, string digOutput, string kommentar)
        {
            Nr = nr;
            Zeit = zeit;
            Ergebnis = ergebnis;
            DigInput = digInput;
            DigOutput = digOutput;
            Kommentar = kommentar;
        }
    }
}