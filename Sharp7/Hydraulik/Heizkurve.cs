namespace Hydraulik
{
    public class Heizkurve
    {

        private readonly double _neigung;
        private readonly double _niveau;
        private readonly double _vorlaufMindestTemperatur;
        private readonly double _vorlaufMaximalTemperatur;

        public Heizkurve(double neigung, double niveau, double vorlaufMindestTemperatur, double vorlaufMaximalTemperatur)
        {
            _neigung = neigung;
            _niveau = niveau;
            _vorlaufMindestTemperatur = vorlaufMindestTemperatur;
            _vorlaufMaximalTemperatur = vorlaufMaximalTemperatur;
        }


        public double GetVorlaufSollTemperatur(double raumtemperatur, double witterungstemperatur)
        {
            // https://www.viessmann-community.com/t5/Experten-fragen/Mathematische-Formel-f%C3%BCr-Vorlauftemperatur-aus-den-vier/qaq-p/68843
            // oder Manager/Heizung_Modul.cpp

            var dar = witterungstemperatur - raumtemperatur;
            var vl = raumtemperatur + _niveau - _neigung * dar * (1.4347 + 0.021 * dar + 0.0002479 * dar * dar);

            if (vl < _vorlaufMindestTemperatur) return _vorlaufMindestTemperatur;
            return vl > _vorlaufMaximalTemperatur ? _vorlaufMaximalTemperatur : vl;
        }
    }
}
