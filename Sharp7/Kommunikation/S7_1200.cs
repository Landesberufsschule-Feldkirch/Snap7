using Newtonsoft.Json;
using Sharp7;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using Utilities;

namespace Kommunikation
{
#pragma warning disable S101 // Types should be named in PascalCase
    public class S7_1200
#pragma warning restore S101 // Types should be named in PascalCase
    {
        private enum Datenbausteine
        {
            DigIn = 1,
            DigOut,
            AnIn,
            AnOut
        }

        private enum BytePosition
        {
            Byte_0 = 0
        }

        readonly Action<byte[], byte[]> CallbackInput;
        readonly Action<byte[], byte[]> CallbackOutput;

        private byte[] DigInput = new byte[1024];
        private byte[] DigOutput = new byte[1024];
        private byte[] AnalogInput = new byte[1024];
        private byte[] AnalogOutput = new byte[1024];

        private readonly int AnzahlByteDigInput;
        private readonly int AnzahlByteDigOutput;
        private readonly int AnzahlByteAnalogInput;
        private readonly int AnzahlByteAnalogOutput;

        private readonly S7Client Client = new S7Client();
        public const int SPS_Timeout = 1000;
        public const int SPS_Rack = 0;
        public const int SPS_Slot = 0;
        private bool TaskAktive = true;
        string SpsStatus = "Keine Verbindung zur S7-1200!";
        readonly IpAdressen SPS_Client;

        public S7_1200(int anzahlByteDigInput, int anzahlByteDigOutput, int anzahlByteAnalogInput, int anzahlByteAnalogOutput, Action<byte[], byte[]> callbackInput, Action<byte[], byte[]> callbackOutput)
        {
            SPS_Client = JsonConvert.DeserializeObject<IpAdressen>(File.ReadAllText(@"IpAdressen.json"));

            AnzahlByteDigInput = anzahlByteDigInput;
            AnzahlByteDigOutput = anzahlByteDigOutput;
            AnzahlByteAnalogInput = anzahlByteAnalogInput;
            AnzahlByteAnalogOutput = anzahlByteAnalogOutput;

            CallbackInput = callbackInput;
            CallbackOutput = callbackOutput;

            Array.Clear(DigInput, 0, DigInput.Length);
            Array.Clear(DigOutput, 0, DigOutput.Length);
            Array.Clear(AnalogInput, 0, AnalogInput.Length);
            Array.Clear(AnalogOutput, 0, AnalogOutput.Length);

            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
        }

        ~S7_1200()
        {
            TaskAktive = false;
        }

        public string GetSpsStatus()
        {
            return SpsStatus;
        }
        private void SPS_Pingen_Task()
        {
            while (TaskAktive)
            {
                var pingSender = new Ping();
                var reply = pingSender.Send(SPS_Client.Adress, SPS_Timeout);

                CallbackInput(DigInput, AnalogInput); // zum Testen ohne SPS

                if (reply.Status == IPStatus.Success)
                {
                    SpsStatus = "S7-1200 sichtbar (Ping: {reply.RoundtripTime.ToString() }ms)";
                    var res = Client?.ConnectTo(SPS_Client.Adress, SPS_Rack, SPS_Slot);
                    if (res == 0)
                    {
                        while (TaskAktive)
                        {
                            CallbackInput(DigInput, AnalogInput);

                            if (AnzahlByteDigInput > 0) Client?.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte_0, AnzahlByteDigInput, DigInput);
                            if (AnzahlByteDigOutput > 0) Client?.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte_0, AnzahlByteDigOutput, DigOutput);
                            if (AnzahlByteAnalogInput > 0) Client?.DBWrite((int)Datenbausteine.AnIn, (int)BytePosition.Byte_0, AnzahlByteAnalogInput, AnalogInput);
                            if (AnzahlByteAnalogOutput > 0) Client?.DBWrite((int)Datenbausteine.AnOut, (int)BytePosition.Byte_0, AnzahlByteAnalogOutput, AnalogOutput);

                            CallbackOutput(DigOutput, AnalogOutput);

                            Thread.Sleep(10);
                        }
                    }

                    //  else TODO fehlerbehandlung
                }
                else
                {
                    SpsStatus = "Keine Verbindung zur S7-1200!";
                }

                CallbackOutput(DigOutput, AnalogOutput);// zum Testen ohne SPS

                Thread.Sleep(50);
            }

            Client.Disconnect();
        }
    }
}