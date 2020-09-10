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


        public const int SpsTimeout = 1000;
        public const int SpsRack = 0;
        public const int SpsSlot = 0;

        private readonly Action<byte[], byte[]> _callbackInput;
        private readonly Action<byte[], byte[]> _callbackOutput;

        private readonly byte[] _befehleSps = new byte[1024];
        private readonly byte[] _versionInput = new byte[1024];
        private readonly byte[] _digInput = new byte[1024];
        private readonly byte[] _digOutput = new byte[1024];
        private readonly byte[] _analogInput = new byte[1024];
        private readonly byte[] _analogOutput = new byte[1024];

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

            Array.Clear(_befehleSps, 0, _befehleSps.Length);
            Array.Clear(_versionInput, 0, _versionInput.Length);
            Array.Clear(_digInput, 0, _digInput.Length);
            Array.Clear(_digOutput, 0, _digOutput.Length);
            Array.Clear(_analogInput, 0, _analogInput.Length);
            Array.Clear(_analogOutput, 0, _analogOutput.Length);

            System.Threading.Tasks.Task.Run(SPS_Pingen_Task);
        }
        
        public void SPS_Pingen_Task()
        {
            while (_taskRunning)
            {
                var pingSender = new Ping();
                var reply = pingSender.Send(_spsClient.Adress, SpsTimeout);

                _callbackInput(_digInput, _analogInput); // zum Testen ohne SPS

                if (reply != null && reply.Status == IPStatus.Success)
                {
                    _spsStatus = "S7-1200 sichtbar (Ping: " + reply.RoundtripTime.ToString() + "ms)";
                    int? res = _client?.ConnectTo(_spsClient.Adress, SpsRack, SpsSlot);
                    if (res == 0)
                    {
                        while (true)
                        {
                            var fehlerAktiv = false;

                            _callbackInput(_digInput, _analogInput);

                            int? resultError;
                            if (_anzahlByteVersionInput > 0)
                            {
                                _befehleSps[0]++;
                                resultError = _client.DBWrite((int)Datenbausteine.VersionIn, (int)BytePosition.Byte0, (int)AnzahlByte.EinByte, _befehleSps);
                                if (resultError != 0)
                                {
                                    fehlerAktiv = true;
                                    _spsStatus = ErrorAnzeigen(resultError.GetValueOrDefault());
                                }

                                resultError = _client.DBRead((int)Datenbausteine.VersionIn, (int)BytePosition.Byte4, _anzahlByteVersionInput, _versionInput);
                                if (resultError != 0)
                                {
                                    fehlerAktiv = true;
                                    _spsStatus = ErrorAnzeigen(resultError.GetValueOrDefault());
                                }
                            }
                            if (_anzahlByteDigInput > 0)
                            {
                                resultError = _client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte0, _anzahlByteDigInput, _digInput);
                                if (resultError != 0)
                                {
                                    fehlerAktiv = true;
                                    _spsStatus = ErrorAnzeigen(resultError.GetValueOrDefault());
                                }
                            }
                            if (_anzahlByteDigOutput > 0)
                            {
                                resultError = _client.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte0, _anzahlByteDigOutput, _digOutput);
                                if (resultError != 0)
                                {
                                    fehlerAktiv = true;
                                    _spsStatus = ErrorAnzeigen(resultError.GetValueOrDefault());
                                }
                            }
                            if (_anzahlByteAnalogInput > 0)
                            {
                                resultError = _client.DBWrite((int)Datenbausteine.AnIn, (int)BytePosition.Byte0, _anzahlByteAnalogInput, _analogInput);
                                if (resultError != 0)
                                {
                                    fehlerAktiv = true;
                                    _spsStatus = ErrorAnzeigen(resultError.GetValueOrDefault());
                                }
                            }
                            if (_anzahlByteAnalogOutput > 0)
                            {
                                resultError = _client.DBRead((int)Datenbausteine.AnOut, (int)BytePosition.Byte0, _anzahlByteAnalogOutput, _analogOutput);
                                if (resultError != 0)
                                {
                                    fehlerAktiv = true;
                                    _spsStatus = ErrorAnzeigen(resultError.GetValueOrDefault());
                                }
                            }

                            _callbackOutput(_digOutput, _analogOutput);

                            if (fehlerAktiv)
                            {
                                _spsError = true;
                                break;
                            }
                            else
                            {
                                _spsError = false;
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
                    _spsStatus = "Keine Verbindung zur S7-1200!";
                }

                _callbackOutput(_digOutput, _analogOutput);// zum Testen ohne SPS

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
        public string GetVersion() => Encoding.ASCII.GetString(_versionInput, 0, _versionInput.Length);
        public string GetModel() => "S7-1200";
        public void SetTaskRunning(bool active) => _taskRunning = active;
        public void SetAutomatischerSoftwareTestAktiv(bool aktiv) => _versionInput[0] = aktiv ? (byte) 1 : (byte) 0;
        public void SetBitAt(Datenbausteine db, int bitPos, bool value) => throw new NotImplementedException();
        public ushort GetUint16At(Datenbausteine db, int bytePos) => throw new NotImplementedException();
    }
}