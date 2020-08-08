namespace LAP_2010_4_Abfuellanlage.SetManual
{
    public partial class SetManual
    {
        public SetManual(LAP_2010_4_Abfuellanlage.ViewModel.ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}