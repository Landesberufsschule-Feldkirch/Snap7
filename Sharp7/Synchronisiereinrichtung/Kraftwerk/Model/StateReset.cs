namespace Synchronisiereinrichtung.kraftwerk.Model
{
    class StateReset
    {
        private readonly Kraftwerk kraftWerk;

        public StateReset(Kraftwerk kw) { kraftWerk = kw; }
        public void OnEntry()
        {
            kraftWerk.Q1 = false;
            kraftWerk.MaschineTot = false;
            kraftWerk.generator.Reset();
            kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.Neustart);
        }
    }
}