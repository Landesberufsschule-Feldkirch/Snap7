using Kommunikation;

namespace LAP_2010_5_Pumpensteuerung;

public class DatenRangieren
{
    private readonly LAP_2010_5_Pumpensteuerung.ViewModel.ViewModel _viewModel;
    private IPlc _plc;

    private enum BitPosAusgang
    {
        P1 = 0,     // "Pumpe Ein"
        P2 = 1,     // "Störung"
        Q1 = 2       // Motor Pumpe

    }

    private enum BitPosEingang
    {
        B1 = 0,     // Schwimmerschalter oben
        B2 = 1,     // Schwimmerschalter unten
        F1 = 2,     // Thermorelais
        S1 = 3,     // Wahlschalter Hand
        S2 = 4,     // Wahlschalter Automatik
        S3 = 5      // Störung quittieren

    }

    public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
    {
        if (eingaengeRangieren)
        {
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Pumpensteuerung.B1);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.Pumpensteuerung.B2);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _viewModel.Pumpensteuerung.F1);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Pumpensteuerung.S1);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Pumpensteuerung.S2);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Pumpensteuerung.S3);
        }

        _viewModel.Pumpensteuerung.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
        _viewModel.Pumpensteuerung.P2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
        _viewModel.Pumpensteuerung.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
    }

    public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    public void ReferenzUebergeben(IPlc plc) => _plc = plc;
}