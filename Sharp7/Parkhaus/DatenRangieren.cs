using Kommunikation;
using Sharp7;

namespace Parkhaus
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;


        public enum BitPosAusgang
        {
            Pr1 = 0,    // Reihe 1
            Pr2,
            Pr3,
            Pr4
        }

        private enum BitPosEingang
        {
            Ps1 = 0, // Spalte 1
            Ps2,
            Ps3,
            Ps4,
            Ps5,
            Ps6,
            Ps7,
            Ps8
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps1, _viewModel.Parkhaus.ParkhausSpalte1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps2, _viewModel.Parkhaus.ParkhausSpalte2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps3, _viewModel.Parkhaus.ParkhausSpalte3);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps4, _viewModel.Parkhaus.ParkhausSpalte4);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps5, _viewModel.Parkhaus.ParkhausSpalte5);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps6, _viewModel.Parkhaus.ParkhausSpalte6);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps7, _viewModel.Parkhaus.ParkhausSpalte7);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps8, _viewModel.Parkhaus.ParkhausSpalte8);
            }


            _viewModel.Parkhaus.ParkhausReihe1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Pr1);
            _viewModel.Parkhaus.ParkhausReihe2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Pr2);
            _viewModel.Parkhaus.ParkhausReihe3 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Pr3);
            _viewModel.Parkhaus.ParkhausReihe4 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Pr4);

            _viewModel.Parkhaus.FreieParkplaetze = S7.GetUIntAt(datenstruktur.AnalogOutput, 0);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}