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
        public PlcDaemon PlcDaemon { get; set; }
        public string VersionInfoLokal { get; set; }
        public ConfigPlc.Plc ConfigPlc { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public double[] WertLeuchtMelder { get; set; } = new double[5_000];
        public TestAutomat.TestAutomat TestAutomat { get; set; }
        public DisplayPlc.DisplayPlc DisplayPlc { get; set; }
        public BeschriftungenPlc BeschriftungenPlc { get; set; }
        public DatenRangieren DatenRangieren { get; set; }


        private readonly ViewModel.ViewModel _viewModel;
        private int _nextDataIndex = 1;

        public MainWindow()
        {
            const string versionText = "Blinklicht Fibonacci";
            const string versionNummer = "V2.0";

            const int anzByteDigInput = 1;
            const int anzByteDigOutput = 1;
            const int anzByteAnalogInput = 0;
            const int anzByteAnalogOutput = 0;


            VersionInfoLokal = versionText + " " + versionNummer;

            Datenstruktur = new Datenstruktur(anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput);
            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");
            BeschriftungenPlc = new BeschriftungenPlc();

            var viewModel = new ViewModel.ViewModel(this);
            InitializeComponent();
            DataContext = viewModel;

            DatenRangieren = new DatenRangieren(viewModel);
            PlcDaemon = new PlcDaemon(Datenstruktur, DatenRangieren.Rangieren);
            DatenRangieren.ReferenzUebergeben(PlcDaemon.Plc);

            DisplayPlc = new DisplayPlc.DisplayPlc(Datenstruktur, ConfigPlc, BeschriftungenPlc);

            TestAutomat = new TestAutomat.TestAutomat(Datenstruktur, DisplayPlc.EventBeschriftungAktualisieren, BeschriftungenPlc, PlcDaemon.Plc);
            TestAutomat.SetTestConfig("./ConfigTests/");
            TestAutomat.TabItemFuellen(TabItemAutomatischerSoftwareTest, DisplayPlc);

            Closing += (_, e) =>
            {
                e.Cancel = true;
                Schliessen();
            };

            var zeitachse = DataGen.Consecutive(5000);
            WpfPlot.Plot.XAxis.Label("Zeit [ms]");
            WpfPlot.Plot.YLabel("Leuchtmelder");
            WpfPlot.Plot.AddScatter(zeitachse, WertLeuchtMelder, Color.Magenta, 1, 1, MarkerShape.none, LineStyle.Solid, "LED");

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
            WpfPlot.Plot.Render();
        }
        private void PlcButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DisplayPlc.FensterAktiv) DisplayPlc.Schliessen();
            else DisplayPlc.Oeffnen();
        }
    }
}