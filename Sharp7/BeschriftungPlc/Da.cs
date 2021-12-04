using System.Collections.ObjectModel;

namespace BeschriftungPlc;

public class Da
{
    public Da(ObservableCollection<DaDaten> daBeschriftung)
    {
        DaBeschriftung = daBeschriftung;
    }

    public ObservableCollection<DaDaten> DaBeschriftung { get; set; }
}

public class DaDaten
{
    public int Byte { get; set; }
    public int Bit { get; set; }
    public string Bezeichnung { get; set; }
    public string Kommentar { get; set; }


    public DaDaten()
    {
        Byte = 0;
        Bit = 0;
        Bezeichnung = "";
        Kommentar = "";
    }
}