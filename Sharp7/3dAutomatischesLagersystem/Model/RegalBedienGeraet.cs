namespace AutomatischesLagersystem.Model
{


    public class RegalBedienGeraet
    {




        private double x;
        private double y;
        private double z;


        public RegalBedienGeraet()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public double GetX() => x;
        public double GetY() => y;
        public double GetZ() => z;
        public void SetX(double pos) { x = pos; }
        public void SetY(double pos) { y = pos; }
        public void SetZ(double pos) { z = pos; }
        internal void FahreX(double geschwindigkeit) { x = Utilities.S7Analog.Clamp((x + geschwindigkeit), -1, 1); }
        internal void FahreY(double geschwindigkeit) { y = Utilities.S7Analog.Clamp((y + geschwindigkeit), -1, 1); }
        internal void FahreZ(double geschwindigkeit) { z = Utilities.S7Analog.Clamp((z + geschwindigkeit), -1, 1); }
    }
}
