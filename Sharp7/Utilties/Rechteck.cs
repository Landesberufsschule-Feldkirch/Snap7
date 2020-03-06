namespace Utilities
{
    public class Rechteck
    {
        public enum RichtungX { nachLinks, steht, nachRechts }
        public enum RichtungY { nachOben, steht, nachUnten }
        public Punkt Punkt { get; set; }
        private readonly double breite;
        private readonly double hoehe;

        public Rechteck(Punkt p, double b, double h)
        {
            Punkt = p;
            breite = b;
            hoehe = h;
        }



        public static bool Kollision(Rechteck r1, Rechteck r2)
        {
            return r1.Punkt.X < r2.Punkt.X + r2.breite &&
                   r2.Punkt.X < r1.Punkt.X + r1.breite &&
                   r1.Punkt.Y < r2.Punkt.Y + r2.hoehe &&
                   r2.Punkt.Y < r1.Punkt.Y + r1.hoehe;
        }


        public static bool Ausgebremst(Rechteck bewegt, Rechteck hinderniss, RichtungX x, RichtungY y)
        {
            bool stop = false;

            if (Kollision(bewegt, hinderniss))
            {
                switch (x)
                {
                    case RichtungX.nachRechts: stop |= hinderniss.Punkt.X > bewegt.Punkt.X; break;
                    case RichtungX.nachLinks: stop |= hinderniss.Punkt.X < bewegt.Punkt.X; break;
                    case RichtungX.steht:
                    default: break;
                }
                switch (y)
                {
                    case RichtungY.nachOben: stop |= hinderniss.Punkt.Y < bewegt.hoehe; break;
                    case RichtungY.nachUnten: stop |= hinderniss.Punkt.Y > bewegt.Punkt.Y; break;
                    case RichtungY.steht:
                    default: break;
                }
            }
            return stop;
        }

    }
}