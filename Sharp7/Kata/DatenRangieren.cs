using Kommunikation;

namespace Kata
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

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

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Kata.S1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Kata.S2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Kata.S3);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S4, _viewModel.Kata.S4);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S5, _viewModel.Kata.S5);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S6, _viewModel.Kata.S6);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S7, _viewModel.Kata.S7);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S8, _viewModel.Kata.S8);
            }

            _viewModel.Kata.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Kata.P2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.Kata.P3 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3);
            _viewModel.Kata.P4 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P4);
            _viewModel.Kata.P5 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P5);
            _viewModel.Kata.P6 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P6);
            _viewModel.Kata.P7 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P7);
            _viewModel.Kata.P8 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P8);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}