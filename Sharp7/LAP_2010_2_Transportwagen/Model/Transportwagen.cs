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

        private const double geschwindigkeit = 1;
        private const double randLinks = 30;
        private const double randRechts = 430;

        public Transportwagen()
        {
            Position = 30;
            AbstandRadRechts = 100;

            F1 = true;
            S2 = true;

            System.Threading.Tasks.Task.Run(() => TransportwagtenTask());
        }

        private void TransportwagtenTask()
        {
            while (true)
            {
                if (Q1) Position -= geschwindigkeit;
                if (Q2) Position += geschwindigkeit;

                if (Position < randLinks) Position = randLinks;
                if (Position > randRechts) Position = randRechts;

                B1 = Position < (randLinks + 2);
                B2 = Position > (randRechts - 2);

                Thread.Sleep(10);
            }
        }

        internal void SetF1()
        {
            if (F1) F1 = false; else F1 = true;
        }

        internal void SetS2()
        {
            if (S2) S2 = false; else S2 = true;
        }
    }
}