using DigitalerZwillingMitAutoTests;
using Kommunikation;

namespace LaborLinearachse.Model;

public class DatenRangieren
{
    private readonly Linearachse _linearachse;
    private readonly DtAutoTests _dtAutoTests;

    private enum BitPosAusgang
    {
        Q1 = 0,     // 0.0  Linearachse Rechtslauf
        Q2 = 1,     // 0.1  Linearachse Linkslauf
        P1 = 2,     // 0.2  Meldeleuchte im Taster S1/S2 (weiß) 
        P2 = 3,     // 0.3  Meldeleuchte weiß
        P3 = 4,     // 0.4  Meldeleuchte rot
        P4 = 5      // 0.5  Meldeleuchte grün
    }

    private enum BitPosEingang
    {
        B1 = 0,     // 0.0  Linearachse Endlage links → Öffner
        B2 = 1,     // 0.1  Linearachse Endlage rechts → Öffner
        S2 = 2,     // 0.2  Taster ( ⓪ ) → Öffner 
        S1 = 3,     // 0.3  Taster ( ① ) → Schliesser 
        S4 = 4,     // 0.4  Taster ( Ⅱ ) → Schliesser 
        S3 = 5,     // 0.5  Taster ( Ⅰ ) → Schliesser
        S6 = 6,     // 0.6  Taster ( ↓ ) → Schliesser
        S5 = 7,     // 0.7  Taster ( ↑ ) → Schliesser 
        S8 = 8,     // 1.0  Taster ( － ) → Schliesser 
        S7 = 9,     // 1.1  Taster ( + ) → Schliesser 
        S9 = 10,    // 1.2  Taster ( STOP ) → Öffner 
        S10 = 11,   // 1.3  Not-Halt → Öffner
        S11 = 12    // 1.4  Not-Halt → Schliesser 
    }

    public DatenRangieren(Linearachse linearachse, DtAutoTests dtAutoTests)
    {
        _linearachse = linearachse;
        _dtAutoTests = dtAutoTests;
    }

    public void Rangieren()
    {
        if (_dtAutoTests is not { Datenstruktur: { } }) return;

        if (_dtAutoTests.Datenstruktur.BetriebsartProjekt != BetriebsartProjekt.AutomatischerSoftwareTest)
        {
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.B1, _linearachse.B1);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.B2, _linearachse.B2);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S1, _linearachse.S1);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S2, _linearachse.S2);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S3, _linearachse.S3);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S4, _linearachse.S4);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S5, _linearachse.S5);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S6, _linearachse.S6);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S7, _linearachse.S7);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S8, _linearachse.S8);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S9, _linearachse.S9);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S10, _linearachse.S10);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S11, _linearachse.S11);
        }

        _linearachse.P1 = _dtAutoTests.PlcDaemon.Plc.GetBitAt(_dtAutoTests.Datenstruktur.DigOutput, (int)BitPosAusgang.P1);
        _linearachse.P2 = _dtAutoTests.PlcDaemon.Plc.GetBitAt(_dtAutoTests.Datenstruktur.DigOutput, (int)BitPosAusgang.P2);
        _linearachse.P3 = _dtAutoTests.PlcDaemon.Plc.GetBitAt(_dtAutoTests.Datenstruktur.DigOutput, (int)BitPosAusgang.P3);
        _linearachse.P4 = _dtAutoTests.PlcDaemon.Plc.GetBitAt(_dtAutoTests.Datenstruktur.DigOutput, (int)BitPosAusgang.P4);
        _linearachse.Q1 = _dtAutoTests.PlcDaemon.Plc.GetBitAt(_dtAutoTests.Datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
        _linearachse.Q2 = _dtAutoTests.PlcDaemon.Plc.GetBitAt(_dtAutoTests.Datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
    }
}