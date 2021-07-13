using System.Threading;

namespace LAP_2018_4_Niveauregelung.Model
{
    public class NiveauRegelung
    {
        public bool B1 { get; set; }
        public bool B2 { get; set; }
        public bool B3 { get; set; }
        public bool F1 { get; set; }
        public bool F2 { get; set; }
        public bool S1 { get; set; }
        public bool S2 { get; set; }
        public bool S3 { get; set; }
        public bool Q1 { get; set; }
        public bool Q2 { get; set; }
        public bool P1 { get; set; }
        public bool P2 { get; set; }
        public bool P3 { get; set; }
        public bool Y1 { get; set; }
        public double Pegel { get; set; }

        public NiveauRegelung()
        {
            S2 = true;
            F1 = true;
            F2 = true;
            Pegel = 0.95;

            System.Threading.Tasks.Task.Run(NiveauRegelungTask);
        }

        private void NiveauRegelungTask()
        {
            const double fuellGeschwindigkeit = 0.0008;
            const double leerGeschwindigkeit = 0.001;

            while (true)
            {
                if (Q1) Pegel += fuellGeschwindigkeit;
                if (Q2) Pegel += fuellGeschwindigkeit;
                if (Y1) Pegel -= leerGeschwindigkeit;

                if (Pegel > 1) Pegel = 1;
                if (Pegel < 0) Pegel = 0;

                B1 = Pegel > 0.1;   // Schliesser
                B2 = Pegel > 0.5;   // Schliesser
                B3 = Pegel < 0.9;   // Öffner

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}