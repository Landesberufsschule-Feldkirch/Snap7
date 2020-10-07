using Sharp7;

namespace Tiefgarage
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosEingang
        {
            B1 = 0,
            B2
        }

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.AlleFahrzeugePersonen.B1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.AlleFahrzeugePersonen.B2);
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _viewModel.AlleFahrzeugePersonen.AnzahlAutos = S7.GetUint8At(datenstruktur.DigOutput, 0);
            _viewModel.AlleFahrzeugePersonen.AnzahlPersonen = S7.GetUint8At(datenstruktur.DigOutput, 1);
        }

        public DatenRangieren(Tiefgarage.ViewModel.ViewModel vm) => _viewModel = vm;
    }
}