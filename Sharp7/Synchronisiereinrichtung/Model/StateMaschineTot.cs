namespace Synchronisiereinrichtung.Model
{
    internal class StateMaschineTot
    {
        private readonly Kraftwerk.Model.Kraftwerk _kraftWerk;

        public StateMaschineTot(Kraftwerk.Model.Kraftwerk kw) => _kraftWerk = kw;
        public void OnEntry() => _kraftWerk.MaschineTot = true;
    }
}