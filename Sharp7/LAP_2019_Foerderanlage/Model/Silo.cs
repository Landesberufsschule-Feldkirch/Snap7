namespace LAP_2019_Foerderanlage.Model
{
    public class Silo
    {
        private double Fuellstand;
        private const double MaterialSiloFuellen = 0.001;
        private const double MaterialSiloLeeren = 0.0002;

        public Silo()
        {
            Fuellstand = 0.9;
        }

        public void Fuellen()
        {
            Fuellstand += MaterialSiloFuellen;
            if (Fuellstand > 1) Fuellstand = 1;

        }
        public void Leeren()
        {
            Fuellstand -= MaterialSiloLeeren;
            if (Fuellstand < 0) Fuellstand = 0;
        }

        public double GetFuellstand() { return Fuellstand; }
    }
}
