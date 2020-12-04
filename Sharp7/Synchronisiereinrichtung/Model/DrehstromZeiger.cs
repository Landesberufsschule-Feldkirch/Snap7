using System;
using Utilities;

namespace Synchronisiereinrichtung.Model
{
    public static class DrehstromZeiger
    {
        public static double WinkelBerechnen(double zeit, double freqenz, double winkel)
        {
            var periodenDauer = 1000 / freqenz; // in ms
            winkel += 360 * zeit / periodenDauer;
            if (winkel > 360) winkel -= 360;
            return winkel;
        }

        public static Punkt GetSpannung(double winkel, double spannung)
        {
            var strangSpannung = spannung / Math.Sqrt(3);
            return new Punkt(strangSpannung, winkel, 0);
        }
    }
}