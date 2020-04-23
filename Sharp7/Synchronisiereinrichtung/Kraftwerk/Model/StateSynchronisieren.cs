namespace Synchronisiereinrichtung.kraftwerk.Model
{
    internal class StateSynchronisieren
    {
        private readonly Kraftwerk kraftWerk;

        public StateSynchronisieren(Kraftwerk kw)
        {
            kraftWerk = kw;
        }

        public void OnEntry()
        {
            kraftWerk.Q1 = true;
            kraftWerk.MessgeraetAnzeigen = false;

            switch (kraftWerk.SynchAuswahl)
            {
                case SynchronisierungAuswahl.U_f:
                    if ((kraftWerk.FrequenzDifferenz > 0.5) || (kraftWerk.SpannungsdifferenzGeneratorNetz > 5)) kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.MaschineTot);
                    else kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.Belasten);
                    break;

                case SynchronisierungAuswahl.U_f_Phase:
                case SynchronisierungAuswahl.U_f_Phase_Leistung:
                case SynchronisierungAuswahl.U_f_Phase_Leistungsfaktor:
                    if ((kraftWerk.FrequenzDifferenz > 0.5) || (kraftWerk.SpannungsdifferenzGeneratorNetz > 100)) kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.MaschineTot);
                    else kraftWerk.kraftwerkStatemachine.Fire(Statemachine.Trigger.Belasten);
                    break;

                default:
                    break;
            }
        }
    }
}