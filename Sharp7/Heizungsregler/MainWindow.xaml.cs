using Kommunikation;
using System.Windows;

namespace Heizungsregler
{
    public partial class MainWindow : Window
    {

        public bool DebugWindowAktiv { get; set; }

        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private RealTimeGraphWindow realTimeGraphWindow;
        private readonly Heizungsregler.ViewModel.ViewModel viewModel;
        private SetManualWindow setManualWindow;

        public MainWindow()
        {
            viewModel = new Heizungsregler.ViewModel.ViewModel(this);

            InitializeComponent();

            DataContext = viewModel;

            datenRangieren = new DatenRangieren(this, viewModel);

            S7_1200 = new S7_1200(1, 1, 20, 4, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            if (System.Diagnostics.Debugger.IsAttached) btnDebugWindow.Visibility = System.Windows.Visibility.Visible;
            else btnDebugWindow.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            setManualWindow = new SetManualWindow(viewModel);
            setManualWindow.Show();
        }

        private void GraphWindow_Click(object sender, RoutedEventArgs e)
        {
            realTimeGraphWindow = new RealTimeGraphWindow(viewModel);
            realTimeGraphWindow.Show();
        }
    }
}
