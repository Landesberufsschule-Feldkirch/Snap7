using System.Threading;

namespace LAP_2010_1_Kompressoranlage.Model
{
    public class Kompressoranlage
    {
        public bool B1 { get; set; }    // Druckschalter
        public bool B2 { get; set; }    // Temperatursensor Kompressor
        public bool F1 { get; set; }    // Thermorelais
        public bool P1 { get; set; }    // Störung
        public bool P2 { get; set; }    // Betriebsbereit
        public bool Q1 { get; set; }    // Netzschütz
        public bool Q2 { get; set; }    // Sternschütz
        public bool Q3 { get; set; }    // Dreieckschütz
        public bool S1 { get; set; }    // Aus
        public bool S2 { get; set; }    // Ein

        public double Druck { get; set; }

        private const double DruckVerlust = 0.998;
        private const double DruckAnstieg = 0.04;

        public Kompressoranlage()
        {
            Druck = 0;
            F1 = true;
            B1 = true;

            System.Threading.Tasks.Task.Run(KompressoranlageTask);
        }

        private void KompressoranlageTask()
        {
            while (true)
            {
                if (Q1 && Q3) Druck += DruckAnstieg;
                Druck *= DruckVerlust;

                if (Druck > 10) Druck = 10;

                if (B2) { if (Druck > 8) B2 = false; }
                else { if (Druck < 7) B2 = true; }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void BtnF1()
        {
            F1 = !F1;
        }

        internal void BtnB1()
        {
            B1 = !B1;
        }
    }
}