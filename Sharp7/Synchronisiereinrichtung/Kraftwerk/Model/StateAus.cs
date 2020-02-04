﻿namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    public class StateAus
    {
        private readonly Kraftwerk kraftWerk;

        public StateAus(Kraftwerk kw)
        {
            kraftWerk = kw;
        }

        public void OnEntry()
        {
            // nichts zu tun
        }

        public void Doing()
        {
            kraftWerk.Generator_n = 0;
            kraftWerk.Generator_U = 0;
            kraftWerk.Generator_f = 0;
            kraftWerk.Generator_P = 0;
            kraftWerk.Generator_CosPhi = 1;

            kraftWerk.ViAnzeige.MessgeraetAnzeigen(false);

            if (kraftWerk.Ventil_Y > 0) kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.Hochfahren);
        }

        public void OnExit()
        {
            // nichts zu tun
        }
    }
}