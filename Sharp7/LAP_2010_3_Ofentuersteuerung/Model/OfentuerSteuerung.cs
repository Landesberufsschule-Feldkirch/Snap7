using System.Threading;

namespace LAP_2010_3_Ofentuersteuerung.Model
{
    public class OfentuerSteuerung
    {
        public bool H3 { get; set; }    // "Schliessen"
        public bool K1 { get; set; }    // Motor LL (Öffnen)
        public bool K2 { get; set; }    // Motor RL (Schliessen)
        public bool S1 { get; set; }    // Taster "Halt" 
        public bool S2 { get; set; }    // Taster "Öffnen" 
        public bool S3 { get; set; }    // Taster "Schliessen" 
        public bool S7 { get; set; }    // Endschalter Offen 
        public bool S8 { get; set; }    // Endschalter Geschlossen 
        public bool S9 { get; set; }    // Lichtschranke
        public double PositionZahnstange { get; set; }
        public double PositionOfentuere { get; set; }
        public double WinkelZahnrad { get; set; }


        private readonly double zahnstangeLinks = -177;
        private readonly double zahnstangeWeg = 220;
        private readonly double zahnstangeGeschwindigkeit = 0.5;
        private readonly double abstandZahnstangeOfentuere = 297;
        private readonly double faktorPositionWinkel = 1.25;
        private readonly double offsetWinkel = 6;

        public OfentuerSteuerung()
        {
            S9 = true;

            PositionZahnstange = zahnstangeLinks + zahnstangeWeg;
            PositionOfentuere = zahnstangeLinks + abstandZahnstangeOfentuere;
            WinkelZahnrad = 0;

            System.Threading.Tasks.Task.Run(() => OfentuerSteuerungTask());
        }

        private void OfentuerSteuerungTask()
        {
            while (true)
            {
                if (K1) { PositionZahnstange -= zahnstangeGeschwindigkeit; }
                if (K2) { PositionZahnstange += zahnstangeGeschwindigkeit; }

                if (PositionZahnstange < zahnstangeLinks) PositionZahnstange = zahnstangeLinks;
                if (PositionZahnstange > zahnstangeLinks + zahnstangeWeg) PositionZahnstange = zahnstangeLinks + zahnstangeWeg;

                PositionOfentuere = PositionZahnstange + abstandZahnstangeOfentuere;
                WinkelZahnrad = offsetWinkel + PositionOfentuere * faktorPositionWinkel;

                S7 = PositionZahnstange < (zahnstangeLinks + 5);
                S8 = PositionZahnstange > (zahnstangeLinks + zahnstangeWeg - 5);

                Thread.Sleep(10);
            }
        }
    }
}
