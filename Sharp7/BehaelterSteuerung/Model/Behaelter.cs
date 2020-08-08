namespace BehälterSteuerung.Model
{
    public class Behaelter
    {
        public double Pegel { get; set; }
        public bool SchwimmerschalterOben { get; set; }
        public bool SchwimmerschalterUnten { get; set; }
        public bool VentilOben { get; set; }
        public bool VentilUnten { get; set; }

        private bool _automatikModus;
        private double _internerPegel;
        private readonly double sinkGeschwindigkeit = 0.005;
        private readonly double fuellGeschwindigkeit = 0.2 * 0.005;
        private readonly double positionSchwimmerschalterOben = 0.95;
        private readonly double positionSchwimmerschalterUnten = 0.05;

        public Behaelter(double pegel)
        {
            _automatikModus = false;
            Pegel = pegel;
        }

        public bool AutomatikModus() => _automatikModus;

        public void PegelUeberwachen()
        {
            if (_automatikModus && _internerPegel < 0.01)
            {
                _automatikModus = false;
                VentilUnten = false;
            }

            if (_internerPegel > 0)
            {
                _internerPegel -= sinkGeschwindigkeit;
                Pegel = _internerPegel;
            }
            else
            {
                if (VentilOben) Pegel += fuellGeschwindigkeit;
                if (VentilUnten) Pegel -= sinkGeschwindigkeit;
            }

            if (Pegel > 1) Pegel = 1;
            if (Pegel < 0) Pegel = 0;

            SchwimmerschalterOben = (Pegel > positionSchwimmerschalterOben);
            SchwimmerschalterUnten = (Pegel > positionSchwimmerschalterUnten);
        }

        public void AutomatikmodusStarten(double startpegel)
        {
            _automatikModus = true;
            this._internerPegel = startpegel;
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
}