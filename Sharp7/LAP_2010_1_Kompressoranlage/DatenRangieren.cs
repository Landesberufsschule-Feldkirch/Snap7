using Sharp7;

namespace LAP_2010_1_Kompressoranlage
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

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

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Kompressoranlage.B1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.Kompressoranlage.B2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _viewModel.Kompressoranlage.F1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Kompressoranlage.S1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Kompressoranlage.S2);
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _viewModel.Kompressoranlage.P1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Kompressoranlage.P2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.Kompressoranlage.Q1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.Kompressoranlage.Q2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
            _viewModel.Kompressoranlage.Q3 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q3);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    }
}