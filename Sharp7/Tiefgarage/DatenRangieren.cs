using Sharp7;

namespace Tiefgarage
{
    public class DatenRangieren
    {
        readonly MainWindow mainWindow;

        enum BitPosAusgang
        {
        }
        enum BitPosEingang
        {
            B1 = 0,
            B2
        }


        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, mainWindow.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, mainWindow.B2);
        }
        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            mainWindow.AnzahlFahrzeuge = S7.GetByteAt(digOutput, 0);
            mainWindow.AnzahlPersonen = S7.GetByteAt(digOutput, 1);
        }

        public DatenRangieren(MainWindow window)
        {
            mainWindow = window;
        }
    }
}
