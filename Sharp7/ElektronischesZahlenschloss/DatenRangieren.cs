namespace ElektronischesZahlenschloss
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            P1 = 0, // Lampe rot
            P2      // Lampe grün
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetUint8At(digInput, 1, (byte)_viewModel.zahlenschloss.Zeichen);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            _viewModel.zahlenschloss.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
            _viewModel.zahlenschloss.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);

            _viewModel.zahlenschloss.CodeAnzeige = S7.GetUint16At(digOutput, 1);
        }

        public DatenRangieren(ViewModel.ViewModel vm)
        {
            _viewModel = vm;
        }
    }
}