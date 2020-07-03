using System.Windows;

namespace Heizungsregler
{
    public partial class RealTimeGraphWindow : Window
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