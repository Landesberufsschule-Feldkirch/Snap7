using Sharp7;
using Utilities;

namespace LAP_2018_Abfuellanlage
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private readonly ViewModel.AbfuellanlageViewModel viewModel;

        private enum BitPosAusgang
        {
            M1 = 0,
            K1,
            K2,
            P1,
            P2
        }

        private enum BitPosEingang
        {
            B1 = 0,
            F5,
            S1,
            S2,
            S3,
            S4
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, viewModel.abfuellAnlage.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.F5, viewModel.abfuellAnlage.F5);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, viewModel.abfuellAnlage.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, viewModel.abfuellAnlage.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, viewModel.abfuellAnlage.S3);
            S7.SetBitAt(digInput, (int)BitPosEingang.S4, viewModel.abfuellAnlage.S4);

            S7.SetUInt_8_At(anInput, 0, (byte)(viewModel.abfuellAnlage.Pegel * 100.0));
            S7.SetSint_16_At(anInput, 2, S7Analog.S7_Analog_2_Int16(viewModel.abfuellAnlage.Pegel, 1));
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            if (!mainWindow.DebugWindowAktiv)
            {
                viewModel.abfuellAnlage.M1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.M1);
                viewModel.abfuellAnlage.K1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K1);
                viewModel.abfuellAnlage.K2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K2);
                viewModel.abfuellAnlage.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
                viewModel.abfuellAnlage.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
            }
        }

        public DatenRangieren(MainWindow mw, ViewModel.AbfuellanlageViewModel vm)
        {
            mainWindow = mw;
            viewModel = vm;
        }
    }
}