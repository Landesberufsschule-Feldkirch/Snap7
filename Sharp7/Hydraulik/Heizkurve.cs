namespace Hydraulik
{
    public class Heizkurve
    {

        private double Neigung;
        private double Niveau;
        private double RaumSollTemperatur;
        private double VorlaufMindestTemperatur;
        private double VorlaufMaximalTemperatur;
        private double AussenTemperatur;

        public Heizkurve(double neigung, double niveau, double vorlaufMindestTemperatur, double vorlaufMaximalTemperatur)
        {
            Neigung = neigung;
            Niveau = niveau;
            VorlaufMindestTemperatur = vorlaufMindestTemperatur;
            VorlaufMaximalTemperatur = vorlaufMaximalTemperatur;
        }

        public void SetAussenTemperatur(double aussenTemperatur) => AussenTemperatur = aussenTemperatur;

        public double GetVorlaufSollTemperatur()
        {
            // https://www.viessmann-community.com/t5/Experten-fragen/Mathematische-Formel-f%C3%BCr-Vorlauftemperatur-aus-den-vier/qaq-p/68843
            //oder Manager/Heizung_Modul.cpp

            var dar = AussenTemperatur - RaumSollTemperatur;
            var vl = RaumSollTemperatur + Niveau - (Neigung * dar * (1.4347 + (0.021 * dar) + (0.0002479 * dar * dar)));

            if (vl < VorlaufMindestTemperatur) return VorlaufMindestTemperatur;
            return vl > VorlaufMaximalTemperatur ? VorlaufMaximalTemperatur : vl;
        }
    }
}
