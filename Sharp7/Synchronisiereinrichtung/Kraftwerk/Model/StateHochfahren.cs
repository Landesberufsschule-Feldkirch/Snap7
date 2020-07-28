using System;

namespace Synchronisiereinrichtung.kraftwerk.Model
{
    internal class StateHochfahren
    {
        private readonly Kraftwerk kraftWerk;

        public StateHochfahren(Kraftwerk kw) => kraftWerk = kw;

        public void Doing()
        {
            kraftWerk.generator.MaschineAntreiben(kraftWerk.Ventil_Y);
            kraftWerk.Generator_n = kraftWerk.generator.GetDrehzahl();
            kraftWerk.Generator_U = kraftWerk.generator.GetSpannung(kraftWerk.Generator_Ie);
            kraftWerk.Generator_f = kraftWerk.generator.GetFrequenz();
            kraftWerk.Generator_P = 0;
            kraftWerk.Generator_CosPhi = 1;

            kraftWerk.MessgeraetAnzeigen = (Math.Abs(kraftWerk.FrequenzDifferenz) < 2);
            if (kraftWerk.Q1) kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.Synchronisieren);
            if (kraftWerk.Generator_n > 3000) kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.MaschineTot);
            if (kraftWerk.Ventil_Y == 0) kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.VentilGeschlossen);
        }
    }
}