namespace AutomatischesLagersystem.DreiD
{
    public class DreiDKisten
    {
        private bool _sichtbar;
        private double _posX;
        private double _posY;
        private double _posZ;

        public DreiDKisten()
        {
            _sichtbar = false;
            _posX = 0;
            _posY = 0;
            _posZ = 0;
        }

        public bool GetSichtbar() => _sichtbar;

        public double GetX() => _posX;

        public double GetY() => _posY;

        public double GetZ() => _posZ;

        internal void Reset()
        {
            _sichtbar = false;
            _posX = 0;
            _posY = 0;
            _posZ = 0;
        }
    }
}