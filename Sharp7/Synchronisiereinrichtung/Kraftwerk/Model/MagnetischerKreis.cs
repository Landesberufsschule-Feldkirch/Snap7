namespace Synchronisiereinrichtung.kraftwerk.Model
{
    using System;

    public class MagnetischerKreis
    {
        private readonly double kennlinie;
        public MagnetischerKreis(double kennlinie) => this.kennlinie = kennlinie;
        public double Magnetisierungskennlinie(double strom) => 1 - Math.Exp(-kennlinie * strom);
    }
}