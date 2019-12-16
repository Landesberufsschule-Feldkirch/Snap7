using Sharp7;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace PlcConnect
{
    public partial class MainWindow
    {
        private S7Client Client;
        public const string SPS_IP_Adresse = "192.168.0.10";    //S7-1200 DC/DC/DC hängt bei jedem Platz auf der IP Adresse 192.168.0.10
        public const int SPS_Timeout = 1000;
        public const int SPS_Rack = 0;
        public const int SPS_Slot = 0;

        private byte[] DigOutput = new byte[1024];
        private byte[] DigInput = new byte[1024];
        private byte[] AnalogOutput = new byte[1024];
        private byte[] AnalogInput = new byte[1024];

        public void VerbindungErstellen()
        {
            int Result = -2;
            if (Client == null) Client = new S7Client();

            Result = Client.ConnectTo(SPS_IP_Adresse, SPS_Rack, SPS_Slot);
            if (Result == 0)
            {
                btnConnect.IsEnabled = false;
                btnDisconnect.IsEnabled = true;

                TaskAktiv = true;
                System.Threading.Tasks.Task.Run(() => DatenRangieren_Task());
            }
        }

        public void VerbindungTrennen()
        {
            TaskAktiv = false;
            Client.Disconnect();

            btnConnect.IsEnabled = true;
            btnDisconnect.IsEnabled = false;
        }

        public void SPS_Pingen_Task()
        {
            while (FensterAktiv)
            {
                Ping pingSender = new Ping();

                PingReply reply = pingSender.Send(SPS_IP_Adresse, SPS_Timeout);

                this.Dispatcher.Invoke(() =>
                {
                    if (FensterAktiv)
                    {
                        if (reply.Status == IPStatus.Success)
                        {
                            lblPlcPing.Content = "S7-1200 sichtbar (Ping: " + reply.RoundtripTime.ToString() + "ms)";
                            btnConnect.IsEnabled = true;
                        }
                        else
                        {
                            lblPlcPing.Content = "Keine Verbindung zur S7-1200!";
                            btnConnect.IsEnabled = false;
                        }
                    }
                });

                Thread.Sleep(100);
            }
        }

    }
}
