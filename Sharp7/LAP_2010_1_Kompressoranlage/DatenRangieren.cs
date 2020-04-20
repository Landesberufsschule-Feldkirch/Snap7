using Sharp7;

namespace LAP_2010_1_Kompressoranlage
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel viewModel;

        private enum BitPosAusgang
        {
            P1 = 0, // Störung
            P2,     // Betriebsbereit
            Q1,     // Netzschütz
            Q2,     // Sternschütz
            Q3      // Dreieckschütz
        }

        private enum BitPosEingang
        {
            B1 = 0, // Druckschalter
            B2,     // Temperaturfühler Kompressor
            F1,     // Störung Motorschutzschalter
            S1,     // Taster Aus
            S2      // Taster Ein
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, viewModel.kompressoranlage.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, viewModel.kompressoranlage.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.F1, viewModel.kompressoranlage.F1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, viewModel.kompressoranlage.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, viewModel.kompressoranlage.S2);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            viewModel.kompressoranlage.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
            viewModel.kompressoranlage.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
            viewModel.kompressoranlage.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
            viewModel.kompressoranlage.Q2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q2);
            viewModel.kompressoranlage.Q3 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q3);
        }

        public DatenRangieren(ViewModel.ViewModel vm)
        {
            viewModel = vm;
        }
    }
}