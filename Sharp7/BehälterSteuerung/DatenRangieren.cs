using Sharp7;

namespace BehaelterSteuerung
{
    public class DatenRangieren
    {
        private readonly BehaelterSteuerung.ViewModel.ViewModel viewModel;
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

        public DatenRangieren(ViewModel.ViewModel behaelterViewModel)
        {
            viewModel = behaelterViewModel;
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, viewModel.AlleBehaelter.AlleBehaelter[0].SchwimmerschalterOben);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, viewModel.AlleBehaelter.AlleBehaelter[0].SchwimmerschalterUnten);
            S7.SetBitAt(digInput, (int)BitPosEingang.B3, viewModel.AlleBehaelter.AlleBehaelter[1].SchwimmerschalterOben);
            S7.SetBitAt(digInput, (int)BitPosEingang.B4, viewModel.AlleBehaelter.AlleBehaelter[1].SchwimmerschalterUnten);
            S7.SetBitAt(digInput, (int)BitPosEingang.B5, viewModel.AlleBehaelter.AlleBehaelter[2].SchwimmerschalterOben);
            S7.SetBitAt(digInput, (int)BitPosEingang.B6, viewModel.AlleBehaelter.AlleBehaelter[2].SchwimmerschalterUnten);
            S7.SetBitAt(digInput, (int)BitPosEingang.B7, viewModel.AlleBehaelter.AlleBehaelter[3].SchwimmerschalterOben);
            S7.SetBitAt(digInput, (int)BitPosEingang.B8, viewModel.AlleBehaelter.AlleBehaelter[3].SchwimmerschalterUnten);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            viewModel.AlleBehaelter.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);

            viewModel.AlleBehaelter.AlleBehaelter[0].VentilOben = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
            viewModel.AlleBehaelter.AlleBehaelter[1].VentilOben = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q3);
            viewModel.AlleBehaelter.AlleBehaelter[2].VentilOben = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q5);
            viewModel.AlleBehaelter.AlleBehaelter[3].VentilOben = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q7);
        }
    }
}