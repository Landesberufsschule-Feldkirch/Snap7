﻿using Newtonsoft.Json;
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
            // ReSharper disable once UnusedMember.Local
            // ReSharper disable once UnusedMember.Global
            Byte1,
            Byte2,
            Byte3,
            Byte4
        }

        public byte[] ManDigInput { get; set; }

        public const int SpsTimeout = 1000;
        public const int SpsRack = 0;
        public const int SpsSlot = 0;

        private readonly S7Client _client = new S7Client();
        private readonly Action<Datenstruktur> _callbackInput;
        private readonly Action<Datenstruktur> _callbackOutput;
        private readonly Datenstruktur _datenstruktur;
        private readonly IpAdressen _spsClient;

        private readonly byte[] _zulStringLaenge = new byte[1024];
        private readonly byte[] _zeichenLaenge = new byte[1024];

        private int _zyklusZeitKommunikation = 10;
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

                if (_datenstruktur.GetBetriebsartProjekt() != BetriebsartProjekt.AutomatischerSoftwareTest) _callbackInput(_datenstruktur); // zum Testen ohne SPS

                if (reply?.Status == IPStatus.Success)
                {
                    _spsStatus = "S7-1200 sichtbar (Ping: " + reply.RoundtripTime + "ms)";
                    var res = _client?.ConnectTo(_spsClient.Adress, SpsRack, SpsSlot);
                    if (res == 0)
                    {
                        while (_taskRunning)
                        {
                            var fehlerAktiv = false;

                            if (_datenstruktur.GetBetriebsartProjekt() != BetriebsartProjekt.AutomatischerSoftwareTest) _callbackInput(_datenstruktur);

                            if (_datenstruktur.VersionInputSps.Length > 0 && _taskRunning)
                            {                               
                                //2 Byte Offset +  2 Byte Header (Zul. Stringlänge + Zeichenlänge) 
                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.VersionIn, (int)BytePosition.Byte2, 1, _zulStringLaenge));
                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.VersionIn, (int)BytePosition.Byte3, 1, _zeichenLaenge));

                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.VersionIn, (int)BytePosition.Byte4, _zeichenLaenge[4], _datenstruktur.VersionInputSps));
                            }

                            if (_taskRunning)
                            {
                                if (_datenstruktur.GetBetriebsartProjekt() != BetriebsartProjekt.LaborPlatte)
                                {
                                    _datenstruktur.BefehleSps[0] = 1;
                                }
                                else
                                {
                                    _datenstruktur.BefehleSps[0] = 0;
                                }
                                fehlerAktiv |= FehlerAktiv(_client.DBWrite((int)Datenbausteine.VersionIn, (int)BytePosition.Byte0, 1, _datenstruktur.BefehleSps));
                            }

                            if (_datenstruktur.AnzahlByteDigitalInput > 0 && _taskRunning)
                            {
                                if (_datenstruktur.GetBetriebsartProjekt() == BetriebsartProjekt.AutomatischerSoftwareTest)
                                {
                                    _datenstruktur.DigInput[0] = ManDigInput[0];
                                    _datenstruktur.DigInput[1] = ManDigInput[1];
                                }
                                fehlerAktiv |= FehlerAktiv(_client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte0, _datenstruktur.AnzahlByteDigitalInput, _datenstruktur.DigInput));
                            }

                            if (_datenstruktur.AnzahlByteAnalogInput > 0 && _taskRunning)
                            {
                                fehlerAktiv |= FehlerAktiv(_client.DBWrite((int)Datenbausteine.AnIn, (int)BytePosition.Byte0, _datenstruktur.AnzahlByteAnalogInput, _datenstruktur.AnalogInput));
                            }

                            if (_datenstruktur.AnzahlByteDigitalOutput > 0 && _taskRunning)
                            {
                                fehlerAktiv |= FehlerAktiv(_client.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte0, _datenstruktur.AnzahlByteDigitalOutput, _datenstruktur.DigOutput));
                            }

                            if (_datenstruktur.AnzahlByteAnalogOutput > 0 && _taskRunning)
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

        public string GetVersion()
        {
            if (_zeichenLaenge[0] <= 0) return "Uups";

            var enc = new ASCIIEncoding();
            return enc.GetString(_datenstruktur.VersionInputSps, 0, _zeichenLaenge[0]);
        }

        public string GetSpsStatus() => _spsStatus;
        public bool GetSpsError() => _spsError;
        public string GetPlcModus() => _plcModus;

        public void SetZyklusZeitKommunikation(int zeit) => _zyklusZeitKommunikation = zeit;
        public void SetPlcModus(string modus) => _plcModus = modus;
        public void SetTaskRunning(bool active) => _taskRunning = active;
        public void SetManualModeReferenz(Datenstruktur manualModeDatenstruktur) => ManDigInput = manualModeDatenstruktur.DigInput;
        public void SetBitAt(Datenbausteine db, int bitPos, bool value) => throw new NotImplementedException();
        public byte GetUint8At(Datenbausteine db, int bytePos) => throw new NotImplementedException();
        public ushort GetUint16At(Datenbausteine db, int bytePos) => throw new NotImplementedException();
    }
}