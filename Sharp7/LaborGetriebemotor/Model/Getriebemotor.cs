using System.Threading;

namespace LaborGetriebemotor.Model
{
    public class Getriebemotor
    {
        public bool B1 { get; set; }    // Lichtschranke 0° 
        public bool B2 { get; set; }    // Lichtschranke 45° CCW
        public bool S1 { get; set; }    // Taster ( ① ) → Schliesser
        public bool S2 { get; set; }    // Taster ( ⓪ ) → Öffner
        public bool S4 { get; set; }    // Taster ( STOP ) → Öffner 
        public bool S3 { get; set; }    // Taster ( Ⅰ ) → Schliesser 
        public bool S5 { get; set; }    // Taster ( Ⅱ ) → Schliesser 
        public bool S7 { get; set; }    // Taster (STOP) → Öffner
        public bool S6 { get; set; }    // Taster (←) → Schliesser
        public bool S8 { get; set; }    // Taster (→) → Schliesser
        public bool S91 { get; set; }   // Not-Halt → Schliesser 
        public bool S92 { get; set; }   // Not-Halt → Öffner
        public bool P1 { get; set; }    // Meldeleuchte weiß
        public bool P2 { get; set; }    // Meldeleuchte rot
        public bool P3 { get; set; }    // Meldeleuchte grün
        public bool Q1 { get; set; }    // Getriebemotor Schnell Rechtslauf
        public bool Q2 { get; set; }    // Getriebemotor Linkslauf
        public bool Q3 { get; set; }    // Getriebemotor Langsam Rechtslauf

        public double WinkelGetriebemotor { get; set; }

        private const double GeschwindigkeitGetriebemotorLangsam = 1;
        private const double GeschwindigkeitGetriebemotorSchnell = 2 * GeschwindigkeitGetriebemotorLangsam;

        public Getriebemotor()
        {
            S2 = true;
            S4 = true;
            S7 = true;
            S92 = true;

            System.Threading.Tasks.Task.Run(GetriebemotorTask);
        }
        private void GetriebemotorTask()
        {
            while (true)
            {
                if (Q2)
                {
                    // Linkslauf
                    if (Q1) WinkelGetriebemotor -= GeschwindigkeitGetriebemotorSchnell;
                    if (Q3) WinkelGetriebemotor -= GeschwindigkeitGetriebemotorLangsam;
                }
                else
                {
                    // Rechtslauf
                    if (Q1) WinkelGetriebemotor += GeschwindigkeitGetriebemotorSchnell;
                    if (Q3) WinkelGetriebemotor += GeschwindigkeitGetriebemotorLangsam;
                }

                if (WinkelGetriebemotor > 360) WinkelGetriebemotor -= 360;
                if (WinkelGetriebemotor < 0) WinkelGetriebemotor += 360;

                B1 = WinkelGetriebemotor > 350 || WinkelGetriebemotor < 10;
                B2 = WinkelGetriebemotor > 35 && WinkelGetriebemotor < 55;

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}