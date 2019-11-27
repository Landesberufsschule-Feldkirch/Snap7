using System.Windows;



namespace LAP_2018_Niveauregelung
{

    public partial class MainWindow : Window
    {

        public bool DatenRangierenAktiv = true;
        public bool FensterAktiv = true;


        public MainWindow()
        {
            InitializeComponent();
            EinAusgabeFelderInitialisieren();
            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
            System.Threading.Tasks.Task.Run(() => Logikfunktionen_Task());

        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            VerbindungErstellen();
        }

        private void ButtonDisconnect_Click(object sender, RoutedEventArgs e)
        {
            VerbindungTrennen();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }


        private void VentilClick(object sender, RoutedEventArgs e)
        {
            Y1 = !Y1;
        }


        private void BtnF1_Click(object sender, RoutedEventArgs e)
        {
            F1 = !F1;
        }

        private void BtnF2_Click(object sender, RoutedEventArgs e)
        {
            F2 = !F2;
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

    }
}
