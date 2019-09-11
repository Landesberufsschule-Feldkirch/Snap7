using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Sharp7;

namespace PlcConnect
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private S7Client Client;
        private bool TaskAktiv;

        private byte[] DigOutput = new byte[1024];
        private byte[] DigInput = new byte[1024];
        public MainWindow()
        {
            InitializeComponent();

        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            int Result = -2;

            if (Client == null)
            {
                Client = new S7Client();
            }

            TaskAktiv = true;
            System.Threading.Tasks.Task.Run(() => BackgroundWorker());

            LabelPlcStatus.Content = "";
            LabelPlcError.Content = "";

            //S7-1200 DC/DC/DC hängt bei jedem Platz auf der IP Adresse 192.168.0.10
            Result = Client.ConnectTo("192.168.0.10", 0, 0);
            ShowResult(Result);
            if (Result == 0)
            {
                LabelPlcError.Content = LabelPlcError.Content + " PDU Negotiated : " + Client.PduSizeNegotiated.ToString();
                ButtonConnect.IsEnabled = false;
                ButtonDisconnect.IsEnabled = true;

                ShowPlcStatus();
                ReadOrderCode();
            }
        }

        private void ButtonDisconnect_Click(object sender, RoutedEventArgs e)
        {
            TaskAktiv = false;

            LabelPlcStatus.Content = "Disconnected";
            LabelPlcError.Content = "";
            LabelPlcOrderCode.Content = "";

            ButtonConnect.IsEnabled = true;
            ButtonDisconnect.IsEnabled = false;
        }

        private void ShowResult(int Result)
        {
            if (Result == 0)
            {
                LabelPlcError.Content = LabelPlcError.Content + " (" + Client.ExecutionTime.ToString() + " ms)";
                LabelPlcError.Background = Brushes.LawnGreen;
            }
            else
            {
                LabelPlcError.Content = "ERROR! " + Client.ErrorText(Result);
                LabelPlcError.Background = Brushes.Red;
            }
        }

        void ReadOrderCode()
        {
            S7Client.S7OrderCode Info = new S7Client.S7OrderCode();
            int Result = Client.GetOrderCode(ref Info);
            ShowResult(Result);
            if (Result == 0)
            {
                LabelPlcOrderCode.Content = Info.Code + " " + Info.V1.ToString() + "." + Info.V2.ToString() + "." + Info.V3.ToString();
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
                        LabelPlcStatus.Content = "RUN";
                        LabelPlcStatus.Background = Brushes.LawnGreen;
                        break;

                    case S7Consts.S7CpuStatusStop:
                        LabelPlcStatus.Content = "STOP";
                        LabelPlcStatus.Background = Brushes.Red;
                        break;

                    default:
                        LabelPlcStatus.Content = "Unknown";
                        LabelPlcStatus.Background = Brushes.Cyan;
                        break;
                }
            }
            else
            {
                LabelPlcStatus.Content = "Unknown";
                LabelPlcStatus.Background = Brushes.Cyan;
            }
        }

        public void BackgroundWorker()
        {
            while (TaskAktiv)
            {
                DatenRangieren();

                if (Client != null)
                {
                    Client.WriteArea(S7Consts.S7AreaDB, 1, 0, 1, S7Consts.S7WLByte, DigInput);
                    Client.ReadArea(S7Consts.S7AreaDB, 2, 0, 1, S7Consts.S7WLByte, DigOutput);
                }

                Task.Delay(50);
            }

            Client.Disconnect();

        }

    }
}
