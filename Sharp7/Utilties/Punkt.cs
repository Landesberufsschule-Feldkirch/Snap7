using System;

namespace Utilities
{
    public class Punkt
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Punkt(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Punkt(double radius, double winkel, int nix)
        {
            // Winkel in Grad --> für Synchronisiereinrichtung
            X = radius * Math.Cos(Utilities.Winkel.Deg2Rad(winkel));
            Y = radius * Math.Sin(Utilities.Winkel.Deg2Rad(winkel));
        }
    }
}