using System.Threading;

namespace LAP_2018_Abfuellanlage
{
    public class Logikfunktionen
    {
        private readonly MainWindow mainWindow;
        private readonly double LeerGeschwindigkeit = 0.000_01;
        private readonly int SiemensAnalogwertMax = 27_648;

        public Logikfunktionen(MainWindow window)
        {
            mainWindow = window;
        }

        public void Logikfunktionen_Task()
        {
            while (mainWindow.FensterAktiv)
            {
                if (mainWindow.K1) mainWindow.Pegel -= LeerGeschwindigkeit;
                if (mainWindow.Pegel < 0) mainWindow.Pegel = 0;

                mainWindow.PegelByte = (byte)(100 * mainWindow.Pegel);
                mainWindow.PegelInt = (ushort)(SiemensAnalogwertMax * mainWindow.Pegel);

                if (mainWindow.K2)
                {
                    int AktFlasche = mainWindow.gAlleFlaschen[0].GetAktuelleFlasche();
                    mainWindow.gAlleFlaschen[AktFlasche].FlascheVereinzeln();
                }

                mainWindow.B1 = false;
                foreach (Flaschen flasche in mainWindow.gAlleFlaschen) { mainWindow.B1 |= flasche.FlascheBewegen(mainWindow.M1); }

                Thread.Sleep(100);
            }
        }
    }
}