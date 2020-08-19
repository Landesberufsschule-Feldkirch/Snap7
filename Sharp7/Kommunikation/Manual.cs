using System;
using System.Text;
using System.Threading;

namespace Kommunikation
{
    public class Manual : IPlc
    {

        private enum Datenbausteine
        {
            VersionIn = 1,
            DigIn,
            DigOut,
            AnIn,
            AnOut
        }

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
    }
}