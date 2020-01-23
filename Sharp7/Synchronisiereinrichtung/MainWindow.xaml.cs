using Kommunikation;
using Synchronisiereinrichtung.Kraftwerk.ViewModel;
using System.Windows;

namespace Synchronisiereinrichtung
{
    public partial class MainWindow : Window
    {
        public bool Q1;
        public bool Q1alt;
        public bool S1;
        public bool S2;

        public int Y;
        public int Ie;

        public bool TaskAktiv;
        public bool DatenRangierenAktiv = true;
        public bool FensterAktiv = true;
        public bool DebugWindowAktiv;

        private DatenRangieren datenRangieren;

        public SetManualWindow setManualWindow;
        public RealTimeGraphWindow realTimeGraphWindow;
        public KraftwerkViewModel _kraftwerkViewModel;

        public MainWindow()
        {
            datenRangieren = new DatenRangieren(this);

            _kraftwerkViewModel = new KraftwerkViewModel();

            InitializeComponent();
            DataContext = _kraftwerkViewModel;

            GaugeDifferenzSpannung.DataContext = _kraftwerkViewModel;

            S7_1200 s7_1200 = new S7_1200(2, 2, 100, 100, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            if (System.Diagnostics.Debugger.IsAttached) btnDebugWindow.Visibility = System.Windows.Visibility.Visible;
            else btnDebugWindow.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
            Application.Current.Shutdown();
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            setManualWindow = new SetManualWindow(_kraftwerkViewModel);
            setManualWindow.Show();
        }

        private void GraphWindow_Click(object sender, RoutedEventArgs e)
        {
            realTimeGraphWindow = new RealTimeGraphWindow(_kraftwerkViewModel);
            realTimeGraphWindow.Show();
        }
    }
}