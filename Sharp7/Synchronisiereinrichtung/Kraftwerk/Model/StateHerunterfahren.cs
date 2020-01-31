namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    class StateHerunterfahren
    {
        private readonly Kraftwerk kraftWerk;

        public StateHerunterfahren(Kraftwerk kw)
        {
            kraftWerk = kw;
        }

        public void OnEntry()
        {
            // nichts zu tun
        }
        

        public void Doing()
        {
            // nichts zu tun
        }

        public void OnExit()
        {
            // nichts zu tun
        }
    }
}