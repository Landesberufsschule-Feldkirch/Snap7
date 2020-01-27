namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    public class KraftwerkStateAus
    {
        private readonly Kraftwerk kraftWerk;

        public KraftwerkStateAus(Kraftwerk kw)
        {
            kraftWerk = kw;
        }

        public void KraftwerkAusErsterDurchlauf()
        {
            kraftWerk.ViAnzeige.LeistungsschalterEinschalten(false);
        }

        public KraftwerkStatemachine.Trigger Aus()
        {
            kraftWerk.Generator_n = 0;
            kraftWerk.Generator_U = 0;
            kraftWerk.Generator_f = 0;

            if (kraftWerk.Ventil_Y > 0) return KraftwerkStatemachine.Trigger.Starten;

            return KraftwerkStatemachine.Trigger.Null;
        }

    }
}
