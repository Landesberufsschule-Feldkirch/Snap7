using System.Threading;

namespace LAP_2010_1_Kompressoranlage.Model
{
    public class Kompressoranlage
    {
        public bool F5 { get; set; }    // Thermorelais
        public bool H1 { get; set; }    // Störung
        public bool H2 { get; set; }    // Betriebsbereit
        public bool K1 { get; set; }    // Netzschütz
        public bool K2 { get; set; }    // Sternschütz
        public bool K3 { get; set; }    // Dreieckschütz
        public bool S1 { get; set; }    // Aus
        public bool S2 { get; set; }    // Ein
        public bool S7 { get; set; }    // Druckschalter
        public bool S8 { get; set; }    // Temperatursensor Kompressor

        public double Druck { get; set; }
        private const double DruckVerlust = 0.998;
        private const double DruckAnstieg = 0.04;

        public Kompressoranlage()
        {
            Druck = 0;
            F5 = true;
            S7 = true;

            System.Threading.Tasks.Task.Run(() => KompressoranlageTask());
        }

        private void KompressoranlageTask()
        {

            while (true)
            {

                if (K1 && K3) Druck += DruckAnstieg;
                Druck *= DruckVerlust;

                if (Druck > 10) Druck = 10;

                if (S8)
                {
                    if (Druck > 8) S8 = false;
                }
                else
                {
                    if (Druck < 7) S8 = true;
                }

                Thread.Sleep(10);
            }
        }

        internal void BtnF5() { if (F5) F5 = false; else F5 = true; }
        internal void BtnS7() { if (S7) S7 = false; else S7 = true; }
    }
}
