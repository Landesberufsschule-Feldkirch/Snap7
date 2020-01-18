namespace Synchronisiereinrichtung.Kraftwerk.Model
{

    using System;

    public class MagnetischerKreis
    {
        readonly double Kennlinie;

        public MagnetischerKreis(double kennlinie)
        {
            Kennlinie = kennlinie;
        }

        public double Magnetisierungskennlinie(double strom)
        {
            return 1 - Math.Exp(-Kennlinie * strom);
        }


    }
}
