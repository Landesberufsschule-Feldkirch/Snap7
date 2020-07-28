namespace Synchronisiereinrichtung.kraftwerk.Model
{
    internal class StateBelasten
    {
        private readonly Kraftwerk kraftWerk;

        public StateBelasten(Kraftwerk kw) => kraftWerk = kw;

        public void OnEntry()
        {
            kraftWerk.generator.SetSynchronisierungVentil(kraftWerk.Ventil_Y);// gibt ab jetzt die Leistung und nicht mehr die Drehzahl vor
            kraftWerk.generator.SetSynchronisierungErregerstrom(kraftWerk.Generator_Ie); // gibt ab jetzt den Leistungsfaktor und nicht mehr die Spannung vor
        }

        public void Doing()
        {
            kraftWerk.generator.SetSynchronisiertFrequenz(kraftWerk.Netz_f); // Die Drehzahl wird durch die Netzfrequenz vorgegeben
            kraftWerk.Generator_n = kraftWerk.generator.GetDrehzahl();
            kraftWerk.Generator_U = kraftWerk.Netz_U;
            kraftWerk.Generator_f = kraftWerk.Netz_f;
            kraftWerk.MessgeraetAnzeigen = false;

            kraftWerk.generator.MaschineLeistungFahren(kraftWerk.Ventil_Y);
            kraftWerk.generator.PhasenSchieberbetrieb(kraftWerk.Generator_Ie);
            kraftWerk.Generator_P = kraftWerk.generator.GetLeistung();
            kraftWerk.Generator_CosPhi = kraftWerk.generator.GetCosPhi();

            if (kraftWerk.Generator_P > 5000) kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.MaschineTot);
            if (!kraftWerk.Q1) kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.LeistungsschalterAus);
        }
    }
}