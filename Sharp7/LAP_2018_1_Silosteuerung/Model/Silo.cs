namespace LAP_2018_1_Silosteuerung.Model
{
    public class Silo
    {
        private double _fuellstand;
        private const double MaterialSiloFuellen = 0.001;
        private const double MaterialSiloLeeren = 0.0002;

        public void Fuellen()
        {
            _fuellstand += MaterialSiloFuellen;
            if (_fuellstand > 1) _fuellstand = 1;
        }

        public void Leeren()
        {
            _fuellstand -= MaterialSiloLeeren;
            if (_fuellstand < 0) _fuellstand = 0;
        }

        public Silo() => _fuellstand = 0.9;
        public double GetFuellstand() => _fuellstand;
    }
}