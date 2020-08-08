using Sharp7;

namespace BehälterSteuerung
{
    public class DatenRangieren
    {
        private readonly BehälterSteuerung.ViewModel.ViewModel _viewModel;

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

        public DatenRangieren(BehälterSteuerung.ViewModel.ViewModel behaelterViewModel)
        {
            _viewModel = behaelterViewModel;
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, _viewModel.AlleBehaelter.AlleBehaelter[0].SchwimmerschalterOben);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, _viewModel.AlleBehaelter.AlleBehaelter[0].SchwimmerschalterUnten);
            S7.SetBitAt(digInput, (int)BitPosEingang.B3, _viewModel.AlleBehaelter.AlleBehaelter[1].SchwimmerschalterOben);
            S7.SetBitAt(digInput, (int)BitPosEingang.B4, _viewModel.AlleBehaelter.AlleBehaelter[1].SchwimmerschalterUnten);
            S7.SetBitAt(digInput, (int)BitPosEingang.B5, _viewModel.AlleBehaelter.AlleBehaelter[2].SchwimmerschalterOben);
            S7.SetBitAt(digInput, (int)BitPosEingang.B6, _viewModel.AlleBehaelter.AlleBehaelter[2].SchwimmerschalterUnten);
            S7.SetBitAt(digInput, (int)BitPosEingang.B7, _viewModel.AlleBehaelter.AlleBehaelter[3].SchwimmerschalterOben);
            S7.SetBitAt(digInput, (int)BitPosEingang.B8, _viewModel.AlleBehaelter.AlleBehaelter[3].SchwimmerschalterUnten);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            _viewModel.AlleBehaelter.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);

            _viewModel.AlleBehaelter.AlleBehaelter[0].VentilOben = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
            _viewModel.AlleBehaelter.AlleBehaelter[1].VentilOben = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q3);
            _viewModel.AlleBehaelter.AlleBehaelter[2].VentilOben = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q5);
            _viewModel.AlleBehaelter.AlleBehaelter[3].VentilOben = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q7);
        }
    }
}