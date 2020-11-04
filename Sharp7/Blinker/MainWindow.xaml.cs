using Kommunikation;
using ScottPlot;
using System;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace Blinker
{
    public partial class MainWindow
    {
        public IPlc Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public ManualMode.ManualMode ManualMode { get; set; }
        public double[] WertLeuchtMelder = new double[5_000];

        private readonly DatenRangieren _datenRangieren;
        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        private readonly ViewModel.ViewModel _viewModel;
        private int _nextDataIndex = 1;

        public MainWindow()
        {
            const string versionText = "Blinker";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInputSps = Encoding.ASCII.GetBytes(VersionInfoLokal)
            };

            _viewModel = new ViewModel.ViewModel(this);
            _datenRangieren = new DatenRangieren(_viewModel);



            InitializeComponent();
            DataContext = _viewModel;

            Plc = new S71200(Datenstruktur, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);
            Plc.SetZyklusZeitKommunikation(2);

            ManualMode = new ManualMode.ManualMode(Datenstruktur);

            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Di, "./ManualConfig/DI.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Da, "./ManualConfig/DA.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Ai, "./ManualConfig/AI.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Aa, "./ManualConfig/AA.json");

            BtnManualMode.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;



            var zeitachse = DataGen.Consecutive(5000);


            WpfPlot.plt.YLabel("Leuchtmelder");
            WpfPlot.plt.XLabel("Zeit [ms]");

            WpfPlot.plt.PlotScatter(zeitachse, WertLeuchtMelder, Color.Magenta, label: "LED");


            // create a timer to modify the data
            var updateDataTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            updateDataTimer.Tick += UpdateData;
            updateDataTimer.Start();

            // create a timer to update the GUI
            var renderTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            renderTimer.Tick += Render;
            renderTimer.Start();
        }


        private void UpdateData(object sender, EventArgs e)
        {
            if (_nextDataIndex >= 4_990)
            {
                _nextDataIndex = 0;
            }

            for (var i = 0; i < 10; i++)
            {
                WertLeuchtMelder[_nextDataIndex + i] = _viewModel.Blinker.P1 ? 1 : 0;
            }
            _nextDataIndex += 10;
        }

        private void Render(object sender, EventArgs e)
        {
            WpfPlot.plt.AxisAuto(0);
            WpfPlot.Render(true);
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
