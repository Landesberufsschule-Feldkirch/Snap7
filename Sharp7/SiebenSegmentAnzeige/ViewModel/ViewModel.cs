namespace SiebenSegmentAnzeige.ViewModel
{
    public class ViewModel
    {
        public SiebenSegmentAnzeige SiebenSegmentAnzeige{ get; }
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(UserControl uc)
        {
            SiebenSegmentAnzeige = new SiebenSegmentAnzeige();
            ViAnzeige = new VisuAnzeigen(uc);
        }
    }
}