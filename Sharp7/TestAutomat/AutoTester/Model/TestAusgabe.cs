using System;
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
            DigInput = digInput.GetBinFormatiert();
            DigOutput = digOutput.GetBinFormatiert();
            Kommentar = kommentar;
        }
        private static string Dez2Bin(uint bin)
        {
            var binaer = Convert.ToString(bin, 2).PadLeft(16, '0');

            return $"2#{binaer.Substring(0, 4)}_{binaer.Substring(4, 4)}_{binaer.Substring(8, 4)}_{binaer.Substring(12, 4)}";
        }
    }
}
