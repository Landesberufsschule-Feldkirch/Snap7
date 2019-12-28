using Kommunikation;
using System.Windows;

namespace BehälterSteuerung
{
    public partial class MainWindow : Window
    {

        public enum AutomatikModus
        {
            Modus_1234 = 0,
            Modus_1324,
            Modus_1432,
            Modus_4321
        }

        public bool FensterAktiv = true;
        public bool Leuchte_P1 = false;

        public double PegelOffset_1;
        public double PegelOffset_2;
        public double PegelOffset_3;
        public double PegelOffset_4;

        public bool Tank_1_AutomatischEntleeren;
        public bool Tank_2_AutomatischEntleeren;
        public bool Tank_3_AutomatischEntleeren;
        public bool Tank_4_AutomatischEntleeren;

        public bool AutomatikModusNochAktiv = false;

        Logikfunktionen logikfunktionen;
        DatenRangieren datenRangieren;
        public MainWindow()
        {
            logikfunktionen = new Logikfunktionen(this);
            datenRangieren = new DatenRangieren(this);

            InitializeComponent();
            AlleBehaelterInitialisieren();

            S7_1200 s7_1200 = new S7_1200(10, 0, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            System.Threading.Tasks.Task.Run(() => logikfunktionen.Logikfunktionen_Task());
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


        public void AutomatikBetriebStarten(AutomatikModus Modus)
        {
            switch (Modus)
            {
                case AutomatikModus.Modus_1234:
                    gAlleBehaelter[0].AutomatikmodusStarten(1.2);
                    gAlleBehaelter[1].AutomatikmodusStarten(2.4);
                    gAlleBehaelter[2].AutomatikmodusStarten(3.6);
                    gAlleBehaelter[3].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus_1324:
                    gAlleBehaelter[0].AutomatikmodusStarten(1.2);
                    gAlleBehaelter[2].AutomatikmodusStarten(2.4);
                    gAlleBehaelter[1].AutomatikmodusStarten(3.6);
                    gAlleBehaelter[3].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus_1432:
                    gAlleBehaelter[0].AutomatikmodusStarten(1.2);
                    gAlleBehaelter[3].AutomatikmodusStarten(2.4);
                    gAlleBehaelter[2].AutomatikmodusStarten(3.6);
                    gAlleBehaelter[1].AutomatikmodusStarten(4.8);
                    break;

                case AutomatikModus.Modus_4321:
                    gAlleBehaelter[3].AutomatikmodusStarten(1.2);
                    gAlleBehaelter[2].AutomatikmodusStarten(2.4);
                    gAlleBehaelter[1].AutomatikmodusStarten(3.6);
                    gAlleBehaelter[0].AutomatikmodusStarten(4.8);
                    break;

                default:
                    gAlleBehaelter[3].AutomatikmodusStarten(0);
                    gAlleBehaelter[2].AutomatikmodusStarten(0);
                    gAlleBehaelter[1].AutomatikmodusStarten(0);
                    gAlleBehaelter[0].AutomatikmodusStarten(0);
                    break;
            }
        }


    }
}
