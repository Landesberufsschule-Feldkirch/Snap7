namespace TestAutomat.Model;

public class TestAusgabe
{
    public int Nr { get; set; }
    public string Zeit { get; set; }
    public AutoTester.TestErgebnis Ergebnis { get; set; }
    public string DigInput { get; set; }
    public string DigOutSoll { get; set; }
    public string DigOutIst { get; set; }
    public string Kommentar { get; set; }

    public TestAusgabe(int nr, string zeit, AutoTester.TestErgebnis ergebnis, string digInput, string digOutSoll, string digOutIst, string kommentar)
    {
        Nr = nr;
        Zeit = zeit;
        Ergebnis = ergebnis;
        DigInput = digInput;
        DigOutSoll = digOutSoll;
        DigOutIst = digOutIst;
        Kommentar = kommentar;
    }
}