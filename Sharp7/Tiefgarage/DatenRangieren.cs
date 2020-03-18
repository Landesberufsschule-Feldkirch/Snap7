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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Nicht verwendete Parameter entfernen", Justification = "<Ausstehend>")]
        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, viewModel.alleFahrzeugePersonen.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, viewModel.alleFahrzeugePersonen.B2);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Nicht verwendete Parameter entfernen", Justification = "<Ausstehend>")]
        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
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