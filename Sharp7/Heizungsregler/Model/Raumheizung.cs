using Hydraulik;

namespace Heizungsregler.Model
{
    public class Raumheizung
    {
        private readonly Heizkurve _heizkurve;
        private double _vorlaufSolltemperatur;

        private const double Neigung = 1.6;
        private const double Niveau = 10;
        private const double Raumtemperatur = 20;
        private const double VorlaufMindestTemperatur = 10;
        private const double VorlaufMaximalTemperatur = 60;

        public Raumheizung() => _heizkurve = new Heizkurve(Neigung, Niveau, VorlaufMindestTemperatur, VorlaufMaximalTemperatur);
        public void RaumheizungAktualisieren(double witterungsTemperatur) => _vorlaufSolltemperatur = _heizkurve.GetVorlaufSollTemperatur(Raumtemperatur, witterungsTemperatur);
        internal double GetVorlaufSolltemperatur() => _vorlaufSolltemperatur;
    }
}