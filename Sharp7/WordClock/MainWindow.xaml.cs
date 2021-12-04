using BeschriftungPlc;
using Kommunikation;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WordClock;

public partial class MainWindow
{
    public PlcDaemon PlcDaemon { get; set; }
    public string VersionInfoLokal { get; set; }
    public ConfigPlc.Plc ConfigPlc { get; set; }
    public Datenstruktur Datenstruktur { get; set; }
    public DisplayPlc.DisplayPlc DisplayPlc { get; set; }
    public BeschriftungenPlc BeschriftungenPlc { get; set; }
    public DatenRangieren DatenRangieren { get; set; }

    public MainWindow()
    {
        const string versionText = "WorkClock";
        const string versionNummer = "V2.0";


        const int anzByteDigInput = 9;
        const int anzByteDigOutput = 0;
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

        /*
        DisplayPlc = new DisplayPlc.DisplayPlc(Datenstruktur, ConfigPlc, BeschriftungenPlc);

        TestAutomat = new TestAutomat.TestAutomat(Datenstruktur, DisplayPlc.EventBeschriftungAktualisieren, BeschriftungenPlc, PlcDaemon.Plc);
        TestAutomat.SetTestConfig("./ConfigTests/");
        TestAutomat.TabItemFuellen(TabItemAutomatischerSoftwareTest, DisplayPlc);
        */

        for (double i = 0; i < 360; i += 30) RotiertesRechteckHinzufuegen(8, 30, i);
        for (double i = 0; i < 360; i += 6) RotiertesRechteckHinzufuegen(2, 10, i);
    }

    private void RotiertesRechteckHinzufuegen(double breite, double hoehe, double winkel)
    {
        var rect = new Rectangle
        {
            Width = breite,
            Height = hoehe,
            Fill = Brushes.Black
        };

        var rotateTransform = new RotateTransform(winkel, breite / 2, 150);
        rect.RenderTransform = rotateTransform;

        Canvas.SetLeft(rect, 150 - breite / 2);
        Canvas.SetTop(rect, 0);

        CanvasUhr.Children.Add(rect);
    }
}