using Kommunikation;

namespace LAP_2018_3_Hydraulikaggregat
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            K1 = 0, // Ventil "Zylinder ausfahren"
            K2,     // Ventil "Zylinder einfahren"
            P1,     // Meldeleuchte "Betriebsbereit"
            P2,     // Meldeleuchte "Störung Motor
            P3,     // Meldeleuchte "Überdruck"
            P4,     // Meldeleuchte "Druck erreicht"
            P5,     // Meldeleuchte "Ölstand min"
            P6,     // Meldeleuchte "Lüfter Ölkühler"
            P7,     // Meldeleuchte "Ölfilter welchseln"
            Q1,     // Netzschütz
            Q2,     // Sternschütz
            Q3,     // Dreieckschütz
            Q4      // Lüfter (Ölkühler)
        }

        private enum BitPosEingang
        {
            B1 = 0, // Sensor Ölstand
            B2,     // Sensor Druck erreicht
            B3,     // Sensor Überdruck
            B4,     // Temperatur Ölbehälter
            B5,     // Motorschutzschalter
            F1,     // Motorstörung
            S1,     // Taster Start
            S2,     // Taster Stop
            S3,     // Taster Quittieren
            S4      // Taster Zylinder
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Hydraulikaggregat.B1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.Hydraulikaggregat.B2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B3, _viewModel.Hydraulikaggregat.B3);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B4, _viewModel.Hydraulikaggregat.B4);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B5, _viewModel.Hydraulikaggregat.B5);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _viewModel.Hydraulikaggregat.F1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Hydraulikaggregat.S1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Hydraulikaggregat.S2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Hydraulikaggregat.S3);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S4, _viewModel.Hydraulikaggregat.S4);
            }

            _viewModel.Hydraulikaggregat.K1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K1);
            _viewModel.Hydraulikaggregat.K2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K2);
            _viewModel.Hydraulikaggregat.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Hydraulikaggregat.P2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.Hydraulikaggregat.P3 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3);
            _viewModel.Hydraulikaggregat.P4 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P4);
            _viewModel.Hydraulikaggregat.P5 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P5);
            _viewModel.Hydraulikaggregat.P6 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P6);
            _viewModel.Hydraulikaggregat.P7 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P7);
            _viewModel.Hydraulikaggregat.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.Hydraulikaggregat.Q2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
            _viewModel.Hydraulikaggregat.Q3 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q3);
            _viewModel.Hydraulikaggregat.Q4 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q4);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}