using System;

namespace Utilities
{
    public class Rechteck
    {
        public enum RichtungX { NachLinks, Steht, NachRechts }
        public enum RichtungY { NachOben, Steht, NachUnten }

        private readonly Punkt _linksOben;
        private readonly double _breite;
        private readonly double _hoehe;

        public Rechteck(Punkt linksOben, double b, double h)
        {
            _linksOben = linksOben;
            _breite = b;
            _hoehe = h;
        }

        public Punkt GetPosition() => _linksOben;
        public double GetLinks() => _linksOben.X;
        public double GetRechts() => _linksOben.X + _breite;
        public double GetOben() => _linksOben.Y;
        public double GetUnten() => _linksOben.Y + _hoehe;

        public static bool Kollision(Rechteck r1, Rechteck r2)
        {
            return r1.GetLinks() < r2.GetRechts() &&
                   r2.GetLinks() < r1.GetRechts() &&
                   r1.GetOben() < r2.GetUnten() &&
                   r2.GetOben() < r1.GetUnten();
        }

        public static bool Ausgebremst(Rechteck bewegt, Rechteck hinderniss, RichtungX x, RichtungY y)
        {
            var stop = false;

            if (!Kollision(bewegt, hinderniss)) return false;

            switch (x)
            {
                case RichtungX.NachRechts: stop |= hinderniss.GetLinks() > bewegt.GetLinks(); break;
                case RichtungX.NachLinks: stop |= hinderniss.GetLinks() < bewegt.GetLinks(); break;
                case RichtungX.Steht:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(x), x, null);
            }
            switch (y)
            {
                case RichtungY.NachOben: stop |= hinderniss.GetOben() < bewegt._hoehe; break;
                case RichtungY.NachUnten: stop |= hinderniss.GetOben() > bewegt.GetOben(); break;
                case RichtungY.Steht:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(y), y, null);
            }
            return stop;
        }
    }
}