namespace Synchronisiereinrichtung.Model
{
    internal class StateBelasten
    {
        private readonly Kraftwerk.Model.Kraftwerk _kraftWerk;

        public StateBelasten(Kraftwerk.Model.Kraftwerk kw) => _kraftWerk = kw;
        public void OnEntry()
        {
            _kraftWerk.Generator.SetSynchronisierungVentil(_kraftWerk.VentilY);// gibt ab jetzt die Leistung und nicht mehr die Drehzahl vor
            _kraftWerk.Generator.SetSynchronisierungErregerstrom(_kraftWerk.GeneratorIe); // gibt ab jetzt den Leistungsfaktor und nicht mehr die Spannung vor
        }
        public void Doing()
        {
            _kraftWerk.Generator.SetSynchronisiertFrequenz(_kraftWerk.NetzF); // Die Drehzahl wird durch die Netzfrequenz vorgegeben
            _kraftWerk.GeneratorN = _kraftWerk.Generator.GetDrehzahl();
            _kraftWerk.GeneratorU = _kraftWerk.NetzU;
            _kraftWerk.GeneratorF = _kraftWerk.NetzF;
            _kraftWerk.MessgeraetAnzeigen = false;

            _kraftWerk.Generator.MaschineLeistungFahren(_kraftWerk.VentilY);
            _kraftWerk.Generator.PhasenSchieberbetrieb(_kraftWerk.GeneratorIe);
            _kraftWerk.GeneratorP = _kraftWerk.Generator.GetLeistung();
            _kraftWerk.GeneratorCosPhi = _kraftWerk.Generator.GetCosPhi();

            if (_kraftWerk.GeneratorP > 5000) _kraftWerk.KraftwerkStatemachine.Fire(Statemachine.Trigger.MaschineTot);
            if (!_kraftWerk.Q1) _kraftWerk.KraftwerkStatemachine.Fire(Statemachine.Trigger.LeistungsschalterAus);
        }
    }
}