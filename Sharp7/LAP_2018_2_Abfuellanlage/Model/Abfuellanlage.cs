using System.Collections.Generic;
using System.Threading;

namespace LAP_2018_2_Abfuellanlage.Model
{
    public class Abfuellanlage
    {
        public List<Flaschen> AlleFlaschen { get; set; }
        public bool B1 { get; set; }
        public bool F1 { get; set; }
        public bool K1 { get; set; }
        public bool K2 { get; set; }
        public bool Q1 { get; set; }
        public bool P1 { get; set; }
        public bool P2 { get; set; }
        public bool S1 { get; set; }
        public bool S2 { get; set; }
        public bool S3 { get; set; }
        public bool S4 { get; set; }
        public double Pegel { get; set; }

        private readonly int anzahlFlaschen;
        private int aktuelleFlasche;

        public Abfuellanlage()
        {
            AlleFlaschen = new List<Flaschen>
            {
                new Flaschen(anzahlFlaschen++),
                new Flaschen(anzahlFlaschen++),
                new Flaschen(anzahlFlaschen++),
                new Flaschen(anzahlFlaschen++),
                new Flaschen(anzahlFlaschen++),
                new Flaschen(anzahlFlaschen++)
            };

            S2 = false;
            F1 = true;
            Pegel = 0.4;

            System.Threading.Tasks.Task.Run(() => AbfuellanlageTask());
        }


        private void AbfuellanlageTask()
        {
            double LeerGeschwindigkeit = 0.001;

            while (true)
            {
                if (K1) Pegel -= LeerGeschwindigkeit;
                if (Pegel < 0) Pegel = 0;

                if (K2) AlleFlaschen[aktuelleFlasche].FlascheVereinzeln();

                B1 = false;
                foreach (Flaschen flasche in AlleFlaschen)
                {
                    var stop = KollisionErkennen(flasche);
                    bool lichtschranke;
                    (lichtschranke, aktuelleFlasche) = flasche.FlascheBewegen(Q1, anzahlFlaschen, aktuelleFlasche, stop);
                    B1 |= lichtschranke;
                }

                Thread.Sleep(10);
            }
        }

        private bool KollisionErkennen(Flaschen bierflasche)
        {
            bool stop = false;
            var (lx, ly) = bierflasche.GetRichtung();

            foreach (Flaschen flasche in AlleFlaschen)
            {
                if (bierflasche.ID != flasche.ID)
                {
                    var (hx, hy) = flasche.GetRichtung();
                    if (hx != Utilities.Rechteck.RichtungX.steht || hy != Utilities.Rechteck.RichtungY.steht)
                    {
                        stop |= Utilities.Rechteck.Ausgebremst(bierflasche.Position, flasche.Position, lx, ly);
                    }
                }
            }

            return stop;
        }

        internal void TasterNachfuellen() { Pegel = 1; }
        internal void TasterF1() { if (F1) F1 = false; else F1 = true; }
        internal void AllesReset()
        {
            Pegel = 0.4;
            aktuelleFlasche = 0;
            foreach (Flaschen flasche in AlleFlaschen) { flasche.Reset(); }
        }
    }
}
