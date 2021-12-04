using Kommunikation;
using System;
using System.IO;
using BeschriftungPlc;
using TestAutomat.Model;

namespace TestAutomat;

public partial class TestAutomat
{
    public DirectoryInfo AktuellesProjekt { get; set; }
    public OrdnerLesen ConfigOrdner { get; set; }
    public BeschriftungPlc.BeschriftungenPlc BeschriftungenPlc { get; set; }

    private AutoTesterWindow _autoTesterWindow;
    private  Datenstruktur _datenstruktur;
    private  Action<Datenstruktur> _callbackPlcWindow;
    private  IPlc _plc;

    private int _pltNextDataIndex = 1;

    public TestAutomat()
    {
    }
    public TestAutomat(Datenstruktur datenstruktur, Action<Datenstruktur> cbPlcWindow, BeschriftungPlc.BeschriftungenPlc beschriftungenPlc, IPlc plc)
    {
        BeschriftungenPlc = beschriftungenPlc;
        _datenstruktur = datenstruktur;
        _callbackPlcWindow = cbPlcWindow;
        _plc = plc;

        PlotInitialisieren();
    }

    public void SetTestConfig(string ordnerConfigTests) => ConfigOrdner = new OrdnerLesen(ordnerConfigTests);
    public void SetRefDatenstruktur(Datenstruktur datenstruktur)=>_datenstruktur=datenstruktur;
    public void SetRefEvent(System.Action<Datenstruktur> eventBeschriftungAktualisieren) => _callbackPlcWindow = eventBeschriftungAktualisieren;
    public void SetRefBeschriftungPlc(BeschriftungenPlc beschriftungenPlc)=> BeschriftungenPlc = beschriftungenPlc;
    public void SetRefPlcDaemon(IPlc plc) => _plc = plc;
    public void Starten() => PlotInitialisieren();
    public void TaskBeenden()
    {
        // funktioniert nicht
    }
}