namespace PaternosterLager.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class KomplettesKettenRegal
    {
        public ObservableCollection<KettengliedRegal> AlleKettengliedRegale { get; set; }
        private readonly MainWindow mainWindow;

        public KomplettesKettenRegal(MainWindow mw)
        {
            mainWindow = mw;
            AlleKettengliedRegale = new ObservableCollection<KettengliedRegal>();
            for (var i = 0; i < 20; i++) AlleKettengliedRegale.Add(new KettengliedRegal(i));
        }


        public void SetGeschwindigkeit(double geschwindigkeit)
        {
            foreach (var kettengliedRegal in AlleKettengliedRegale)
            {
                kettengliedRegal.SetGeschwindigkeit(geschwindigkeit);
            }
        }

        public void LagerHinzufuegen()
        {
            foreach (var kettengliedRegal in AlleKettengliedRegale)
            {
                
                var myPath = new Path
                {
                    Fill = Brushes.LemonChiffon,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    Data = kettengliedRegal.GetKettengliedRegal()
                };

                Canvas.SetLeft(myPath, kettengliedRegal.getPosX());
                Canvas.SetTop(myPath, kettengliedRegal.getPosY());

                mainWindow.ZeichenFlaeche.Children.Add(myPath);
                
            }
        }
    }
}