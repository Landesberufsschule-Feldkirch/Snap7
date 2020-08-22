using System;
using System.Text;
using System.Threading;

namespace Kommunikation
{
    public class Manual : IPlc
    {



        private enum BytePosition
        {
            Byte_0 = 0
        }

        public const int SPS_Timeout = 1000;
        public const int SPS_Rack = 0;
        public const int SPS_Slot = 0;

        private readonly Action<byte[], byte[]> callbackInput;
        private readonly Action<byte[], byte[]> callbackOutput;

        private readonly byte[] versionInput = new byte[1024];
        private readonly byte[] digInput = new byte[1024];
        private readonly byte[] digOutput = new byte[1024];
        private readonly byte[] analogInput = new byte[1024];
        private readonly byte[] analogOutput = new byte[1024];

        private readonly int anzahlByteVersionInput;
        private readonly int anzahlByteDigInput;
        private readonly int anzahlByteDigOutput;
        private readonly int anzahlByteAnalogInput;
        private readonly int anzahlByteAnalogOutput;

        private string spsStatus = "Keine Verbindung zur S7-1200!";
        private bool spsError;
        private bool _taskRunning = true;

        public Manual(int anzByteVersionInput, int anzByteDigInput, int anzByteDigOutput, int anzByteAnalogInput, int anzByteAnalogOutput, Action<byte[], byte[]> cbInput, Action<byte[], byte[]> cbOutput)
        {
            anzahlByteVersionInput = anzByteVersionInput;
            anzahlByteDigInput = anzByteDigInput;
            anzahlByteDigOutput = anzByteDigOutput;
            anzahlByteAnalogInput = anzByteAnalogInput;
            anzahlByteAnalogOutput = anzByteAnalogOutput;

            callbackInput = cbInput;
            callbackOutput = cbOutput;

            Array.Clear(versionInput, 0, versionInput.Length);
            Array.Clear(digInput, 0, digInput.Length);
            Array.Clear(digOutput, 0, digOutput.Length);
            Array.Clear(analogInput, 0, analogInput.Length);
            Array.Clear(analogOutput, 0, analogOutput.Length);

            versionInput = Encoding.ASCII.GetBytes("KeineVersionsinfo");
            System.Threading.Tasks.Task.Run(SPS_Pingen_Task);
        }





        public void SPS_Pingen_Task()
        {
            while (_taskRunning)
            {
                spsStatus = "Manueller Modus aktiv";
                spsError = false;

                callbackInput(digInput, analogInput);
                callbackOutput(digOutput, analogOutput);

                Thread.Sleep(10);
            }
        }

        public string GetSpsStatus() => "";
        public bool GetSpsError() => false;
        public string ErrorAnzeigen(int resultError) => string.Empty;
        public string GetVersion() => "42";
        public string GetModel() => "Manual";
        public void SetTaskRunning(bool active) => _taskRunning = active;

        public void SetBitAt(Datenbausteine db, int bitPos, bool value)
        {
            byte[] Mask = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };

            var Bit = bitPos % 8;
            var Pos = bitPos / 8;

            if (Bit < 0) Bit = 0;
            if (Bit > 7) Bit = 7;

            switch (db)
            {
                case Datenbausteine.DigOut:
                    if (value)
                        digOutput[Pos] = (byte)(digOutput[Pos] | Mask[Bit]);
                    else
                        digOutput[Pos] = (byte)(digOutput[Pos] & ~Mask[Bit]);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        public ushort GetUint16At(Datenbausteine db, int bytePos)
        {
            switch (db)
            {
                case Datenbausteine.AnIn:
                    return (ushort)((analogInput[bytePos] << 8) | analogInput[bytePos + 1]);
                case Datenbausteine.DigIn:
                    return (ushort)((digInput[bytePos] << 8) | digInput[bytePos + 1]);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}