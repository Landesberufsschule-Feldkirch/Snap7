using Kommunikation;

namespace PaternosterLager
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            Q1 = 0,     // Aufwärts
            Q2 = 1      // Abwärts
        }

        private enum BitPosEingang
        {
            B1 = 0,     // Initiator Regal 0
            B2 = 1,     // Initiator irgendein Regal
            S1 = 2,     // Auf
            S2 = 3      // Ab
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Paternosterlager.B1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.Paternosterlager.B2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Paternosterlager.S1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Paternosterlager.S2);

                _plc.SetUsIntAt(datenstruktur.DigInput, 1, (byte)_viewModel.Paternosterlager.Zeichen);
            }


            _viewModel.Paternosterlager.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.Paternosterlager.Q2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);

            _viewModel.Paternosterlager.IstPos = _plc.GetUsIntAt(datenstruktur.DigOutput, 1);
            _viewModel.Paternosterlager.SollPos = _plc.GetUsIntAt(datenstruktur.DigOutput, 2);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}