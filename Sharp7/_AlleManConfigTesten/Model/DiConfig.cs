using System.Collections.ObjectModel;

namespace _AlleManConfigTesten.Model;

public class DiConfig
{
    public ObservableCollection<DiEinstellungen> DigitaleEingaenge { get; set; } = new();
}

public class DiEinstellungen
{
    public int LaufendeNr { get; set; }
    public int StartByte { get; set; }
    public int StartBit { get; set; }
    public int AnzahlBit { get; set; }
    public ManConfigTesten.PlcEinUndAusgaengeTypen Type { get; set; }
    public string Bezeichnung { get; set; }
    public string Kommentar { get; set; }

    public DiEinstellungen()
    {
        LaufendeNr = 0;
        StartByte = 0;
        StartBit = 0;
        AnzahlBit = 0;
        Type = ManConfigTesten.PlcEinUndAusgaengeTypen.Default;
        Bezeichnung = "";
        Kommentar = "";
    }
}