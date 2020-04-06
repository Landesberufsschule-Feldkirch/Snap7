using Kommunikation;
using PaternosterLager.ViewModel;
using System;
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
            Rectangle rect = new Rectangle
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Black
            };


            Canvas.SetLeft(rect, 150 - 20 / 2);
            Canvas.SetTop(rect, 0);

            ZeichenFlaeche.Children.Add(rect);

            foreach(KettengliedRegal kettengliedRegal in KomplettesKettenRegal.AlleKettengliedRegale)
            {
                Path myPath = new Path();
                myPath.Fill = Brushes.LemonChiffon;
                myPath.Stroke = Brushes.Black;
                myPath.StrokeThickness = 1;
                myPath.Data = kettengliedRegal.GetKettengliedRegal();

                Canvas.SetLeft(myPath, 10);

                ZeichenFlaeche.Children.Add(myPath);
            }
        }
    }
}
