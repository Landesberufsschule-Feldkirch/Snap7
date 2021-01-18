using Heizungsregler.Model;
using Kommunikation;
using ScottPlot;
using System;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Threading;


namespace Heizungsregler
{
    public partial class MainWindow
    {
        public S71200.BetriebsartProjekt BetriebsartProjekt { get; set; }
        public IPlc Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public WohnHaus WohnHaus { get; set; }
        public ManualMode.ManualMode ManualMode { get; set; }
        public Datenstruktur Datenstruktur { get; set; }


        private PlotWindow _plotWindow;
        public DatenRangieren DatenRangieren { get; set; }
        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public double[] Zeitachse = new double[100];
        public double[] KesselTemperatur = new double[100];
        public double[] VorlaufSolltemperatur = new double[100];
        private int _nextDataIndex = 1;

        public MainWindow()
        {
            BetriebsartProjekt = S71200.BetriebsartProjekt.LaborPlatte;
            
            const string versionText = "Heizungsregler";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInputSps = Encoding.ASCII.GetBytes(VersionInfoLokal)
            };

            var viewModel = new ViewModel.ViewModel(this);

            InitializeComponent();
            DataContext = viewModel;

            WohnHaus = new WohnHaus();

            DatenRangieren = new DatenRangieren(this, viewModel);

            Plc = new S71200(Datenstruktur, DatenRangieren.RangierenInput, DatenRangieren.RangierenOutput);

            ManualMode = new ManualMode.ManualMode(Datenstruktur, Plc, BetriebsartProjekt, DatenRangieren.RangierenInput, DatenRangieren.RangierenOutput);

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

        private void PlotWindowOeffnen(object sender, RoutedEventArgs e)
        {
            Zeitachse = DataGen.Consecutive(100);

            _plotWindow = new PlotWindow();
            _plotWindow.Show();

            _plotWindow.WpfPlot.plt.YLabel("Temperatur");
            _plotWindow.WpfPlot.plt.XLabel("Zeit");

            _plotWindow.WpfPlot.plt.PlotScatter(Zeitachse, KesselTemperatur, Color.Magenta, label: "Kesseltemperatur");
            _plotWindow.WpfPlot.plt.PlotScatter(Zeitachse, VorlaufSolltemperatur, Color.Green, label: "VorlaufSolltemperatur");
            _plotWindow.WpfPlot.plt.Legend(fixedLineWidth: false);


            // create a timer to modify the data
            var updateDataTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            updateDataTimer.Tick += UpdateData;
            updateDataTimer.Start();

            // create a timer to update the GUI
            var renderTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            renderTimer.Tick += Render;
            renderTimer.Start();
        }


        private void UpdateData(object sender, EventArgs e)
        {
            if (_nextDataIndex >= 100)
            {
                _nextDataIndex = 0;
            }


            KesselTemperatur[_nextDataIndex] = WohnHaus.KesselTemperatur;
            VorlaufSolltemperatur[_nextDataIndex] = WohnHaus.VorlaufSolltemperatur;

            _nextDataIndex++;
        }

        private void Render(object sender, EventArgs e)
        {
            _plotWindow.WpfPlot.plt.AxisAuto(0);
            _plotWindow.WpfPlot.Render(true);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => Application.Current.Shutdown();

    }
}