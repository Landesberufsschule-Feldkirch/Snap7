using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using TwinCAT.Ads;

namespace Kommunikation
{
    public class Cx9020 : IPlc
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

        public byte[] Versionsinfo = new byte[256];

        public const int SpsTimeout = 1000;

        private readonly AdsClient _adsClient;

        private readonly Action<Datenstruktur, bool> _callbackRangieren;

        private readonly Datenstruktur _datenstruktur;
        private readonly IpAdressenBeckhoff _spsClient;

        private readonly byte[] _versionsStringDaten = new byte[1024];

        private int _zyklusZeitKommunikation = 10;
        private string _spsStatus = "Keine Verbindung zur CX9020!";
        private string _plcModus = "CX9020";
        private bool _spsError;
        private bool _taskRunning = true;


        public Cx9020(Datenstruktur datenstruktur, Action<Datenstruktur, bool> cbRangieren)
        {
            _datenstruktur = datenstruktur;
            _callbackRangieren = cbRangieren;

            _spsClient = JsonConvert.DeserializeObject<IpAdressenBeckhoff>(File.ReadAllText("IpAdressenBeckhoff.json"));
            _adsClient = new AdsClient();

            System.Threading.Tasks.Task.Run(SpsKommunikationTask);
        }


        public void SpsKommunikationTask()
        {
            var cancel = CancellationToken.None;


            while (_taskRunning)
            {
                var pingSender = new Ping();
                var reply = pingSender.Send(_spsClient.IpAdresse, SpsTimeout);

                _callbackRangieren(_datenstruktur, true); // TODO wieder entfernen!!

                if (reply?.Status == IPStatus.Success)
                {
                    _spsStatus = "CX9020 sichtbar (Ping: " + reply.RoundtripTime + "ms)";

                    _adsClient.Connect(_spsClient.AmsNetId, _spsClient.Port);


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

                        Versionsinfo = (byte[])_adsClient.ReadAny(handleVersionsInfo, typeof(byte[]), new int[] { 255 });

                        _datenstruktur.AnalogOutput = (byte[])_adsClient.ReadAny(handleAnalogOutput, typeof(byte[]), new int[] { 1024 });
                        _datenstruktur.DigOutput = (byte[])_adsClient.ReadAny(handleDigOutput, typeof(byte[]), new int[] { 1024 });

                        _adsClient.WriteAny(handleBefehle, betriebsartPlc);
                        _adsClient.WriteAny(handleAnalogInput, _datenstruktur.AnalogInput);
                        _adsClient.WriteAny(handleDigInput, _datenstruktur.DigInput);

                        for (var i = 0; i < 100; i++) _datenstruktur.VersionInputSps[1 + i] = Versionsinfo[i];

                        Thread.Sleep(10);
                    }

                }

                _spsStatus = "Keine Verbindung zur CX9020!";


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
            const string errorText = "??"; //_client?.ErrorText(resultError);
            return "Nr: " + resultError + " Text: " + errorText;
        }

        public string GetVersion()
        {
            var textLaenge = 0;

            if (Versionsinfo.Length < 1) return "Uups";

            for (var i = 0; i < 255; i++)
            {
                if (Versionsinfo[i] == 0)
                {
                    textLaenge = i;
                    break;
                }
            }

            var enc = new ASCIIEncoding();
            return enc.GetString(Versionsinfo, 0, textLaenge);
        }

        public string GetSpsStatus() => _spsStatus;
        public bool GetSpsError() => _spsError;
        public string GetPlcModus() => _plcModus;
        public string GetPlcBezeichnung() => _spsClient.Description;

        public void SetZyklusZeitKommunikation(int zeit) => _zyklusZeitKommunikation = zeit;


        public void SetPlcModus(string modus) => _plcModus = modus;
        public void SetTaskRunning(bool active) => _taskRunning = active;
        public void SetManualModeReferenz(Datenstruktur manualModeDatenstruktur) => ManDigInput = manualModeDatenstruktur.DigInput;


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