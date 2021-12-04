using System.Collections.ObjectModel;

namespace ConfigPlc;

public class Ai
{
    public Ai(ObservableCollection<AiEinstellungen> analogeEingaenge)
    {
        AnalogeEingaenge = analogeEingaenge;
    }

    public ObservableCollection<AiEinstellungen> AnalogeEingaenge { get; set; }
}

public class AiEinstellungen
{
    public int LaufendeNr { get; set; }
    public int StartByte { get; set; }
    public int StartBit { get; set; }
    public int AnzahlBit { get; set; }
    public PlcEinUndAusgaengeTypen Type { get; set; }
    public string Bezeichnung { get; set; }
    public string Kommentar { get; set; }

    public AiEinstellungen()
    {
        LaufendeNr = 0;
        StartByte = 0;
        StartBit = 0;
        AnzahlBit = 0;
        Type = PlcEinUndAusgaengeTypen.Default;
        Bezeichnung = "";
        Kommentar = "";
    }
}