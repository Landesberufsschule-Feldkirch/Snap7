using System;

namespace Utilities
{
    public class Zeiger
    {
        private readonly Punkt Anfang;
        private readonly Punkt Ende;

        public Zeiger(Punkt anfang, Punkt ende)
        {
            Anfang = anfang;
            Ende = ende;
        }

        public double Laenge()
        {
            double x, y;
            x = Anfang.X - Ende.X;
            y = Anfang.Y - Ende.Y;

            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }
}