namespace AutomatischesLagersystem.DreiD
{
    public class DreiDKisten
    {
        private bool sichtbar;
        private double posX;
        private double posY;
        private double posZ;

        public DreiDKisten()
        {
            sichtbar = false;
            posX = 0;
            posY = 0;
            posZ = 0;
        }

        public bool GetSichtbar() => sichtbar;
        public double GetX() => posX;
        public double GetY() => posY;
        public double GetZ() => posZ;

        internal void Reset()
        {
            sichtbar = false;
            posX = 0;
            posY = 0;
            posZ = 0;
        }
    }
}
