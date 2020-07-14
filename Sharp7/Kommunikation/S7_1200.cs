using Newtonsoft.Json;
using Sharp7;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;

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

        public const int SPS_Timeout = 1000;
        public const int SPS_Rack = 0;
        public const int SPS_Slot = 0;

        private readonly Action<byte[], byte[]> callbackInput;
        private readonly Action<byte[], byte[]> callbackOutput;

        private readonly byte[] digInput = new byte[1024];
        private readonly byte[] digOutput = new byte[1024];
        private readonly byte[] analogInput = new byte[1024];
        private readonly byte[] analogOutput = new byte[1024];

        private readonly int anzahlByteDigInput;
        private readonly int anzahlByteDigOutput;
        private readonly int anzahlByteAnalogInput;
        private readonly int anzahlByteAnalogOutput;

        private readonly S7Client client = new S7Client();

        private string spsStatus = "Keine Verbindung zur S7-1200!";
        private bool spsError;
        private readonly IpAdressen spsClient;

        public S7_1200(int anzahlByteDigInput, int anzahlByteDigOutput, int anzahlByteAnalogInput, int anzahlByteAnalogOutput, Action<byte[], byte[]> callbackInput, Action<byte[], byte[]> callbackOutput)
        {
            spsClient = JsonConvert.DeserializeObject<IpAdressen>(File.ReadAllText("IpAdressen.json"));

            this.anzahlByteDigInput = anzahlByteDigInput;
            this.anzahlByteDigOutput = anzahlByteDigOutput;
            this.anzahlByteAnalogInput = anzahlByteAnalogInput;
            this.anzahlByteAnalogOutput = anzahlByteAnalogOutput;

            this.callbackInput = callbackInput;
            this.callbackOutput = callbackOutput;

            Array.Clear(digInput, 0, digInput.Length);
            Array.Clear(digOutput, 0, digOutput.Length);
            Array.Clear(analogInput, 0, analogInput.Length);
            Array.Clear(analogOutput, 0, analogOutput.Length);

            System.Threading.Tasks.Task.Run(SPS_Pingen_Task);
        }

        public string GetSpsStatus() => spsStatus;

        public bool GetSpsError() => spsError;

        private void SPS_Pingen_Task()
        {
            bool FehlerAktiv;

            while (true)
            {
                int? ResultError;

                var pingSender = new Ping();
                var reply = pingSender.Send(spsClient.Adress, SPS_Timeout);

                callbackInput(digInput, analogInput); // zum Testen ohne SPS

                if (reply.Status == IPStatus.Success)
                {
                    spsStatus = "S7-1200 sichtbar (Ping: " + reply.RoundtripTime.ToString() + "ms)";
                    int? res = client?.ConnectTo(spsClient.Adress, SPS_Rack, SPS_Slot);
                    if (res == 0)
                    {
                        while (true)
                        {
                            FehlerAktiv = false;

                            callbackInput(digInput, analogInput);

                            if (anzahlByteDigInput > 0)
                            {
                                ResultError = client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte_0, anzahlByteDigInput, digInput);
                                if (ResultError != 0)
                                {
                                    FehlerAktiv = true;
                                    spsStatus = ErrorAnzeigen(ResultError.GetValueOrDefault());
                                }
                            }
                            if (anzahlByteDigOutput > 0)
                            {
                                ResultError = client.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte_0, anzahlByteDigOutput, digOutput);
                                if (ResultError != 0)
                                {
                                    FehlerAktiv = true;
                                    spsStatus = ErrorAnzeigen(ResultError.GetValueOrDefault());
                                }
                            }
                            if (anzahlByteAnalogInput > 0)
                            {
                                ResultError = client.DBWrite((int)Datenbausteine.AnIn, (int)BytePosition.Byte_0, anzahlByteAnalogInput, analogInput);
                                if (ResultError != 0)
                                {
                                    FehlerAktiv = true;
                                    spsStatus = ErrorAnzeigen(ResultError.GetValueOrDefault());
                                }
                            }
                            if (anzahlByteAnalogOutput > 0)
                            {
                                ResultError = client.DBRead((int)Datenbausteine.AnOut, (int)BytePosition.Byte_0, anzahlByteAnalogOutput, analogOutput);
                                if (ResultError != 0)
                                {
                                    FehlerAktiv = true;
                                    spsStatus = ErrorAnzeigen(ResultError.GetValueOrDefault());
                                }
                            }

                            callbackOutput(digOutput, analogOutput);

                            if (FehlerAktiv)
                            {
                                spsError = true;
                                break;
                            }
                            else
                            {
                                spsError = false;
                            }

                            Thread.Sleep(10);
                        }
                    }
                    else
                    {
                        ErrorAnzeigen(res.GetValueOrDefault());
                    }
                }
                else
                {
                    spsStatus = "Keine Verbindung zur S7-1200!";
                }

                callbackOutput(digOutput, analogOutput);// zum Testen ohne SPS

                Thread.Sleep(50);
            }
        }

        private string ErrorAnzeigen(int ResultError)
        {
            var ErrorText = client?.ErrorText(ResultError);
            return "Nr: " + ResultError + " Text: " + ErrorText;
        }
    }
}