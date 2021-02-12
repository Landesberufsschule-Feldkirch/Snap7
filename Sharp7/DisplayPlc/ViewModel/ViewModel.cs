using Kommunikation;

namespace DisplayPlc.ViewModel
{
    public class ViewModel
    {
        public VisuAnzeigen DisplayPlcAnzeige { get; set; }
        public ViewModel(Datenstruktur datenstruktur, DisplayPlc displayPlc) => DisplayPlcAnzeige = new VisuAnzeigen(datenstruktur, displayPlc);
    }
}