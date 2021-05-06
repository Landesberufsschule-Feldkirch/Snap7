using Kommunikation;

namespace LAP_2018_3_Hydraulikaggregat
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            P1 = 0, // Meldeleuchte Störung Motor
            P2,     // Meldeleuchte Überdruck
            P3,     // Meldeleuchte Druck erreicht
            P4,     // Meldeleuchte Ölstand min.
            Q1,     // Netzschütz
            Q2,     // Sternschütz
            Q3      // Dreieckschütz
        }

        private enum BitPosEingang
        {
            B1 = 0, // Sensor Ölstand
            B2,     // Sensor Druck erreicht
            B3,     // Sensor Überdruck
            F1,     // Motorstörung
            S1,     // Taster Start
            S2,     // Taster Stop
            S3      // Taster Quittieren
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Hydraulikaggregat.B1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.Hydraulikaggregat.B2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B3, _viewModel.Hydraulikaggregat.B3);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _viewModel.Hydraulikaggregat.F1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Hydraulikaggregat.S1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Hydraulikaggregat.S2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Hydraulikaggregat.S3);
            }


            _viewModel.Hydraulikaggregat.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Hydraulikaggregat.P2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.Hydraulikaggregat.P3 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3);
            _viewModel.Hydraulikaggregat.P4 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P4);
            _viewModel.Hydraulikaggregat.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.Hydraulikaggregat.Q2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
            _viewModel.Hydraulikaggregat.Q3 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q3);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}