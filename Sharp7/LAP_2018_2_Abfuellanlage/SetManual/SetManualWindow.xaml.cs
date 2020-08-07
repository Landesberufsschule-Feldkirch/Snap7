namespace LAP_2018_2_Abfuellanlage.SetManual
{
    public partial class SetManual
    {
        public SetManual(LAP_2018_2_Abfuellanlage.ViewModel.ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}