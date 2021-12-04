using System;

namespace Synchronisiereinrichtung.Model;

internal class StateSynchronisieren
{
    private readonly Kraftwerk.Model.Kraftwerk _kraftWerk;

    public StateSynchronisieren(Kraftwerk.Model.Kraftwerk kw) => _kraftWerk = kw;
    public void OnEntry()
    {
        _kraftWerk.Q1 = true;
        _kraftWerk.MessgeraetAnzeigen = false;

        switch (_kraftWerk.SynchAuswahl)
        {
            case SynchronisierungAuswahl.Uf:
                if (_kraftWerk.FrequenzDifferenz > 0.5 || _kraftWerk.SpannungsdifferenzGeneratorNetz > 5) _kraftWerk.KraftwerkStatemachine.Fire(Statemachine.Trigger.MaschineTot);
                else _kraftWerk.KraftwerkStatemachine.Fire(Statemachine.Trigger.Belasten);
                break;

            case SynchronisierungAuswahl.UfPhase:
            case SynchronisierungAuswahl.UfPhaseLeistung:
            case SynchronisierungAuswahl.UfPhaseLeistungsfaktor:
                if (_kraftWerk.FrequenzDifferenz > 0.5 || _kraftWerk.SpannungsdifferenzGeneratorNetz > 100) _kraftWerk.KraftwerkStatemachine.Fire(Statemachine.Trigger.MaschineTot);
                else _kraftWerk.KraftwerkStatemachine.Fire(Statemachine.Trigger.Belasten);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}