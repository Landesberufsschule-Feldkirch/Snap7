namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    internal class StateMaschineTot
    {
        private readonly Kraftwerk _kraftWerk;

        public StateMaschineTot(Kraftwerk kw) => _kraftWerk = kw;
        public void OnEntry() => _kraftWerk.MaschineTot = true;
    }
}