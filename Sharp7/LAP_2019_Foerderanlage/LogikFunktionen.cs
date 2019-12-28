using System.Threading;

namespace LAP_2019_Foerderanlage
{
    public class Logikfunktionen
    {
        private readonly MainWindow mainWindow;

        public Logikfunktionen(MainWindow window)
        {
            mainWindow = window;
        }

        public void Logikfunktionen_Task()
        {
            while (mainWindow.FensterAktiv)
            {
                

                Thread.Sleep(100);
            }
        }
    }
}