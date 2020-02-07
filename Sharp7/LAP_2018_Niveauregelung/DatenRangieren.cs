using Sharp7;

namespace LAP_2018_Niveauregelung
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private readonly LAP_2018_Niveauregelung.ViewModel.NiveauRegelungViewModel viewModel;
        private enum BitPosAusgang
        {
            M1 = 0,
            M2,
            P1,
            P2,
            P3
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

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, viewModel.niveauRegelung.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, viewModel.niveauRegelung.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.B3, viewModel.niveauRegelung.B3);
            S7.SetBitAt(digInput, (int)BitPosEingang.F1, viewModel.niveauRegelung.F1);
            S7.SetBitAt(digInput, (int)BitPosEingang.F2, viewModel.niveauRegelung.F2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, viewModel.niveauRegelung.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, viewModel.niveauRegelung.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, viewModel.niveauRegelung.S3);
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            if (!mainWindow.DebugWindowAktiv)
            {
                viewModel.niveauRegelung.M1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.M1);
                viewModel.niveauRegelung.M2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.M2);
                viewModel.niveauRegelung.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
                viewModel.niveauRegelung.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
                viewModel.niveauRegelung.P3 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P3);
            }
        }

        public DatenRangieren(MainWindow mw, LAP_2018_Niveauregelung.ViewModel.NiveauRegelungViewModel vm)
        {
            mainWindow = mw;
            viewModel = vm;
        }
    }
}