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

        private readonly Action<byte[], byte[]> CallbackInput;
        private readonly Action<byte[], byte[]> CallbackOutput;

        private readonly byte[] DigInput = new byte[1024];
        private readonly byte[] DigOutput = new byte[1024];
        private readonly byte[] AnalogInput = new byte[1024];
        private readonly byte[] AnalogOutput = new byte[1024];

        private readonly int AnzahlByteDigInput;
        private readonly int AnzahlByteDigOutput;
        private readonly int AnzahlByteAnalogInput;
        private readonly int AnzahlByteAnalogOutput;

        private readonly S7Client Client = new S7Client();
        public const int SPS_Timeout = 1000;
        public const int SPS_Rack = 0;
        public const int SPS_Slot = 0;
        private string SpsStatus = "Keine Verbindung zur S7-1200!";
        private bool SpsError;
        private readonly IpAdressen SPS_Client;



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


        public string GetSpsStatus() { return SpsStatus; }
        public bool GetSpsError() { return SpsError; }

        private void SPS_Pingen_Task()
        {
            bool FehlerAktiv;

            while (true)
            {
                int? ResultError;

                var pingSender = new Ping();
                var reply = pingSender.Send(SPS_Client.Adress, SPS_Timeout);

                CallbackInput(DigInput, AnalogInput); // zum Testen ohne SPS

                if (reply.Status == IPStatus.Success)
                {
                    SpsStatus = "S7-1200 sichtbar (Ping: " + reply.RoundtripTime.ToString() + "ms)";
                    int? res = Client?.ConnectTo(SPS_Client.Adress, SPS_Rack, SPS_Slot);
                    if (res == 0)
                    {
                        while (true)
                        {
                            FehlerAktiv = false;

                            CallbackInput(DigInput, AnalogInput);

                            if (AnzahlByteDigInput > 0)
                            {
                                ResultError = Client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte_0, AnzahlByteDigInput, DigInput);
                                if (ResultError != 0)
                                {
                                    FehlerAktiv = true;
                                    SpsStatus = ErrorAnzeigen(ResultError.GetValueOrDefault());
                                }
                            }
                            if (AnzahlByteDigOutput > 0)
                            {
                                ResultError = Client.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte_0, AnzahlByteDigOutput, DigOutput);
                                if (ResultError != 0)
                                {
                                    FehlerAktiv = true;
                                    SpsStatus = ErrorAnzeigen(ResultError.GetValueOrDefault());
                                }
                            }
                            if (AnzahlByteAnalogInput > 0)
                            {
                                ResultError = Client.DBWrite((int)Datenbausteine.AnIn, (int)BytePosition.Byte_0, AnzahlByteAnalogInput, AnalogInput);
                                if (ResultError != 0)
                                {
                                    FehlerAktiv = true;
                                    SpsStatus = ErrorAnzeigen(ResultError.GetValueOrDefault());
                                }
                            }
                            if (AnzahlByteAnalogOutput > 0)
                            {
                                ResultError = Client.DBRead((int)Datenbausteine.AnOut, (int)BytePosition.Byte_0, AnzahlByteAnalogOutput, AnalogOutput);
                                if (ResultError != 0)
                                {
                                    FehlerAktiv = true;
                                    SpsStatus = ErrorAnzeigen(ResultError.GetValueOrDefault());
                                }
                            }

                            CallbackOutput(DigOutput, AnalogOutput);

                            if (FehlerAktiv)
                            {
                                SpsError = true;
                                break;
                            }
                            else
                            {
                                SpsError = false;
                            }


                            Thread.Sleep(10);
                        }
                    }
                    else ErrorAnzeigen(res.GetValueOrDefault());
                }
                else
                {
                    SpsStatus = "Keine Verbindung zur S7-1200!";
                }

                CallbackOutput(DigOutput, AnalogOutput);// zum Testen ohne SPS

                Thread.Sleep(50);
            }
        }

        private string ErrorAnzeigen(int ResultError)
        {
            var ErrorText = Client?.ErrorText(ResultError);
            return ("Nr: " + ResultError + " Text: " + ErrorText);
        }
    }
}