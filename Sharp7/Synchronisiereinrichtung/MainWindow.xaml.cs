using Kommunikation;
using Synchronisiereinrichtung.Kraftwerk.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Synchronisiereinrichtung
{

    public partial class MainWindow : Window
    {
        public enum SynchronisierungAuswahl
        {
            U_f = 0,
            U_f_Phase,
            U_f_Phase_Leistung,
            U_f_Phase_Leistungsfaktor
        }

        // Kraftwerk.Kraftwerk kraftwerk;


        public bool Q1;
        public bool Q1alt;
        public bool S1;
        public bool S2;

        public int Y;
        public int Ie;




        public bool TaskAktiv;
        public bool DatenRangierenAktiv = true;
        public bool FensterAktiv = true;
        public bool DebugWindowAktiv;

        Messgeraet MessgeraetDifferenzSpannung;

        Logikfunktionen logikfunktionen;
        DatenRangieren datenRangieren;

        public SetManualWindow setManualWindow;
        public RealTimeGraphWindow realTimeGraphWindow;
        public KraftwerkViewModel _kraftwerkViewModel;

        public MainWindow()
        {
            realTimeGraphWindow = new RealTimeGraphWindow(this);

            logikfunktionen = new Logikfunktionen(this);
            datenRangieren = new DatenRangieren(this);

            _kraftwerkViewModel = new KraftwerkViewModel();

            InitializeComponent();
            DataContext = _kraftwerkViewModel;






            S7_1200 s7_1200 = new S7_1200(2, 2, 100, 100, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

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

            MessgeraetDifferenzSpannung = new Messgeraet(0);
            GaugeDifferenzSpannung.DataContext = MessgeraetDifferenzSpannung;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
            Application.Current.Shutdown();
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            setManualWindow = new SetManualWindow();
            setManualWindow._kraftwerkViewModel = _kraftwerkViewModel;
            setManualWindow.Show();
        }

        private void GraphWindow_Click(object sender, RoutedEventArgs e)
        {
            realTimeGraphWindow = new RealTimeGraphWindow(this);
            realTimeGraphWindow.Show();
        }

        public void AuswahlGeaendert(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            /*
            switch (radioButton.Name)
            {
                case "U_f": AuswahlSynchronisierung = SynchronisierungAuswahl.U_f; break;
                case "U_f_Phasenlage": AuswahlSynchronisierung = SynchronisierungAuswahl.U_f_Phase; break;
                case "U_f_Leistung": AuswahlSynchronisierung = SynchronisierungAuswahl.U_f_Phase_Leistung; break;
                case "U_f_Leistungsfaktor": AuswahlSynchronisierung = SynchronisierungAuswahl.U_f_Phase_Leistungsfaktor; break;
                default:
                    break;
            }
            */
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
