using System;
using System.Collections.Generic;
using System.Threading;

namespace LAP_2010_4_Abfuellanlage.Model
{
    public class AbfuellAnlage
    {
        public List<CampbellSoup> AlleDosen { get; set; }
        public bool S1 { get; set; } // Taster Reset
        public bool S2 { get; set; } // Taster Start       
        public bool S7 { get; set; } // Behälter Füllstand
        public bool S8 { get; set; } // Position Dose Füllen
        public bool K1 { get; set; } // Motor Förderband
        public bool K2 { get; set; } // Spule Magazin Förderband
        public bool K3 { get; set; } // Spule Füllen
        public bool H4 { get; set; } // Meldeleuchte Behälter Leer
        public double Pegel { get; set; }


        private readonly int anzahlDosen;
        private int aktuelleDose;
        private readonly double LeerGeschwindigkeit = 0.002;

        public AbfuellAnlage()
        {
            Pegel = 0.9;

            AlleDosen = new List<CampbellSoup>
            {
                new CampbellSoup(anzahlDosen++),
                new CampbellSoup(anzahlDosen++),
                new CampbellSoup(anzahlDosen++),
                new CampbellSoup(anzahlDosen++)
            };

            System.Threading.Tasks.Task.Run(() => AbfuellAnlageTask());
        }

        private void AbfuellAnlageTask()
        {
            while (true)
            {
                if (K3) Pegel -= LeerGeschwindigkeit;
                if (Pegel < 0) Pegel = 0;

                S7 = Pegel > 0.1;

                if (K2) AlleDosen[aktuelleDose].DosenVereinzeln();

                S8 = false;
                foreach (CampbellSoup dose in AlleDosen)
                {
                    bool lichtschranke;
                    var stop = KollisionErkennen(dose);
                    (lichtschranke, aktuelleDose) = dose.DosenBewegen(K1, anzahlDosen, aktuelleDose, stop);
                    S8 |= lichtschranke;
                }

                Thread.Sleep(10);
            }
        }

        private bool KollisionErkennen(CampbellSoup campbellSoup)
        {
            bool stop = false;
            var (lx, ly) = campbellSoup.GetRichtung();

            foreach (CampbellSoup dose in AlleDosen)
            {
                if (campbellSoup.ID != dose.ID)
                {
                    var (hx, hy) = dose.GetRichtung();
                    if (hx != Utilities.Rechteck.RichtungX.steht || hy != Utilities.Rechteck.RichtungY.steht)
                    {
                        stop |= Utilities.Rechteck.Ausgebremst(campbellSoup.Position, dose.Position, lx, ly);
                    }
                }
            }

            return stop;
        }

        internal void Nachfuellen() { Pegel = 1; }
        internal void AllesReset()
        {
            aktuelleDose = 0;
            foreach (CampbellSoup dose in AlleDosen) { dose.Reset(); }
        }
    }
}
