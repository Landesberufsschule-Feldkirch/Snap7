using Kommunikation;
using System.Text;
using System.Windows;

namespace PaternosterLager
{
    public partial class MainWindow
    {
        public ManualMode.ManualMode ManualMode { get; set; }
        public IPlc Plc { get; set; }
        public bool FensterAktiv { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public const double AnzahlKisten = 16;
        public Datenstruktur Datenstruktur { get; set; }

        public DatenRangieren DatenRangieren { get; set; }
        private readonly ViewModel.ViewModel _viewModel;
        private const int AnzByteDigInput = 2;
        private const int AnzByteDigOutput = 2;
        private const int AnzByteAnalogInput = 2;
        private const int AnzByteAnalogOutput = 2;

        public MainWindow()
        {
            const string versionText = "Paternosterlager";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInputSps = Encoding.ASCII.GetBytes(VersionInfoLokal)
            };

            FensterAktiv = true;
            _viewModel = new ViewModel.ViewModel(this, AnzahlKisten);
            DatenRangieren = new DatenRangieren(_viewModel);

            InitializeComponent();
            DataContext = _viewModel;
            Plc = new S71200(Datenstruktur, DatenRangieren.RangierenInput, DatenRangieren.RangierenOutput);

            ManualMode = new ManualMode.ManualMode(Datenstruktur, Plc,  DatenRangieren.RangierenInput, DatenRangieren.RangierenOutput);

            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Di, "./ManualConfig/DI.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Da, "./ManualConfig/DA.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Ai, "./ManualConfig/AI.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Aa, "./ManualConfig/AA.json");

            BtnManualMode.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;
        }

        private void ManualModeOeffnen(object sender, RoutedEventArgs e)
        {
            if (Plc.GetPlcModus() == "S7-1200")
            {
                Plc.SetTaskRunning(false);
                Plc = new Manual(Datenstruktur, DatenRangieren.RangierenInput, DatenRangieren.RangierenOutput);
            }

            ManualMode.FensterAnzeigen();
        }

        private void PolygonAufPressed(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S1 = true;
        private void PolygonAufReleased(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S1 = false;
        private void PolygonAbPressed(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S2 = true;
        private void PolygonAbReleased(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S2 = false;
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e) => FensterAktiv = false;
    }
}