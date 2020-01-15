using Kommunikation;
using System.Windows;

namespace LAP_2019_Foerderanlage
{
    public partial class MainWindow : Window
    {
        public SetManualWindow setManualWindow;

        public bool S0;     // Anlage Aus
        public bool S1;     // Anlage Ein
        public bool S2;     // Not-Halt
        public bool F4;     // Störung Motorschutzschalter
        public bool S4;
        public bool S5;
        public bool S6;
        public bool S7;     // Wagen Position rechts
        public bool S8;     // Wagen voll


        public bool P1;     // Anlage Ein
        public bool P2;     // Sammelstörung
        public bool Q3_RL;  // Förderband Rechtslauf
        public bool Q4_LL;  // Förderband Linkslauf
        public bool XFU;    // Freigabe FU (Schneckenförderer)

        public bool Y1;     // Materialschieber Silo

        public short MaterialsiloPegel; // für die SPS1
        public int FuSpeed; // von der SPS

        public enum Richtung
        {
            stehen = 0,
            nachLinks,
            nachRechts
        }

        public Richtung WagenRichtung;
        public double WagenPosition_X;
        public double WagenPosition_Y;
        public double WagenFuellstand;

        public double MaterialSiloFuellstand = 0.9;

        public bool DebugWindowAktiv;

        public bool FensterAktiv = true;

        public bool AnimationGestartet;

        Logikfunktionen logikfunktionen;
        DatenRangieren datenRangieren;

        public MainWindow()
        {

            logikfunktionen = new Logikfunktionen(this);
            datenRangieren = new DatenRangieren(this);

            InitializeComponent();

            S7_1200 s7_1200 = new S7_1200(2, 2, 2, 2, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            System.Threading.Tasks.Task.Run(() => logikfunktionen.Logikfunktionen_Task());
            System.Threading.Tasks.Task.Run(() => Display_Task());

            if (System.Diagnostics.Debugger.IsAttached)
            {
                btnDebugWindow.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                btnDebugWindow.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            setManualWindow = new SetManualWindow();
            setManualWindow.Show();
        }

        private void WagenNachLinks_Click(object sender, RoutedEventArgs e)
        {
            WagenRichtung = Richtung.nachLinks;
        }

        private void WagenNachRechts_Click(object sender, RoutedEventArgs e)
        {
            WagenRichtung = Richtung.nachRechts;
        }

        private void AnimatedLoaded(object sender, RoutedEventArgs e)
        {
            AnimationGestartet = true;
        }
    }
}
