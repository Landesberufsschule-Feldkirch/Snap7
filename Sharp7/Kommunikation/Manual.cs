using System;
using System.Text;
using System.Threading;

namespace Kommunikation
{
    public class Manual : IPlc
    {
        private readonly Action<Datenstruktur> _callbackInput;
        private readonly Action<Datenstruktur> _callbackOutput;

        private readonly Datenstruktur _datenstruktur;

        private string _spsStatus;
        private bool _spsError;
        private bool _taskRunning = true;

        public Manual(Datenstruktur datenstruktur, Action<Datenstruktur> cbInput, Action<Datenstruktur> cbOutput)
        {
            _datenstruktur = datenstruktur;

            _callbackInput = cbInput;
            _callbackOutput = cbOutput;

            _datenstruktur.VersionInputSps = Encoding.ASCII.GetBytes("KeineVersionsinfo");
            System.Threading.Tasks.Task.Run(SPS_Pingen_Task);
        }


        public void SPS_Pingen_Task()
        {
            while (_taskRunning)
            {
                _spsStatus = "Manueller Modus aktiv";
                _spsError = false;

                _callbackInput(_datenstruktur);
                _callbackOutput(_datenstruktur);

                Thread.Sleep(10);
            }
        }


        public string GetSpsStatus() => _spsStatus;
        public bool GetSpsError() => _spsError;
        public string GetVersion() => "42";
        public string GetPlcModus() => "Manual";
        public void SetTaskRunning(bool active) => _taskRunning = active;
        public void SetZyklusZeitKommunikation(int zeit) => throw new NotImplementedException();

        public void SetBitAt(Datenbausteine db, int bitPos, bool value)
        {
            byte[] mask = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };

            var bit = bitPos % 8;
            var pos = bitPos / 8;

            if (bit < 0) bit = 0;
            if (bit > 7) bit = 7;

            switch (db)
            {
                case Datenbausteine.DigOut:
                    _datenstruktur.DigOutput[pos] = value
                        ? (byte)(_datenstruktur.DigOutput[pos] | mask[bit])
                        : (byte)(_datenstruktur.DigOutput[pos] & ~mask[bit]);
                    break;
                case Datenbausteine.VersionIn:
                    break;
                case Datenbausteine.DigIn:
                    break;
                case Datenbausteine.AnIn:
                    break;
                case Datenbausteine.AnOut:
                    break;
                default:
                    throw new NotImplementedException(nameof(Datenbausteine));
            }
        }

        public ushort GetUint16At(Datenbausteine db, int bytePos)
        {
            switch (db)
            {
                case Datenbausteine.AnIn:
                    return (ushort)((_datenstruktur.AnalogInput[bytePos] << 8) | _datenstruktur.AnalogInput[bytePos + 1]);
                case Datenbausteine.DigIn:
                    return (ushort)((_datenstruktur.DigInput[bytePos] << 8) | _datenstruktur.DigInput[bytePos + 1]);
                case Datenbausteine.VersionIn:
                    break;
                case Datenbausteine.DigOut:
                    break;
                case Datenbausteine.AnOut:
                    break;
                default:
                    throw new NotImplementedException(nameof(Datenbausteine));
            }

            return 0;
        }
    }
}