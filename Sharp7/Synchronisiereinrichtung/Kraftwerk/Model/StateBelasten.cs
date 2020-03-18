namespace Synchronisiereinrichtung.kraftwerk.Model
{
    class StateBelasten
    {
        private readonly Kraftwerk kraftWerk;

        public StateBelasten(Kraftwerk kw) { kraftWerk = kw; }
        public void OnEntry()
        {
            kraftWerk.generator.SynchronisierungVentil(kraftWerk.Ventil_Y);// gibt ab jetzt die Leistung und nicht mehr die Drehzahl vor
            kraftWerk.generator.SynchronisierungErregerstrom(kraftWerk.Generator_Ie); // gibt ab jetzt den Leistungsfaktor und nicht mehr die Spannung vor
        }

        public void Doing()
        {
            kraftWerk.generator.SynchronisiertFrequenz(kraftWerk.Netz_f); // Die Drehzahl wird durch die Netzfrequenz vorgegeben
            kraftWerk.Generator_n = kraftWerk.generator.Drehzahl();
            kraftWerk.Generator_U = kraftWerk.Netz_U;
            kraftWerk.Generator_f = kraftWerk.Netz_f;
            kraftWerk.MessgeraetAnzeigen = false;

            kraftWerk.generator.MaschineLeistungFahren(kraftWerk.Ventil_Y);
            kraftWerk.generator.PhasenSchieberbetrieb(kraftWerk.Generator_Ie);
            kraftWerk.Generator_P = kraftWerk.generator.Leistung();
            kraftWerk.Generator_CosPhi = kraftWerk.generator.CosPhi();

            if (kraftWerk.Generator_P > 5000) kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.MaschineTot);
            if (!kraftWerk.Q1) kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.LeistungsschalterAus);
        }
    }
}