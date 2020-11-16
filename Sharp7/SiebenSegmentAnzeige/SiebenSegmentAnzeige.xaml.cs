namespace SiebenSegmentAnzeige
{
    public partial class SiebenSegmentDisplay
    {
        public SiebenSegmentDisplay()
        {

            ColorAllSegments = "yellow";

            VisibilitySegmentA = "visible";
            VisibilitySegmentB = "visible";
            VisibilitySegmentC = "visible";
            VisibilitySegmentD = "visible";
            VisibilitySegmentE = "visible";
            VisibilitySegmentF = "visible";
            VisibilitySegmentG = "visible";
            VisibilitySegmentDp = "visible";

            var viewModel = new ViewModel.ViewModel(this);

            DataContext = viewModel;
            InitializeComponent();
        }
    }
}