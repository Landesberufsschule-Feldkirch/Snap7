using System.Diagnostics;
using System.Threading;

namespace LAP_2018_3_Hydraulikaggregat.Model
{
    public class Hydraulikaggregat
    {
        public bool B1 { get; set; }    // Sensor Ölstand
        public bool B2 { get; set; }    // Sensor Druck erreicht
        public bool B3 { get; set; }    // Sensor Überdruck
        public bool F1 { get; set; }    // Motorstörung
        public bool P1 { get; set; }    // Meldeleuchte Störung Motor
        public bool P2 { get; set; }    // Meldeleuchte Überdruck
        public bool P3 { get; set; }    // Meldeleuchte Druck erreicht
        public bool P4 { get; set; }    // Meldeleuchte Ölstand min.
        public bool Q1 { get; set; }    // Netzschütz
        public bool Q2 { get; set; }    // Sternschütz
        public bool Q3 { get; set; }    // Dreieckschütz
        public bool S1 { get; set; }    // Taster Start
        public bool S2 { get; set; }    // Taster Stop
        public bool S3 { get; set; }    // Taster Quittieren

        public double Druck { get; set; }
        public double Pegel { get; set; }
        public Stopwatch Stopwatch { get; set; }

        private const double druckVerlust = 0.998;
        private const double druckAnstieg = 0.04;
        private const double pegelVerlust = 0.999;
        private const int ZeitBisStoerung = 10000;

        public Hydraulikaggregat()
        {
            Druck = 0;
            Pegel = 0.8;
            B3 = true;
            F1 = true;

            Stopwatch = new Stopwatch();
            Stopwatch.Restart();

            System.Threading.Tasks.Task.Run(HydraulikaggregatTask);
        }

        private void HydraulikaggregatTask()
        {
            while (true)
            {
                if (Q1 && Q3)
                {
                    Pegel *= pegelVerlust;
                    Druck += druckAnstieg;
                    Stopwatch.Start();
                }
                else Stopwatch.Stop();

                Druck *= druckVerlust;

                if (Druck > 10) Druck = 10;

                if (B2)
                { if (Druck > 8) B2 = false; }
                else
                { if (Druck < 7) B2 = true; }

                if (Pegel > 0.45) B1 = true; else B1 = false;

                if (Stopwatch.ElapsedMilliseconds > ZeitBisStoerung) B3 = false; else B3 = true;

                Thread.Sleep(10);
            }
        }

        internal void BtnF1()
        {
            if (F1) F1 = false; else F1 = true;
        }

        internal void BtnNachfuellen() => Pegel = 1;
    }
}