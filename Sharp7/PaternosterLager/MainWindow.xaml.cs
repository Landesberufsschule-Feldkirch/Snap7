using Kommunikation;
using System.Windows;

namespace PaternosterLager
{
    public partial class MainWindow : Window
    {
        public SetManual.SetManualWindow SetManualWindow { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public S7_1200 S7_1200 { get; set; }
        public bool FensterAktiv { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.ViewModel viewModel;

        public MainWindow()
        {
            FensterAktiv = true;
            viewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(this, viewModel);

            InitializeComponent();
            DataContext = viewModel;
            S7_1200 = new S7_1200(2, 2, 2, 2, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManualWindow(viewModel);
            SetManualWindow.Show();
        }

        private void PolygonAufPressed(object sender, System.Windows.Input.MouseButtonEventArgs e) => viewModel.paternosterlager.S1 = true;

        private void PolygonAufReleased(object sender, System.Windows.Input.MouseButtonEventArgs e) => viewModel.paternosterlager.S1 = false;

        private void PolygonAbPressed(object sender, System.Windows.Input.MouseButtonEventArgs e) => viewModel.paternosterlager.S2 = true;

        private void PolygonAbReleased(object sender, System.Windows.Input.MouseButtonEventArgs e) => viewModel.paternosterlager.S2 = false;

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e) => FensterAktiv = false;
    }
}