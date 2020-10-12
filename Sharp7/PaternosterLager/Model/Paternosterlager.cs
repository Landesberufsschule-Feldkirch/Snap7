using System;
using System.Threading;

namespace PaternosterLager.Model
{
    public class Paternosterlager
    {
        public bool ManualAuf { get; set; }
        public bool ManualAb { get; set; }
        public char Zeichen { get; internal set; }
        public bool S1 { get; internal set; }
        public bool S2 { get; internal set; }
        public bool B1 { get; set; }
        public bool B2 { get; set; }
        public bool Q1 { get; set; }
        public bool Q2 { get; set; }
        public int IstPos { get; set; }
        public int SollPos { get; set; }
        public double Position { get; set; }
        public double GesamtLaenge { get; set; }

        public Paternosterlager(double anzahlKisten)
        {
            System.Threading.Tasks.Task.Run(() => PaternosterLagerTask(anzahlKisten));
        }

        private void PaternosterLagerTask(double anzahlKisten)
        {
            const int bereichInitiator = 10;
            const double geschwindigkeit = 1;

            while (true)
            {
                var abstandRegale = Convert.ToInt32(GesamtLaenge / anzahlKisten);
                var pos = Convert.ToInt32(Position);
                
                if (S1) Position += geschwindigkeit;
                if (S2) Position -= geschwindigkeit;

                if (abstandRegale > 0)
                {
                    if (pos > GesamtLaenge) Position -= GesamtLaenge;
                    if (pos < 0) Position += GesamtLaenge;

                    if (pos >= 0 && pos < bereichInitiator) B1 = true; else B1 = false;
                    if (pos % abstandRegale >= 0 && pos % abstandRegale < bereichInitiator) B2 = true; else B2 = false;
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void AllesReset() => Position = 0;
    }
}