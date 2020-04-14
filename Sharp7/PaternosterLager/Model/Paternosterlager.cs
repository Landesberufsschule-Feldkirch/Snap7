using PaternosterLager.ViewModel;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

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

            //  AlleKettengliedRegale = new ObservableCollection<KettengliedRegal>();
            //  for (var i = 0; i < 20; i++) AlleKettengliedRegale.Add(new KettengliedRegal(i));

            System.Threading.Tasks.Task.Run(() => PaternosterLagerTask());
        }

        private void PaternosterLagerTask()
        {
            while (true)
            {
                /*
                Dispatcher.Invoke(() =>
                {
                    mainWindow.ZeichenFlaeche.Children.Clear();


                });
                */
                Thread.Sleep(10);
            }
        }


     



    }
}