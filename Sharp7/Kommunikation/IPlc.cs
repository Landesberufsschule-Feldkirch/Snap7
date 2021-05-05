namespace Kommunikation
{
    public interface IPlc
    {
        string GetSpsStatus();
        bool GetSpsError();
        string GetVersion();
        string GetPlcModus();
        void SetPlcModus(string modus);
        void SetTaskRunning(bool active);
        void SetZyklusZeitKommunikation(int zeit);


        bool GetBitAt(byte[] buffer, int bitPos);
        int GetSIntAt(byte[] buffer, int pos);
        byte GetUsIntAt(byte[] buffer, int pos);
        short GetIntAt(byte[] buffer, int pos);
        ushort GetUIntAt(byte[] buffer, int pos);


        void SetBitAt(byte[] buffer, int bitPos, bool value);
        void SetSIntAt(byte[] buffer, int pos, int value);
        void SetUsIntAt(byte[] buffer, int pos, byte value);
        void SetIntAt(byte[] buffer, int pos, short value);
        void SetUIntAt(byte[] buffer, int pos, ushort value);
    }
}