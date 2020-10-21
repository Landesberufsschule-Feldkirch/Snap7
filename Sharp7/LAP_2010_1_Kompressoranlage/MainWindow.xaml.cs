using System;
using Kommunikation;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using GaugeControl = GaugeControl.GaugeControl;

namespace LAP_2010_1_Kompressoranlage
{
    public partial class MainWindow
    {
        public IPlc Plc { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public ManualMode.ManualMode ManualMode { get; set; }

        private readonly DatenRangieren _datenRangieren;
        private readonly ViewModel.ViewModel _viewModel;

        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "LAP 2010/1 Kompressoranlage";
            VersionNummer = "V2.0";

            VersionInfo = versionText + " - " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInput = Encoding.ASCII.GetBytes(VersionInfo)
            };


            _viewModel = new ViewModel.ViewModel(this);
            _datenRangieren = new DatenRangieren(_viewModel);
            
            InitializeComponent();
            DataContext = _viewModel;

            Plc = new S71200(Datenstruktur, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);

            ManualMode = new ManualMode.ManualMode(Datenstruktur);

            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Di, "./ManualConfig/DI.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Da, "./ManualConfig/DA.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Ai, "./ManualConfig/AI.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Aa, "./ManualConfig/AA.json");

            BtnManualMode.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;

            // create a timer to update the GUI
            var renderTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            renderTimer.Tick += UpdateDruckAnzeige;
            renderTimer.Start();
        }

        private void UpdateDruckAnzeige(object sender, EventArgs e)
        {

            //DruckAnzeige.setValue(_viewModel.Kompressoranlage.Druck);
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