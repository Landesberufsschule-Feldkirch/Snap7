namespace Kommunikation
{
    public interface IPlc
    {

        string GetSpsStatus();
        bool GetSpsError();
        void SPS_Pingen_Task();
        string ErrorAnzeigen(int resultError);
        string GetVersion();
        string GetModel();
        void SetTaskRunning(bool active);

         void SetBitAt(Datenbausteine db, int bitPos, bool value);
         ushort GetUint16At(Datenbausteine db, int bytePos);
    }
}