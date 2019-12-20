using Sharp7;
using System.Net.NetworkInformation;
using System.Threading;

namespace AmpelsteuerungKieswerk
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
                TaskAktiv = true;
                System.Threading.Tasks.Task.Run(() => DatenRangieren_Task());
            }
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
                            if (TaskAktiv) lbl_PlcPing.Content = "S7-1200 sichtbar (Ping: " + reply.RoundtripTime.ToString() + "ms)";
                            else VerbindungErstellen();
                        }
                        else
                        {
                            lbl_PlcPing.Content = "Keine Verbindung zur S7-1200!";
                        }
                    }
                });

                Thread.Sleep(100);
            }
        }     
  

    }
}
