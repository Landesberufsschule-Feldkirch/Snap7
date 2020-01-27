namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    class KraftwerkStateHochfahren
    {
        private readonly Kraftwerk kraftWerk;

        public KraftwerkStateHochfahren(Kraftwerk kw)
        {
            kraftWerk = kw;
        }

        public void KraftwerkHochfahrenErsterDurchlauf()
        {
            kraftWerk.ViAnzeige.LeistungsschalterEinschalten(false);
        }

        public KraftwerkStatemachine.Trigger Hochfahren()
        {
            kraftWerk.generator.MaschineAntreiben(kraftWerk.Ventil_Y);
            kraftWerk.Generator_n = kraftWerk.generator.Drehzahl();
            kraftWerk.Generator_U = kraftWerk.generator.Spannung(kraftWerk.Generator_Ie);
            kraftWerk.Generator_f = kraftWerk.generator.Frequenz();

            return KraftwerkStatemachine.Trigger.Null;
        }

    }
}
