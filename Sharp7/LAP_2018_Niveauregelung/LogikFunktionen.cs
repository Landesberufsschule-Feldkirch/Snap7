using System.Threading;

namespace LAP_2018_Niveauregelung
{
    public partial class MainWindow
    {
        private double Pegel = 0.95;

        private readonly double FuellGeschwindigkeit = 0.0008;
        private readonly double LeerGeschwindigkeit = 0.001;

        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                if (M1) Pegel += FuellGeschwindigkeit;
                if (M2) Pegel += FuellGeschwindigkeit;
                if (Y1) Pegel -= LeerGeschwindigkeit;

                if (Pegel > 1) Pegel = 1;
                if (Pegel < 0) Pegel = 0;

                B1 = (Pegel > 0.1);
                B2 = (Pegel > 0.5);
                B3 = (Pegel > 0.9);

                Thread.Sleep(10);
            }
        }
    }
}
