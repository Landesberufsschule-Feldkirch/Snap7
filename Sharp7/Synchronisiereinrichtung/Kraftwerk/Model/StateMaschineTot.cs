namespace Synchronisiereinrichtung.kraftwerk.Model
{
    class StateMaschineTot
    {
        private readonly Kraftwerk kraftWerk;

        public StateMaschineTot(Kraftwerk kw) { kraftWerk = kw; }
        public void OnEntry() { kraftWerk.MaschineTot = true; }
        public void Doing() {   /* nichts zu tun*/  }
        public void OnExit() {   /* nichts zu tun*/  }
    }
}