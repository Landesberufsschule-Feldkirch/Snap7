using Kommunikation;

namespace BehaelterSteuerung
{
    public class DatenRangieren
    {
        private readonly BehaelterSteuerung.ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BitPosEingang
        {
            B1 = 0,
            B2,
            B3,
            B4,
            B5,
            B6,
            B7,
            B8
        }

        private enum BitPosAusgang
        {
            Q1 = 0,
            Q3,
            Q5,
            Q7,
            P1
        }

        public DatenRangieren(BehaelterSteuerung.ViewModel.ViewModel behaelterViewModel) => _viewModel = behaelterViewModel;

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.AlleBehaelter.AlleMeineBehaelter[0].SchwimmerschalterOben);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.AlleBehaelter.AlleMeineBehaelter[0].SchwimmerschalterUnten);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B3, _viewModel.AlleBehaelter.AlleMeineBehaelter[1].SchwimmerschalterOben);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B4, _viewModel.AlleBehaelter.AlleMeineBehaelter[1].SchwimmerschalterUnten);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B5, _viewModel.AlleBehaelter.AlleMeineBehaelter[2].SchwimmerschalterOben);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B6, _viewModel.AlleBehaelter.AlleMeineBehaelter[2].SchwimmerschalterUnten);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B7, _viewModel.AlleBehaelter.AlleMeineBehaelter[3].SchwimmerschalterOben);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B8, _viewModel.AlleBehaelter.AlleMeineBehaelter[3].SchwimmerschalterUnten);
            }

            _viewModel.AlleBehaelter.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);

            _viewModel.AlleBehaelter.AlleMeineBehaelter[0].VentilOben = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.AlleBehaelter.AlleMeineBehaelter[1].VentilOben = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q3);
            _viewModel.AlleBehaelter.AlleMeineBehaelter[2].VentilOben = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q5);
            _viewModel.AlleBehaelter.AlleMeineBehaelter[3].VentilOben = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q7);
        }

        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}