using System.Windows;

namespace Synchronisiereinrichtung
{
    public partial class RealTimeGraphWindow : Window
    {
        private readonly MainWindow mainWindow;
        public RealTimeGraphWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
            DataContext = new MainWindowVM(mainWindow);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

#pragma warning disable S125 // Sections of code should not be commented out
            // Application.Current.Shutdown();
#pragma warning restore S125 // Sections of code should not be commented out
        }
    }
}
