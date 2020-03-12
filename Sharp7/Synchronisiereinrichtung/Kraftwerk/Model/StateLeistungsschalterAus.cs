namespace Synchronisiereinrichtung.kraftwerk.Model
{
    class StateLeistungsschalterAus
    {
        private readonly Kraftwerk kraftWerk;

        public StateLeistungsschalterAus(Kraftwerk kw) { kraftWerk = kw; }
        public void OnEntry() {   /* nichts zu tun*/  }
        public void Doing()
        {
            kraftWerk.generator.MaschineAntreiben(kraftWerk.Ventil_Y);
            kraftWerk.Generator_n = kraftWerk.generator.Drehzahl();
            kraftWerk.Generator_U = kraftWerk.generator.Spannung(kraftWerk.Generator_Ie);
            kraftWerk.Generator_f = kraftWerk.generator.Frequenz();
            kraftWerk.Generator_P = 0;
            kraftWerk.Generator_CosPhi = 1;

            kraftWerk.MessgeraetAnzeigen = false;

            if (kraftWerk.Ventil_Y == 0) kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.VentilGeschlossen);
            if (kraftWerk.Generator_n > 3000) kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.MaschineTot);
        }

        public void OnExit() {   /* nichts zu tun*/  }
    }
}