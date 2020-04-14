namespace Utilities
{
    public class Rampen
    {
        private double aktuell;
        private readonly double minimum;
        private readonly double maximum;
        private readonly double steigung;

        public Rampen(double min, double max, double aenderung)
        {
            minimum = min;
            maximum = max;
            steigung = aenderung;
        }

        public double GetWert(double neuerWert)
        {
            if (neuerWert > aktuell) aktuell += steigung;
            if (neuerWert < aktuell) aktuell -= steigung;
            aktuell = Clamp(aktuell, minimum, maximum);
            return aktuell;
        }

        private double Clamp(double val, double min, double max)
        {
            if (val < min) return min;
            if (val > max) return max;
            return val;
        }
    }
}