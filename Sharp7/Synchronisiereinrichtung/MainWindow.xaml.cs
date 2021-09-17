using Kommunikation;
using ScottPlot;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Synchronisiereinrichtung
{
    public partial class MainWindow
    {
        public bool DebugWindowAktiv { get; set; }
        public IPlc Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public ConfigPlc.Plc ConfigPlc { get; set; }

        public double[] Zeitachse { get; set; }
        public double[] PlotVentilOeffnung { get; set; }
        public double[] PlotErregerstrom { get; set; }
        public double[] PlotFrequenz { get; set; }
        public double[] PlotGeneratorSpannung { get; set; }
        public double[] PlotSpannungsdifferenz { get; set; }
        public double[] PlotLeistung { get; set; }
        public DatenRangieren DatenRangieren { get; set; }

        private int _nextDataIndex = 1;
        private PlotWindow.PlotWindow _plotWindow;
        private readonly ViewModel.ViewModel _viewModel;
        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 20;
        private const int AnzByteAnalogOutput = 4;

        public MainWindow()
        {
            Zeitachse = new double[1000];
            PlotVentilOeffnung = new double[1000];
            PlotErregerstrom = new double[1000];
            PlotFrequenz = new double[1000];
            PlotGeneratorSpannung = new double[1000];
            PlotSpannungsdifferenz = new double[1000];
            PlotLeistung = new double[1000];

            const string versionText = "Synchronisiereinrichtung";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);

            _viewModel = new ViewModel.ViewModel(this);

            InitializeComponent();
            DataContext = _viewModel;

            DatenRangieren = new DatenRangieren(this, _viewModel);

            var befehlszeile = Environment.GetCommandLineArgs();
            Plc = befehlszeile.Length == 2 && befehlszeile[1].Contains("CX9020")
                ? new Cx9020(Datenstruktur, DatenRangieren.Rangieren)
                : new S71200(Datenstruktur, DatenRangieren.Rangieren);

            DatenRangieren.ReferenzUebergeben(Plc);

            Title = Plc.GetPlcBezeichnung() + ": " + versionText + " " + VersionNummer;


            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GraphWindow_Click(object sender, RoutedEventArgs e)
        {
            Zeitachse = DataGen.Consecutive(1000);

            _plotWindow = new PlotWindow.PlotWindow();
            _plotWindow.Show();

            _plotWindow.WpfPlot.Plot.YLabel("Ventilöffnung Y");
            _plotWindow.WpfPlot.Plot.YLabel("Erregerstrom IE");
            _plotWindow.WpfPlot.Plot.YLabel("Frequenz f");
            _plotWindow.WpfPlot.Plot.YLabel("Generatorspannung UG");
            _plotWindow.WpfPlot.Plot.YLabel("Spannungsdifferenz Ud");
            _plotWindow.WpfPlot.Plot.YLabel("Leistung P");

            _plotWindow.WpfPlot.Plot.XLabel("Zeit");

            _plotWindow.WpfPlot.Plot.AddScatter(Zeitachse, PlotVentilOeffnung, label: "Kesseltemperatur");
            _plotWindow.WpfPlot.Plot.AddScatter(Zeitachse, PlotErregerstrom, label: "Erregerstrom");
            _plotWindow.WpfPlot.Plot.AddScatter(Zeitachse, PlotFrequenz, label: "Frequenz");
            _plotWindow.WpfPlot.Plot.AddScatter(Zeitachse, PlotGeneratorSpannung, label: "Generatorspannung");
            _plotWindow.WpfPlot.Plot.AddScatter(Zeitachse, PlotSpannungsdifferenz, label: "Spannungsdifferenz");
            _plotWindow.WpfPlot.Plot.AddScatter(Zeitachse, PlotLeistung, label: "Leistung");

            _plotWindow.WpfPlot.Plot.Legend();

            _plotWindow.WpfPlot.Plot.XLabel("Zeit");
            _plotWindow.WpfPlot.Plot.YLabel("Y");

            // create a timer to modify the data
            var updateDataTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            updateDataTimer.Tick += UpdateData;
            updateDataTimer.Start();

            // create a timer to update the GUI
            var renderTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            renderTimer.Tick += Render;
            renderTimer.Start();
        }

        private void UpdateData(object sender, EventArgs e)
        {
            if (_nextDataIndex >= 1000) _nextDataIndex = 0;

            PlotVentilOeffnung[_nextDataIndex] = _viewModel.Kraftwerk.VentilY;
            PlotErregerstrom[_nextDataIndex] = _viewModel.Kraftwerk.GeneratorIe;
            PlotFrequenz[_nextDataIndex] = _viewModel.Kraftwerk.GeneratorF;
            PlotGeneratorSpannung[_nextDataIndex] = _viewModel.Kraftwerk.GeneratorU;
            PlotSpannungsdifferenz[_nextDataIndex] = _viewModel.Kraftwerk.SpannungsDifferenz;
            PlotLeistung[_nextDataIndex] = _viewModel.Kraftwerk.GeneratorP;

            _nextDataIndex++;
        }

        private void Render(object sender, EventArgs e)
        {
            _plotWindow.WpfPlot.Plot.AxisAuto(0);
            _plotWindow.WpfPlot.Render();
        }
    }
}