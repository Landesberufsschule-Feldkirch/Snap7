using System;

namespace Synchronisiereinrichtung.Model
{
    public class MagnetischerKreis
    {
        private readonly double _kennlinie;

        public MagnetischerKreis(double kennlinie) => _kennlinie = kennlinie;

        public double Magnetisierungskennlinie(double strom) => 1 - Math.Exp(-_kennlinie * strom);
    }
}