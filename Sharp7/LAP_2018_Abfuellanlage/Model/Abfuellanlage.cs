using System.Collections.Generic;
using System.Threading;

namespace LAP_2018_Abfuellanlage.Model
{
    public class Abfuellanlage
    {
        public  List<Flaschen> AlleFlaschen { get; set; }
        public bool B1 { get; set; }
        public bool F5 { get; set; }
        public bool K1 { get; set; }
        public bool K2 { get; set; }
        public bool M1 { get; set; }
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
            F5 = true;
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
                    var lichtschranke = false;
                    (lichtschranke, aktuelleFlasche) = flasche.FlascheBewegen(M1, anzahlFlaschen, aktuelleFlasche);
                    B1 |= lichtschranke;
                }

                Thread.Sleep(10);
            }
        }
        internal void TasterNachfuellen() { Pegel = 1; }
        internal void TasterF5() { if (F5) F5 = false; else F5 = true; }
    }
}
