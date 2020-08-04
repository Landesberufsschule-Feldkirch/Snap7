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

        public Punkt()
        {
            X = 0;
            Y = 0;
        }

        public Punkt(double radius, double winkel, int nix)
        {
            // Winkel in Grad --> für Synchronisiereinrichtung
            X = radius * Math.Cos(Winkel.Deg2Rad(winkel));
            Y = radius * Math.Sin(Winkel.Deg2Rad(winkel));
        }

        public Punkt Clone()
        {
            var p = new Punkt(X, Y);
            return p;
        }
    }
}