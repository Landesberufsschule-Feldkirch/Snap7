using System;
using System.Threading;

namespace LAP_2010_2_Transportwagen.Model
{
    public class Transportwagen
    {
        public bool K1 { get; set; }    // Motor LL 
        public bool K2 { get; set; }    // Motor RL 
        public bool H3 { get; set; }    // Störung


        public bool S1 { get; set; }    // Taster "Start" 
        public bool S2 { get; set; }    // NotHalt 
        public bool F3 { get; set; }    // Thermorelais 
        public bool S7 { get; set; }    // Endschalter Links 
        public bool S8 { get; set; }    // Endschalter Rechts 

        public double Position { get; set; }
        public double AbstandRadRechts { get; set; }
        private const double Geschwindigkeit = 1;
        private const double RandLinks = 30;
        private const double RandRechts = 430;

        public Transportwagen()
        {
            Position = 30;
            AbstandRadRechts = 100;

            F3 = true;
            S2 = true;

            System.Threading.Tasks.Task.Run(() => TransportwagtenTask());
        }

        private void TransportwagtenTask()
        {
            while (true)
            {
                if (K1) Position -= Geschwindigkeit;
                if (K2) Position += Geschwindigkeit;

                if (Position < RandLinks) Position = RandLinks;
                if (Position > RandRechts) Position = RandRechts;

                S7 = Position < (RandLinks + 2);
                S8 = Position > (RandRechts - 2);

                Thread.Sleep(10);
            }
        }

        internal void SetF3() { if (F3) F3 = false; else F3 = true; }
        internal void SetS2() { if (S2) S2 = false; else S2 = true; }
    }
}
