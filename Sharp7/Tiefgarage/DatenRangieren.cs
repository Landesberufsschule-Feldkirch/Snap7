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

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, _viewModel.AlleFahrzeugePersonen.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, _viewModel.AlleFahrzeugePersonen.B2);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            _viewModel.AlleFahrzeugePersonen.AnzahlAutos = S7.GetUint8At(digOutput, 0);
            _viewModel.AlleFahrzeugePersonen.AnzahlPersonen = S7.GetUint8At(digOutput, 1);
        }

        public DatenRangieren(Tiefgarage.ViewModel.ViewModel vm) => _viewModel = vm;
    }
}