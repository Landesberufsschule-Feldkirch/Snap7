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
        public ConfigPlc.Plc ConfigPlc { get; set; }
        public double[] WertLeuchtMelder { get; set; } = new double[5_000];
        public DatenRangieren DatenRangieren { get; set; }

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

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);

            _viewModel = new ViewModel.ViewModel(this);
            DatenRangieren = new DatenRangieren(_viewModel);

            InitializeComponent();
            DataContext = _viewModel;

            Plc = new S71200(Datenstruktur, DatenRangieren.RangierenInput, DatenRangieren.RangierenOutput);
            Plc.SetZyklusZeitKommunikation(2);

            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");

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

            Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.Simulation;
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
    }
}
