namespace LAP_2010_2_Transportwagen.SetManual
{
    public partial class SetManual
    {
        public SetManual(LAP_2010_2_Transportwagen.ViewModel.ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}