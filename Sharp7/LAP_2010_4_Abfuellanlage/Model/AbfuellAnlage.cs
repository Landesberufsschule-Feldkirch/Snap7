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

        public AbfuellAnlage()
        {
            AlleDosen = new List<CampbellSoup>
            {
                new CampbellSoup(anzahlDosen++),
                new CampbellSoup(anzahlDosen++),
                new CampbellSoup(anzahlDosen++),
                new CampbellSoup(anzahlDosen++)
            };

            Pegel = 0.9;

            System.Threading.Tasks.Task.Run(() => AbfuellAnlageTask());
        }

        private void AbfuellAnlageTask()
        {
            double LeerGeschwindigkeit = 0.002;

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
                    (lichtschranke, aktuelleDose) = dose.DosenBewegen(K1, anzahlDosen, aktuelleDose);
                    S8 |= lichtschranke;
                }

                Thread.Sleep(10);
            }
        }
    }
}
