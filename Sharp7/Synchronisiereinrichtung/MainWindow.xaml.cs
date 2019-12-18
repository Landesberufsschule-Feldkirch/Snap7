using System.Windows;
using System.Windows.Controls;

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
    }
}
