﻿using System.Collections.Generic;
using System.Threading;

namespace LAP_2018_Abfuellanlage.Model
{
    public class Abfuellanlage
    {
        public List<Flaschen> AlleFlaschen { get; set; }
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
                    var stop = KollisionErkennen(flasche);
                    (lichtschranke, aktuelleFlasche) = flasche.FlascheBewegen(M1, anzahlFlaschen, aktuelleFlasche, stop);
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
        internal void TasterF5() { if (F5) F5 = false; else F5 = true; }
        internal void AllesReset()
        {
            Pegel = 0.4;
            aktuelleFlasche = 0;
            foreach (Flaschen flasche in AlleFlaschen) { flasche.Reset(); }
        }
    }
}
