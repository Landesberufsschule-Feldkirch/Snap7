namespace Synchronisiereinrichtung.kraftwerk.Model
{
    internal class StateMaschineTot
    {
        private readonly Kraftwerk kraftWerk;

        public StateMaschineTot(Kraftwerk kw) => kraftWerk = kw;

        public void OnEntry() => kraftWerk.MaschineTot = true;
    }
}