using System;

namespace TestAutomat.Model
{
    public class TestAusgabe
    {
        public int Nr { get; set; }
        public string DigInput { get; set; }
        public string DigOutput { get; set; }
        public  string Kommentar { get; set; }

        public TestAusgabe(int nr, int digInput, int digOutput, string kommentar)
        {
            Nr = nr;
            DigInput = Dez2Bin(digInput);
            DigOutput = Dez2Bin(digOutput);
            Kommentar = kommentar;
        }

        private static string Dez2Bin(int bin)
        {
            var binaer = Convert.ToString(bin, 2);

            var ergebnis = $"2#0000_0000_0000_00{binaer}";
            
            return ergebnis;
        }
    }
}
