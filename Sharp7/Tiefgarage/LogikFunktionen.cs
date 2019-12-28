using System.Threading;

namespace Tiefgarage
{
    public class Logikfunktionen
    {
        readonly MainWindow mainWindow;
        public Logikfunktionen(MainWindow window)
        {
            mainWindow = window;
        }
        public void Logikfunktionen_Task()
        {
            while (mainWindow.FensterAktiv)
            {
                Thread.Sleep(10);
            }
        }
    }
}
