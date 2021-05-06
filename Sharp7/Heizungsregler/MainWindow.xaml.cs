using Heizungsregler.Model;
using Kommunikation;
using ScottPlot;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Threading;

namespace Heizungsregler
{
    public partial class MainWindow
    {
        public IPlc Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public WohnHaus WohnHaus { get; set; }
        public ConfigPlc.Plc ConfigPlc { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public DatenRangieren DatenRangieren { get; set; }
        public double[] Zeitachse { get; set; } = new double[100];
        public double[] KesselTemperatur { get; set; } = new double[100];
        public double[] VorlaufSolltemperatur { get; set; } = new double[100];

        private PlotWindow _plotWindow;
        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;
        private int _nextDataIndex = 1;

        public MainWindow()
        {
            const string versionText = "Heizungsregler";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);

            var viewModel = new ViewModel.ViewModel(this);

            InitializeComponent();
            DataContext = viewModel;

            WohnHaus = new WohnHaus();

            DatenRangieren = new DatenRangieren(this, viewModel);

            var befehlszeile = Environment.GetCommandLineArgs();
            if (befehlszeile.Length == 2 && befehlszeile[1].Contains("CX9020")) Plc = new Cx9020(Datenstruktur, DatenRangieren.Rangieren);
            else Plc = new S71200(Datenstruktur, DatenRangieren.Rangieren);

            DatenRangieren.ReferenzUebergeben(Plc);

            Title = Plc.GetPlcBezeichnung() + ": " + versionText + " " + VersionNummer;

            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");
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