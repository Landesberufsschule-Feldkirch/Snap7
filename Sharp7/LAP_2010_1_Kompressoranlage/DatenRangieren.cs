using Sharp7;

namespace LAP_2010_1_Kompressoranlage
{
    public class DatenRangieren
    {
        private readonly ViewModel.KompressoranlageViewModel kompressoranlageViewModel;

        private enum BitPosAusgang
        {
            H1 = 0, // Störung
            H2,     // Betriebsbereit
            K1,     // Netzschütz
            K2,     // Sternschütz
            K3      // Dreieckschütz
        }

        private enum BitPosEingang
        {
            F5 = 0, // Störung Motorschutzschalter
            S1,     // Taster Aus
            S2,     // Taster Ein
            S7,     // Druckschalter
            S8      // Temperaturfühler Kompressor
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.F5, kompressoranlageViewModel.kompressoranlage.F5);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, kompressoranlageViewModel.kompressoranlage.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, kompressoranlageViewModel.kompressoranlage.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S7, kompressoranlageViewModel.kompressoranlage.S7);
            S7.SetBitAt(digInput, (int)BitPosEingang.S8, kompressoranlageViewModel.kompressoranlage.S8);
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            kompressoranlageViewModel.kompressoranlage.H1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.H1);
            kompressoranlageViewModel.kompressoranlage.H2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.H2);
            kompressoranlageViewModel.kompressoranlage.K1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K1);
            kompressoranlageViewModel.kompressoranlage.K2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K2);
            kompressoranlageViewModel.kompressoranlage.K3 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K3);
        }

        public DatenRangieren(ViewModel.KompressoranlageViewModel vm)
        {
            kompressoranlageViewModel = vm;
        }
    }
}