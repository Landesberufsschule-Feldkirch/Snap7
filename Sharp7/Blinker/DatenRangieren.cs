using Sharp7;

namespace Blinker
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            P1 = 0
        }

        private enum BitPosEingang
        {
            S1= 0,
            S2,
            S3,
            S4,
            S5
        }

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Blinker.S1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Blinker.S2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Blinker.S3);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S4, _viewModel.Blinker.S4);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S5, _viewModel.Blinker.S5);
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _viewModel.Blinker.P1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
        }

        public DatenRangieren(ViewModel.ViewModel vm)
        {
            _viewModel = vm;
        }
    }
}