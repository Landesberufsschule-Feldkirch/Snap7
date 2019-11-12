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
using System.Collections.ObjectModel;

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

        public int FahrzeugPersonGeklickt = -1;
        public enum Rolle
        {
            Auto = 0,
            Person
        }
        public enum FahrzeugPerson
        {
            Auto_1 = 0,
            Auto_2,
            Auto_3,
            Auto_4,
            Person_1,
            Person_2,
            Person_3,
            Person_4
        }


        public class AlleFahrzeugePersonen
        {
            public Button btnBezeichnung { get; set; }
            public Rolle Rolle { get; set; }
            public double x_aktuell { get; set; }
            public double y_aktuell { get; set; }
            public double x_draussen { get; set; }
            public double y_draussen { get; set; }
            public double x_drinnen { get; set; }
            public double y_drinnen { get; set; }
            public string Bewegung { get; set; }
            public AlleFahrzeugePersonen(Button btnBezeichnung, Rolle Rolle, double x_draussen, double y_draussen, double x_drinnen, double y_drinnen, string Bewegung)
            {
                this.btnBezeichnung = btnBezeichnung;
                this.Rolle = Rolle;
                this.x_draussen = x_draussen;
                this.y_draussen = y_draussen;
                this.x_drinnen = x_drinnen;
                this.y_drinnen = y_drinnen;
                this.Bewegung = Bewegung;
            }
            public void draussenParken()
            {
                x_aktuell = x_draussen;
                y_aktuell = y_draussen;
            }
            public void drinnenParken()
            {
                x_aktuell = x_drinnen;
                y_aktuell = y_drinnen;
            }
            public void updatePosition()
            {
                btnBezeichnung.SetValue(Canvas.LeftProperty, x_aktuell);
                btnBezeichnung.SetValue(Canvas.TopProperty, y_aktuell);
            }

            public void btnAktivieren()
            {
                btnBezeichnung.IsEnabled = true;
            }
            public void btnDeaktivieren()
            {
                btnBezeichnung.IsEnabled = false;
            }
        }

        public ObservableCollection<AlleFahrzeugePersonen> gAlleFahrzeugePersonen = new ObservableCollection<AlleFahrzeugePersonen>();



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

                BitmusterSchreiben(Pegel_B1, DigInput, Startbyte_0, BitPos_B1);
                BitmusterSchreiben(Pegel_B2, DigInput, Startbyte_0, BitPos_B2);


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

        private void btn_Click(object sender, RoutedEventArgs e)
        {

            Button btn = sender as Button;

            switch (btn.Name)
            {
                case "btn_auto_1": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Auto_1; break;
                case "btn_auto_2": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Auto_2; break;
                case "btn_auto_3": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Auto_3; break;
                case "btn_auto_4": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Auto_4; break;

                case "btn_person_1": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Person_1; break;
                case "btn_person_2": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Person_2; break;
                case "btn_person_3": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Person_3; break;
                case "btn_person_4": FahrzeugPersonGeklickt = (int)FahrzeugPerson.Person_4; break;

                default:
                    break;
            }

            foreach (AlleFahrzeugePersonen fp in gAlleFahrzeugePersonen)
            {
                fp.btnDeaktivieren();
            }
        }

        private void AlleDraussenParken_Click(object sender, RoutedEventArgs e)
        {
            foreach (AlleFahrzeugePersonen fp in gAlleFahrzeugePersonen)
            {
                fp.draussenParken();
            }
        }

        private void AlleDrinnenParken_Click(object sender, RoutedEventArgs e)
        {
            foreach (AlleFahrzeugePersonen fp in gAlleFahrzeugePersonen)
            {
                fp.drinnenParken();
            }
        }
    }
}
