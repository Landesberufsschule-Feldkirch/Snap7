using System.Collections.Generic;
using System.Threading;

namespace LAP_2010_4_Abfuellanlage.Model
{
    public class AbfuellAnlage
    {
        public List<CampbellSoup> AlleDosen { get; set; }
        public bool S1 { get; set; } // Taster Reset
        public bool S2 { get; set; } // Taster Start
        public bool B1 { get; set; } // Behälter Füllstand
        public bool B2 { get; set; } // Position Dose Füllen
        public bool Q1 { get; set; } // Motor Förderband
        public bool K1 { get; set; } // Spule Magazin Förderband
        public bool K2 { get; set; } // Spule Füllen
        public bool P1 { get; set; } // Meldeleuchte Behälter Leer
        public double Pegel { get; set; }

        private readonly int anzahlDosen;
        private int aktuelleDose;
        private readonly double leerGeschwindigkeit = 0.002;

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

            System.Threading.Tasks.Task.Run(AbfuellAnlageTask);
        }

        private void AbfuellAnlageTask()
        {
            while (true)
            {
                if (K2) Pegel -= leerGeschwindigkeit;
                if (Pegel < 0) Pegel = 0;

                B1 = Pegel > 0.1;

                if (K1) AlleDosen[aktuelleDose].DosenVereinzeln();

                B2 = false;
                foreach (var dose in AlleDosen)
                {
                    bool lichtschranke;
                    var stop = KollisionErkennen(dose);
                    (lichtschranke, aktuelleDose) = dose.DosenBewegen(Q1, anzahlDosen, aktuelleDose, stop);
                    B2 |= lichtschranke;
                }

                Thread.Sleep(10);
            }
        }

        private bool KollisionErkennen(CampbellSoup campbellSoup)
        {
            bool stop = false;
            var (lx, ly) = campbellSoup.GetRichtung();

            foreach (var dose in AlleDosen)
            {
                if (campbellSoup.ID != dose.ID)
                {
                    var (hx, hy) = dose.GetRichtung();
                    if (hx != Utilities.Rechteck.RichtungX.Steht || hy != Utilities.Rechteck.RichtungY.Steht) { stop |= Utilities.Rechteck.Ausgebremst(campbellSoup.Position, dose.Position, lx, ly); }
                }
            }

            return stop;
        }

        internal void Nachfuellen() => Pegel = 1;

        internal void AllesReset()
        {
            aktuelleDose = 0;
            foreach (var dose in AlleDosen) { dose.Reset(); }
        }
    }
}