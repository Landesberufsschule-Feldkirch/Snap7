using Sharp7;
using Utilities;

namespace LAP_2018_2_Abfuellanlage
{
    public class DatenRangieren
    {
        private readonly MainWindow _mainWindow;
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            K1 = 0,
            K2,
            P1,
            P2,
            Q1
        }

        private enum BitPosEingang
        {
            B1 = 0,
            F1,
            S1,
            S2,
            S3,
            S4
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, _viewModel.abfuellAnlage.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.F1, _viewModel.abfuellAnlage.F1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, _viewModel.abfuellAnlage.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, _viewModel.abfuellAnlage.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, _viewModel.abfuellAnlage.S3);
            S7.SetBitAt(digInput, (int)BitPosEingang.S4, _viewModel.abfuellAnlage.S4);

            S7.SetUInt_8_At(anInput, 0, (byte)(_viewModel.abfuellAnlage.Pegel * 100.0));
            S7.SetSint_16_At(anInput, 2, S7Analog.S7_Analog_2_Int16(_viewModel.abfuellAnlage.Pegel, 1));
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            if (!_mainWindow.DebugWindowAktiv)
            {
                _viewModel.abfuellAnlage.K1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K1);
                _viewModel.abfuellAnlage.K2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K2);
                _viewModel.abfuellAnlage.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
                _viewModel.abfuellAnlage.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
                _viewModel.abfuellAnlage.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
            }
        }

        public DatenRangieren(MainWindow mw, ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _viewModel = vm;
        }
    }
}