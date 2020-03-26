namespace LAP_2019_Foerderanlage.Model
{
    public class Silo
    {
        private double fuellstand;
        private const double materialSiloFuellen = 0.001;
        private const double materialSiloLeeren = 0.0002;


        public void Fuellen()
        {
            fuellstand += materialSiloFuellen;
            if (fuellstand > 1) fuellstand = 1;

        }
        public void Leeren()
        {
            fuellstand -= materialSiloLeeren;
            if (fuellstand < 0) fuellstand = 0;
        }

        public Silo() { fuellstand = 0.9; }
        public double GetFuellstand() => fuellstand;
    }
}
