using Kommunikation;

namespace LAP_2018_4_Niveauregelung
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            P1 = 0,
            P2 = 1,
            P3 = 2,
            Q1 = 3,
            Q2 = 4
        }

        private enum BitPosEingang
        {
            B1 = 0,
            B2 = 1,
            B3 = 2,
            F1 = 3,
            F2 = 4,
            S1 = 5,
            S2 = 6,
            S3 = 7
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.NiveauRegelung.B1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.NiveauRegelung.B2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B3, _viewModel.NiveauRegelung.B3);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _viewModel.NiveauRegelung.F1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F2, _viewModel.NiveauRegelung.F2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.NiveauRegelung.S1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.NiveauRegelung.S2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.NiveauRegelung.S3);
            }


            _viewModel.NiveauRegelung.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.NiveauRegelung.P2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.NiveauRegelung.P3 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3);
            _viewModel.NiveauRegelung.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.NiveauRegelung.Q2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}