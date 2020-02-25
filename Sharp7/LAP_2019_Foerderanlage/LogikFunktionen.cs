using System.Threading;
using Utilities;

namespace LAP_2019_Foerderanlage
{
    public class Logikfunktionen
    {
        private const double WagenGeschwindigkeit = 3;
        private const double WagenPositionLinks = 0;
        private const double WagenPositionRechts = 125;

        private const double WagenFuellstandLeeren = 5;
        private const double WagenFuellstandFuellen = 1;
        private const double WagenFuellstandVoll = 88;

        private const double MaterialSiloFuellen = 0.01;
        private const double MaterialSiloLeeren = 0.002;

        private readonly MainWindow mainWindow;

        public Logikfunktionen(MainWindow window)
        {
            mainWindow = window;
        }

        public void Logikfunktionen_Task()
        {
            /*
            while (mainWindow.FensterAktiv)
            {
              

                // Materialsilo fuellen/leeren
                if (mainWindow.XFU) mainWindow.MaterialSiloFuellstand += MaterialSiloFuellen;

                if (mainWindow.MaterialSiloFuellstand > 0 & mainWindow.Q4_LL & mainWindow.Y1)
                {
                    mainWindow.WagenFuellstand += WagenFuellstandFuellen;
                    mainWindow.MaterialSiloFuellstand -= MaterialSiloLeeren;
                }
                if (mainWindow.WagenFuellstand > WagenFuellstandVoll) mainWindow.WagenFuellstand = WagenFuellstandVoll;
                if (mainWindow.WagenFuellstand < 0) mainWindow.WagenFuellstand = 0;

                if (mainWindow.MaterialSiloFuellstand > 1) mainWindow.MaterialSiloFuellstand = 1;
                if (mainWindow.MaterialSiloFuellstand < 0) mainWindow.MaterialSiloFuellstand = 0;

                mainWindow.MaterialsiloPegel = S7Analog.S7_Analog_2_Short(mainWindow.MaterialSiloFuellstand, 1);

                Thread.Sleep(100);
            }
            */
        }
    }
}