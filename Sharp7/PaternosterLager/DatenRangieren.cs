using Sharp7;

namespace PaternosterLager
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private readonly ViewModel.ViewModel vewModel;

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

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            /*
            S7.SetBitAt(digInput, (int)BitPosEingang.F5, vewModel.kompressoranlage.F5);
            */
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            /*
            vewModel.kompressoranlage.H1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.H1);
            */
        }

        public DatenRangieren(MainWindow mw, ViewModel.ViewModel vm)
        {
            mainWindow = mw;
            vewModel = vm;
        }
    }
}