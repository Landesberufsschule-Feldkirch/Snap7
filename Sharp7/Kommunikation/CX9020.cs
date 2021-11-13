using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace Kommunikation
{
    public class Cx9020 : IPlc
    {
        public byte[] Versionsinfo = new byte[256];

        private readonly AdsClient _adsClient;
        private readonly int _anzDi;
        private readonly int _anzDa;
        private readonly int _anzAi;
        private readonly int _anzAa;
        private readonly Action<Datenstruktur, bool> _callbackRangieren;
        private IpAdressenBeckhoff _spsCx9020;
        private readonly Datenstruktur _datenstruktur;
        private string _spsStatus = "Keine Verbindung zur CX9020!";
        private string _plcModus = "CX9020";
        private readonly bool _spsError;
        private bool _taskRunning = true;
        private const int SpsTimeout = 1000;

        public Cx9020(IpAdressenBeckhoff spsCx9020, Datenstruktur datenstruktur, Action<Datenstruktur, bool> cbRangieren)
        {
            _spsCx9020 = spsCx9020;
            _datenstruktur = datenstruktur;
            _callbackRangieren = cbRangieren;

            _anzDi = datenstruktur.AnzahlByteDigitalInput;
            _anzDa = datenstruktur.AnzahlByteDigitalOutput;
            _anzAi = datenstruktur.AnzahlByteAnalogInput;
            _anzAa = datenstruktur.AnzahlByteAnalogOutput;

            _adsClient = new AdsClient();

            _spsError = false;
            Task.Run(SpsKommunikationTask);
        }
        public void SpsKommunikationTask()
        {
            while (_taskRunning)
            {
                var pingSender = new Ping();
                var reply = pingSender.Send(_spsCx9020.IpAdresse, SpsTimeout);

                if (reply?.Status == IPStatus.Success)
                {
                    _spsStatus = "CX9020 sichtbar (Ping: " + reply.RoundtripTime + "ms)";
                    _adsClient.Connect(_spsCx9020.AmsNetId, _spsCx9020.Port);

                    _callbackRangieren(_datenstruktur, true);

                    var handleVersionsInfo = _adsClient.CreateVariableHandle("VersionsInfo.Ver");
                    var handleBefehle = _adsClient.CreateVariableHandle("VersionsInfo.Befehle");
                    var handleAnalogInput = _adsClient.CreateVariableHandle("AnalogInput.AI");
                    var handleAnalogOutput = _adsClient.CreateVariableHandle("AnalogOutput.AA");
                    var handleDigInput = _adsClient.CreateVariableHandle("DigInput.DI");
                    var handleDigOutput = _adsClient.CreateVariableHandle("DigOutput.DA");

                    while (true)
                    {
                        byte betriebsartPlc = 0;

                        _callbackRangieren(_datenstruktur, true);

                        if (_datenstruktur.BetriebsartProjekt != BetriebsartProjekt.LaborPlatte) betriebsartPlc = 1;

                        Versionsinfo = (byte[])_adsClient.ReadAny(handleVersionsInfo, typeof(byte[]), new[] { 255 });

                        if (_anzAa > 0) _datenstruktur.AnalogOutput = (byte[])_adsClient.ReadAny(handleAnalogOutput, typeof(byte[]), new[] { 1024 });
                        if (_anzDa > 0) _datenstruktur.DigOutput = (byte[])_adsClient.ReadAny(handleDigOutput, typeof(byte[]), new[] { 1024 });

                        _adsClient.WriteAny(handleBefehle, betriebsartPlc);
                        if (_anzAi > 0) _adsClient.WriteAny(handleAnalogInput, _datenstruktur.AnalogInput);
                        if (_anzDi > 0) _adsClient.WriteAny(handleDigInput, _datenstruktur.DigInput);

                        for (var i = 0; i < 100; i++) _datenstruktur.VersionInputSps[1 + i] = Versionsinfo[i];

                        Thread.Sleep(10);
                    }

                }

                _spsStatus = "Keine Verbindung zur CX9020!";

                Thread.Sleep(50);
            }
        }
        public string GetVersion()
        {
            var textLaenge = 0;

            if (Versionsinfo.Length < 1) return "Uups";

            for (var i = 0; i < 255; i++)
            {
                if (Versionsinfo[i] != 0) continue;
                textLaenge = i;
                break;
            }

            var enc = new ASCIIEncoding();
            return enc.GetString(Versionsinfo, 0, textLaenge);
        }

        public string GetSpsStatus() => _spsStatus;
        public bool GetSpsError() => _spsError;
        public string GetPlcModus() => _plcModus;
        public string GetPlcBezeichnung() => _spsCx9020.Description;
        public void SetPlcModus(string modus) => _plcModus = modus;
        public void SetTaskRunning(bool active) => _taskRunning = active;
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