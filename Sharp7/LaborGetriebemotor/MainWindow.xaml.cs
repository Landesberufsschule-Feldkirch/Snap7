using Kommunikation;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace LaborGetriebemotor
{
    public partial class MainWindow
    {
        public IPlc Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public ManualMode.ManualMode ManualMode { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public TestAutomat.TestAutomat TestAutomat { get; set; }
        public DatenRangieren DatenRangieren { get; set; }

        private const int AnzByteDigInput = 2;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "Labor Getriebemotor";
            VersionNummer = "V2.0";

            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInputSps = Encoding.ASCII.GetBytes(VersionInfoLokal)
            };

            var viewModel = new ViewModel.ViewModel(this);
            DatenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            Plc = new S71200(Datenstruktur, DatenRangieren.RangierenInput, DatenRangieren.RangierenOutput);

            ManualMode = new ManualMode.ManualMode(Datenstruktur, Plc, DatenRangieren.RangierenInput, DatenRangieren.RangierenOutput);
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Di, "./ManualConfig/DI.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Da, "./ManualConfig/DA.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Ai, "./ManualConfig/AI.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Aa, "./ManualConfig/AA.json");

            BtnManualMode.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;

            Plc.SetManualModeReferenz(ManualMode.Datenstruktur);

            TestAutomat = new TestAutomat.TestAutomat(Datenstruktur, ManualMode);
            TestAutomat.SetTestConfig("./AutoTestConfig/");
            TestAutomat.TabItemFuellen(TabItemAutomatischerSoftwareTest);
        }
        private void ManualModeOeffnen(object sender, RoutedEventArgs e) => ManualMode.ManualModeStarten();
        private void BetriebsartProjektChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not TabControl tc) return;

            switch (tc.SelectedIndex)
            {
                case 0: Datenstruktur.SetBetriebsartProjekt(BetriebsartProjekt.LaborPlatte); break;
                case 1: Datenstruktur.SetBetriebsartProjekt(BetriebsartProjekt.Simulation); break;
                case 2: Datenstruktur.SetBetriebsartProjekt(BetriebsartProjekt.AutomatischerSoftwareTest); break;
            }

            ManualMode.SetSichtbarkeitFenster();
        }
    }
}