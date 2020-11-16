namespace SiebenSegmentAnzeige.ViewModel
{
    public class ViewModel
    {
     //   public  SiebenSegmentDisplay SiebenSegmentDisplay { get; }
        public VisuAnzeigen VisAnzeige { get; set; }

        public ViewModel(SiebenSegmentDisplay siebenSegmentDisplay)
        {
           // SiebenSegmentDisplay = new SiebenSegmentDisplay();
            VisAnzeige = new VisuAnzeigen(siebenSegmentDisplay);
        }
    }
}