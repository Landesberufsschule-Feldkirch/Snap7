using Kommunikation;
using PaternosterLager.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaternosterLager
{

    public partial class MainWindow : Window
    {
        public PaternosterLager.ViewModel.KomplettesKettenRegal KomplettesKettenRegal { get; set; }

        public SetManual.SetManualWindow SetManualWindow { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.ViewModel viewModel;

        public MainWindow()
        {
            viewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(this, viewModel);

            InitializeComponent();
            DataContext = viewModel;

            S7_1200 = new S7_1200(2, 2, 2, 2, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            KomplettesKettenRegal = new KomplettesKettenRegal();
            LagerHinzufuegen();
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManualWindow(viewModel);
            SetManualWindow.Show();
        }

        private void LagerHinzufuegen()
        {
            foreach(var kettengliedRegal in KomplettesKettenRegal.AlleKettengliedRegale)
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

                ZeichenFlaeche.Children.Add(myPath);
            }
        }
    }
}
