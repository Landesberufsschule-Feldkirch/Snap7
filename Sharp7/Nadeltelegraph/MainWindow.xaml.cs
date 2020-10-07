using System.Text;
using Kommunikation;

namespace Nadeltelegraph
{
    public partial class MainWindow
    {
        public IPlc Plc { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }
        public ManualMode.ManualMode ManualMode { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        private readonly DatenRangieren _datenRangieren;

        private const int AnzByteDigInput = 0;
        private const int AnzByteDigOutput = 2;
        private const int AnzByteAnalogInput = 1;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "Nadeltelegraph";
            VersionNummer = "V2.0";

            VersionInfo = versionText + " - " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInput = Encoding.ASCII.GetBytes(VersionInfo)
            };

            var viewModel = new ViewModel.ViewModel(this);
             _datenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            Plc = new S7_1200(Datenstruktur, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);

            ManualMode = new ManualMode.ManualMode(Datenstruktur);

            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Di, "./ManualConfig/DI.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Da, "./ManualConfig/DA.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Ai, "./ManualConfig/AI.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Aa, "./ManualConfig/AA.json");
        }

        private void ManualModeOeffnen(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Plc.GetModel() == "S7-1200")
            {
                Plc.SetTaskRunning(false);
                Plc = new Manual(Datenstruktur, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);
            }

            ManualMode.FensterAnzeigen();
        }
    }
}