namespace Nadeltelegraph.SetManual
{
    public partial class SetManual
    {
        public SetManual(Nadeltelegraph.ViewModel.ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}