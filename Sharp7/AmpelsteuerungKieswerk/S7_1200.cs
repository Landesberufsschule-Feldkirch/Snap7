using Sharp7;
using System.Net.NetworkInformation;
using System.Threading;

namespace AmpelsteuerungKieswerk
{
    public class S7_1200
    {
        private readonly S7Client Client = new S7Client();
        public const string SPS_IP_Adresse = "192.168.0.10";    //S7-1200 DC/DC/DC hängt bei jedem Platz auf der IP Adresse 192.168.0.10
        public const int SPS_Timeout = 1000;
        public const int SPS_Rack = 0;
        public const int SPS_Slot = 0;
        readonly DatenRangieren DatenRangieren;
        readonly MainWindow MainWindow;
        private bool TaskAktive = true;
        public S7_1200(MainWindow window, DatenRangieren datenRangieren)
        {
            MainWindow = window;
            DatenRangieren = datenRangieren;

            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
        }

        ~S7_1200()
        {
            TaskAktive = false;
        }

        private void SPS_Pingen_Task()
        {
            while (TaskAktive)
            {
                var pingSender = new Ping();
                var reply = pingSender.Send(SPS_IP_Adresse, SPS_Timeout);

                if (reply.Status == IPStatus.Success)
                {
                    MainWindow.SpsDatenschreiben($"S7-1200 sichtbar (Ping: {reply.RoundtripTime.ToString() }ms)");
                    var res = Client?.ConnectTo(SPS_IP_Adresse, SPS_Rack, SPS_Slot);
                    if (res == 0)
                    {
                        while (TaskAktive)
                        {
                            DatenRangieren.Task(Client);

                            Thread.Sleep(10);
                        }
                    }

                    //  else TODO fehlerbehandlung
                }
                else
                {
                    MainWindow.SpsDatenschreiben("Keine Verbindung zur S7-1200!");
                }
                Thread.Sleep(100);
            }

            Client.Disconnect();
        }
    }
}