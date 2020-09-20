namespace Utilities
{
    public class Rampen
    {
        private double _aktuell;
        private readonly double _minimum;
        private readonly double _maximum;
        private readonly double _steigung;

        public Rampen(double min, double max, double aenderung)
        {
            _minimum = min;
            _maximum = max;
            _steigung = aenderung;
        }

        public double GetWert(double neuerWert)
        {
            if (neuerWert > _aktuell) _aktuell += _steigung;
            if (neuerWert < _aktuell) _aktuell -= _steigung;
            _aktuell = Clamp(_aktuell, _minimum, _maximum);
            return _aktuell;
        }

        private static double Clamp(double val, double min, double max)
        {
            if (val < min) return min;
            return val > max ? max : val;
        }
    }
}