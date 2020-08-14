using System;

namespace Utilities
{
    public class Zeiger
    {
        private readonly Punkt _anfang;
        private readonly Punkt _ende;

        public Zeiger(Punkt anfang, Punkt ende)
        {
            _anfang = anfang;
            _ende = ende;
        }

        public double Laenge()
        {
            var x = _anfang.X - _ende.X;
            var y = _anfang.Y - _ende.Y;

            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }
}