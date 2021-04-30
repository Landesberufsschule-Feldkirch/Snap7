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

        void SetManualModeReferenz(Datenstruktur manualModeDatenstruktur);
    }
}