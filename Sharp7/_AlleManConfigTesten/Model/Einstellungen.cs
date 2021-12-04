using System.Collections.ObjectModel;

namespace _AlleManConfigTesten.Model;

public class Einstellungen
{
    public ObservableCollection<DateiFilter> AlleDateienFilter { get; set; } = new();
}

public class DateiFilter
{
    public DateiFilter()
    {
        Beschreibung = "";
        Einlesen = false;
        Quelle = "";
    }

    public string Beschreibung { get; set; }
    public bool Einlesen { get; set; }
    public string Quelle { get; set; }
}