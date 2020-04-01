using Sharp7;

namespace LAP_2018_4_Niveauregelung
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private readonly ViewModel.ViewModel viewModel;
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
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, viewModel.niveauRegelung.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, viewModel.niveauRegelung.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.B3, viewModel.niveauRegelung.B3);
            S7.SetBitAt(digInput, (int)BitPosEingang.F1, viewModel.niveauRegelung.F1);
            S7.SetBitAt(digInput, (int)BitPosEingang.F2, viewModel.niveauRegelung.F2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, viewModel.niveauRegelung.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, viewModel.niveauRegelung.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, viewModel.niveauRegelung.S3);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            if (!mainWindow.DebugWindowAktiv)
            {
                viewModel.niveauRegelung.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
                viewModel.niveauRegelung.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
                viewModel.niveauRegelung.P3 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P3);
                viewModel.niveauRegelung.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
                viewModel.niveauRegelung.Q2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q2);
            }
        }

        public DatenRangieren(MainWindow mw, LAP_2018_4_Niveauregelung.ViewModel.ViewModel vm)
        {
            mainWindow = mw;
            viewModel = vm;
        }
    }
}