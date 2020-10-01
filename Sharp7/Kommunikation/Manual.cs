using System;
using System.Text;
using System.Threading;

namespace Kommunikation
{
    public class Manual : IPlc
    {
        private readonly Action<byte[], byte[]> _callbackInput;
        private readonly Action<byte[], byte[]> _callbackOutput;

        private readonly Datenstruktur _datenstruktur;

        private string _spsStatus;
        private bool _spsError;
        private bool _taskRunning = true;

        public Manual(Datenstruktur datenstruktur, Action<byte[], byte[]> cbInput, Action<byte[], byte[]> cbOutput)
        {
            _datenstruktur = datenstruktur;

            _callbackInput = cbInput;
            _callbackOutput = cbOutput;

            _datenstruktur.VersionInput = Encoding.ASCII.GetBytes("KeineVersionsinfo");
            System.Threading.Tasks.Task.Run(SPS_Pingen_Task);
        }


        public void SPS_Pingen_Task()
        {
            while (_taskRunning)
            {
                _spsStatus = "Manueller Modus aktiv";
                _spsError = false;

                _callbackInput(_digInput, _analogInput);
                _callbackOutput(_digOutput, _analogOutput);

                Thread.Sleep(10);
            }
        }


        public string GetSpsStatus() => _spsStatus;
        public bool GetSpsError() => _spsError;
        public string GetVersion() => "42";
        public string GetModel() => "Manual";
        public void SetTaskRunning(bool active) => _taskRunning = active;

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
                    if (value)
                        _digOutput[pos] = (byte)(_digOutput[pos] | mask[bit]);
                    else
                        _digOutput[pos] = (byte)(_digOutput[pos] & ~mask[bit]);
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
                    return (ushort)((_analogInput[bytePos] << 8) | _analogInput[bytePos + 1]);
                case Datenbausteine.DigIn:
                    return (ushort)((_digInput[bytePos] << 8) | _digInput[bytePos + 1]);
                default:
                    throw new NotImplementedException(nameof(Datenbausteine));
            }
        }
    }
}