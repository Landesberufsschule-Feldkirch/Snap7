namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    class StateSynchronisieren
    {
        private readonly Kraftwerk kraftWerk;

        public StateSynchronisieren(Kraftwerk kw)
        {
            kraftWerk = kw;
        }

        public void OnEntry()
        {
            kraftWerk.Q1 = true;

            switch (kraftWerk.ViSoll.SynchAuswahl)
            {
                case SynchronisierungAuswahl.U_f:
                    if (kraftWerk.FrequenzDifferenz > 2) kraftWerk.ViAnzeige.MaschineTot(true);
                    if (kraftWerk.SpannungsdifferenzGeneratorNetz > 5) kraftWerk.ViAnzeige.MaschineTot(true);
                    break;

                case SynchronisierungAuswahl.U_f_Phase:
                case SynchronisierungAuswahl.U_f_Phase_Leistung:
                case SynchronisierungAuswahl.U_f_Phase_Leistungsfaktor:
                    if (kraftWerk.FrequenzDifferenz > 0.5) kraftWerk.ViAnzeige.MaschineTot(true);
                    if (kraftWerk.SpannungsdifferenzGeneratorNetz > 100) kraftWerk.ViAnzeige.MaschineTot(true);
                    break;

                default:
                    break;
            }
        }

        public void Doing()
        {
            kraftWerk.ViAnzeige.MessgeraetAnzeigen(false);

            if (kraftWerk.ViAnzeige.IstMaschineTot())
            {
                kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.Ausschalten);
            }
            else
            {
                kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.Belasten);
            }
        }

        public void OnExit()
        {
            // nichts zu tun
        }
    }
}