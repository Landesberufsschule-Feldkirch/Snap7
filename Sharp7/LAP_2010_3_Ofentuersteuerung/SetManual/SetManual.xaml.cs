namespace LAP_2010_3_Ofentuersteuerung.SetManual
{
    public partial class SetManual
    {
        public SetManual(LAP_2010_3_Ofentuersteuerung.ViewModel.ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}