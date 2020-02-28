using System;
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

        private readonly double OfentuereLinks = 120;
        private readonly double ZahnstangeLinks = -177;
        private readonly double ZahnstangeWeg = 220;
        private readonly double ZahnstangeGeschwindigkeit = 0.5;
        private readonly double ZahnradGeschwindigkeit = 0.75;
        public OfentuerSteuerung()
        {
            S9 = true;

            PositionZahnstange = ZahnstangeLinks + ZahnstangeWeg;
            PositionOfentuere = OfentuereLinks + ZahnstangeWeg;
            WinkelZahnrad = 0;

            System.Threading.Tasks.Task.Run(() => OfentuerSteuerungTask());
        }

        private void OfentuerSteuerungTask()
        {
            while (true)
            {
                if (K1)
                {
                    PositionOfentuere -= ZahnstangeGeschwindigkeit;
                    PositionZahnstange -= ZahnstangeGeschwindigkeit;
                    WinkelZahnrad -= ZahnradGeschwindigkeit;
                }
                if (K2)
                {
                    PositionOfentuere += ZahnstangeGeschwindigkeit;
                    PositionZahnstange += ZahnstangeGeschwindigkeit;
                    WinkelZahnrad += ZahnradGeschwindigkeit;
                }

                S7 = PositionOfentuere < (OfentuereLinks + 5);
                S8 = PositionOfentuere > (OfentuereLinks + ZahnstangeWeg - 5);

                
                if (PositionOfentuere < OfentuereLinks) PositionOfentuere = OfentuereLinks;
                if (PositionZahnstange < ZahnstangeLinks) PositionZahnstange = ZahnstangeLinks;

                if (PositionOfentuere > OfentuereLinks + ZahnstangeWeg) PositionOfentuere = OfentuereLinks + ZahnstangeWeg;
                if (PositionZahnstange > ZahnstangeLinks + ZahnstangeWeg) PositionZahnstange = ZahnstangeLinks + ZahnstangeWeg;

                if (WinkelZahnrad > 360) WinkelZahnrad = 0;
                if (WinkelZahnrad < 0) WinkelZahnrad = 360;

                Thread.Sleep(10);
            }

        }
    }
}
