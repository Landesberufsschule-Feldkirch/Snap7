using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LAP_2018_2_Abfuellanlage.Model
{
    public class Abfuellanlage
    {
        public List<Flaschen> AlleFlaschen { get; set; }
        public bool B1 { get; set; }
        public bool F1 { get; set; }
        public bool S1 { get; set; }
        public bool S2 { get; set; }
        public bool S3 { get; set; }
        public bool S4 { get; set; }

        public bool K1 { get; set; }
        public bool K2 { get; set; }
        public bool P1 { get; set; }
        public bool P2 { get; set; }
        public bool Q1 { get; set; }

        public double Pegel { get; set; }

        private readonly int _anzahlFlaschen;
        private int _aktuelleFlasche;

        public Abfuellanlage()
        {
            AlleFlaschen = new List<Flaschen>
            {
                new(_anzahlFlaschen++),
                new(_anzahlFlaschen++),
                new(_anzahlFlaschen++),
                new(_anzahlFlaschen++),
                new(_anzahlFlaschen++),
                new(_anzahlFlaschen++)
            };

            S2 = true;
            F1 = true;
            Pegel = 0.4;

            System.Threading.Tasks.Task.Run(AbfuellanlageTask);
        }

        private void AbfuellanlageTask()
        {
            const double leerGeschwindigkeit = 0.0005;

            while (true)
            {
                if (K1) Pegel -= leerGeschwindigkeit;
                if (Pegel < 0) Pegel = 0;

                if (K2) AlleFlaschen[_aktuelleFlasche].FlascheVereinzeln();

                B1 = false;
                foreach (var flasche in AlleFlaschen)
                {
                    var stop = KollisionErkennen(flasche);
                    bool lichtschranke;
                    (lichtschranke, _aktuelleFlasche) = flasche.FlascheBewegen(Q1, _anzahlFlaschen, _aktuelleFlasche);
                    B1 |= lichtschranke;
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private bool KollisionErkennen(Flaschen bierflasche)
        {
            var stop = false;
            var (lx, ly) = bierflasche.GetRichtung();

            foreach (var flasche in AlleFlaschen.Where(flasche => bierflasche.Id != flasche.Id))
            {
                var (hx, hy) = flasche.GetRichtung();
                if (hx != Utilities.Rechteck.RichtungX.Steht || hy != Utilities.Rechteck.RichtungY.Steht) { stop |= Utilities.Rechteck.Ausgebremst(bierflasche.EineFlasche, flasche.EineFlasche, lx, ly); }
            }

            return stop;
        }

        internal void TasterNachfuellen() => Pegel = 1;

        internal void TasterF1() => F1 = !F1;

        internal void AllesReset()
        {
            Pegel = 0.4;
            _aktuelleFlasche = 0;
            foreach (var flasche in AlleFlaschen) { flasche.Reset(); }
        }
    }
}