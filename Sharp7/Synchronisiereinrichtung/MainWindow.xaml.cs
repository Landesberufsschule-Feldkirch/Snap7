using Kommunikation;
using Synchronisiereinrichtung.kraftwerk.ViewModel;
using System.Windows;

namespace Synchronisiereinrichtung
{
    public partial class MainWindow : Window
    {
        public bool DebugWindowAktiv { get; set; }
        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private SetManualWindow setManualWindow;
        private RealTimeGraphWindow realTimeGraphWindow;
        private readonly ViewModel viewModel;
        private const int anzByteVersion = 10;
        private const int anzByteDigInput = 1;
        private const int anzByteDigOutput = 1;
        private const int anzByteAnalogInput = 20;
        private const int anzByteAnalogOutput = 4;
        public MainWindow()
        {
            viewModel = new ViewModel(this);

            InitializeComponent();

            DataContext = viewModel;
            GaugeDifferenzSpannung.DataContext = viewModel;
            datenRangieren = new DatenRangieren(this, viewModel);
            GaugeDifferenzSpannung.ApplyTemplate();

            S7_1200 = new S7_1200(anzByteVersion, anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput,datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

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