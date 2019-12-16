using Sharp7;
using System.Windows;
using System.Windows.Media;

namespace PlcConnect
{
    public partial class MainWindow : Window
    {

        public bool TaskAktiv;
        public bool FensterAktiv = true;

        public MainWindow()
        {
            InitializeComponent();
            BilderEinlesen();
            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
            System.Threading.Tasks.Task.Run(() => Display_Task());
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            VerbindungErstellen();
        }

        private void ButtonDisconnect_Click(object sender, RoutedEventArgs e)
        {
            VerbindungTrennen();
        }

        private void ShowResult(int Result)
        {
            if (Result == 0)
            {
                lblPlcError.Content = lblPlcError.Content + " (" + Client.ExecutionTime.ToString() + " ms)";
                lblPlcError.Background = Brushes.LawnGreen;
            }
            else
            {
                lblPlcError.Content = "ERROR! " + Client.ErrorText(Result);
                lblPlcError.Background = Brushes.Red;
            }
        }

        void ReadOrderCode()
        {
            S7Client.S7OrderCode Info = new S7Client.S7OrderCode();
            int Result = Client.GetOrderCode(ref Info);
            ShowResult(Result);
            if (Result == 0)
            {
                lblPlcOrderCode.Content = Info.Code + " " + Info.V1.ToString() + "." + Info.V2.ToString() + "." + Info.V3.ToString();
            }
        }

        void ShowPlcStatus()
        {
            int Status = 0;
            int Result = Client.PlcGetStatus(ref Status);
            ShowResult(Result);
            if (Result == 0)
            {
                switch (Status)
                {
                    case S7Consts.S7CpuStatusRun:
                        lblPlcStatus.Content = "RUN";
                        lblPlcStatus.Background = Brushes.LawnGreen;
                        break;

                    case S7Consts.S7CpuStatusStop:
                        lblPlcStatus.Content = "STOP";
                        lblPlcStatus.Background = Brushes.Red;
                        break;

                    default:
                        lblPlcStatus.Content = "Unknown";
                        lblPlcStatus.Background = Brushes.Cyan;
                        break;
                }
            }
            else
            {
                lblPlcStatus.Content = "Unknown";
                lblPlcStatus.Background = Brushes.Cyan;
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }
    }
}
