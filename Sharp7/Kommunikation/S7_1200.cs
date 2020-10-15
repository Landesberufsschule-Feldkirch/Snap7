using Newtonsoft.Json;
using Sharp7;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace Kommunikation
{
    public class S71200 : IPlc
    {
        private enum BytePosition
        {
            Byte0 = 0,
            Byte4 = 3
        }

        private enum AnzahlByte
        {
            EinByte = 1
        }


        public const int SpsTimeout = 1000;
        public const int SpsRack = 0;
        public const int SpsSlot = 0;

        private readonly S7Client _client = new S7Client();
        private readonly Action<Datenstruktur> _callbackInput;
        private readonly Action<Datenstruktur> _callbackOutput;
        private readonly Datenstruktur _datenstruktur;
        private readonly IpAdressen _spsClient;

        private int _zyklusZeitKommunikation = 100;
        private string _spsStatus = "Keine Verbindung zur S7-1200!";
        private bool _spsError;

        private bool _taskRunning = true;

        public S71200(Datenstruktur datenstruktur, Action<Datenstruktur> cbInput, Action<Datenstruktur> cbOutput)
        {
            _spsClient = JsonConvert.DeserializeObject<IpAdressen>(File.ReadAllText("IpAdressen.json"));

            _datenstruktur = datenstruktur;

            _callbackInput = cbInput;
            _callbackOutput = cbOutput;

            System.Threading.Tasks.Task.Run(SPS_Pingen_Task);
        }

        public void SPS_Pingen_Task()
        {
            while (_taskRunning)
            {
                var pingSender = new Ping();
                var reply = pingSender.Send(_spsClient.Adress, SpsTimeout);

                _callbackInput(_datenstruktur); // zum Testen ohne SPS

                if (reply != null && reply.Status == IPStatus.Success)
                {
                    _spsStatus = "S7-1200 sichtbar (Ping: " + reply.RoundtripTime + "ms)";
                    var res = _client?.ConnectTo(_spsClient.Adress, SpsRack, SpsSlot);
                    if (res == 0)
                    {
                        while (true)
                        {
                            var fehlerAktiv = false;

                            _callbackInput(_datenstruktur);

                            if (_datenstruktur.VersionInput.Length > 0)
                            {
                                _datenstruktur.BefehleSps[0]++;
                                fehlerAktiv |= FehlerAktiv(_client.DBWrite((int)Datenbausteine.VersionIn, (int)BytePosition.Byte0, (int)AnzahlByte.EinByte, _datenstruktur.BefehleSps));

                                //2 Byte Offset +  2 Byte Header (Zul. Stringlänge + Zeichenlänge) 
                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.VersionIn, (int)BytePosition.Byte4, _datenstruktur.VersionInput.Length, _datenstruktur.VersionInput));

                            }
                            if (_datenstruktur.AnzahlByteDigitalInput > 0)
                            {
                                fehlerAktiv |= FehlerAktiv(_client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte0, _datenstruktur.AnzahlByteDigitalInput, _datenstruktur.DigInput));
                            }
                            if (_datenstruktur.AnzahlByteDigitalOutput > 0)
                            {
                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte0, _datenstruktur.AnzahlByteDigitalOutput, _datenstruktur.DigOutput));
                            }
                            if (_datenstruktur.AnzahlByteAnalogInput > 0)
                            {
                                fehlerAktiv |= FehlerAktiv(_client.DBWrite((int)Datenbausteine.AnIn, (int)BytePosition.Byte0, _datenstruktur.AnzahlByteAnalogInput, _datenstruktur.AnalogInput));
                            }
                            if (_datenstruktur.AnzahlByteAnalogOutput > 0)
                            {
                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.AnOut, (int)BytePosition.Byte0, _datenstruktur.AnzahlByteAnalogOutput, _datenstruktur.AnalogOutput));
                            }

                            _callbackOutput(_datenstruktur);

                            if (fehlerAktiv)
                            {
                                _spsError = true;
                                break;
                            }

                            _spsError = false;

                            Thread.Sleep(_zyklusZeitKommunikation);
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

                _callbackOutput(_datenstruktur);// zum Testen ohne SPS

                Thread.Sleep(50);
            }
        }

        private bool FehlerAktiv(int? error)
        {
            if (error == 0) return false;

            _spsStatus = ErrorAnzeigen(error.GetValueOrDefault());
            return true;

        }

        public string ErrorAnzeigen(int resultError)
        {
            var errorText = _client?.ErrorText(resultError);
            return "Nr: " + resultError + " Text: " + errorText;
        }

        public void SetZyklusZeitKommunikation(int zeit) => _zyklusZeitKommunikation = zeit;
        public string GetSpsStatus() => _spsStatus;
        public bool GetSpsError() => _spsError;
        public string GetVersion() => Encoding.ASCII.GetString(_datenstruktur.VersionInput, 0, _datenstruktur.VersionInput.Length);
        public string GetPlcModus() => "S7-1200";
        public void SetTaskRunning(bool active) => _taskRunning = active;
        public void SetBitAt(Datenbausteine db, int bitPos, bool value) => throw new NotImplementedException();
        public ushort GetUint16At(Datenbausteine db, int bytePos) => throw new NotImplementedException();
    }
}