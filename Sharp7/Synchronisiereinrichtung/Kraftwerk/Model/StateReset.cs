namespace Synchronisiereinrichtung.kraftwerk.Model
{
    internal class StateReset
    {
        private readonly Kraftwerk _kraftWerk;

        public StateReset(Kraftwerk kw) => _kraftWerk = kw;

        public void OnEntry()
        {
            _kraftWerk.Q1 = false;
            _kraftWerk.MaschineTot = false;
            _kraftWerk.Generator.Reset();
            _kraftWerk.KraftwerkStatemachine.Fire(Statemachine.Trigger.Neustart);
        }
    }
}