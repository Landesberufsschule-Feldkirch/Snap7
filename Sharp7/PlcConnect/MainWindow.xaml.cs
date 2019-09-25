using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Net.NetworkInformation;
using Sharp7;

namespace PlcConnect
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string SPS_IP_Adresse = "192.168.0.10";    //S7-1200 DC/DC/DC hängt bei jedem Platz auf der IP Adresse 192.168.0.10
        public const int SPS_Timeout = 1000;
        public const int SPS_Rack = 0;
        public const int SPS_Slot = 0;

        public const int DB_DigInput = 1;
        public const int DB_DigOutput = 2;
        public const int Startbyte_0 = 0;
        public const int AnzahlByte_1 = 1;
        public const int BitMuster_01 = 0x01;
        public const int BitMuster_02 = 0x02;

        public const float Rahmenbreite_1px = 1;
        public const float Rahmenbreite_5px = 5;

        private S7Client Client;
        public bool TaskAktiv;
        public bool FensterAktiv = true;

        private byte[] DigOutput = new byte[1024];
        private byte[] DigInput = new byte[1024];
        public MainWindow()
        {
            InitializeComponent();
            BilderEinlesen();
            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            int Result = -2;

            if (Client == null)
            {
                Client = new S7Client();
            }

            TaskAktiv = true;
            System.Threading.Tasks.Task.Run(() => DatenRangieren_Task());


            LabelPlcStatus.Content = "";
            LabelPlcError.Content = "";

            Result = Client.ConnectTo(SPS_IP_Adresse, SPS_Rack, SPS_Slot);
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
            Client.Disconnect();

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

        public void DatenRangieren_Task()
        {
            while (TaskAktiv && FensterAktiv)
            {
                DatenRangieren();

                if ((Client != null) && TaskAktiv)
                {
                    Client.DBWrite(DB_DigInput, Startbyte_0, AnzahlByte_1, DigInput);
                    Client.DBRead(DB_DigOutput, Startbyte_0, AnzahlByte_1, DigOutput);
                }

                Task.Delay(50);
            }
        }

        public void SPS_Pingen_Task()
        {
            while (FensterAktiv)
            {
                Ping pingSender = new Ping();

                PingReply reply = pingSender.Send(SPS_IP_Adresse, SPS_Timeout);
                if (reply.Status == IPStatus.Success)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        LabelPlcPing.Content = "S7-1200 sichtbar (Ping: " + reply.RoundtripTime.ToString() + "ms)";
                        if (!TaskAktiv)
                        {
                            ButtonConnect.IsEnabled = true;
                        }
                    });
                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        LabelPlcPing.Content = "Keine Verbindung zur S7-1200!";
                        ButtonConnect.IsEnabled = false;
                    });
                }

                Task.Delay(500);

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }
    }
}
