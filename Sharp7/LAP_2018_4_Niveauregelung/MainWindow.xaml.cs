using Kommunikation;
using System.Text;
using System.Windows;

namespace LAP_2018_4_Niveauregelung
{
    public partial class MainWindow
    {
        public IPlc Plc { get; set; }
        public ManualMode.ManualMode ManualMode { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        private readonly DatenRangieren _datenRangieren;
        public readonly ViewModel.ViewModel ViewModel;
        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "LAP 2018/4 Niveauregelung";
            VersionNummer = "V2.0";
            VersionInfo = versionText + " - " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInput = Encoding.ASCII.GetBytes(VersionInfo)
            };

            ViewModel = new ViewModel.ViewModel(this);

            _datenRangieren = new DatenRangieren(ViewModel);

            InitializeComponent();
            DataContext = ViewModel;

            Plc = new S71200(Datenstruktur, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);

            ManualMode = new ManualMode.ManualMode(Datenstruktur);

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
                Plc = new Manual(Datenstruktur, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);
            }

            ManualMode.FensterAnzeigen();
        }
    }
}