using System.Windows;

namespace BehälterSteuerung
{
    public partial class MainWindow : Window
    {
        public bool TaskAktiv;
        public bool DatenRangierenAktiv = true;
        public bool FensterAktiv = true;

        Logikfunktionen logikfunktionen;
        DatenRangieren datenRangieren;
        public MainWindow()
        {
            logikfunktionen = new Logikfunktionen();
            datenRangieren = new DatenRangieren(logikfunktionen);

            InitializeComponent();
            EinAusgabeFelderInitialisieren();
            AlleBehaelterInitialisieren();

            S7_1200 s7_1200 = new S7_1200(10, 0, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
            System.Threading.Tasks.Task.Run(() => Logikfunktionen_Task());
            System.Threading.Tasks.Task.Run(() => Display_Task());
        }
           
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }

        private void btn_Q2_Ein_Click(object sender, RoutedEventArgs e)
        {
            if (!AutomatikModusNochAktiv) gAlleBehaelter[0].VentilUnten = false;
        }
        private void btn_Q2_Aus_Click(object sender, RoutedEventArgs e)
        {
            if (!AutomatikModusNochAktiv) gAlleBehaelter[0].VentilUnten = true;
        }
        private void btn_Q4_Ein_Click(object sender, RoutedEventArgs e)
        {
            if (!AutomatikModusNochAktiv) gAlleBehaelter[1].VentilUnten = false;
        }
        private void btn_Q4_Aus_Click(object sender, RoutedEventArgs e)
        {
            if (!AutomatikModusNochAktiv) gAlleBehaelter[1].VentilUnten = true;
        }
        private void btn_Q6_Ein_Click(object sender, RoutedEventArgs e)
        {
            if (!AutomatikModusNochAktiv) gAlleBehaelter[2].VentilUnten = false;
        }
        private void btn_Q6_Aus_Click(object sender, RoutedEventArgs e)
        {
            if (!AutomatikModusNochAktiv) gAlleBehaelter[2].VentilUnten = true;
        }
        private void btn_Q8_Ein_Click(object sender, RoutedEventArgs e)
        {
            if (!AutomatikModusNochAktiv) gAlleBehaelter[3].VentilUnten = false;
        }
        private void btn_Q8_Aus_Click(object sender, RoutedEventArgs e)
        {
            if (!AutomatikModusNochAktiv) gAlleBehaelter[3].VentilUnten = true;
        }

        private void btn_1234_Click(object sender, RoutedEventArgs e)
        {
            AutomatikBetriebStarten(AutomatikModus.Modus_1234);
            AutomatikKnoepfeDeaktivieren();
        }
        private void btn_1324_Click(object sender, RoutedEventArgs e)
        {
            AutomatikBetriebStarten(AutomatikModus.Modus_1324);
            AutomatikKnoepfeDeaktivieren();
        }
        private void btn_1432_Click(object sender, RoutedEventArgs e)
        {
            AutomatikBetriebStarten(AutomatikModus.Modus_1432);
            AutomatikKnoepfeDeaktivieren();
        }
        private void btn_4321_Click(object sender, RoutedEventArgs e)
        {
            AutomatikBetriebStarten(AutomatikModus.Modus_4321);
            AutomatikKnoepfeDeaktivieren();
        }


    }
}
