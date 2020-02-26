using System;
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
        public Kompressoranlage()
        {
            Druck = 0;
            F5 = true;

            System.Threading.Tasks.Task.Run(() => KompressoranlageTask());
        }

        private void KompressoranlageTask()
        {
            double wert = 0;
            while (true)
            {
                Druck = 5+ 5 * Math.Sin(wert);
                wert += 0.01;


                Thread.Sleep(10);
            }
        }

        internal void BtnF5()
        {
            throw new NotImplementedException();
        }
    }
}
