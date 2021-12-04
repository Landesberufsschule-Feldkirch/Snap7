using Kommunikation;

namespace Blinklicht_Fibonacci;

public class DatenRangieren
{
    private readonly ViewModel.ViewModel _viewModel;
    private IPlc _plc;
    private enum BitPosAusgang
    {
        P1 = 0  // Meldeleuchte
    }

    private enum BitPosEingang
    {
        S1 = 0 // Taster Start
    }

    public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
    {
        if (eingaengeRangieren)
        {
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.BlinklichtFibonacci.S1);
        }


        _viewModel.BlinklichtFibonacci.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
    }

    public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    public void ReferenzUebergeben(IPlc plc) => _plc = plc;
}