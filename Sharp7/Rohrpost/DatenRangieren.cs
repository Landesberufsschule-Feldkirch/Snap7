using Kommunikation;

namespace Rohrpost
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private IPlc _plc;

        public void Rangieren(Kommunikation.Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                //
            }

        }

        public DatenRangieren(MainWindow window) => mainWindow = window;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}