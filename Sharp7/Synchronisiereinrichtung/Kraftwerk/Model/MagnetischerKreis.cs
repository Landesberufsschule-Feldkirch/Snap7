using System;

namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    public class MagnetischerKreis
    {
        double Kennlinie;

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
