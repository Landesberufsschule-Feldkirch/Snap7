namespace LAP_2018_3_Hydraulikaggregat.SetManual
{
    public partial class SetManualWindow
    {
        public SetManualWindow(ViewModel.ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}