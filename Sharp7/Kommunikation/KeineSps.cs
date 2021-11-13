using System;
using System.Collections.Generic;
using System.Text;

namespace Kommunikation
{
    public class KeineSps : IPlc
    {
        public bool GetBitAt(byte[] buffer, int bitPos) => true;

        public short GetIntAt(byte[] buffer, int pos) => 0;

        public string GetPlcBezeichnung() => "keine SPS";

        public string GetPlcModus() => "-";

        public int GetSIntAt(byte[] buffer, int pos) => 0;

        public bool GetSpsError() => false;

        public string GetSpsStatus() => "-";

        public ushort GetUIntAt(byte[] buffer, int pos) => 0;

        public byte GetUsIntAt(byte[] buffer, int pos) => 0;

        public string GetVersion() => "-";

        public void SetBitAt(byte[] buffer, int bitPos, bool value)
        {
            
        }

        public void SetIntAt(byte[] buffer, int pos, short value)
        {
            
        }

        public void SetPlcModus(string modus)
        {
            
        }

        public void SetSIntAt(byte[] buffer, int pos, int value)
        {
            
        }

        public void SetTaskRunning(bool active)
        {
            
        }

        public void SetUIntAt(byte[] buffer, int pos, ushort value)
        {
            
        }

        public void SetUsIntAt(byte[] buffer, int pos, byte value)
        {
            
        }

        public void SetZyklusZeitKommunikation(int zeit)
        {
            
        }
    }
}