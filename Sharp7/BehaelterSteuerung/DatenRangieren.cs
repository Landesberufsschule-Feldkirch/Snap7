using Kommunikation;

namespace BehaelterSteuerung;

public class DatenRangieren
{
    private readonly BehaelterSteuerung.ViewModel.ViewModel _viewModel;
    private IPlc _plc;

    private enum BitPosEingang
    {
        B1 = 0,
        B2 = 1,
        B3 = 2,
        B4 = 3,
        B5 = 4,
        B6 = 5,
        B7 = 6,
        B8 = 7
    }

    private enum BitPosAusgang
    {
        Q1 = 0,
        Q3 = 1,
        Q5 = 2,
        Q7 = 3,
        P1 = 4
    }

    public DatenRangieren(ViewModel.ViewModel behaelterViewModel) => _viewModel = behaelterViewModel;

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