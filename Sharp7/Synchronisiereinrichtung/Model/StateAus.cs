namespace Synchronisiereinrichtung.Model;

public class StateAus
{
    private readonly Kraftwerk.Model.Kraftwerk _kraftWerk;

    public StateAus(Kraftwerk.Model.Kraftwerk kw) => _kraftWerk = kw;
    public void Doing()
    {
        _kraftWerk.GeneratorN = 0;
        _kraftWerk.GeneratorU = 0;
        _kraftWerk.GeneratorF = 0;
        _kraftWerk.GeneratorP = 0;
        _kraftWerk.GeneratorCosPhi = 1;

        _kraftWerk.MessgeraetAnzeigen = false;

        if (_kraftWerk.VentilY > 0) _kraftWerk.KraftwerkStatemachine.Fire(Statemachine.Trigger.Hochfahren);
    }
}