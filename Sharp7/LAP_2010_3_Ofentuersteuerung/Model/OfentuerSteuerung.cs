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

        private readonly double ZahnstangeLinks = -177;
        private readonly double AbstandZahnstangeOfentuere = 297;
        private readonly double ZahnstangeWeg = 220;
        private readonly double ZahnstangeGeschwindigkeit = 0.5;
        private readonly double FaktorPositionWinkel = 1.25;
        private readonly double OffsetWinkel = 6;

        public OfentuerSteuerung()
        {
            S9 = true;

            PositionZahnstange = ZahnstangeLinks + ZahnstangeWeg;
            PositionOfentuere = ZahnstangeLinks + AbstandZahnstangeOfentuere;
            WinkelZahnrad = 0;

            System.Threading.Tasks.Task.Run(() => OfentuerSteuerungTask());
        }

        private void OfentuerSteuerungTask()
        {
            while (true)
            {
                if (K1) { PositionZahnstange -= ZahnstangeGeschwindigkeit; }
                if (K2) { PositionZahnstange += ZahnstangeGeschwindigkeit; }

                if (PositionZahnstange < ZahnstangeLinks) PositionZahnstange = ZahnstangeLinks;
                if (PositionZahnstange > ZahnstangeLinks + ZahnstangeWeg) PositionZahnstange = ZahnstangeLinks + ZahnstangeWeg;

                PositionOfentuere = PositionZahnstange + AbstandZahnstangeOfentuere;
                WinkelZahnrad = OffsetWinkel + PositionOfentuere * FaktorPositionWinkel;

                S7 = PositionZahnstange < (ZahnstangeLinks + 5);
                S8 = PositionZahnstange > (ZahnstangeLinks + ZahnstangeWeg - 5);

                Thread.Sleep(10);
            }
        }
    }
}
