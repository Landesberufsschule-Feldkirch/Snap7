using System.Threading;

namespace LAP_2010_3_Ofentuersteuerung.Model
{
    public class OfentuerSteuerung
    {
        public bool P1 { get; set; }    // "Schliessen"
        public bool Q1 { get; set; }    // Motor LL (Öffnen)
        public bool Q2 { get; set; }    // Motor RL (Schliessen)
        public bool S1 { get; set; }    // Taster "Halt"
        public bool S2 { get; set; }    // Taster "Öffnen"
        public bool S3 { get; set; }    // Taster "Schliessen"
        public bool B1 { get; set; }    // Endschalter Offen
        public bool B2 { get; set; }    // Endschalter Geschlossen
        public bool B3 { get; set; }    // Lichtschranke
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
            B3 = true;

            PositionZahnstange = zahnstangeLinks + zahnstangeWeg;
            PositionOfentuere = zahnstangeLinks + abstandZahnstangeOfentuere;
            WinkelZahnrad = 0;

            System.Threading.Tasks.Task.Run(() => OfentuerSteuerungTask());
        }

        private void OfentuerSteuerungTask()
        {
            while (true)
            {
                if (Q1) { PositionZahnstange -= zahnstangeGeschwindigkeit; }
                if (Q2) { PositionZahnstange += zahnstangeGeschwindigkeit; }

                if (PositionZahnstange < zahnstangeLinks) PositionZahnstange = zahnstangeLinks;
                if (PositionZahnstange > zahnstangeLinks + zahnstangeWeg) PositionZahnstange = zahnstangeLinks + zahnstangeWeg;

                PositionOfentuere = PositionZahnstange + abstandZahnstangeOfentuere;
                WinkelZahnrad = offsetWinkel + PositionOfentuere * faktorPositionWinkel;

                B1 = PositionZahnstange < (zahnstangeLinks + 5);
                B2 = PositionZahnstange > (zahnstangeLinks + zahnstangeWeg - 5);

                Thread.Sleep(10);
            }
        }
    }
}