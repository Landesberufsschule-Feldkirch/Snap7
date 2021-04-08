using System.Threading;

namespace LAP_2018_1_Silosteuerung.Model
{
    public class Silosteuerung
    {
        public Wagen Wagen { get; set; }
        public Silo Silo { get; set; }

        public bool B1 { get; set; }        // Wagen Position rechts
        public bool B2 { get; set; }        // Wagen voll
        public bool F1 { get; set; }        // Motorschutzschalter Förderband
        public bool F2 { get; set; }        // Motorschutzschalter Schneckenförderer
        public bool S0 { get; set; }        // Taster Aus
        public bool S1 { get; set; }        // Taster Ein
        public bool S2 { get; set; }        // Not-Halt
        public bool S3 { get; set; }        // Taster Störungen quittieren

        public bool P1 { get; set; }        // Anlage Ein
        public bool P2 { get; set; }        // Sammelstörung
        public bool Q1 { get; set; }        // Förderband 
        public bool Q2 { get; set; }        // Freigabe FU Schneckenförderer
        public bool Y1 { get; set; }        // Magnetventil Silo


        public Silosteuerung()
        {
            Wagen = new Wagen();
            Silo = new Silo();

            F1 = true;
            F2 = true;

            S0 = true;
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

                if (Y1) Silo.Leeren();

                if (Q2) Silo.Fuellen();

                if (Silo.GetFuellstand() > 0 && Q1 && Y1) Wagen.Fuellen();

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void BtnF1() => F1 = !F1;
        internal void BtnF2() => F2 = !F2;
        internal void WagenNachLinks() => Wagen.NachLinks();
        internal void WagenNachRechts() => Wagen.NachRechts();
    }
}