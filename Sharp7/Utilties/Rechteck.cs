namespace Utilities
{
    public class Rechteck
    {
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
        public static bool KollisionObenRechtsUnten(Rechteck bewegt, Rechteck hinderniss)
        {
            return bewegt.Punkt.X < hinderniss.Punkt.X + hinderniss.breite &&
                  hinderniss.Punkt.X < bewegt.Punkt.X + bewegt.breite &&
                  bewegt.Punkt.Y < hinderniss.Punkt.Y + hinderniss.hoehe &&
                  hinderniss.Punkt.Y < bewegt.Punkt.Y + bewegt.hoehe;
        }

        public static bool KollisionObenLinksUnten(Rechteck bewegt, Rechteck hinderniss)
        {
            return bewegt.Punkt.X < hinderniss.Punkt.X + hinderniss.breite &&
                  hinderniss.Punkt.X < bewegt.Punkt.X + bewegt.breite &&
                  bewegt.Punkt.Y < hinderniss.Punkt.Y + hinderniss.hoehe &&
                  hinderniss.Punkt.Y < bewegt.Punkt.Y + bewegt.hoehe;
        }
    }
}