using Sharp7;
using System.Net.NetworkInformation;
using System.Threading.Tasks;


namespace WordClock
{
    public partial class MainWindow
    {
        private S7Client Client;
        public const string SPS_IP_Adresse = "192.168.0.10";    //S7-1200 DC/DC/DC hängt bei jedem Platz auf der IP Adresse 192.168.0.10
        public const int SPS_Timeout = 1000;
        public const int SPS_Rack = 0;
        public const int SPS_Slot = 0;

        public const int DB_DigInput = 1;
        public const int DB_DigOutput = 2;
        public const int Startbyte_0 = 0;
        public const int AnzahlByte_1 = 1;
        public const int AnzahlByte_2 = 2;

        private byte[] DigOutput = new byte[1024];
        private byte[] DigInput = new byte[1024];

        public void VerbindungErstellen()
        {
            int Result = -2;
            if (Client == null) Client = new S7Client();

            Result = Client.ConnectTo(SPS_IP_Adresse, SPS_Rack, SPS_Slot);
            if (Result == 0)
            {
                btn_Connect.IsEnabled = false;
                btn_Disconnect.IsEnabled = true;

                TaskAktiv = true;
                System.Threading.Tasks.Task.Run(() => DatenRangieren_Task());
            }
        }

        public void VerbindungTrennen()
        {
            TaskAktiv = false;
            Client.Disconnect();

            btn_Connect.IsEnabled = true;
            btn_Disconnect.IsEnabled = false;
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
                            lbl_PlcPing.Content = "S7-1200 sichtbar (Ping: " + reply.RoundtripTime.ToString() + "ms)";
                            btn_Connect.IsEnabled = true;
                        }
                        else
                        {
                            lbl_PlcPing.Content = "Keine Verbindung zur S7-1200!";
                            btn_Connect.IsEnabled = false;
                        }
                    }
                });

                Task.Delay(500);
            }
        }

        void EinAusgabeFelderInitialisieren()
        {
            foreach (byte b in DigInput) DigInput[b] = 0;
            foreach (byte b in DigOutput) DigOutput[b] = 0;
        }

    }
}
