using Sharp7;

namespace Blinklicht_Fibonacci
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            P1 = 0  // Meldeleuchte
        }

        private enum BitPosEingang
        {
            S1 = 0 // Taster Start
        }

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur) => S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.BlinklichtFibonacci.S1);
        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur) => _viewModel.BlinklichtFibonacci.P1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    }
}