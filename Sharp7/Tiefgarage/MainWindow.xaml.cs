using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.NetworkInformation;
using Sharp7;

namespace Tiefgarage
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
        public const int AnzahlByte_2 = 2;

        private S7Client Client;
        public bool TaskAktiv;
        public bool DatenRangierenAktiv = true;
        public bool FensterAktiv = true;

        private byte[] DigOutput = new byte[1024];
        private byte[] DigInput = new byte[1024];

        public MainWindow()
        {
            InitializeComponent();
            EinAusgabeFelderInitialisieren();
            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
            System.Threading.Tasks.Task.Run(() => Logikfunktionen_Task());
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            int Result = -2;

            if (Client == null)
            {
                Client = new S7Client();
            }


            Result = Client.ConnectTo(SPS_IP_Adresse, SPS_Rack, SPS_Slot);
            if (Result == 0)
            {
                btn_Connect.IsEnabled = false;
                btn_Disconnect.IsEnabled = true;

                TaskAktiv = true;
                System.Threading.Tasks.Task.Run(() => DatenRangieren_Task());
            }


        }

        private void ButtonDisconnect_Click(object sender, RoutedEventArgs e)
        {
            TaskAktiv = false;
            Client.Disconnect();

            btn_Connect.IsEnabled = true;
            btn_Disconnect.IsEnabled = false;
        }



        public void DatenRangieren_Task()
        {
            while (TaskAktiv && FensterAktiv)
            {
                if ((Client != null) && TaskAktiv)
                {
                    Client.DBWrite(DB_DigInput, Startbyte_0, AnzahlByte_1, DigInput);
                    Client.DBRead(DB_DigOutput, Startbyte_0, AnzahlByte_2, DigOutput);
                }

                Task.Delay(100);
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
                        lbl_PlcPing.Content = "S7-1200 sichtbar (Ping: " + reply.RoundtripTime.ToString() + "ms)";
                        if (!TaskAktiv)
                        {
                            btn_Connect.IsEnabled = true;
                        }
                    });
                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        lbl_PlcPing.Content = "Keine Verbindung zur S7-1200!";
                        btn_Connect.IsEnabled = false;
                    });
                }

                Task.Delay(500);

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }

        private void btn_auto_1_draussen_Click(object sender, RoutedEventArgs e)
        {           
        }
        private void btn_auto_2_draussen_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btn_auto_3_draussen_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btn_auto_4_draussen_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btn_mensch_1_draussen_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btn_mensch_2_draussen_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btn_mensch_3_draussen_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btn_mensch_4_draussen_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
