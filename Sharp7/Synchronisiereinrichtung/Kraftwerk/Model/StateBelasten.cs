namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    class StateBelasten
    {
        private readonly Kraftwerk kraftWerk;

        public StateBelasten(Kraftwerk kw)
        {
            kraftWerk = kw;
        }

        public void OnEntry()
        {
            kraftWerk.generator.SynchronisierungVentil(kraftWerk.Ventil_Y);
            kraftWerk.generator.SynchronisierungErregerstrom(kraftWerk.Generator_Ie);
        }

        public void Doing()
        {
            kraftWerk.generator.SynchronisiertFrequenz(kraftWerk.Netz_f); // Die Drehzahl wird durch die Netzfrequenz vorgegeben
            kraftWerk.Generator_n = kraftWerk.generator.Drehzahl();
            kraftWerk.Generator_U = kraftWerk.Netz_U;
            kraftWerk.Generator_f = kraftWerk.Netz_f;
            kraftWerk.ViAnzeige.MessgeraetAnzeigen(false);

            kraftWerk.generator.MaschineLeistungFahren(kraftWerk.Ventil_Y);
            kraftWerk.generator.PhasenSchieberbetrieb(kraftWerk.Generator_Ie);
            kraftWerk.Generator_P = kraftWerk.generator.Leistung();
            kraftWerk.Generator_Phasenverschiebung = kraftWerk.generator.Winkel();

        }

        public void OnExit()
        {
            // nichts zu tun
        }
    }
}