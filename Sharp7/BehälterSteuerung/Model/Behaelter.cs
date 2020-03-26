namespace BehaelterSteuerung.Model
{
    public class Behaelter
    {
        public double Pegel { get; set; }
        public bool SchwimmerschalterOben { get; set; }
        public bool SchwimmerschalterUnten { get; set; }
        public bool VentilOben { get; set; }
        public bool VentilUnten { get; set; }

        private bool automatikModus;
        private double internerPegel;
        private readonly double sinkGeschwindigkeit = 0.005;
        private readonly double fuellGeschwindigkeit = 0.2 * 0.005;
        private readonly double positionSchwimmerschalterOben = 0.95;
        private readonly double positionSchwimmerschalterUnten = 0.05;

        public Behaelter(double pegel)
        {
            automatikModus = false;
            Pegel = pegel;
        }
        public bool AutomatikModus() => automatikModus;

        public void PegelUeberwachen()
        {
            if (automatikModus && internerPegel < 0.01)
            {
                automatikModus = false;
                VentilUnten = false;
            }

            if (internerPegel > 0)
            {
                internerPegel -= sinkGeschwindigkeit;
                Pegel = internerPegel;
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

        public void AutomatikmodusStarten(double Startpegel)
        {
            automatikModus = true;
            this.internerPegel = Startpegel;
            VentilUnten = true;
        }
        internal void VentilUntenUmschalten()
        {
            if (!automatikModus)
            {
                if (VentilUnten) VentilUnten = false; else VentilUnten = true;
            }
        }
    }
}