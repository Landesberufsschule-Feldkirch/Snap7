using Kommunikation;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace LaborGetriebemotor
{
    public partial class MainWindow
    {
        public S71200.BetriebsartProjekt BetriebsartProjekt { get; set; }
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
            BetriebsartProjekt = S71200.BetriebsartProjekt.LaborPlatte;

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

            ManualMode = new ManualMode.ManualMode(Datenstruktur, Plc, BetriebsartProjekt, DatenRangieren.RangierenInput, DatenRangieren.RangierenOutput);
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Di, "./ManualConfig/DI.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Da, "./ManualConfig/DA.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Ai, "./ManualConfig/AI.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Aa, "./ManualConfig/AA.json");

            BtnManualMode.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;

            Plc = new S71200(Datenstruktur, ManualMode.Datenstruktur.DigInput, DatenRangieren.RangierenInput, DatenRangieren.RangierenOutput);

            TestAutomat = new TestAutomat.TestAutomat(Datenstruktur, ManualMode);
            TestAutomat.SetTestConfig("./AutoTestConfig/");
            TestAutomat.TabItemFuellen(TabItemAutomatischerSoftwareTest);
        }
        private void ManualModeOeffnen(object sender, RoutedEventArgs e) => ManualMode.ManualModeStarten();
        private void BetriebsartProjektChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (sender is not TabControl tc) return;
            BetriebsartProjekt = tc.SelectedIndex switch
            {
                0 => S71200.BetriebsartProjekt.LaborPlatte,
                1 => S71200.BetriebsartProjekt.Simulation,
                2 => S71200.BetriebsartProjekt.AutomatischerSoftwareTest,
                _ => S71200.BetriebsartProjekt.LaborPlatte
            };
            Plc.SetBetriebsartProjekt(BetriebsartProjekt);
        }
    }
}