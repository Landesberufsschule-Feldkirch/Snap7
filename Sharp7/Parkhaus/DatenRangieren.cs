using Sharp7;

namespace Parkhaus
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;


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

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps1, _viewModel.Parkhaus.ParkhausSpalte1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps2, _viewModel.Parkhaus.ParkhausSpalte2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps3, _viewModel.Parkhaus.ParkhausSpalte3);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps4, _viewModel.Parkhaus.ParkhausSpalte4);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps5, _viewModel.Parkhaus.ParkhausSpalte5);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps6, _viewModel.Parkhaus.ParkhausSpalte6);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps7, _viewModel.Parkhaus.ParkhausSpalte7);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.Ps8, _viewModel.Parkhaus.ParkhausSpalte8);
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _viewModel.Parkhaus.ParkhausReihe1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Pr1);
            _viewModel.Parkhaus.ParkhausReihe2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Pr2);
            _viewModel.Parkhaus.ParkhausReihe3 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Pr3);
            _viewModel.Parkhaus.ParkhausReihe4 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Pr4);

            _viewModel.Parkhaus.FreieParkplaetze = S7.GetUint16At(datenstruktur.AnalogOutput, 0);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    }
}