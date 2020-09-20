namespace Heizungsregler.Model
{
    public class Heizkessel
    {
        private readonly Utilities.Rampen _kesselRampe;
        private double _kesselTemperatur;

        private const double Temperaturanstieg = 0.2;
        private const double KesselMinTemperatur = 0;
        private const double KeselMaxTemperatur = 160;
        public Heizkessel() => _kesselRampe = new Utilities.Rampen(KesselMinTemperatur, KeselMaxTemperatur, Temperaturanstieg);
        public void SetBrenner(double brennerLeistung) => _kesselTemperatur = _kesselRampe.GetWert(brennerLeistung);
        public double GetKesselTemperatur() => _kesselTemperatur;
    }
}