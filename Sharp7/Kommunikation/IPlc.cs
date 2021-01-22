﻿namespace Kommunikation
{
    public interface IPlc
    {
        S71200.BetriebsartProjekt IBbetriebsartProjekt { get; set; }
        string GetSpsStatus();
        bool GetSpsError();
        string GetVersion();
        string GetPlcModus();
        void SetPlcModus(string modus);
        void SetTaskRunning(bool active);
        void SetZyklusZeitKommunikation(int zeit);

        // ReSharper disable once UnusedMember.Global
        void SetBitAt(Datenbausteine db, int bitPos, bool value);

        // ReSharper disable once UnusedMember.Global
        byte GetUint8At(Datenbausteine db, int bytePos);
        // ReSharper disable once UnusedMember.Global
        ushort GetUint16At(Datenbausteine db, int bytePos);
        S71200.BetriebsartProjekt GetBetriebsartProjekt();
        void SetBetriebsartProjekt(S71200.BetriebsartProjekt betriebsartProjekt);
        void SetManualModeReferenz(Datenstruktur manualModeDatenstruktur);
    }
}