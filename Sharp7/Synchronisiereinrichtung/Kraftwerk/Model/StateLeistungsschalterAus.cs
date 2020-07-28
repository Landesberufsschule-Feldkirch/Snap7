﻿namespace Synchronisiereinrichtung.kraftwerk.Model
{
    internal class StateLeistungsschalterAus
    {
        private readonly Kraftwerk kraftWerk;

        public StateLeistungsschalterAus(Kraftwerk kw) => kraftWerk = kw;

        public void Doing()
        {
            kraftWerk.generator.MaschineAntreiben(kraftWerk.Ventil_Y);
            kraftWerk.Generator_n = kraftWerk.generator.GetDrehzahl();
            kraftWerk.Generator_U = kraftWerk.generator.GetSpannung(kraftWerk.Generator_Ie);
            kraftWerk.Generator_f = kraftWerk.generator.GetFrequenz();
            kraftWerk.Generator_P = 0;
            kraftWerk.Generator_CosPhi = 1;

            kraftWerk.MessgeraetAnzeigen = false;

            if (kraftWerk.Ventil_Y == 0) kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.VentilGeschlossen);
            if (kraftWerk.Generator_n > 3000) kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.MaschineTot);
        }
    }
}