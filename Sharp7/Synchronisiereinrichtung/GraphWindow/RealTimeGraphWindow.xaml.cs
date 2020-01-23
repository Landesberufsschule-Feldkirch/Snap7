using System.Windows;

namespace Synchronisiereinrichtung
{
    public partial class RealTimeGraphWindow : Window
    {
        public RealTimeGraphWindow(Synchronisiereinrichtung.Kraftwerk.ViewModel.KraftwerkViewModel kraftwerkViewModel)
        {
            InitializeComponent();
            DataContext = kraftwerkViewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
#pragma warning disable S125 // Sections of code should not be commented out
            // Application.Current.Shutdown();
#pragma warning restore S125 // Sections of code should not be commented out
        }
    }
}