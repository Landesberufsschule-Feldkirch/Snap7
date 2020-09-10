namespace Kommunikation
{
    public interface IPlc
    {
        string GetSpsStatus();
        bool GetSpsError();
        string GetVersion();
        string GetModel();
        void SetTaskRunning(bool active);

         void SetBitAt(Datenbausteine db, int bitPos, bool value);
         ushort GetUint16At(Datenbausteine db, int bytePos);
    }
}