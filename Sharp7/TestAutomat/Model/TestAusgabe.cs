namespace TestAutomat.Model
{
    public class TestAusgabe
    {
        public int Nr { get; set; }
        public  string Zeit { get; set; }
        public AutoTester.TestErgebnis Ergebnis { get; set; }
        public string DigInput { get; set; }
        public string DigOutputSoll { get; set; }
        public string DigOutputIst { get; set; }
        public string Kommentar { get; set; }

        public TestAusgabe(int nr, string zeit, AutoTester.TestErgebnis ergebnis, string digInput, string digOutputSoll, string digOutputIst, string kommentar)
        {
            Nr = nr;
            Zeit = zeit;
            Ergebnis = ergebnis;
            DigInput = digInput;
            DigOutputSoll = digOutputSoll;
            DigOutputIst = digOutputIst;
            Kommentar = kommentar;
        }
    }
}