namespace Utilities
{
    public class Rechteck
    {
        public enum RichtungX { NachLinks, Steht, NachRechts }

        public enum RichtungY { NachOben, Steht, NachUnten }

        public Punkt Punkt { get; set; }
        private readonly double _breite;
        private readonly double _hoehe;

        public Rechteck(Punkt p, double b, double h)
        {
            Punkt = p;
            _breite = b;
            _hoehe = h;
        }

        public static bool Kollision(Rechteck r1, Rechteck r2)
        {
            return r1.Punkt.X < r2.Punkt.X + r2._breite &&
                   r2.Punkt.X < r1.Punkt.X + r1._breite &&
                   r1.Punkt.Y < r2.Punkt.Y + r2._hoehe &&
                   r2.Punkt.Y < r1.Punkt.Y + r1._hoehe;
        }

        public static bool Ausgebremst(Rechteck bewegt, Rechteck hinderniss, RichtungX x, RichtungY y)
        {
            bool stop = false;

            if (Kollision(bewegt, hinderniss))
            {
                switch (x)
                {
                    case RichtungX.NachRechts: stop |= hinderniss.Punkt.X > bewegt.Punkt.X; break;
                    case RichtungX.NachLinks: stop |= hinderniss.Punkt.X < bewegt.Punkt.X; break;
                    default: break;
                }
                switch (y)
                {
                    case RichtungY.NachOben: stop |= hinderniss.Punkt.Y < bewegt._hoehe; break;
                    case RichtungY.NachUnten: stop |= hinderniss.Punkt.Y > bewegt.Punkt.Y; break;
                    default: break;
                }
            }
            return stop;
        }
    }
}