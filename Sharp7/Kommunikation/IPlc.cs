namespace Kommunikation
{
    public interface IPlc
    {
        string GetSpsStatus();
        bool GetSpsError();
        string GetVersion();
        string GetModel();
        void SetTaskRunning(bool active);

        // ReSharper disable once UnusedMember.Global
        void SetBitAt(Datenbausteine db, int bitPos, bool value);
        // ReSharper disable once UnusedMember.Global
        ushort GetUint16At(Datenbausteine db, int bytePos);
    }
}