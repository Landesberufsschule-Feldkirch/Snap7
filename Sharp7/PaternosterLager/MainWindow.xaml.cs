using System.Text;
using Kommunikation;
using System.Windows;

namespace PaternosterLager
{
    public partial class MainWindow
    {
        public SetManual.SetManualWindow SetManualWindow { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public IPlc Plc { get; set; }
        public bool FensterAktiv { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }
        public const double AnzahlKisten = 16;
        public Datenstruktur Datenstruktur { get; set; }

        private readonly DatenRangieren _datenRangieren;
        private readonly ViewModel.ViewModel _viewModel;
        private const int AnzByteDigInput = 2;
        private const int AnzByteDigOutput = 2;
        private const int AnzByteAnalogInput = 2;
        private const int AnzByteAnalogOutput = 2;

        public MainWindow()
        {
            const string versionText = "Paternosterlager";
            VersionNummer = "V2.0";
            VersionInfo = versionText + " - " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInput = Encoding.ASCII.GetBytes(VersionInfo)
            };

            FensterAktiv = true;
            _viewModel = new ViewModel.ViewModel(this, AnzahlKisten);
            _datenRangieren = new DatenRangieren(this, _viewModel);

            InitializeComponent();
            DataContext = _viewModel;
            Plc = new S7_1200(Datenstruktur, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);

            BtnDebugWindow.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManualWindow(_viewModel);
            SetManualWindow.Show();
        }

        private void PolygonAufPressed(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S1 = true;
        private void PolygonAufReleased(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S1 = false;
        private void PolygonAbPressed(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S2 = true;
        private void PolygonAbReleased(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S2 = false;
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e) => FensterAktiv = false;
    }
}