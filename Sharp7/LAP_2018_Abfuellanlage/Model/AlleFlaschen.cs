using System.Collections.Generic;
using System.Threading;

namespace LAP_2018_Abfuellanlage.Model
{
    public class AlleFlaschen
    {
        public readonly List<Flaschen> alleFlaschen = new List<Flaschen>();

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

        private readonly int AnzahlFlaschen;
        private int AktuelleFlasche;

        public AlleFlaschen()
        {
            alleFlaschen.Add(new Flaschen(AnzahlFlaschen++));
            alleFlaschen.Add(new Flaschen(AnzahlFlaschen++));
            alleFlaschen.Add(new Flaschen(AnzahlFlaschen++));
            alleFlaschen.Add(new Flaschen(AnzahlFlaschen++));
            alleFlaschen.Add(new Flaschen(AnzahlFlaschen++));
            alleFlaschen.Add(new Flaschen(AnzahlFlaschen++));

            S2 = false;
            F5 = true;
            Pegel = 0.4;

            System.Threading.Tasks.Task.Run(() => AlleFlaschenTask());
        }

        private void AlleFlaschenTask()
        {
            double LeerGeschwindigkeit = 0.001;

            while (true)
            {
                if (K1) Pegel -= LeerGeschwindigkeit;
                if (Pegel < 0) Pegel = 0;

                if (K2) alleFlaschen[AktuelleFlasche].FlascheVereinzeln();

                B1 = false;
                foreach (Flaschen flasche in alleFlaschen)
                {
                    var lichtschranke = false;
                    (lichtschranke, AktuelleFlasche) = flasche.FlascheBewegen(M1, AnzahlFlaschen, AktuelleFlasche);
                    B1 |= lichtschranke;
                }

                Thread.Sleep(10);
            }
        }
        internal void TasterNachfuellen() { Pegel = 1; }
        internal void TasterF5() { if (F5) F5 = false; else F5 = true; }
    }
}
