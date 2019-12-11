using System.Windows;

namespace LAP_2018_Abfuellanlage
{

    public partial class MainWindow : Window
    {
        public bool TaskAktiv;
        public bool DatenRangierenAktiv = true;
        public bool FensterAktiv = true;
        public MainWindow()
        {
            InitializeComponent();
            EinAusgabeFelderInitialisieren();
            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
            System.Threading.Tasks.Task.Run(() => Logikfunktionen_Task());

        }

            private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }

        private void btnF5_Click(object sender, RoutedEventArgs e)
        {
            F5 = !F5;
        }

        private void btnS1_Click(object sender, RoutedEventArgs e)
        {
            S1_Zaehler = 10;
        }
        private void btnS2_Click(object sender, RoutedEventArgs e)
        {
            S2_Zaehler = 10;
        }
        private void btnS3_Click(object sender, RoutedEventArgs e)
        {
            S3_Zaehler = 10;
        }
        private void btnS4_Click(object sender, RoutedEventArgs e)
        {
            S4_Zaehler = 10;
        }
    }
}
