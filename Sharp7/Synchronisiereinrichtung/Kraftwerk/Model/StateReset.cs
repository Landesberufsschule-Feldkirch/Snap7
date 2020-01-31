namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    class StateReset
    {
        private readonly Kraftwerk kraftWerk;

        public StateReset(Kraftwerk kw)
        {
            kraftWerk = kw;
        }
        public void OnEntry()
        {
            kraftWerk.Q1 = false;
            kraftWerk.ViAnzeige.MaschineTot(false);
        }
       public void Doing()
        {
            kraftWerk.generator.Reset();

            kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.Ausschalten);
        }

        public void OnExit()
        {
            // nichts zu tun
        }
    }
}