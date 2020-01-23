using System.Threading;

namespace BehälterSteuerung
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
                mainWindow.AutomatikModusNochAktiv = false;

                foreach (Behaelter beh in mainWindow.gAlleBehaelter)
                {
                    beh.PegelUeberwachen();
                    if ((beh.InternerPegel > 0.01)) mainWindow.AutomatikModusNochAktiv = true;
                }

                if (!mainWindow.AutomatikModusNochAktiv) mainWindow.AutomatikKnoepfeAktivieren();

                Thread.Sleep(10);
            }
        }
    }
}