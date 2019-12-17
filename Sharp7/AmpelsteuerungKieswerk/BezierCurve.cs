using System;

namespace AmpelsteuerungKieswerk
{
    public class BezierCurve
    {
        // http://csharphelper.com/blog/2014/12/draw-a-bezier-curve-by-hand-in-c/

        private readonly double pt_x0;
        private readonly double pt_x1;
        private readonly double pt_x2;
        private readonly double pt_x3;

        private readonly double pt_y0;
        private readonly double pt_y1;
        private readonly double pt_y2;
        private readonly double pt_y3;

        public BezierCurve(double px0, double py0, double px1, double py1, double px2, double py2, double px3, double py3)
        {
            pt_x0 = px0;
            pt_x1 = px1;
            pt_x2 = px2;
            pt_x3 = px3;

            pt_y0 = py0;
            pt_y1 = py1;
            pt_y2 = py2;
            pt_y3 = py3;
        }

        public Tuple<double, double> PunktBestimmen(double t)
        {
            return new Tuple<double, double>(X_Berechnen(t), Y_Berechnen(t));
        }

        private double X_Berechnen(double t)
        {
            return (double)(
                pt_x0 * Math.Pow((1 - t), 3) +
                pt_x1 * 3 * t * Math.Pow((1 - t), 2) +
                pt_x2 * 3 * Math.Pow(t, 2) * (1 - t) +
                pt_x3 * Math.Pow(t, 3)
            );
        }
        private double Y_Berechnen(double t)
        {
            return (double)(
                pt_y0 * Math.Pow((1 - t), 3) +
                pt_y1 * 3 * t * Math.Pow((1 - t), 2) +
                pt_y2 * 3 * Math.Pow(t, 2) * (1 - t) +
                pt_y3 * Math.Pow(t, 3)
            );
        }

    }
}