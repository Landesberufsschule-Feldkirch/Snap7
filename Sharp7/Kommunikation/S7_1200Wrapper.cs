/*

using System;
using System.Diagnostics;

namespace Kommunikation
{
    public class S71200Wrapper
    {
        
        private readonly IPlc _plc;
        public S71200Wrapper(int anzByteVersionInput, int anzByteDigInput, int anzByteDigOutput, int anzByteAnalogInput, int anzByteAnalogOutput, Action<byte[], byte[]> cbInput, Action<byte[], byte[]> cbOutput)
        {
            if (Debugger.IsAttached) 
                _plc = new Manual(anzByteVersionInput, anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput, cbInput, cbOutput);
            else 
                _plc = new S7_1200(anzByteVersionInput, anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput, cbInput, cbOutput);
            
        }
        public bool GetSpsError() => _plc.GetSpsError();

        public void SPS_Pingen_Task() => _plc.SPS_Pingen_Task();

        public string ErrorAnzeigen(int resultError) => _plc.ErrorAnzeigen(resultError);

        public string GetVersion() => _plc.GetVersion();

        public string GetSpsStatus() => _plc.GetSpsStatus();
       
    }
} 
        */