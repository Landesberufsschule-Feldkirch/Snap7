namespace Nadeltelegraph
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

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

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetUint8At(datenstruktur.DigInput, 0, (byte)_viewModel.Nadeltelegraph.Zeichen);
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _viewModel.Nadeltelegraph.P1R = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1R);
            _viewModel.Nadeltelegraph.P1L = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1L);
            _viewModel.Nadeltelegraph.P2R = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2R);
            _viewModel.Nadeltelegraph.P2L = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2L);
            _viewModel.Nadeltelegraph.P3R = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3R);
            _viewModel.Nadeltelegraph.P3L = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3L);
            _viewModel.Nadeltelegraph.P4R = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P4R);
            _viewModel.Nadeltelegraph.P4L = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P4L);
            _viewModel.Nadeltelegraph.P5R = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P5R);
            _viewModel.Nadeltelegraph.P5L = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P5L);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    }
}