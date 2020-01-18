namespace Synchronisiereinrichtung
{

    using System.Threading;
    public class Logikfunktionen
    {
        readonly MainWindow mainWindow;

        public Logikfunktionen(MainWindow window)
        {
            mainWindow = window;
        }
        public void Logikfunktionen_Task()
        {
            const double Zeitdauer = 10;

            while (mainWindow.FensterAktiv)
            {

                Thread.Sleep((int)Zeitdauer);
            }
        }
    }
}
