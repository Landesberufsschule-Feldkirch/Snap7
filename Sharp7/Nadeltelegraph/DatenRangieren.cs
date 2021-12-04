using Kommunikation;

namespace Nadeltelegraph;

public class DatenRangieren
{
    private readonly ViewModel.ViewModel _viewModel;
    private IPlc _plc;

    public enum BitPosAusgang
    {
        P1R = 0,
        P1L,
        P2R,
        P2L,
        P3R,
        P3L,
        P4R,
        P4L,
        P5R,
        P5L
    }

    public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
    {
        if (eingaengeRangieren)
        {
            _plc.SetUsIntAt(datenstruktur.DigInput, 0, (byte)_viewModel.Nadeltelegraph.Zeichen);
        }


        _viewModel.Nadeltelegraph.P1R = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1R);
        _viewModel.Nadeltelegraph.P1L = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1L);
        _viewModel.Nadeltelegraph.P2R = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2R);
        _viewModel.Nadeltelegraph.P2L = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2L);
        _viewModel.Nadeltelegraph.P3R = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3R);
        _viewModel.Nadeltelegraph.P3L = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3L);
        _viewModel.Nadeltelegraph.P4R = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P4R);
        _viewModel.Nadeltelegraph.P4L = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P4L);
        _viewModel.Nadeltelegraph.P5R = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P5R);
        _viewModel.Nadeltelegraph.P5L = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P5L);
    }

    public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    public void ReferenzUebergeben(IPlc plc) => _plc = plc;
}