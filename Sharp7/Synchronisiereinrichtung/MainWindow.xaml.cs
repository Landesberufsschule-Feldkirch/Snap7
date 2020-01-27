using Kommunikation;
using Synchronisiereinrichtung.Kraftwerk.ViewModel;
using System.Windows;

namespace Synchronisiereinrichtung
{
    public partial class MainWindow : Window
    {
        public bool DebugWindowAktiv;

        private DatenRangieren datenRangieren;
        private SetManualWindow setManualWindow;
        private RealTimeGraphWindow realTimeGraphWindow;
        private KraftwerkViewModel kraftwerkViewModel;

        public MainWindow()
        {
            kraftwerkViewModel = new KraftwerkViewModel();

            InitializeComponent();

            DataContext = kraftwerkViewModel;
            GaugeDifferenzSpannung.DataContext = kraftwerkViewModel;
            datenRangieren = new DatenRangieren(this, kraftwerkViewModel);


            S7_1200 s7_1200 = new S7_1200(2, 2, 100, 100, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

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
            setManualWindow = new SetManualWindow(kraftwerkViewModel);
            setManualWindow.Show();
        }

        private void GraphWindow_Click(object sender, RoutedEventArgs e)
        {
            realTimeGraphWindow = new RealTimeGraphWindow(kraftwerkViewModel);
            realTimeGraphWindow.Show();
        }
    }
}