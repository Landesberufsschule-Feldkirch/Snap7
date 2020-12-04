using System;
using System.Drawing;
using Kommunikation;
using Synchronisiereinrichtung.SetManual;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using ScottPlot;

namespace Synchronisiereinrichtung
{
    public partial class MainWindow
    {
        public bool DebugWindowAktiv { get; set; }
        public IPlc Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }


        public double[] Zeitachse = new double[100];
        public double[] KesselTemperatur = new double[100];
        public double[] VorlaufSolltemperatur = new double[100];
        private int _nextDataIndex = 1;




        private SetManualWindow _setManualWindow;
        private PlotWindow.PlotWindow _plotWindow;
        private readonly ViewModel.ViewModel _viewModel;
        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 20;
        private const int AnzByteAnalogOutput = 4;

        public MainWindow()
        {
            const string versionText = "Synchronisiereinrichtung";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInputSps = Encoding.ASCII.GetBytes(VersionInfoLokal)
            };

            _viewModel = new ViewModel.ViewModel(this);

            InitializeComponent();

            DataContext = _viewModel;

            var datenRangieren = new DatenRangieren(this, _viewModel);


            //  GaugeDifferenzSpannung.DataContext = _viewModel;
            // GaugeDifferenzSpannung.ApplyTemplate();

            Plc = new S71200(Datenstruktur, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            BtnDebugWindow.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            _setManualWindow = new SetManualWindow(_viewModel);
            _setManualWindow.Show();
        }

        private void GraphWindow_Click(object sender, RoutedEventArgs e)
        {
            Zeitachse = DataGen.Consecutive(100);

            _plotWindow = new PlotWindow.PlotWindow();
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


            // KesselTemperatur[_nextDataIndex] = WohnHaus.KesselTemperatur;
            //  VorlaufSolltemperatur[_nextDataIndex] = WohnHaus.VorlaufSolltemperatur;

            _nextDataIndex++;
        }

        private void Render(object sender, EventArgs e)
        {
            _plotWindow.WpfPlot.plt.AxisAuto(0);
            _plotWindow.WpfPlot.Render(true);
        }
    }
}