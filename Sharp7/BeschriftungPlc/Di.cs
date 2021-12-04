using System.Collections.ObjectModel;

namespace BeschriftungPlc;

public class Di
{
    public Di(ObservableCollection<DiDaten> diBeschriftung)
    {
        DiBeschriftung = diBeschriftung;
    }

    public ObservableCollection<DiDaten> DiBeschriftung { get; set; }
}

public class DiDaten
{
    public int Byte { get; set; }
    public int Bit { get; set; }
    public string Bezeichnung { get; set; }
    public string Kommentar { get; set; }

    public DiDaten()
    {
        Byte = 0;
        Bit = 0;
        Bezeichnung = "";
        Kommentar = "";
    }
}