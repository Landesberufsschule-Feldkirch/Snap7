namespace PaternosterLager
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly MainWindow _mainWindow;
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            Q1 = 0, // Aufwärts
            Q2      // Abwärts
        }

        private enum BitPosEingang
        {
            B1 = 0, // Initiator Regal 0
            B2,     // Initiator irgendein Regal
            S1,     // Auf
            S2      // Ab
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, _viewModel.Paternosterlager.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, _viewModel.Paternosterlager.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, _viewModel.Paternosterlager.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, _viewModel.Paternosterlager.S2);

            S7.SetUint8At(digInput, 1, (byte)_viewModel.Paternosterlager.Zeichen);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            if (!_mainWindow.DebugWindowAktiv)
            {
                _viewModel.Paternosterlager.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
                _viewModel.Paternosterlager.Q2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q2);
            }

            _viewModel.Paternosterlager.IstPos = S7.GetUint8At(digOutput, 1);
            _viewModel.Paternosterlager.SollPos = S7.GetUint8At(digOutput, 2);
        }

        public DatenRangieren(MainWindow mw, ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _viewModel = vm;
        }
    }
}