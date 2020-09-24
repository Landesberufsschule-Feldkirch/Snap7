using Newtonsoft.Json;
using Sharp7;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace Kommunikation
{

    public class S7_1200 : IPlc
    {
        private enum BytePosition
        {
            Byte0 = 0,
            Byte1,
            Byte2,
            Byte3,
            Byte4
        }

        private enum AnzahlByte
        {
            KeinByte = 0,
            EinByte,
            ZweiByte
        }


        public byte[] BefehleSps { get; set; } = new byte[1024];
        public byte[] VersionInput { get; set; } = new byte[1024];
        public byte[] DigInput { get; set; } = new byte[1024];
        public byte[] DigOutput { get; set; } = new byte[1024];
        public byte[] AnalogInput { get; set; } = new byte[1024];
        public byte[] AnalogOutput { get; set; } = new byte[1024];

        public const int SpsTimeout = 1000;
        public const int SpsRack = 0;
        public const int SpsSlot = 0;

        private readonly Action<byte[], byte[]> _callbackInput;
        private readonly Action<byte[], byte[]> _callbackOutput;



        private readonly int _anzahlByteVersionInput;
        private readonly int _anzahlByteDigInput;
        private readonly int _anzahlByteDigOutput;
        private readonly int _anzahlByteAnalogInput;
        private readonly int _anzahlByteAnalogOutput;

        private readonly S7Client _client = new S7Client();

        private string _spsStatus = "Keine Verbindung zur S7-1200!";
        private bool _spsError;
        private readonly IpAdressen _spsClient;
        private bool _taskRunning = true;

        public S7_1200(int anzByteVersionInput, int anzByteDigInput, int anzByteDigOutput, int anzByteAnalogInput, int anzByteAnalogOutput, Action<byte[], byte[]> cbInput, Action<byte[], byte[]> cbOutput)
        {
            _spsClient = JsonConvert.DeserializeObject<IpAdressen>(File.ReadAllText("IpAdressen.json"));

            _anzahlByteVersionInput = anzByteVersionInput;
            _anzahlByteDigInput = anzByteDigInput;
            _anzahlByteDigOutput = anzByteDigOutput;
            _anzahlByteAnalogInput = anzByteAnalogInput;
            _anzahlByteAnalogOutput = anzByteAnalogOutput;

            _callbackInput = cbInput;
            _callbackOutput = cbOutput;





            Array.Clear(BefehleSps, 0, BefehleSps.Length);
            Array.Clear(VersionInput, 0, VersionInput.Length);
            Array.Clear(DigInput, 0, DigInput.Length);
            Array.Clear(DigOutput, 0, DigOutput.Length);
            Array.Clear(AnalogInput, 0, AnalogInput.Length);
            Array.Clear(AnalogOutput, 0, AnalogOutput.Length);

            System.Threading.Tasks.Task.Run(SPS_Pingen_Task);
        }

        public void SPS_Pingen_Task()
        {
            while (_taskRunning)
            {
                var pingSender = new Ping();
                var reply = pingSender.Send(_spsClient.Adress, SpsTimeout);

                _callbackInput(DigInput, AnalogInput); // zum Testen ohne SPS

                if (reply != null && reply.Status == IPStatus.Success)
                {
                    _spsStatus = "S7-1200 sichtbar (Ping: " + reply.RoundtripTime + "ms)";
                    var res = _client?.ConnectTo(_spsClient.Adress, SpsRack, SpsSlot);
                    if (res == 0)
                    {
                        while (true)
                        {
                            var fehlerAktiv = false;

                            _callbackInput(DigInput, AnalogInput);

                            int? resultError;
                            if (_anzahlByteVersionInput > 0)
                            {
                                BefehleSps[0]++;
                                resultError = _client.DBWrite((int)Datenbausteine.VersionIn, (int)BytePosition.Byte0, (int)AnzahlByte.EinByte, BefehleSps);
                                if (resultError != 0)
                                {
                                    fehlerAktiv = true;
                                    _spsStatus = ErrorAnzeigen(resultError.GetValueOrDefault());
                                }
                                //2 Byte Offset +  2 Byte Header (Zul. Stringlänge + Zeichenlänge) 
                                resultError = _client.DBRead((int)Datenbausteine.VersionIn, (int)BytePosition.Byte4, _anzahlByteVersionInput, VersionInput);
                                if (resultError != 0)
                                {
                                    fehlerAktiv = true;
                                    _spsStatus = ErrorAnzeigen(resultError.GetValueOrDefault());
                                }
                            }
                            if (_anzahlByteDigInput > 0)
                            {
                                resultError = _client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte0, _anzahlByteDigInput, DigInput);
                                if (resultError != 0)
                                {
                                    fehlerAktiv = true;
                                    _spsStatus = ErrorAnzeigen(resultError.GetValueOrDefault());
                                }
                            }
                            if (_anzahlByteDigOutput > 0)
                            {
                                resultError = _client.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte0, _anzahlByteDigOutput, DigOutput);
                                if (resultError != 0)
                                {
                                    fehlerAktiv = true;
                                    _spsStatus = ErrorAnzeigen(resultError.GetValueOrDefault());
                                }
                            }
                            if (_anzahlByteAnalogInput > 0)
                            {
                                resultError = _client.DBWrite((int)Datenbausteine.AnIn, (int)BytePosition.Byte0, _anzahlByteAnalogInput, AnalogInput);
                                if (resultError != 0)
                                {
                                    fehlerAktiv = true;
                                    _spsStatus = ErrorAnzeigen(resultError.GetValueOrDefault());
                                }
                            }
                            if (_anzahlByteAnalogOutput > 0)
                            {
                                resultError = _client.DBRead((int)Datenbausteine.AnOut, (int)BytePosition.Byte0, _anzahlByteAnalogOutput, AnalogOutput);
                                if (resultError != 0)
                                {
                                    fehlerAktiv = true;
                                    _spsStatus = ErrorAnzeigen(resultError.GetValueOrDefault());
                                }
                            }

                            _callbackOutput(DigOutput, AnalogOutput);

                            if (fehlerAktiv)
                            {
                                _spsError = true;
                                break;
                            }

                            _spsError = false;

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
                    _spsStatus = "Keine Verbindung zur S7-1200!";
                }

                _callbackOutput(DigOutput, AnalogOutput);// zum Testen ohne SPS

                Thread.Sleep(50);
            }
        }

        public string ErrorAnzeigen(int resultError)
        {
            var errorText = _client?.ErrorText(resultError);
            return "Nr: " + resultError + " Text: " + errorText;
        }



        public string GetSpsStatus() => _spsStatus;
        public bool GetSpsError() => _spsError;
        public string GetVersion() => Encoding.ASCII.GetString(VersionInput, 0, VersionInput.Length);
        public string GetModel() => "S7-1200";
        public void SetTaskRunning(bool active) => _taskRunning = active;
        public void SetAutomatischerSoftwareTestAktiv(bool aktiv) => VersionInput[0] = aktiv ? (byte)1 : (byte)0;
        public void SetBitAt(Datenbausteine db, int bitPos, bool value) => throw new NotImplementedException();
        public ushort GetUint16At(Datenbausteine db, int bytePos) => throw new NotImplementedException();
    }
}