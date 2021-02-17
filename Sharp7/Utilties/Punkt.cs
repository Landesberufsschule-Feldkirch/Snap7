using System;

namespace Utilities
{
    public class Punkt
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly int _nix;
        public double X { get; set; }
        public double Y { get; set; }

        public Punkt(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Punkt(double radius, double winkel, int nix)
        {
            _nix = nix;
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