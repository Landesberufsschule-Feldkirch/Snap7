using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using CircularGauge;

namespace Synchronisiereinrichtung
{

    public partial class MainWindow : Window
    {
        public SecondWindow secondWindow;
        public RealTimeGraph realTimeGraph;
        public bool TaskAktiv;
        public bool DatenRangierenAktiv = true;
        public bool FensterAktiv = true;
        public bool DebugWindowAktiv;
        Messgeraet MessgeraetDifferenzSpannung;


        public MainWindow()
        {
            InitializeComponent();
            EinAusgabeFelderInitialisieren();

            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
            System.Threading.Tasks.Task.Run(() => Logikfunktionen_Task());
            System.Threading.Tasks.Task.Run(() => Display_Task());

            MessgeraetDifferenzSpannung = new Messgeraet(0);
            GaugeDifferenzSpannung.DataContext = MessgeraetDifferenzSpannung;
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

        private void GraphWindow_Click(object sender, RoutedEventArgs e)
        {
            realTimeGraph = new RealTimeGraph();
            realTimeGraph.Show();
        }

        public void AuswahlGeaendert(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;


            switch (radioButton.Name)
            {
                case "U_f": AuswahlSynchronisierung = SynchronisierungAuswahl.U_f; break;
                case "U_f_Phasenlage": AuswahlSynchronisierung = SynchronisierungAuswahl.U_f_Phase; break;
                case "U_f_Leistung": AuswahlSynchronisierung = SynchronisierungAuswahl.U_f_Phase_Leistung; break;
                case "U_f_Leistungsfaktor": AuswahlSynchronisierung = SynchronisierungAuswahl.U_f_Phase_Leistungsfaktor; break;
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


    public class Messgeraet : INotifyPropertyChanged
    {
        private double score;

        public double Score
        {
            get { return score; }
            set
            {
                score = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Score"));
                }
            }
        }


        public Messgeraet(double scr)
        {
            this.Score = scr;
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
