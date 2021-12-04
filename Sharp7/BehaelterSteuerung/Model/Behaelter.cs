namespace BehaelterSteuerung.Model;

public class Behaelter
{
    public double Pegel { get; set; }
    public bool SchwimmerschalterOben { get; set; }
    public bool SchwimmerschalterUnten { get; set; }
    public bool VentilOben { get; set; }
    public bool VentilUnten { get; set; }

    private bool _automatikModus;
    private double _internerPegel;
    private const double SinkGeschwindigkeit = 0.005;
    private const double FuellGeschwindigkeit = 0.2 * 0.005;
    private const double PositionSchwimmerschalterOben = 0.95;
    private const double PositionSchwimmerschalterUnten = 0.05;

    public Behaelter(double pegel)
    {
        _automatikModus = false;
        Pegel = pegel;
    }

    public void PegelUeberwachen()
    {
        if (_automatikModus && _internerPegel < 0.01)
        {
            _automatikModus = false;
            VentilUnten = false;
        }

        if (_internerPegel > 0)
        {
            _internerPegel -= SinkGeschwindigkeit;
            Pegel = _internerPegel;
        }
        else
        {
            if (VentilOben) Pegel += FuellGeschwindigkeit;
            if (VentilUnten) Pegel -= SinkGeschwindigkeit;
        }

        if (Pegel > 1) Pegel = 1;
        if (Pegel < 0) Pegel = 0;

        SchwimmerschalterOben = Pegel > PositionSchwimmerschalterOben;
        SchwimmerschalterUnten = Pegel > PositionSchwimmerschalterUnten;
    }

    public void AutomatikmodusStarten(double startpegel)
    {
        _automatikModus = true;
        _internerPegel = startpegel;
        VentilUnten = true;
    }

    internal void VentilUntenUmschalten()
    {
        if (!_automatikModus)
        {
            VentilUnten = !VentilUnten;
        }
    }
}