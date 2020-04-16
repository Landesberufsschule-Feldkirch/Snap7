using System;

namespace PaternosterLager.Model
{
    public static class PositionBestimmen
    {

        public static (double x, double y) PositionBerechnen(double pos)
        {
            double x = 350;
            double y;

            double obenBahn = 50;
            double linksBahn = 150;
            double breiteBahn = 200;
            double hoeheBahn = 850;
            double umfangBahn;

            umfangBahn = 2 * hoeheBahn + Math.PI * breiteBahn;

            y = obenBahn + hoeheBahn / 2;
  

            return (x, y);
        }
    }
}
