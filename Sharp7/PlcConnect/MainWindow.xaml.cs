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
using Sharp7;

namespace PlcConnect
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private S7Client Client;
        private byte[] Buffer = new byte[65536];
        private byte[] DB_A = new byte[1024];
        private byte[] DB_B = new byte[1024];
        private byte[] DB_C = new byte[1024];

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            int Result;
            //int Rack = System.Convert.ToInt32(TxtRack.Text);
            //int Slot = System.Convert.ToInt32(TxtSlot.Text);
            Result = Client.ConnectTo("192.168.0.10", 0, 0); //s7-1200 DC/DC/DC hängt bei jedem Platz auf der IP Adresse 192.168.0.10
            ShowResult(Result);
            if (Result == 0)
            {
                LabelPlcError.Content = LabelPlcError.Content + " PDU Negotiated : " + Client.PduSizeNegotiated.ToString();
                //TxtIP.Enabled = false;
                //TxtRack.Enabled = false;
                //TxtSlot.Enabled = false;
                //ConnectBtn.Enabled = false;
                //DisconnectBtn.Enabled = true;
                //tabControl.Enabled = true;
            }

        }

        private void ButtonDisconnect_Click(object sender, RoutedEventArgs e)
        {
            Client.Disconnect();
            LabelPlcError.Content = "Disconnected";
            //TxtIP.Enabled = true;
            //TxtRack.Enabled = true;
            //TxtSlot.Enabled = true;
            //ConnectBtn.Enabled = true;
            //DisconnectBtn.Enabled = false;
            //tabControl.Enabled = false;

        }

        private void ShowResult(int Result)
        {
            // This function returns a textual explaination of the error code
            LabelPlcError.Content = Client.ErrorText(Result);
            if (Result == 0)
                LabelPlcError.Content = LabelPlcError.Content + " (" + Client.ExecutionTime.ToString() + " ms)";
        }

        void ReadCPUInfo()
        {
            S7Client.S7CpuInfo Info = new S7Client.S7CpuInfo();
            //txtModuleTypeName.Text = "";
            //txtSerialNumber.Text = "";
            //txtCopyright.Text = "";
            //txtAsName.Text = "";
            //txtModuleName.Text = "";
            int Result = Client.GetCpuInfo(ref Info);
            ShowResult(Result);
            if (Result == 0)
            {
                //txtModuleTypeName.Text = Info.ModuleTypeName;
                //txtSerialNumber.Text = Info.SerialNumber;
                //txtCopyright.Text = Info.Copyright;
               // txtAsName.Text = Info.ASName;
                //txtModuleName.Text = Info.ModuleName;
            }
        }

        void ReadOrderCode()
        {
            S7Client.S7OrderCode Info = new S7Client.S7OrderCode();
           // txtOrderCode.Text = "";
           // txtVersion.Text = "";
            int Result = Client.GetOrderCode(ref Info);
            ShowResult(Result);
            if (Result == 0)
            {
               // txtOrderCode.Text = Info.Code;
               // txtVersion.Text = Info.V1.ToString() + "." + Info.V2.ToString() + "." + Info.V3.ToString();
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
                        {
                           // lblStatus.Text = "RUN";
                           // lblStatus.ForeColor = System.Drawing.Color.LimeGreen;
                            break;
                        }
                    case S7Consts.S7CpuStatusStop:
                        {
                           // lblStatus.Text = "STOP";
                           // lblStatus.ForeColor = System.Drawing.Color.Red;
                            break;
                        }
                    default:
                        {
                           // lblStatus.Text = "Unknown";
                           // lblStatus.ForeColor = System.Drawing.Color.Black;
                            break;
                        }
                }
            }
            else
            {
               // lblStatus.Text = "Unknown";
               // lblStatus.ForeColor = System.Drawing.Color.Black;
            }
        }

        string HexByte(byte B)
        {
            string Result = Convert.ToString(B, 16);
            if (Result.Length < 2)
                Result = "0" + Result;
            return "0x" + Result;
        }

        string HexWord(ushort W)
        {
            string Result = Convert.ToString(W, 16);
            while (Result.Length < 4)
                Result = "0" + Result;
            return "0x" + Result;
        }





    }
}
