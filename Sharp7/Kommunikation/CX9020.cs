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

        public const int SpsTimeout = 1000;

        private readonly AdsClient _adsClient;

        private readonly Action<Datenstruktur> _callbackInput;
        private readonly Action<Datenstruktur> _callbackOutput;
        private readonly Datenstruktur _datenstruktur;
        private readonly IpAdressenBeckhoff _spsClient;

        private readonly byte[] _versionsStringDaten = new byte[1024];

        private int _zyklusZeitKommunikation = 10;
        private string _spsStatus = "Keine Verbindung zum CX9020!";
        private string _plcModus = "CX9020";
        private bool _spsError;
        private bool _taskRunning = true;


        public Cx9020()
        {
            _spsClient = JsonConvert.DeserializeObject<IpAdressenBeckhoff>(File.ReadAllText("IpAdressenBeckhoff.json"));
            _adsClient = new AdsClient();

            System.Threading.Tasks.Task.Run(SPS_Pingen_TaskAsync);
        }


        /*
         public CX9020(Datenstruktur datenstruktur, Action<Datenstruktur> cbInput, Action<Datenstruktur> cbOutput)
              {
                  _spsClient = JsonConvert.DeserializeObject<IpAdressen>(File.ReadAllText("IpAdressen.json"));

                  _datenstruktur = datenstruktur;

                  _callbackInput = cbInput;
                  _callbackOutput = cbOutput;

                  System.Threading.Tasks.Task.Run(SPS_Pingen_TaskAsync);
              }
        */

        public void SPS_Pingen_TaskAsync()
        {
            var cancel = CancellationToken.None;

            var valueWrite = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var valueRead = new byte[100];

            while (_taskRunning)
            {
                var pingSender = new Ping();
                var reply = pingSender.Send(_spsClient.IpAdresse, SpsTimeout);


                if (reply?.Status == IPStatus.Success)
                {
                    _spsStatus = "CX9020 sichtbar (Ping: " + reply.RoundtripTime + "ms)";

                    _adsClient.Connect(_spsClient.AmsNetId, _spsClient.Port);




                    var handleDigInput2 = _adsClient.CreateVariableHandle("Computer.DigInput[2]");
                    var handleDigOutput= _adsClient.CreateVariableHandle("Computer.DigOutput");

                    while (true)
                    {

                        valueRead[2] = (byte)_adsClient.ReadAny(handleDigInput2, typeof(byte));

                        _adsClient.WriteAny(handleDigOutput, valueWrite);

                        valueWrite[0]++;
                        valueWrite[2]++;

                        Thread.Sleep(100);
                    }

                }

                _spsStatus = "Keine Verbindung zur S7-1200!";


                Thread.Sleep(50);
            }

            valueRead[0] = valueRead[2];
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
            if (_versionsStringDaten[3] < 1) return "Uups";

            var textLaenge = _versionsStringDaten[3];
            var enc = new ASCIIEncoding();
            return enc.GetString(_datenstruktur.VersionInputSps, 0, textLaenge);
        }

        public string GetSpsStatus() => _spsStatus;
        public bool GetSpsError() => _spsError;
        public string GetPlcModus() => _plcModus;

        public void SetZyklusZeitKommunikation(int zeit) => _zyklusZeitKommunikation = zeit;
        public void SetPlcModus(string modus) => _plcModus = modus;
        public void SetTaskRunning(bool active) => _taskRunning = active;
        public void SetManualModeReferenz(Datenstruktur manualModeDatenstruktur) => ManDigInput = manualModeDatenstruktur.DigInput;
        //  public void SetBitAt(Datenbausteine db, int bitPos, bool value) => throw new NotImplementedException();
        //   public byte GetUint8At(Datenbausteine db, int bytePos) => throw new NotImplementedException();
        //   public ushort GetUint16At(Datenbausteine db, int bytePos) => throw new NotImplementedException();
    }
}