using System;

namespace Utilities
{
    public class BezierCurve
    {
        // http://csharphelper.com/blog/2014/12/draw-a-bezier-curve-by-hand-in-c/

        private readonly double _ptX0;
        private readonly double _ptX1;
        private readonly double _ptX2;
        private readonly double _ptX3;

        private readonly double _ptY0;
        private readonly double _ptY1;
        private readonly double _ptY2;
        private readonly double _ptY3;

        public BezierCurve(Punkt pt0, Punkt pt1, Punkt pt2, Punkt pt3)
        {
            _ptX0 = pt0.X;
            _ptX1 = pt1.X;
            _ptX2 = pt2.X;
            _ptX3 = pt3.X;

            _ptY0 = pt0.Y;
            _ptY1 = pt1.Y;
            _ptY2 = pt2.Y;
            _ptY3 = pt3.Y;
        }

        public BezierCurve(double px0, double py0, double px1, double py1, double px2, double py2, double px3, double py3)
        {
            _ptX0 = px0;
            _ptX1 = px1;
            _ptX2 = px2;
            _ptX3 = px3;

            _ptY0 = py0;
            _ptY1 = py1;
            _ptY2 = py2;
            _ptY3 = py3;
        }

        public Punkt PunktBestimmen(double t)
        {
            return new Punkt(X_Berechnen(t), Y_Berechnen(t));
        }

        private double X_Berechnen(double t)
        {
            return (
                _ptX0 * Math.Pow((1 - t), 3) +
                _ptX1 * 3 * t * Math.Pow((1 - t), 2) +
                _ptX2 * 3 * Math.Pow(t, 2) * (1 - t) +
                _ptX3 * Math.Pow(t, 3)
            );
        }

        private double Y_Berechnen(double t)
        {
            return (
                _ptY0 * Math.Pow((1 - t), 3) +
                _ptY1 * 3 * t * Math.Pow((1 - t), 2) +
                _ptY2 * 3 * Math.Pow(t, 2) * (1 - t) +
                _ptY3 * Math.Pow(t, 3)
            );
        }
    }
}