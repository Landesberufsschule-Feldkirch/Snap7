using System;
using System.Threading;
using Utilities;


namespace Synchronisiereinrichtung
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
           
            const double Zeitdauer = 10;

            double WinkelGenerator = 0;
            double WinkelNetz = 0;

            while (mainWindow.FensterAktiv)
            {


              
       



          

                if (mainWindow.Q1alt != mainWindow.Q1)
                {
                    mainWindow.Q1alt = mainWindow.Q1;
                   
                }
                Thread.Sleep((int)Zeitdauer);
            }
        }

    }

 


}
