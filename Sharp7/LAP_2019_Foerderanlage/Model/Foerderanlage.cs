namespace LAP_2019_Foerderanlage.Model
{
    using System;
    using System.Threading;
    public class Foerderanlage
    {
        public Wagen Wagen { get; set; }
        public Silo Silo { get; set; }

        public bool S0;     // Anlage Aus
        public bool S1;     // Anlage Ein
        public bool S2;     // Not-Halt
        public bool F4;     // Störung Motorschutzschalter
        public bool S4;
        public bool S5;
        public bool S6;
        public bool S7;     // Wagen Position rechts
        public bool S8;     // Wagen voll

        public bool P1;     // Anlage Ein
        public bool P2;     // Sammelstörung
        public bool Q3_RL;  // Förderband Rechtslauf
        public bool Q4_LL;  // Förderband Linkslauf
        public bool XFU;    // Freigabe FU (Schneckenförderer)

        public bool Y1;     // Materialschieber Silo

      

        public Foerderanlage()
        {
            Wagen = new Wagen();
            Silo = new Silo();

            System.Threading.Tasks.Task.Run(() => FoerderanlageTask());
        }



        private void FoerderanlageTask()
        {
            while (true)
            {
                Wagen.WagenTask();
                S7 = Wagen.IstWagenRechts();
                S8 = Wagen.IstWagenVoll();



                Thread.Sleep(10);
            }
        }

        internal void WagenNachLinks() { Wagen.NachLinks(); }
        internal void WagenNachRechts() { Wagen.NachRechts(); }
    }
}
