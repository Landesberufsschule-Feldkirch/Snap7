namespace Hydraulik
{
    public class Heizkurve
    {

        private double Neigung;
        private double Niveau;
        private double VorlaufMindestTemperatur;
        private double VorlaufMaximalTemperatur;

        public Heizkurve(double neigung, double niveau, double vorlaufMindestTemperatur, double vorlaufMaximalTemperatur)
        {
            Neigung = neigung;
            Niveau = niveau;
            VorlaufMindestTemperatur = vorlaufMindestTemperatur;
            VorlaufMaximalTemperatur = vorlaufMaximalTemperatur;
        }

    
        public double GetVorlaufSollTemperatur(double raumtemperatur, double witterungstemperatur)
        {
            // https://www.viessmann-community.com/t5/Experten-fragen/Mathematische-Formel-f%C3%BCr-Vorlauftemperatur-aus-den-vier/qaq-p/68843
            // oder Manager/Heizung_Modul.cpp

            var dar = witterungstemperatur - raumtemperatur;
            var vl = raumtemperatur + Niveau - (Neigung * dar * (1.4347 + (0.021 * dar) + (0.0002479 * dar * dar)));

            if (vl < VorlaufMindestTemperatur) return VorlaufMindestTemperatur;
            return vl > VorlaufMaximalTemperatur ? VorlaufMaximalTemperatur : vl;
        }
    }
}
