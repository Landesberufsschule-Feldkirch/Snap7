using Kommunikation;
using System.Windows;

namespace Heizungsregler
{
    public partial class MainWindow : Window
    {


        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private RealTimeGraphWindow realTimeGraphWindow;
        private readonly ViewModel viewModel;

        public MainWindow()
        {
            viewModel = new ViewModel(this);

            InitializeComponent();

            DataContext = viewModel;

            datenRangieren = new DatenRangieren(this, viewModel);

            S7_1200 = new S7_1200(1, 1, 20, 4, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GraphWindow_Click(object sender, RoutedEventArgs e)
        {
            realTimeGraphWindow = new RealTimeGraphWindow(viewModel);
            realTimeGraphWindow.Show();
        }
    }
}
