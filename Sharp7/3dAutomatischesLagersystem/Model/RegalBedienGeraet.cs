namespace AutomatischesLagersystem.Model
{
    public class RegalBedienGeraet
    {
        private double _x;
        private double _y;
        private double _z;

        public RegalBedienGeraet()
        {
            _x = 0;
            _y = 0;
            _z = 0;
        }

        public double GetX() => _x;

        public double GetY() => _y;

        public double GetZ() => _z;

        internal double GetXPosition() => _x * 12000;

        internal double GetYPosition() => -1400 * _y;

        internal double GetZPosition() => 2200 * _z;

        public void SetX(double pos) => _x = pos;

        public void SetY(double pos) => _y = pos;

        public void SetZ(double pos) => _z = pos;

        internal void FahreX(double geschwindigkeit) => _x = Utilities.S7Analog.Clamp((_x + geschwindigkeit), -1, 1);

        internal void FahreY(double geschwindigkeit) => _y = Utilities.S7Analog.Clamp((_y + geschwindigkeit), -1, 1);

        internal void FahreZ(double geschwindigkeit) => _z = Utilities.S7Analog.Clamp((_z + geschwindigkeit), -1, 1);
    }
}