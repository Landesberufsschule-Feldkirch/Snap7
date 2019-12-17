using System;
using Utilties;

namespace Utilities
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

        public BezierCurve(Punkt pt0, Punkt pt1, Punkt pt2, Punkt pt3)
        {
            pt_x0 = pt0.X;
            pt_x1 = pt1.X;
            pt_x2 = pt2.X;
            pt_x3 = pt3.X;

            pt_y0 = pt0.Y;
            pt_y1 = pt1.Y;
            pt_y2 = pt2.Y;
            pt_y3 = pt3.Y;
        }

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

        public Punkt PunkteBestimmen(double t)
        {
            return new Punkt(X_Berechnen(t), Y_Berechnen(t));
        }

        public (double x, double y) PunktBestimmen(double t)
        {
            return (X_Berechnen(t), Y_Berechnen(t));
        }

        private double X_Berechnen(double t)
        {
            return (
                pt_x0 * Math.Pow((1 - t), 3) +
                pt_x1 * 3 * t * Math.Pow((1 - t), 2) +
                pt_x2 * 3 * Math.Pow(t, 2) * (1 - t) +
                pt_x3 * Math.Pow(t, 3)
            );
        }
        private double Y_Berechnen(double t)
        {
            return (
                pt_y0 * Math.Pow((1 - t), 3) +
                pt_y1 * 3 * t * Math.Pow((1 - t), 2) +
                pt_y2 * 3 * Math.Pow(t, 2) * (1 - t) +
                pt_y3 * Math.Pow(t, 3)
            );
        }

    }
}