using DigitalerZwillingMitAutoTests;
using Kommunikation;

namespace Kata.Model;

internal class DatenRangieren
{
    private enum BitPosAusgang
    {
        P1 = 0,
        P2 = 1,
        P3 = 2,
        P4 = 3,
        P5 = 4,
        P6 = 5,
        P7 = 6,
        P8 = 7
    }
    private enum BitPosEingang
    {
        S1 = 0,
        S2 = 1,
        S3 = 2,
        S4 = 3,
        S5 = 4,
        S6 = 5,
        S7 = 6,
        S8 = 7
    }

    private readonly Kata _kata;
    private readonly DtAutoTests _dtAutoTests;
       
    public DatenRangieren(Kata kata, DtAutoTests dtAutoTests)
    {
        _kata = kata;
        _dtAutoTests = dtAutoTests;
    }

    public void Rangieren()
    {
        if (_dtAutoTests is not { Datenstruktur: { } }) return;

        if (_dtAutoTests.Datenstruktur.BetriebsartProjekt != BetriebsartProjekt.AutomatischerSoftwareTest)
        {
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S1, _kata.S1);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S2, _kata.S2);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S3, _kata.S3);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S4, _kata.S4);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S5, _kata.S5);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S6, _kata.S6);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S7, _kata.S7);
            _dtAutoTests.PlcDaemon.Plc.SetBitAt(_dtAutoTests.Datenstruktur.DigInput, (int)BitPosEingang.S8, _kata.S8);

        }

        _kata.P1 = _dtAutoTests.PlcDaemon.Plc.GetBitAt(_dtAutoTests.Datenstruktur.DigOutput, (int)BitPosAusgang.P1);
        _kata.P2 = _dtAutoTests.PlcDaemon.Plc.GetBitAt(_dtAutoTests.Datenstruktur.DigOutput, (int)BitPosAusgang.P2);
        _kata.P3 = _dtAutoTests.PlcDaemon.Plc.GetBitAt(_dtAutoTests.Datenstruktur.DigOutput, (int)BitPosAusgang.P3);
        _kata.P4 = _dtAutoTests.PlcDaemon.Plc.GetBitAt(_dtAutoTests.Datenstruktur.DigOutput, (int)BitPosAusgang.P4);
        _kata.P5 = _dtAutoTests.PlcDaemon.Plc.GetBitAt(_dtAutoTests.Datenstruktur.DigOutput, (int)BitPosAusgang.P5);
        _kata.P6 = _dtAutoTests.PlcDaemon.Plc.GetBitAt(_dtAutoTests.Datenstruktur.DigOutput, (int)BitPosAusgang.P6);
        _kata.P7 = _dtAutoTests.PlcDaemon.Plc.GetBitAt(_dtAutoTests.Datenstruktur.DigOutput, (int)BitPosAusgang.P7);
        _kata.P8 = _dtAutoTests.PlcDaemon.Plc.GetBitAt(_dtAutoTests.Datenstruktur.DigOutput, (int)BitPosAusgang.P8);
    }
}