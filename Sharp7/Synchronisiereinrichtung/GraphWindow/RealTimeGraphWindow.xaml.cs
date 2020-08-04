namespace Synchronisiereinrichtung
{
    public partial class RealTimeGraphWindow
    {
        public RealTimeGraphWindow(kraftwerk.ViewModel.ViewModel kraftwerkViewModel)
        {
            InitializeComponent();
            DataContext = kraftwerkViewModel.Schreiber;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //
        }
    }
}