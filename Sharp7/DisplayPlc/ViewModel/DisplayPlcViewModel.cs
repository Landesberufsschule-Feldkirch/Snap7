using Kommunikation;

namespace DisplayPlc.ViewModel
{
    public class DisplayPlcViewModel
    {
        public DisplayPlcVisuAnzeigen DisplayPlcAnzeige { get; set; }
        public DisplayPlcViewModel(Datenstruktur datenstruktur) => DisplayPlcAnzeige = new DisplayPlcVisuAnzeigen(datenstruktur);
    }
}