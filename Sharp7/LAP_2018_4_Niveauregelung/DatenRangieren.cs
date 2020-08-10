using Sharp7;

namespace LAP_2018_4_Niveauregelung
{
    public class DatenRangieren
    {
        private readonly MainWindow _mainWindow;
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            P1 = 0,
            P2,
            P3,
            Q1,
            Q2
        }

        private enum BitPosEingang
        {
            B1 = 0,
            B2,
            B3,
            F1,
            F2,
            S1,
            S2,
            S3
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, _viewModel.NiveauRegelung.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, _viewModel.NiveauRegelung.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.B3, _viewModel.NiveauRegelung.B3);
            S7.SetBitAt(digInput, (int)BitPosEingang.F1, _viewModel.NiveauRegelung.F1);
            S7.SetBitAt(digInput, (int)BitPosEingang.F2, _viewModel.NiveauRegelung.F2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, _viewModel.NiveauRegelung.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, _viewModel.NiveauRegelung.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, _viewModel.NiveauRegelung.S3);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            if (!_mainWindow.DebugWindowAktiv)
            {
                _viewModel.NiveauRegelung.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
                _viewModel.NiveauRegelung.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
                _viewModel.NiveauRegelung.P3 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P3);
                _viewModel.NiveauRegelung.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
                _viewModel.NiveauRegelung.Q2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q2);
            }
        }

        public DatenRangieren(MainWindow mw, LAP_2018_4_Niveauregelung.ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _viewModel = vm;
        }
    }
}