using System.Windows;
using System.Windows.Controls;
using CircularGauge;

namespace Synchronisiereinrichtung
{

    public partial class MainWindow : Window
    {
        public SecondWindow secondWindow;
        public bool TaskAktiv;
        public bool DatenRangierenAktiv = true;
        public bool FensterAktiv = true;
        public bool DebugWindowAktiv;
        public MainWindow()
        {
            InitializeComponent();
            EinAusgabeFelderInitialisieren();

            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
            System.Threading.Tasks.Task.Run(() => Logikfunktionen_Task());
            System.Threading.Tasks.Task.Run(() => Display_Task());

            GaugeDifferenzSpannung.CurrentValue = 100;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            secondWindow = new SecondWindow();
            secondWindow.Show();
        }

        public void AuswahlGeaendert(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            switch (radioButton.Content)
            {
                case "Spannung / Frequenz": AuswahlSynchronisierung = SynchronisierungAuswahl.U_f; break;
                case "U / f / Phasenlage": AuswahlSynchronisierung = SynchronisierungAuswahl.U_f_Phase; break;
                case "U / f / Phasenlage / Leistung": AuswahlSynchronisierung = SynchronisierungAuswahl.U_f_Phase_Leistung; break;
                default:
                    break;
            }
        }

        private void BtnStarten_Click(object sender, RoutedEventArgs e)
        {
            S1 = ButtonFunktionPressReleaseAendern(BtnStarten);
        }

        private void BtnStoppen_Click(object sender, RoutedEventArgs e)
        {
            S2 = !ButtonFunktionPressReleaseAendern(BtnStoppen);
        }


        private bool ButtonFunktionPressReleaseAendern(Button knopf)
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
