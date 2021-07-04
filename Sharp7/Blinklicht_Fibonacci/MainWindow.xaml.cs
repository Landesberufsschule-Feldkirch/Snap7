using BeschriftungPlc;
using Kommunikation;
using ScottPlot;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Blinklicht_Fibonacci
{
    public partial class MainWindow
    {
        public IPlc Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public ConfigPlc.Plc ConfigPlc { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public double[] WertLeuchtMelder { get; set; } = new double[5_000];
        public TestAutomat.TestAutomat TestAutomat { get; set; }
        public DisplayPlc.DisplayPlc DisplayPlc { get; set; }
        public BeschriftungenPlc BeschriftungenPlc { get; set; }
        public DatenRangieren DatenRangieren { get; set; }

        private Plot _plot;

        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;
        private readonly ViewModel.ViewModel _viewModel;
        private int _nextDataIndex = 1;

        public MainWindow()
        {
            const string versionText = "Blinklicht Fibonacci";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);


            _viewModel = new ViewModel.ViewModel(this);
            DatenRangieren = new DatenRangieren(_viewModel);

            InitializeComponent();

            DataContext = _viewModel;

            var befehlszeile = Environment.GetCommandLineArgs();
            Plc = befehlszeile.Length == 2 && befehlszeile[1].Contains("CX9020")
                ? new Cx9020(Datenstruktur, DatenRangieren.Rangieren)
                : new S71200(Datenstruktur, DatenRangieren.Rangieren);

            Title = Plc.GetPlcBezeichnung() + ": " + versionText + " " + VersionNummer;

            DatenRangieren.ReferenzUebergeben(Plc);

            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");
            
            BeschriftungenPlc = new BeschriftungenPlc();
            DisplayPlc = new DisplayPlc.DisplayPlc(Datenstruktur, ConfigPlc, BeschriftungenPlc);

            TestAutomat = new TestAutomat.TestAutomat(Datenstruktur, DisplayPlc.EventBeschriftungAktualisieren, BeschriftungenPlc, Plc);
            TestAutomat.SetTestConfig("./ConfigTests/");
            TestAutomat.TabItemFuellen(TabItemAutomatischerSoftwareTest, DisplayPlc);

            Closing += (_, e) =>
            {
                e.Cancel = true;
                Schliessen();
            };

            /*
 WpfPlot.plt.YLabel("Leuchtmelder");
                        WpfPlot.plt.XLabel("Zeit [ms]");
                        WpfPlot.plt.PlotScatter(zeitachse, WertLeuchtMelder, Color.Magenta, label: "LED");
            */

            var zeitachse = DataGen.Consecutive(5000);
            _plot = new Plot();
            _plot.XAxis.Label("Zeit [ms]");
            _plot.YAxis.Label("Leuchtmelder");
            _plot.AddScatter(zeitachse, WertLeuchtMelder, Color.Magenta, 1, 1, MarkerShape.none, LineStyle.Solid, "LED");
           
            

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

        private void BetriebsartProjektChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not TabControl tc) return;

            // ReSharper disable once ConvertSwitchStatementToSwitchExpression
            switch (tc.SelectedIndex)
            {
                case 0: Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.Simulation; break;
                case 1: Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.AutomatischerSoftwareTest; break;
            }

            DisplayPlc.SetBetriebsartProjekt(Datenstruktur);
        }

        // ReSharper disable once UnusedParameter.Local
        private void Schliessen()
        {
            DisplayPlc.TaskBeenden();
            TestAutomat.TaskBeenden();
            Application.Current.Shutdown();
        }
        private void UpdateData(object sender, EventArgs e)
        {
            if (_nextDataIndex >= 4_990)
            {
                _nextDataIndex = 0;
            }

            for (var i = 0; i < 10; i++)
            {
                WertLeuchtMelder[_nextDataIndex + i] = _viewModel.BlinklichtFibonacci.P1 ? 1 : 0;
            }
            _nextDataIndex += 10;
        }
        private void Render(object sender, EventArgs e)
        {
            _plot.Render();
         //   WpfPlot.plt.AxisAuto(0);
          //  WpfPlot.Render(true);
        }
    }
}