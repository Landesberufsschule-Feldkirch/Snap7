namespace LAP_2019_Foerderanlage.Model
{
    using System.Threading;

    public class Foerderanlage
    {
        public Wagen Wagen { get; set; }
        public Silo Silo { get; set; }
        public bool B1 { get; set; }        // Wagen Position rechts
        public bool B2 { get; set; }        // Wagen voll
        public bool F1 { get; set; }        // Störung Motorschutzschalter
        public bool S0 { get; set; }        // Anlage Aus
        public bool S1 { get; set; }        // Anlage Ein
        public bool S2 { get; set; }        // Not-Halt
        public bool S3 { get; set; }        // Schalter Automatikbetrieb
        public bool S4 { get; set; }        // chalter Handbetrieb
        public bool S5 { get; set; }        // Handbetrieb Förderband RL
        public bool S6 { get; set; }        // Handbetrieb Förderband LL
        public bool S7 { get; set; }        // Handbetrieb Schneckenförderer
        public bool S8 { get; set; }        // Handbetrieb Materialschieber
        public bool K1 { get; set; }        // Materialschieber Silo
        public bool P1 { get; set; }        // Anlage Ein
        public bool P2 { get; set; }        // Sammelstörung
        public bool Q1 { get; set; }        // Förderband Rechtslauf
        public bool Q2 { get; set; }        // Förderband Linkslauf
        public bool T1 { get; set; }        // Freigabe FU (Schneckenförderer)

        public bool Manual_M1_RL { get; set; }
        public bool Manual_M1_LL { get; set; }
        public bool Manual_M2 { get; set; }
        public bool Manual_K1 { get; set; }

        private readonly MainWindow mainWindow;

        public Foerderanlage(MainWindow mw)
        {
            mainWindow = mw;

            Wagen = new Wagen();
            Silo = new Silo();

            F1 = true;
            S2 = true;

            System.Threading.Tasks.Task.Run(FoerderanlageTask);
        }

        private void FoerderanlageTask()
        {
            while (true)
            {
                Wagen.WagenTask();
                B1 = Wagen.IstWagenRechts();
                B2 = Wagen.IstWagenVoll();

                if (T1) Silo.Fuellen();
                if (K1) Silo.Leeren();

                if (Silo.GetFuellstand() > 0 && Q2 && K1) Wagen.Fuellen();

                if (mainWindow.DebugWindowAktiv)
                {
                    Q1 = Manual_M1_RL;
                    Q2 = Manual_M1_LL;
                    T1 = Manual_M2;
                    K1 = Manual_K1;
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void WagenNachLinks() => Wagen.NachLinks();

        internal void WagenNachRechts() => Wagen.NachRechts();

        internal void BtnF1() => F1 = !F1;

        internal void BtnS2() => S2 = !S2;

        internal void Nachfuellen() => Silo.Nachfuellen();
    }
}