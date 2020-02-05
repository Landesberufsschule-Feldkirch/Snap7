namespace BehaelterSteuerung.Model
{
    public class Behaelter
    {        
        public double Pegel { get; set; }
        public bool SchwimmerschalterOben { get; set; }
        public bool SchwimmerschalterUnten { get; set; }
        public bool VentilOben { get; set; }
        public bool VentilUnten { get; set; }

                          
        private double InternerPegel;
        private readonly double SinkGeschwindigkeit = 0.005;
        private readonly double FuellGeschwindigkeit = 0.2 * 0.005;
        private readonly double PositionSchwimmerschalterOben = 0.95;
        private readonly double PositionSchwimmerschalterUnten = 0.05;
        
        public Behaelter(double pegel)
        {
            Pegel = pegel;
        }

        public void PegelUeberwachen()
        {
            if (InternerPegel > 0)
            {
                InternerPegel -= SinkGeschwindigkeit;
                Pegel = InternerPegel;
            }
            else
            {
                if (VentilOben) Pegel += FuellGeschwindigkeit;
                if (VentilUnten) Pegel -= SinkGeschwindigkeit;
            }

            if (Pegel > 1) Pegel = 1;
            if (Pegel < 0) Pegel = 0;

            SchwimmerschalterOben = (Pegel > PositionSchwimmerschalterOben);
            SchwimmerschalterUnten = (Pegel > PositionSchwimmerschalterUnten);
        }
      
        public void AutomatikmodusStarten(double Startpegel)
        {
            this.InternerPegel = Startpegel;
            VentilUnten = true;
        }
        internal void VentilUntenUmschalten()
        {
            if (VentilUnten) VentilUnten = false; else VentilUnten = true; ;
        }
    }
}