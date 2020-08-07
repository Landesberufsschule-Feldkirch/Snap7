namespace LAP_2010_5_Pumpensteuerung.SetManual
{
    public partial class SetManual
    {
        public SetManual(LAP_2010_5_Pumpensteuerung.ViewModel.ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}