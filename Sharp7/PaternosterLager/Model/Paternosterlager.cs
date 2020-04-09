using PaternosterLager.ViewModel;
using System.Threading;

namespace PaternosterLager.Model
{
    public class Paternosterlager
    {
        public bool RichtungAuf { get; set; }
        public bool RichtungAb { get; set; }
        public double Geschwindigkeit { get; set; }

        private const double geschwindigkeit = 0.01;
        private readonly MainWindow mainWindow;


        public Paternosterlager(MainWindow mw)
        {
            mainWindow = mw;
            System.Threading.Tasks.Task.Run(() => PaternosterLagerTask());
        }

        private void PaternosterLagerTask()
        {
            while (true)
            {

                if (mainWindow.KomplettesKettenRegal != null)
                {
                    if (RichtungAuf) mainWindow.KomplettesKettenRegal.SetGeschwindigkeit(geschwindigkeit);
                    if (RichtungAb) mainWindow.KomplettesKettenRegal.SetGeschwindigkeit(-geschwindigkeit);
                }


                Thread.Sleep(10);
            }
        }


    }
}