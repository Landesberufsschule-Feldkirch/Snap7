using Sharp7;

namespace LAP_2018_Niveauregelung
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;

        private enum BitPosAusgang
        {
            M1 = 0,
            M2,
            P1,
            P2,
            P3
        }

        private enum BitPosEingang
        {
            B1 = 0,
            B2,
            B3,
            F1,
            F2,
            S1,
            S2,
            S3
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, mainWindow.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, mainWindow.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.B3, mainWindow.B3);
            S7.SetBitAt(digInput, (int)BitPosEingang.F1, mainWindow.F1);
            S7.SetBitAt(digInput, (int)BitPosEingang.F2, mainWindow.F2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, mainWindow.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, mainWindow.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, mainWindow.S3);
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            mainWindow.M1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.M1);
            mainWindow.M2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.M2);
            mainWindow.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
            mainWindow.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
            mainWindow.P3 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P3);
        }

        public DatenRangieren(MainWindow window)
        {
            this.mainWindow = window;
        }
    }
}