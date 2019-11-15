using Sharp7;
using System.Windows;

namespace BehälterSteuerung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        public const float Rahmenbreite_1px = 1;
        public const float Rahmenbreite_5px = 5;

 
        public bool TaskAktiv;
        public bool DatenRangierenAktiv = true;
        public bool FensterAktiv = true;



        public bool AutomatikModusAktiv;
        public string AutomatikSchrittschaltwerk;
        public bool Automatik_123_Aktiv;
        public bool Automatik_132_Aktiv;
        public bool Automatik_321_Aktiv;

        public MainWindow()
        {
            InitializeComponent();
            EinAusgabeFelderInitialisieren();
            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
            System.Threading.Tasks.Task.Run(() => Logikfunktionen_Task());
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            Pegel_1 = 0.7;
            Pegel_2 = 0.5;
            Pegel_3 = 0.3;

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

        private void btn_Q2_Ein_Click(object sender, RoutedEventArgs e)
        {
            if (!AutomatikModusAktiv) Ventil_Q2 = false;
        }
        private void btn_Q2_Aus_Click(object sender, RoutedEventArgs e)
        {
            if (!AutomatikModusAktiv) Ventil_Q2 = true;
        }
        private void btn_Q4_Ein_Click(object sender, RoutedEventArgs e)
        {
            if (!AutomatikModusAktiv) Ventil_Q4 = false;
        }
        private void btn_Q4_Aus_Click(object sender, RoutedEventArgs e)
        {
            if (!AutomatikModusAktiv) Ventil_Q4 = true;
        }
        private void btn_Q6_Ein_Click(object sender, RoutedEventArgs e)
        {
            if (!AutomatikModusAktiv) Ventil_Q6 = false;
        }
        private void btn_Q6_Aus_Click(object sender, RoutedEventArgs e)
        {
            if (!AutomatikModusAktiv) Ventil_Q6 = true;
        }

        private void btn_123_Click(object sender, RoutedEventArgs e)
        {
            AutomatikModusAktiv = true;
            AutomatikSchrittschaltwerk = "Init";
            Automatik_123_Aktiv = true;

            AutomatikDeaktivieren();
        }
        private void btn_132_Click(object sender, RoutedEventArgs e)
        {
            AutomatikModusAktiv = true;
            AutomatikSchrittschaltwerk = "Init";
            Automatik_132_Aktiv = true;

            AutomatikDeaktivieren();
        }
        private void btn_321_Click(object sender, RoutedEventArgs e)
        {
            AutomatikModusAktiv = true;
            AutomatikSchrittschaltwerk = "Init";
            Automatik_321_Aktiv = true;

            AutomatikDeaktivieren();
        }

        private void AutomatikDeaktivieren()
        {
            this.Dispatcher.Invoke(() =>
            {
                btn_Automatik_123.IsEnabled = false;
                btn_Automatik_132.IsEnabled = false;
                btn_Automatik_321.IsEnabled = false;
            });
        }

        private void AutomatikAktivieren()
        {
            this.Dispatcher.Invoke(() =>
            {
                btn_Automatik_123.IsEnabled = true;
                btn_Automatik_132.IsEnabled = true;
                btn_Automatik_321.IsEnabled = true;
            });
        }
    }
}
