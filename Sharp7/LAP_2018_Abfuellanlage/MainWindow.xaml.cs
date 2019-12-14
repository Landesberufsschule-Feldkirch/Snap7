using System.Windows;
using System.Windows.Controls;

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

        private void BtnF5_Click(object sender, RoutedEventArgs e)
        {
            F5 = !F5;
        }

        private void BtnS1_Click(object sender, RoutedEventArgs e)
        {
            S1 = RichtungUmdrehen(btnS1);
        }
        private void BtnS2_Click(object sender, RoutedEventArgs e)
        {
            S2 = !RichtungUmdrehen(btnS2);
        }
        private void BtnS3_Click(object sender, RoutedEventArgs e)
        {
            S3 = RichtungUmdrehen(btnS3);
        }
        private void BtnS4_Click(object sender, RoutedEventArgs e)
        {
            S4 = RichtungUmdrehen(btnS4);
        }

        private bool RichtungUmdrehen(Button knopf)
        {
            if (knopf.ClickMode == System.Windows.Controls.ClickMode.Press)
            {
                knopf.ClickMode = System.Windows.Controls.ClickMode.Release;
                return true;
            }
            else
            {
                knopf.ClickMode = System.Windows.Controls.ClickMode.Press;
                return false;
            }
        }

    }
}
