using System;

namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    internal class StateHochfahren
    {
        private readonly Kraftwerk _kraftWerk;

        public StateHochfahren(Kraftwerk kw) => _kraftWerk = kw;
        public void Doing()
        {
            _kraftWerk.Generator.MaschineAntreiben(_kraftWerk.VentilY);
            _kraftWerk.GeneratorN = _kraftWerk.Generator.GetDrehzahl();
            _kraftWerk.GeneratorU = _kraftWerk.Generator.GetSpannung(_kraftWerk.GeneratorIe);
            _kraftWerk.GeneratorF = _kraftWerk.Generator.GetFrequenz();
            _kraftWerk.GeneratorP = 0;
            _kraftWerk.GeneratorCosPhi = 1;

            _kraftWerk.MessgeraetAnzeigen = Math.Abs(_kraftWerk.FrequenzDifferenz) < 2;
            if (_kraftWerk.Q1) _kraftWerk.KraftwerkStatemachine.Fire(Statemachine.Trigger.Synchronisieren);
            if (_kraftWerk.GeneratorN > 3000) _kraftWerk.KraftwerkStatemachine.Fire(Statemachine.Trigger.MaschineTot);
            if (Math.Abs(_kraftWerk.VentilY) < 0.001) _kraftWerk.KraftwerkStatemachine.Fire(Statemachine.Trigger.VentilGeschlossen);
        }
    }
}