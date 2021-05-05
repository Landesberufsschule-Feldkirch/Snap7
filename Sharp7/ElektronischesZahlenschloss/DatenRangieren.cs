using Kommunikation;

namespace ElektronischesZahlenschloss
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            P1 = 0, // Lampe rot
            P2      // Lampe grün
        }

        public void Rangieren(Kommunikation.Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetUsIntAt(datenstruktur.AnalogInput, 0, (byte)_viewModel.Zahlenschloss.Zeichen);
            }

            _viewModel.Zahlenschloss.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Zahlenschloss.P2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);

            _viewModel.Zahlenschloss.CodeAnzeige = S7.GetUIntAt(datenstruktur.AnalogOutput, 0);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}