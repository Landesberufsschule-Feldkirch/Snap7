using Kommunikation;

namespace LAP_2010_1_Kompressoranlage
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

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

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Kompressoranlage.B1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.Kompressoranlage.B2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _viewModel.Kompressoranlage.F1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Kompressoranlage.S1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Kompressoranlage.S2);
            }


            _viewModel.Kompressoranlage.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Kompressoranlage.P2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.Kompressoranlage.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.Kompressoranlage.Q2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
            _viewModel.Kompressoranlage.Q3 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q3);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}