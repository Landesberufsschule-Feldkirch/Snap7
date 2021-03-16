using BeschriftungPlc;
using ConfigPlc;
using Kommunikation;

namespace DisplayPlc.ViewModel
{
    public class ViewModel
    {
        public VisuAnzeigen DisplayPlcAnzeige { get; set; }
        public ViewModel(Datenstruktur datenstruktur, Plc configPlc, BeschriftungenPlc beschriftungenPlc,
            DisplayPlc displayPlc) => DisplayPlcAnzeige = new VisuAnzeigen(datenstruktur, configPlc, beschriftungenPlc, displayPlc);
    }
}