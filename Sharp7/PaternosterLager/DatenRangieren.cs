namespace PaternosterLager
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private readonly ViewModel.ViewModel viewModel;

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
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, viewModel.paternosterlager.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, viewModel.paternosterlager.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, viewModel.paternosterlager.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, viewModel.paternosterlager.S2);

            S7.SetUint8At(digInput, 1, (byte)viewModel.paternosterlager.Zeichen);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            if (!mainWindow.DebugWindowAktiv)
            {
                viewModel.paternosterlager.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
                viewModel.paternosterlager.Q2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q2);
            }

            viewModel.paternosterlager.IstPos = S7.GetUint8At(digOutput, 1);
            viewModel.paternosterlager.SollPos = S7.GetUint8At(digOutput, 2);
        }

        public DatenRangieren(MainWindow mw, ViewModel.ViewModel vm)
        {
            mainWindow = mw;
            viewModel = vm;
        }
    }
}