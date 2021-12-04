using Kommunikation;

namespace Ampel_Verbania;

public class DatenRangieren
{
    private readonly ViewModel.ViewModel _viewModel;
    private IPlc _plc;

    private enum BitPosAusgang
    {
        P11 = 0,
        P12 = 1,
        P13 = 2,
        P21 = 3,
        P22 = 4,
        P23 = 5,
        P31 = 6,
        P32 = 7,
        P33 = 8
    }

    private enum BitPosEingang
    {
        S1 = 0
    }

    public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
    {
        if (eingaengeRangieren)
        {
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Kata.S1);
        }

        _viewModel.Kata.P11 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P11);
        _viewModel.Kata.P12 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P12);
        _viewModel.Kata.P13 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P13);
        _viewModel.Kata.P21 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P21);
        _viewModel.Kata.P22 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P22);
        _viewModel.Kata.P23 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P23);
        _viewModel.Kata.P31 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P31);
        _viewModel.Kata.P32 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P32);
        _viewModel.Kata.P33 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P33);

        _viewModel.Kata.AmpelWert = _plc.GetUIntAt(datenstruktur.AnalogOutput, 0);
    }

    public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    public void ReferenzUebergeben(IPlc plc) => _plc = plc;
}