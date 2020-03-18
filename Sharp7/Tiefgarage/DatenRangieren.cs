using Sharp7;

namespace Tiefgarage
{
    public class DatenRangieren
    {
        private readonly ViewModel.TiefgarageViewModel viewModel;

        private enum BitPosEingang
        {
            B1 = 0,
            B2
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, viewModel.alleFahrzeugePersonen.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, viewModel.alleFahrzeugePersonen.B2);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            viewModel.alleFahrzeugePersonen.AnzahlAutos = S7.GetUint8At(digOutput, 0);
            viewModel.alleFahrzeugePersonen.AnzahlPersonen = S7.GetUint8At(digOutput, 1);
        }

        public DatenRangieren(Tiefgarage.ViewModel.TiefgarageViewModel vm)
        {
            viewModel = vm;
        }
    }
}