using System;
using System.Diagnostics;

namespace Kommunikation
{
    public class PlcWrapper
    {
        private readonly IPlc _plc;
        public PlcWrapper(int anzByteVersionInput, int anzByteDigInput, int anzByteDigOutput, int anzByteAnalogInput, int anzByteAnalogOutput, Action<byte[], byte[]> cbInput, Action<byte[], byte[]> cbOutput)
        {
            if (Debugger.IsAttached) // TODO replace with other implementation
                _plc = new MockPlc3DAutomatischesLagersystem(anzByteVersionInput, anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput, cbInput, cbOutput);
            else 
                _plc = new S7_1200(anzByteVersionInput, anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput, cbInput, cbOutput);
            
        }
        public bool GetSpsError()
        {
            return _plc.GetSpsError();
        }

        public void SPS_Pingen_Task()
        {
            _plc.SPS_Pingen_Task();
        }

        public string ErrorAnzeigen(int resultError)
        {
            return _plc.ErrorAnzeigen(resultError);
        }

        public string GetVersion()
        {
            return _plc.GetVersion();
        }

        public string GetSpsStatus()
        {
            return _plc.GetSpsStatus();
        }
    }
}