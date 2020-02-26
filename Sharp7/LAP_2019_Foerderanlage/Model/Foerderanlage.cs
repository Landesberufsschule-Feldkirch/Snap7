namespace LAP_2019_Foerderanlage.Model
{
    using System;
    using System.Threading;
    public class Foerderanlage
    {
        public Wagen Wagen { get; set; }
        public Silo Silo { get; set; }

        public bool S0 { get; set; }        // Anlage Aus
        public bool S1 { get; set; }        // Anlage Ein
        public bool S2 { get; set; }        // Not-Halt
        public bool F4 { get; set; }        // Störung Motorschutzschalter
        public bool S5 { get; set; }        // Schalter Automatikbetrieb 
        public bool S6 { get; set; }        // chalter Handbetrieb 
        public bool S7 { get; set; }        // Wagen Position rechts
        public bool S8 { get; set; }        // Wagen voll
        public bool S9 { get; set; }        // Handbetrieb Förderband RL 
        public bool S10 { get; set; }       // Handbetrieb Förderband LL 
        public bool S11 { get; set; }       // Handbetrieb Schneckenförderer 
        public bool S12 { get; set; }       // Handbetrieb Materialschieber
        public bool P1 { get; set; }        // Anlage Ein
        public bool P2 { get; set; }        // Sammelstörung
        public bool Q3_RL { get; set; }     // Förderband Rechtslauf
        public bool Q4_LL { get; set; }     // Förderband Linkslauf
        public bool XFU { get; set; }       // Freigabe FU (Schneckenförderer)
        public bool Y1 { get; set; }        // Materialschieber Silo



        internal void BtnF4() { if (F4) F4 = false; else F4 = true; }
        internal void BtnS2() { if (S2) S2 = false; else S2 = true; }
        public bool Manual_M1_RL { get; set; }
        public bool Manual_M1_LL { get; set; }
        public bool Manual_M2 { get; set; }
        public bool Manual_Y1 { get; set; }


        private readonly MainWindow mainWindow;
        public Foerderanlage(MainWindow mw)
        {
            mainWindow = mw;

            Wagen = new Wagen();
            Silo = new Silo();

            F4 = true;
            S2 = true;

            System.Threading.Tasks.Task.Run(() => FoerderanlageTask());
        }



        private void FoerderanlageTask()
        {
            while (true)
            {
                Wagen.WagenTask();
                S7 = Wagen.IstWagenRechts();
                S8 = Wagen.IstWagenVoll();

                if (XFU) Silo.Fuellen();
                if (Y1) Silo.Leeren();

                if (Silo.GetFuellstand() > 0 && Q4_LL && Y1) Wagen.Fuellen();


                if (mainWindow.DebugWindowAktiv)
                {
                    Q3_RL = Manual_M1_RL;
                    Q4_LL = Manual_M1_LL;
                    XFU = Manual_M2;
                    Y1 = Manual_Y1;

                }

                Thread.Sleep(10);
            }
        }

        internal void WagenNachLinks() { Wagen.NachLinks(); }
        internal void WagenNachRechts() { Wagen.NachRechts(); }



    }
}
