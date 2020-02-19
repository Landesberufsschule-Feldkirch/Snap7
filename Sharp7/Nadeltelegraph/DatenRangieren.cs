namespace Nadeltelegraph
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private readonly ViewModel.NadeltelegraphViewModel viewModel;



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

        private enum BitPosEingang
        {
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetUint8At(digInput, 0, (byte)viewModel.nadeltelegraph.Zeichen);
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            viewModel.nadeltelegraph.P1R = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1R);
            viewModel.nadeltelegraph.P1L = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1L);
            viewModel.nadeltelegraph.P2R = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2R);
            viewModel.nadeltelegraph.P2L = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2L);
            viewModel.nadeltelegraph.P3R = S7.GetBitAt(digOutput, (int)BitPosAusgang.P3R);
            viewModel.nadeltelegraph.P3L = S7.GetBitAt(digOutput, (int)BitPosAusgang.P3L);
            viewModel.nadeltelegraph.P4R = S7.GetBitAt(digOutput, (int)BitPosAusgang.P4R);
            viewModel.nadeltelegraph.P4L = S7.GetBitAt(digOutput, (int)BitPosAusgang.P4L);
            viewModel.nadeltelegraph.P5R = S7.GetBitAt(digOutput, (int)BitPosAusgang.P5R);
            viewModel.nadeltelegraph.P5L = S7.GetBitAt(digOutput, (int)BitPosAusgang.P5L);
        }

        public DatenRangieren(MainWindow mw, ViewModel.NadeltelegraphViewModel vm)
        {
            mainWindow = mw;
            viewModel = vm;
        }
    }
}