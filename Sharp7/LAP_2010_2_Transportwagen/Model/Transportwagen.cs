using System.Threading;

namespace LAP_2010_2_Transportwagen.Model
{
    public class Transportwagen
    {
        public bool Q1 { get; set; }    // Motor LL
        public bool Q2 { get; set; }    // Motor RL
        public bool P1 { get; set; }    // Störung
        public bool S1 { get; set; }    // Taster "Start"
        public bool S2 { get; set; }    // NotHalt
        public bool S3 { get; set; }    // Taster Reset
        public bool F1 { get; set; }    // Thermorelais
        public bool B1 { get; set; }    // Endschalter Links
        public bool B2 { get; set; }    // Endschalter Rechts
        public double Position { get; set; }
        public double AbstandRadRechts { get; set; }
        public bool Fuellen { get; internal set; }

        private const double Geschwindigkeit = 1;
        private const double RandLinks = 30;
        private const double RandRechts = 430;
        private const double MaximaleFuellzeit = 700; // Zykluszeit ist 10ms --> 7"
        private double _laufzeitFuellen;

        public Transportwagen()
        {
            Position = 30;
            AbstandRadRechts = 100;

            F1 = true;
            S2 = true;

            System.Threading.Tasks.Task.Run(TransportwagtenTask);
        }

        private void TransportwagtenTask()
        {
            while (true)
            {
                if (B1) _laufzeitFuellen = 0;
                if (B2 && _laufzeitFuellen <= MaximaleFuellzeit) _laufzeitFuellen++;
                Fuellen = _laufzeitFuellen > 1 && _laufzeitFuellen < MaximaleFuellzeit;

                if (Q1) Position -= Geschwindigkeit;
                if (Q2) Position += Geschwindigkeit;

                if (Position < RandLinks) Position = RandLinks;
                if (Position > RandRechts) Position = RandRechts;

                B1 = Position < RandLinks + 2;
                B2 = Position > RandRechts - 2;

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void SetF1() => F1 = !F1;

        internal void SetS2() => S2 = !S2;
    }
}