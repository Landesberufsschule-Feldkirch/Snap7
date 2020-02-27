using System;
using System.Threading;

namespace LAP_2010_3_Ofentuersteuerung.Model
{
    public class OfentuerSteuerung
    {

        public bool H3 { get; set; }    // "Schliessen"
        public bool K1 { get; set; }    // Motor RL (Öffnen)
        public bool K2 { get; set; }    // Motor LL (Schliessen)

        public bool S1 { get; set; }    // Taster "Halt" 
        public bool S2 { get; set; }    // Taster "Öffnen" 
        public bool S3 { get; set; }    // Taster "Schliessen" 
        public bool S7 { get; set; }    // Endschalter Offen 
        public bool S8 { get; set; }    // Endschalter Geschlossen 
        public bool S9 { get; set; }    // Lichtschranke

        public OfentuerSteuerung()
        {


            System.Threading.Tasks.Task.Run(() => OfentuerSteuerungTask());
        }

        private void OfentuerSteuerungTask()
        {
            while (true)
            {


                Thread.Sleep(10);
            }

        }
    }
}
