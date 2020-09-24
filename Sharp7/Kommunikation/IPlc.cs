namespace Kommunikation
{
    public interface IPlc
    {
        byte[] BefehleSps { get; set; }
        byte[] VersionInput { get; set; }
        byte[] DigInput { get; set; }
        byte[] DigOutput { get; set; }
        byte[] AnalogInput { get; set; }
        byte[] AnalogOutput { get; set; }



        string GetSpsStatus();
        bool GetSpsError();
        string GetVersion();
        string GetModel();
        void SetTaskRunning(bool active);

        void SetBitAt(Datenbausteine db, int bitPos, bool value);
        ushort GetUint16At(Datenbausteine db, int bytePos);
    }
}