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
            Byte4 = 4
        }

        public const int SpsTimeout = 1000;

        private readonly S7Client _client = new S7Client();
        private readonly Action<Datenstruktur> _callbackInput;
        private readonly Action<Datenstruktur> _callbackOutput;
        private readonly Datenstruktur _datenstruktur;
        private readonly IpAdressen _spsClient;

        private readonly byte[] _versionsStringDaten = new byte[1024];
        private string _spsStatus = "Keine Verbindung zur S7-1200!";
        private string _plcModus = "S7-1200";
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

                if (_datenstruktur.BetriebsartProjekt != BetriebsartProjekt.AutomatischerSoftwareTest) _callbackInput(_datenstruktur); // zum Testen ohne SPS

                if (reply?.Status == IPStatus.Success)
                {
                    _spsStatus = "S7-1200 sichtbar (Ping: " + reply.RoundtripTime + "ms)";
                    var res = _client?.ConnectTo(_spsClient.Adress, 0, 1);
                    if (res == 0)
                    {
                        while (_taskRunning)
                        {
                            var fehlerAktiv = false;

                            if (_datenstruktur.BetriebsartProjekt == BetriebsartProjekt.Simulation) _callbackInput(_datenstruktur);

                            if (_taskRunning)
                            {
                                //2 Byte Offset +  2 Byte Header (Zul. Stringlänge + Zeichenlänge) 
                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.VersionIn, (int)BytePosition.Byte0, 4, _versionsStringDaten));
                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.VersionIn, (int)BytePosition.Byte4, _versionsStringDaten[3], _datenstruktur.VersionInputSps));
                            }

                            if (_taskRunning && !fehlerAktiv)
                            {
                                var betriebsartPlc = _datenstruktur.BetriebsartProjekt != BetriebsartProjekt.LaborPlatte ? 1 : 0;
                                _datenstruktur.BefehleSps[0] = (byte)betriebsartPlc;

                                fehlerAktiv |= FehlerAktiv(_client.DBWrite((int)Datenbausteine.VersionIn, (int)BytePosition.Byte0, 1, _datenstruktur.BefehleSps));
                            }

                            if (_datenstruktur.AnzahlByteDigitalInput > 0 && _taskRunning && !fehlerAktiv)
                            {
                                if (_datenstruktur.BetriebsartProjekt == BetriebsartProjekt.LaborPlatte)
                                {
                                    fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.DigIn, (int)BytePosition.Byte0, _datenstruktur.AnzahlByteDigitalInput, _datenstruktur.DigInput));
                                }
                                else
                                {
                                    fehlerAktiv |= FehlerAktiv(_client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte0, _datenstruktur.AnzahlByteDigitalInput, _datenstruktur.DigInput));
                                }
                            }

                            if (_datenstruktur.AnzahlByteAnalogInput > 0 && _taskRunning && !fehlerAktiv)
                            {
                                fehlerAktiv |= FehlerAktiv(_client.DBWrite((int)Datenbausteine.AnIn, (int)BytePosition.Byte0, _datenstruktur.AnzahlByteAnalogInput, _datenstruktur.AnalogInput));
                            }

                            if (_datenstruktur.AnzahlByteDigitalOutput > 0 && _taskRunning && !fehlerAktiv)
                            {
                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte0, _datenstruktur.AnzahlByteDigitalOutput, _datenstruktur.DigOutput));
                            }

                            if (_datenstruktur.AnzahlByteAnalogOutput > 0 && _taskRunning && !fehlerAktiv)
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

                            Thread.Sleep(1);
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

        public string GetVersion()
        {
            if (_versionsStringDaten[3] < 1) return "Uups";

            var textLaenge = _versionsStringDaten[3];
            var enc = new ASCIIEncoding();
            return enc.GetString(_datenstruktur.VersionInputSps, 0, textLaenge);
        }

        public string GetSpsStatus() => _spsStatus;
        public bool GetSpsError() => _spsError;
        public string GetPlcModus() => _plcModus;

        public void SetPlcModus(string modus) => _plcModus = modus;
        public void SetTaskRunning(bool active) => _taskRunning = active;
        public void SetBitAt(Datenbausteine db, int bitPos, bool value) => throw new NotImplementedException();
        public byte GetUint8At(Datenbausteine db, int bytePos) => throw new NotImplementedException();
        public ushort GetUint16At(Datenbausteine db, int bytePos) => throw new NotImplementedException();
    }
}