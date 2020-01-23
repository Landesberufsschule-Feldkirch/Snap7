using System.Threading;

namespace LAP_2018_Niveauregelung
{
    public class Logikfunktionen
    {
        private readonly MainWindow mainWindow;
        private readonly double FuellGeschwindigkeit = 0.0008;
        private readonly double LeerGeschwindigkeit = 0.001;

        public Logikfunktionen(MainWindow window)
        {
            mainWindow = window;
        }

        public void Logikfunktionen_Task()
        {
            while (mainWindow.FensterAktiv)
            {
                if (mainWindow.M1) mainWindow.Pegel += FuellGeschwindigkeit;
                if (mainWindow.M2) mainWindow.Pegel += FuellGeschwindigkeit;
                if (mainWindow.Y1) mainWindow.Pegel -= LeerGeschwindigkeit;

                if (mainWindow.Pegel > 1) mainWindow.Pegel = 1;
                if (mainWindow.Pegel < 0) mainWindow.Pegel = 0;

                mainWindow.B1 = (mainWindow.Pegel > 0.1);
                mainWindow.B2 = (mainWindow.Pegel > 0.5);
                mainWindow.B3 = (mainWindow.Pegel > 0.9);

                Thread.Sleep(10);
            }
        }
    }
}