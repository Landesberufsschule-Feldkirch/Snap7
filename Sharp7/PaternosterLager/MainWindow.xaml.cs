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
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }
        private readonly string VersionText;
        public const double AnzahlKisten = 16;

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.ViewModel viewModel;
        private const int anzByteDigInput = 2;
        private const int anzByteDigOutput = 2;
        private const int anzByteAnalogInput = 2;
        private const int anzByteAnalogOutput = 2;

        public MainWindow()
        {
            VersionText = "Paternosterlager";
            VersionNummer = "V2.0";
            VersionInfo = VersionText + " - " + VersionNummer;

            FensterAktiv = true;
            viewModel = new ViewModel.ViewModel(this, AnzahlKisten);
            datenRangieren = new DatenRangieren(this, viewModel);

            InitializeComponent();
            DataContext = viewModel;
            S7_1200 = new S7_1200(VersionInfo.Length, anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            if (System.Diagnostics.Debugger.IsAttached) btnDebugWindow.Visibility = System.Windows.Visibility.Visible;
            else btnDebugWindow.Visibility = System.Windows.Visibility.Hidden;
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