using BeschriftungPlc;
using Kommunikation;
using ScottPlot;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Threading;

namespace Blinker;

public partial class MainWindow
{
    public PlcDaemon PlcDaemon { get; set; }
    public string VersionInfoLokal { get; set; }
    public Datenstruktur Datenstruktur { get; set; }
    public DisplayPlc.DisplayPlc DisplayPlc { get; set; }
    public BeschriftungenPlc BeschriftungenPlc { get; set; }
    public ConfigPlc.Plc ConfigPlc { get; set; }
    public double[] WertLeuchtMelder { get; set; } = new double[5_000];
    public DatenRangieren DatenRangieren { get; set; }


    private readonly ViewModel.ViewModel _viewModel;
    private int _nextDataIndex = 1;

    public MainWindow()
    {
        const string versionNummer = "V2.0";
        const string versionText = "Blinker";

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


        var zeitachse = DataGen.Consecutive(5000);
        WpfPlot.Plot.YLabel("Leuchtmelder");
        WpfPlot.Plot.XLabel("Zeit [ms]");

        WpfPlot.Plot.AddScatter(zeitachse, WertLeuchtMelder, Color.Magenta, label: "LED");

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
        WpfPlot.Plot.AxisAuto(0);
        WpfPlot.Render();
    }

    private void PlcButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (DisplayPlc.FensterAktiv) DisplayPlc.Schliessen();
        else DisplayPlc.Oeffnen();
    }
}