namespace SiebenSegmentAnzeige.ViewModel
{
    public class ViewModel
    {
        public  SiebenSegmentDisplay SiebenSegmentDisplay { get; }
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(SiebenSegmentDisplay siebenSegmentDisplay)
        {
            SiebenSegmentDisplay = new SiebenSegmentDisplay();
            ViAnzeige = new VisuAnzeigen(siebenSegmentDisplay);
        }
    }
}