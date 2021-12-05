using System.Windows.Controls;
using BeschriftungPlc;
using ConfigPlc;
using Kommunikation;

namespace DigitalerZwillingMitAutoTests;

public class DtAutoTests
{
    public Datenstruktur Datenstruktur { get; set; }
    public BeschriftungenPlc BeschriftungenPlc { get; set; }
    public Plc ConfigPlc { get; set; }
    public DisplayPlc.DisplayPlc DisplayPlc { get; set; }
    public PlcDaemon PlcDaemon { get; set; }
    public TestAutomat.TestAutomat TestAutomat { get; set; }

    private string? _versionLokal;
    private string? _pathConfigPlc;


    private string? _pathConfigTests;
    private TabItem? _tabItemAutoTest;

    public DtAutoTests()
    {
        Datenstruktur = new Datenstruktur();
        ConfigPlc = new Plc();
        PlcDaemon = new PlcDaemon();
        BeschriftungenPlc = new BeschriftungenPlc();
        DisplayPlc = new DisplayPlc.DisplayPlc();
        TestAutomat = new TestAutomat.TestAutomat();
    }

    

    public string? GetVersionLokal() => _versionLokal;

    public void DisplayPlcFensterOpen() => DisplayPlc.Oeffnen();
    public void DisplayPlcFensterClose()=> DisplayPlc.Schliessen();

    public void SetBetriebsartProjektLaborplatte() => Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.LaborPlatte;
    public void SetBetriebsartProjektSimulation() => Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.Simulation;
    public void SetBetriebsartProjektAutoTest() => Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.AutomatischerSoftwareTest;
    public void SetVersionLokal(string version) => _versionLokal = version;
    public void SetTabItemAutoTests(TabItem tabItemAutoTest) => _tabItemAutoTest = tabItemAutoTest;
    public void SetConfigPlc(string pathConfigPlc) => _pathConfigPlc = pathConfigPlc;
    public void SetConfigTests(string configtests) => _pathConfigTests = configtests;
    public void SetInputOutput(int anzByteDi, int anzByteDo, int anzByteAi, int anzByteAo)
    {
        Datenstruktur.SetDigInput(anzByteDi);
        Datenstruktur.SetDigOutput(anzByteDo);
        Datenstruktur.SetAnalogInput(anzByteAi);
        Datenstruktur.SetAnalogOutput(anzByteAo);
    }

    public void Starten()
    {
        ConfigPlc.SetPath(_pathConfigPlc);

        DisplayPlc.Start(Datenstruktur, ConfigPlc, BeschriftungenPlc);

        PlcDaemon.SetReferenzDatenstruktur(Datenstruktur);
        PlcDaemon.Starten();

        TestAutomat.SetRefDatenstruktur(Datenstruktur);
        TestAutomat.SetRefEvent(DisplayPlc.EventBeschriftungAktualisieren);
        TestAutomat.SetRefBeschriftungPlc(BeschriftungenPlc);
        TestAutomat.SetRefPlcDaemon(PlcDaemon.Plc);
        TestAutomat.SetTestConfig(_pathConfigTests);
        TestAutomat.TabItemFuellen(_tabItemAutoTest, DisplayPlc);
        TestAutomat.Starten();
    }
}