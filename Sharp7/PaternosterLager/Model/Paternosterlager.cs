using PaternosterLager.ViewModel;
using System.Collections.ObjectModel;
using System.Threading;

namespace PaternosterLager.Model
{
    public class Paternosterlager
    {
        public bool RichtungAuf { get; set; }
        public bool RichtungAb { get; set; }
        public double Geschwindigkeit { get; set; }
        public ObservableCollection<KettengliedRegal> AlleKettengliedRegale { get; set; }


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
                Thread.Sleep(10);
            }
        }      
    }
}