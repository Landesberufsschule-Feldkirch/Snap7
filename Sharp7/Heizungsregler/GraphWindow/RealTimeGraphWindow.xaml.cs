namespace Heizungsregler
{
    public partial class RealTimeGraphWindow
    {
        public RealTimeGraphWindow(Heizungsregler.ViewModel.ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel.Schreiber;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //
        }
    }
}