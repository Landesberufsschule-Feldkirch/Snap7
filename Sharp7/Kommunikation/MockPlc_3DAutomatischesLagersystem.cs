using System;
using Sharp7;

namespace Kommunikation
{
    public class MockPlc3DAutomatischesLagersystem : IPlc
    {
        public MockPlc3DAutomatischesLagersystem(int anzByteVersionInput, int anzByteDigInput, int anzByteDigOutput, int anzByteAnalogInput, int anzByteAnalogOutput, Action<byte[], byte[]> cbInput, Action<byte[], byte[]> cbOutput)
        {

        }


        public string GetSpsStatus()
        {
            return "";
        }

        public bool GetSpsError()
        {
            return false;
        }

        public void SPS_Pingen_Task()
        {
            // Method intentionally left empty.
        }

        public string ErrorAnzeigen(int resultError)
        {
            return string.Empty;
        }

        public string GetVersion()
        {
            return "42";
        }
    }
}