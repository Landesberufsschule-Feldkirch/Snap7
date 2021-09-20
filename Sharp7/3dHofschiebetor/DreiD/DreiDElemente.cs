using System.Windows.Media.Media3D;

namespace _3dHofschiebetor.DreiD
{
    public class DreiDElemente
    {
        private readonly double _posX;
        private readonly double _posY;
        private readonly double _posZ;
        private readonly double _ersterWinkel;
        private readonly double _zweiterWinkel;
        private readonly double _dritterWinkel;

        public DreiDElemente(double x, double y, double z, double w1, double w2, double w3)
        {
            _posX = x;
            _posY = y;
            _posZ = z;
            _ersterWinkel = w1;
            _zweiterWinkel = w2;
            _dritterWinkel = w3;
        }

        public Transform3DGroup Transform(double x, double y, double z)
        {
            var transform = new Transform3DGroup();
            if (_ersterWinkel != 0) transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), _ersterWinkel)));
            if (_zweiterWinkel != 0) transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), _zweiterWinkel)));
            if (_dritterWinkel != 0) transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), _dritterWinkel)));
            transform.Children.Add(new TranslateTransform3D(_posX + x, _posY + y, _posZ + z));
            return transform;
        }

        internal double GetX() => _posX;

        internal double GetY() => _posY;

        internal double GetZ() => _posZ;
    }
}