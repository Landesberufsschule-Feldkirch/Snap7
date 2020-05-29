using System.Windows.Media.Media3D;

namespace AutomatischesLagersystem.DreiD
{
    public class DreiDElemente
    {
        private readonly double posX;
        private readonly double posY;
        private readonly double posZ;
        private readonly double ersterWinkel;
        private readonly double zweiterWinkel;
        private readonly double dritterWinkel;

        public DreiDElemente(double x, double y, double z, double w1, double w2, double w3)
        {
            posX = x;
            posY = y;
            posZ = z;
            ersterWinkel = w1;
            zweiterWinkel = w2;
            dritterWinkel = w3;
        }

        public Transform3DGroup Transform(double x, double y, double z)
        {
            var transform = new Transform3DGroup();
            if (ersterWinkel != 0) transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), ersterWinkel)));
            if (zweiterWinkel != 0) transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), zweiterWinkel)));
            if (dritterWinkel != 0) transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), dritterWinkel)));
            transform.Children.Add(new TranslateTransform3D(posX + x, posY + y, posZ + z));
            return transform;
        }

        internal double GetX() => posX;
        internal double GetY() => posY;
        internal double GetZ() => posZ;
    }
}
