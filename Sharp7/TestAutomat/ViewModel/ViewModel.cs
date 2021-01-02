using Kommunikation;

namespace TestAutomat.ViewModel
{
    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(Datenstruktur datenstruktur) => ViAnzeige = new VisuAnzeigen(datenstruktur);
    }
}