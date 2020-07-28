using System;

namespace Utilities
{
    public static class Winkel
    {
        public static double Deg2Rad(double value) => value / 180d * Math.PI;

        public static int Rad2Deg(double value) => (int)(value * 180 / Math.PI);
    }
}