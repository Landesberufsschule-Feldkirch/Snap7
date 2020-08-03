namespace Nadeltelegraph
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
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

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetUint8At(digInput, 0, (byte)_viewModel.nadeltelegraph.Zeichen);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            _viewModel.nadeltelegraph.P1R = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1R);
            _viewModel.nadeltelegraph.P1L = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1L);
            _viewModel.nadeltelegraph.P2R = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2R);
            _viewModel.nadeltelegraph.P2L = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2L);
            _viewModel.nadeltelegraph.P3R = S7.GetBitAt(digOutput, (int)BitPosAusgang.P3R);
            _viewModel.nadeltelegraph.P3L = S7.GetBitAt(digOutput, (int)BitPosAusgang.P3L);
            _viewModel.nadeltelegraph.P4R = S7.GetBitAt(digOutput, (int)BitPosAusgang.P4R);
            _viewModel.nadeltelegraph.P4L = S7.GetBitAt(digOutput, (int)BitPosAusgang.P4L);
            _viewModel.nadeltelegraph.P5R = S7.GetBitAt(digOutput, (int)BitPosAusgang.P5R);
            _viewModel.nadeltelegraph.P5L = S7.GetBitAt(digOutput, (int)BitPosAusgang.P5L);
        }

        public DatenRangieren(ViewModel.ViewModel vm)
        {
            _viewModel = vm;
        }
    }
}