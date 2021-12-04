using BeschriftungPlc;
using Heizungsregler.Model;
using Kommunikation;
using ScottPlot;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Threading;

namespace Heizungsregler;

public partial class MainWindow
{
    public PlcDaemon PlcDaemon { get; set; }
    public string VersionInfoLokal { get; set; }
    public WohnHaus WohnHaus { get; set; }
    public ConfigPlc.Plc ConfigPlc { get; set; }
    public Datenstruktur Datenstruktur { get; set; }
    public DisplayPlc.DisplayPlc DisplayPlc { get; set; }
    public BeschriftungenPlc BeschriftungenPlc { get; set; }
    public DatenRangieren DatenRangieren { get; set; }
    public double[] Zeitachse { get; set; } = new double[100];
    public double[] KesselTemperatur { get; set; } = new double[100];
    public double[] VorlaufSolltemperatur { get; set; } = new double[100];

    private PlotWindow _plotWindow;

    private int _nextDataIndex = 1;

    public MainWindow()
    {
        const string versionText = "Heizungsregler";
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

        DatenRangieren = new DatenRangieren(this);
        PlcDaemon = new PlcDaemon(Datenstruktur, DatenRangieren.Rangieren);
        DatenRangieren.ReferenzUebergeben(PlcDaemon.Plc);

        /*
        DisplayPlc = new DisplayPlc.DisplayPlc(Datenstruktur, ConfigPlc, BeschriftungenPlc);

        TestAutomat = new TestAutomat.TestAutomat(Datenstruktur, DisplayPlc.EventBeschriftungAktualisieren, BeschriftungenPlc, PlcDaemon.Plc);
        TestAutomat.SetTestConfig("./ConfigTests/");
        TestAutomat.TabItemFuellen(TabItemAutomatischerSoftwareTest, DisplayPlc);
        */

        WohnHaus = new WohnHaus();
    }

    private void PlotWindowOeffnen(object sender, RoutedEventArgs e)
    {
        Zeitachse = DataGen.Consecutive(100);

        _plotWindow = new PlotWindow();
        _plotWindow.Show();

        _plotWindow.WpfPlot.Plot.YLabel("Temperatur");
        _plotWindow.WpfPlot.Plot.XLabel("Zeit");

        _plotWindow.WpfPlot.Plot.AddScatter(Zeitachse, KesselTemperatur, Color.Magenta, 1, 5, label: "Kesseltemperatur");
        _plotWindow.WpfPlot.Plot.AddScatter(Zeitachse, VorlaufSolltemperatur, Color.Green, 1, 5, label: "VorlaufSolltemperatur");
        _plotWindow.WpfPlot.Plot.Legend();

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
        _plotWindow.WpfPlot.Plot.AxisAuto(0);
        _plotWindow.WpfPlot.Render(true);
    }
    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => Application.Current.Shutdown();
}