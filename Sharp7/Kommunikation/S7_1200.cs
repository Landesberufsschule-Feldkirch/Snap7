using Sharp7;
using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace Kommunikation
{
    public class S71200 : IPlc
    {
        private readonly S7Client _client = new S7Client();
        private readonly IpAdressenSiemens _spsS7_1200;
        private readonly Datenstruktur _datenstruktur;
        private readonly Action<Datenstruktur, bool> _callbackRangieren;
        private readonly int _anzDi;
        private readonly int _anzDa;
        private readonly int _anzAi;
        private readonly int _anzAa;
        private string _spsStatus = "Keine Verbindung zur S7-1200!";
        private bool _spsError;
        private const int SpsTimeout = 1000;

        public S71200(IpAdressenSiemens spsS7_1200, Datenstruktur datenstruktur, Action<Datenstruktur, bool> cbRangieren)
        {
            _spsS7_1200 = spsS7_1200;
            _datenstruktur = datenstruktur;
            _callbackRangieren = cbRangieren;

            _anzDi = datenstruktur.AnzahlByteDigitalInput;
            _anzDa = datenstruktur.AnzahlByteDigitalOutput;
            _anzAi = datenstruktur.AnzahlByteAnalogInput;
            _anzAa = datenstruktur.AnzahlByteAnalogOutput;

            System.Threading.Tasks.Task.Run(SpsKommunikationTask);
        }
        public void SpsKommunikationTask()
        {
            while (true)
            {
                var pingSender = new Ping();
                var reply = pingSender.Send(_spsS7_1200.Adress, SpsTimeout);

                var verzoegerung = 0;

                if (_datenstruktur.BetriebsartProjekt != BetriebsartProjekt.AutomatischerSoftwareTest) _callbackRangieren(_datenstruktur, true); // zum Testen ohne SPS
                if (reply?.Status == IPStatus.Success)
                {
                    _spsStatus = "S7-1200 sichtbar (Ping: " + reply.RoundtripTime + "ms)";
                    var res = _client?.ConnectTo(_spsS7_1200.Adress, 0, 1);
                    if (res == 0)
                    {
                        while (true)
                        {
                            var fehlerAktiv = false;

                            _callbackRangieren(_datenstruktur, _datenstruktur.BetriebsartProjekt == BetriebsartProjekt.Simulation);

                            if (verzoegerung > 10)
                            {
                                //2 Byte Offset +  2 Byte Header (Zul. Stringlänge + Zeichenlänge) 
                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.VersionIn, 2, 202, _datenstruktur.VersionInputSps));
                                verzoegerung = 0;
                            }
                            verzoegerung++;
                            if (!fehlerAktiv)
                            {
                                var betriebsartPlc = _datenstruktur.BetriebsartProjekt != BetriebsartProjekt.LaborPlatte ? 1 : 0;
                                _datenstruktur.BefehleSps[0] = (byte)betriebsartPlc;

                                fehlerAktiv |= FehlerAktiv(_client.DBWrite((int)Datenbausteine.VersionIn, 0, 1, _datenstruktur.BefehleSps));
                            }

                            if (_anzDi > 0 && !fehlerAktiv)
                            {
                                if (_datenstruktur.BetriebsartProjekt == BetriebsartProjekt.LaborPlatte)
                                {
                                    fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.DigIn, 0, _anzDi, _datenstruktur.DigInput));
                                }
                                else
                                {
                                    fehlerAktiv |= FehlerAktiv(_client.DBWrite((int)Datenbausteine.DigIn, 0, _anzDi, _datenstruktur.DigInput));
                                }
                            }

                            if (_anzAi> 0 && !fehlerAktiv)
                            {
                                fehlerAktiv |= FehlerAktiv(_client.DBWrite((int)Datenbausteine.AnIn, 0, _anzAi, _datenstruktur.AnalogInput));
                            }

                            if (_anzDa > 0 && !fehlerAktiv)
                            {
                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.DigOut, 0, _anzDa, _datenstruktur.DigOutput));
                            }

                            if (_anzAa > 0 && !fehlerAktiv)
                            {
                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.AnOut, 0, _anzAa, _datenstruktur.AnalogOutput));
                            }


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
                        _spsStatus = ErrorAnzeigen(res.GetValueOrDefault());
                    }
                }
                else
                {
                    _spsStatus = "Keine Verbindung zur S7-1200!";
                }

                Thread.Sleep(50);
            }
            // ReSharper disable once FunctionNeverReturns
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
            if (_datenstruktur.VersionInputSps[1] < 1) return "Uups";

            var textLaenge = _datenstruktur.VersionInputSps[1];
            var enc = new ASCIIEncoding();
            return enc.GetString(_datenstruktur.VersionInputSps, 2, textLaenge);
        }
        public string GetPlcModus() => throw new NotImplementedException();
        public string GetPlcBezeichnung() => _spsS7_1200.Description;
        public void SetPlcModus(string modus) => throw new NotImplementedException();
        public void SetTaskRunning(bool active) => throw new NotImplementedException();
        public string GetSpsStatus() => _spsStatus;
        public bool GetSpsError() => _spsError;
        public int ColdStart() => _client.PlcColdStart();
        public int HotStart() => _client.PlcHotStart();
        public (int retval, int status) GetStatus()
        {
            var status = 0;
            var retval = _client.PlcGetStatus(ref status);
            return (retval, status);
        }
        public bool GetBitAt(byte[] buffer, int bitPos) => SharedCode.GetBitAtPosition(buffer, bitPos);
        public int GetSIntAt(byte[] buffer, int pos) => SharedCode.GetSIntAtPosition(buffer, pos);
        public byte GetUsIntAt(byte[] buffer, int pos) => buffer[pos];
        public short GetIntAt(byte[] buffer, int pos) => (short)((buffer[pos] << 8) | buffer[pos + 1]);
        public ushort GetUIntAt(byte[] buffer, int pos) => (ushort)((buffer[pos] << 8) | buffer[pos + 1]);
        public void SetBitAt(byte[] buffer, int bitPos, bool value) => SharedCode.SetBitAtPosition(buffer, bitPos, value);
        public void SetSIntAt(byte[] buffer, int pos, int value) => SharedCode.SetSIntAtPosition(buffer, pos, value);
        public void SetUsIntAt(byte[] buffer, int pos, byte value) => buffer[pos] = value;
        public void SetIntAt(byte[] buffer, int pos, short value) => SharedCode.SetIntAtPosition(buffer, pos, value);
        public void SetUIntAt(byte[] buffer, int pos, ushort value) => SharedCode.SetUIntAtPosition(buffer, pos, value);
    }
}