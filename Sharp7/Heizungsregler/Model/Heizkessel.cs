namespace Heizungsregler.Model
{
    public class Heizkessel
    {
        private readonly Utilities.Rampen _kesselRampe;
        private double _kesselTemperatur;

        public Heizkessel() => _kesselRampe = new Utilities.Rampen(0, 100, 1);
        public void SetBrenner(double brennerLeistung) => _kesselTemperatur = _kesselRampe.GetWert(brennerLeistung);
        public double GetKesselTemperatur() => _kesselTemperatur;
    }
}