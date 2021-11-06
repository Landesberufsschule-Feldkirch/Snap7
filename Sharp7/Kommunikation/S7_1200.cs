using Newtonsoft.Json;
using Sharp7;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows;

namespace Kommunikation
{
    public class S71200 : IPlc
    {
        public const int SpsTimeout = 1000;

        private readonly S7Client _client = new S7Client();
        private readonly Action<Datenstruktur, bool> _callbackRangieren;
        private readonly Datenstruktur _datenstruktur;
        private readonly IpAdressenSiemens _spsClient;

        private string _spsStatus = "Keine Verbindung zur S7-1200!";
        private bool _spsError;

        public S71200(Datenstruktur datenstruktur, Action<Datenstruktur, bool> cbRangieren)
        {
            try
            {
                _spsClient = JsonConvert.DeserializeObject<IpAdressenSiemens>(File.ReadAllText("IpAdressenSiemens.json"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datei nicht gefunden: IpAdressenSiemens.json" + " --> " + ex);
            }
            _datenstruktur = datenstruktur;
            _callbackRangieren = cbRangieren;

            System.Threading.Tasks.Task.Run(SpsKommunikationTask);
        }
        public void SpsKommunikationTask()
        {
            while (true)
            {
                var pingSender = new Ping();
                var reply = pingSender.Send(_spsClient.Adress, SpsTimeout);

                var verzoegerung = 0;

                if (_datenstruktur.BetriebsartProjekt != BetriebsartProjekt.AutomatischerSoftwareTest) _callbackRangieren(_datenstruktur, true); // zum Testen ohne SPS
                if (reply?.Status == IPStatus.Success)
                {
                    _spsStatus = "S7-1200 sichtbar (Ping: " + reply.RoundtripTime + "ms)";
                    var res = _client?.ConnectTo(_spsClient.Adress, 0, 1);
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

                            if (_datenstruktur.AnzahlByteDigitalInput > 0 && !fehlerAktiv)
                            {
                                if (_datenstruktur.BetriebsartProjekt == BetriebsartProjekt.LaborPlatte)
                                {
                                    fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.DigIn, 0, _datenstruktur.AnzahlByteDigitalInput, _datenstruktur.DigInput));
                                }
                                else
                                {
                                    fehlerAktiv |= FehlerAktiv(_client.DBWrite((int)Datenbausteine.DigIn, 0, _datenstruktur.AnzahlByteDigitalInput, _datenstruktur.DigInput));
                                }
                            }

                            if (_datenstruktur.AnzahlByteAnalogInput > 0 && !fehlerAktiv)
                            {
                                fehlerAktiv |= FehlerAktiv(_client.DBWrite((int)Datenbausteine.AnIn, 0, _datenstruktur.AnzahlByteAnalogInput, _datenstruktur.AnalogInput));
                            }

                            if (_datenstruktur.AnzahlByteDigitalOutput > 0 && !fehlerAktiv)
                            {
                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.DigOut, 0, _datenstruktur.AnzahlByteDigitalOutput, _datenstruktur.DigOutput));
                            }

                            if (_datenstruktur.AnzahlByteAnalogOutput > 0 && !fehlerAktiv)
                            {
                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.AnOut, 0, _datenstruktur.AnzahlByteAnalogOutput, _datenstruktur.AnalogOutput));
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
        public string GetPlcModus()
        {
            throw new NotImplementedException();
        }
        public string GetPlcBezeichnung() => _spsClient.Description;
        public void SetPlcModus(string modus)
        {
            throw new NotImplementedException();
        }
        public void SetTaskRunning(bool active)
        {
            throw new NotImplementedException();
        }
        public void SetZyklusZeitKommunikation(int zeit)
        {
            throw new NotImplementedException();
        }
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

        public bool GetBitAt(byte[] buffer, int bitPos)
        {
            byte[] mask = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };

            var bit = bitPos % 8;
            var pos = bitPos / 8;

            if (bit < 0) bit = 0;
            if (bit > 7) bit = 7;
            return (buffer[pos] & mask[bit]) != 0;
        }
        public int GetSIntAt(byte[] buffer, int pos)
        {
            int value = buffer[pos];
            if (value < 128) return value;
            return value - 256;

        }
        public byte GetUsIntAt(byte[] buffer, int pos)
        {
            return buffer[pos];
        }
        public short GetIntAt(byte[] buffer, int pos)
        {
            return (short)((buffer[pos] << 8) | buffer[pos + 1]);
        }
        public ushort GetUIntAt(byte[] buffer, int pos)
        {
            return (ushort)((buffer[pos] << 8) | buffer[pos + 1]);
        }

        public void SetBitAt(byte[] buffer, int bitPos, bool value)
        {
            byte[] mask = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };

            var bit = bitPos % 8;
            var pos = bitPos / 8;

            if (bit < 0) bit = 0;
            if (bit > 7) bit = 7;

            if (value) buffer[pos] = (byte)(buffer[pos] | mask[bit]);
            else buffer[pos] = (byte)(buffer[pos] & ~mask[bit]);
        }
        public void SetSIntAt(byte[] buffer, int pos, int value)
        {
            if (value < -128) value = -128;
            if (value > 127) value = 127;
            buffer[pos] = (byte)value;
        }
        public void SetUsIntAt(byte[] buffer, int pos, byte value)
        {
            buffer[pos] = value;
        }
        public void SetIntAt(byte[] buffer, int pos, short value)
        {
            buffer[pos] = (byte)(value >> 8);
            buffer[pos + 1] = (byte)(value & 0x00FF);
        }
        public void SetUIntAt(byte[] buffer, int pos, ushort value)
        {
            buffer[pos] = (byte)(value >> 8);
            buffer[pos + 1] = (byte)(value & 0x00FF);
        }
    }
}