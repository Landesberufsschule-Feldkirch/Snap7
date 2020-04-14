using PaternosterLager.ViewModel;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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


        internal void LagerHinzufuegen(ViewModel.ViewModel viewModel)
        {
            foreach (var kettengliedRegal in viewModel.ViAnzeige.AlleKettengliedRegale)
            {
                var myPath = new Path
                {
                    Fill = Brushes.LemonChiffon,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    Data = kettengliedRegal.GetKettengliedRegal()
                };

                Canvas.SetLeft(myPath, kettengliedRegal.GetPosX());
                Canvas.SetTop(myPath, kettengliedRegal.GetPosY());

                mainWindow.ZeichenFlaeche.Children.Add(myPath);
            }
        }
    }
}