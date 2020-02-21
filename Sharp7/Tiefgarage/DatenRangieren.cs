using Sharp7;

namespace Tiefgarage
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private readonly ViewModel.TiefgarageViewModel viewModel;
        private enum BitPosAusgang
        {
        }

        private enum BitPosEingang
        {
            B1 = 0,
            B2
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            /*
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, mainWindow.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, mainWindow.B2);
    */
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            /*
            mainWindow.AnzahlFahrzeuge = S7.GetUint8At(digOutput, 0);
            mainWindow.AnzahlPersonen = S7.GetUint8At(digOutput, 1);
    */
        }

        public DatenRangieren(MainWindow window, Tiefgarage.ViewModel.TiefgarageViewModel vm)
        {
            mainWindow = window;
            viewModel = vm;
        }
    }
}