namespace _3dHofschiebetor.SetManual
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