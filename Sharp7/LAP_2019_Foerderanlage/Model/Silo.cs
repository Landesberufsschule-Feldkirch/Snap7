namespace LAP_2019_Foerderanlage.Model
{
    public  class Silo
    {

        private readonly double Fuellstand;

        public Silo()
        {
            Fuellstand  = 0.9;
        }

        public double GetFuellstand() { return Fuellstand; }
    }
}
