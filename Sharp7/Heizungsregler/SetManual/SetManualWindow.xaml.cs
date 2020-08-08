namespace Heizungsregler
{
    public partial class SetManualWindow
    {
        public SetManualWindow(Heizungsregler.ViewModel.ViewModel heizungsreglerViewModel)
        {
            InitializeComponent();
            DataContext = heizungsreglerViewModel;
        }
    }
}