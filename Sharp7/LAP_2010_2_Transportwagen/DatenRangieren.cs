using Kommunikation;

namespace LAP_2010_2_Transportwagen;

public class DatenRangieren
{
    private readonly ViewModel.ViewModel _transportwagenViewModel;
    private IPlc _plc;

    private enum BitPosAusgang
    {
        P1 = 0,     // Störung
        Q1 = 1,     // Motor LL
        Q2 = 2       // Motor RL

    }

    private enum BitPosEingang
    {
        B1 = 0,     // Endschalter Links
        B2 = 1,     // Endschalter Rechts
        F1 = 2,     // Thermorelais
        S1 = 3,     // Taster "Start"
        S2 = 4,     // NotHalt
        S3 = 5       // Taster Reset

    }

    public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
    {
        if (eingaengeRangieren)
        {
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _transportwagenViewModel.Transportwagen.B1);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _transportwagenViewModel.Transportwagen.B2);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _transportwagenViewModel.Transportwagen.F1);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _transportwagenViewModel.Transportwagen.S1);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _transportwagenViewModel.Transportwagen.S2);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _transportwagenViewModel.Transportwagen.S3);
        }

        _transportwagenViewModel.Transportwagen.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
        _transportwagenViewModel.Transportwagen.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
        _transportwagenViewModel.Transportwagen.Q2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
    }

    public DatenRangieren(ViewModel.ViewModel vm) => _transportwagenViewModel = vm;
    public void ReferenzUebergeben(IPlc plc) => _plc = plc;
}