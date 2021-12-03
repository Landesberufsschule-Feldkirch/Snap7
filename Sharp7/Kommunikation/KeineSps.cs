namespace Kommunikation
{
    public class KeineSps : IPlc
    {
        private string _spsStatus = "Keine SPS vorhanden!";
        private string _plcModus = "Keine SPS";
        private bool _taskRunning = true;

        public string GetVersion() => "-??-";
        public string GetPlcBezeichnung() => "Keine SPS erkannt";
        public string GetSpsStatus() => _spsStatus;
        public string GetPlcModus() => "-";
        public bool GetSpsError() => false;

        public void SetPlcModus(string modus) => _plcModus = modus;
        public void SetTaskRunning(bool active) => _taskRunning = active;

        public bool GetBitAt(byte[] buffer, int bitPos) => SharedCode.GetBitAtPosition(buffer, bitPos);
        public int GetSIntAt(byte[] buffer, int pos) => SharedCode.GetSIntAtPosition(buffer, pos);
        public byte GetUsIntAt(byte[] buffer, int pos) => buffer[pos];
        public short GetIntAt(byte[] buffer, int pos) => (short)((buffer[pos] << 8) | buffer[pos + 1]);
        public ushort GetUIntAt(byte[] buffer, int pos) => (ushort)((buffer[pos] << 8) | buffer[pos + 1]);
        public void SetBitAt(byte[] buffer, int bitPos, bool value) => SharedCode.SetBitAtPosition(buffer, bitPos, value);
        public void SetSIntAt(byte[] buffer, int pos, int value) => SharedCode.SetSIntAtPosition(buffer, pos, value);
        public void SetUsIntAt(byte[] buffer, int pos, byte value) => buffer[pos] = value;
        public void SetIntAt(byte[] buffer, int pos, short value) => SharedCode.SetIntAtPosition(buffer, pos, value);
        public void SetUIntAt(byte[] buffer, int pos, ushort value) => SharedCode.SetUIntAtPosition(buffer, pos, value);
    }
}