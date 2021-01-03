using Kommunikation;

namespace TestAutomat.PlcDisplay.ViewModel
{
    public class PlcDisplayViewModel
    {
        public PlcDisplayVisuAnzeigen PlcDisplayAnzeige { get; set; }
        public PlcDisplayViewModel(Datenstruktur datenstruktur) => PlcDisplayAnzeige = new PlcDisplayVisuAnzeigen(datenstruktur);
    }
}