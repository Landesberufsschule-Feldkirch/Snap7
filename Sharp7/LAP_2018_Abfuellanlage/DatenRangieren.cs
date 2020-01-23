using Sharp7;

namespace LAP_2018_Abfuellanlage
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;

        private enum BitPosAusgang
        {
            M1 = 0,
            K1,
            K2,
            P1,
            P2
        }

        private enum BitPosEingang
        {
            B1 = 0,
            F5,
            S1,
            S2,
            S3,
            S4
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, mainWindow.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.F5, mainWindow.F5);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, mainWindow.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, mainWindow.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, mainWindow.S3);
            S7.SetBitAt(digInput, (int)BitPosEingang.S4, mainWindow.S4);

            S7.SetByteAt(anInput, 0, mainWindow.PegelByte);
            S7.SetWordAt(anInput, 2, mainWindow.PegelInt);
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            mainWindow.M1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.M1);
            mainWindow.K1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K1);
            mainWindow.K2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K2);
            mainWindow.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
            mainWindow.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
        }

        public DatenRangieren(MainWindow window)
        {
            mainWindow = window;
        }
    }
}