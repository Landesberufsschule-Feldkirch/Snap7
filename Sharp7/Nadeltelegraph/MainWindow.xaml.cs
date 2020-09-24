using Kommunikation;

using ManualMode;

namespace Nadeltelegraph
{
    public partial class MainWindow
    {
        public bool DebugWindowAktiv { get; set; }
        public SetManual.SetManual SetManualWindow { get; set; }
        public IPlc Plc { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }

        public ManualMode.MainWindow ManualMode { get; set; }

        private readonly SetManual.View.View _viewManual;
        private readonly DatenRangieren _datenRangieren;

        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 2;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;


        public MainWindow()
        {
            const string versionText = "Nadeltelegraph";
            VersionNummer = "V2.0";
            VersionInfo = versionText + " - " + VersionNummer;

            var viewModel = new ViewModel.ViewModel(this);
            _viewManual = new SetManual.View.View(this);
            _datenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            Plc = new S7_1200(VersionInfo.Length, AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);





        }



        private void ManualModeOeffnen(object sender, System.Windows.RoutedEventArgs e)
        {

            //  _mainWindowManualMode.Show();


            if (Plc.GetModel() == "S7-1200")
            {
                Plc.SetTaskRunning(false);
                Plc = new Manual(VersionInfo.Length, AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);
            }



            ManualMode = new ManualMode.MainWindow(Plc.DigInput, Plc.DigOutput, Plc.AnalogInput, Plc.AnalogOutput, AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);
            //ManualMode.SetSnap7Arrays(Plc.)

            ManualMode.SetManualConfig(global::ManualMode.MainWindow.ManualModeConfig.Di, "./ManualConfig/DI.json");
            ManualMode.SetManualConfig(global::ManualMode.MainWindow.ManualModeConfig.Da, "./ManualConfig/DA.json");
            ManualMode.SetManualConfig(global::ManualMode.MainWindow.ManualModeConfig.Ai, "./ManualConfig/AI.json");
            ManualMode.SetManualConfig(global::ManualMode.MainWindow.ManualModeConfig.Aa, "./ManualConfig/AA.json");

            ManualMode.FensterAnzeigen();

            DebugWindowAktiv = true;
            //SetManualWindow = new SetManual.SetManual(_viewManual);
            //SetManualWindow.Show();


        }
    }
}