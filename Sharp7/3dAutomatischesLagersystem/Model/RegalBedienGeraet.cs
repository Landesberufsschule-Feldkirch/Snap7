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
    }
}
