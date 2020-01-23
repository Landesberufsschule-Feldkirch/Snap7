using Sharp7;

namespace Synchronisiereinrichtung
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;

        private enum BitPosAusgang
        {
            Q1 = 0
        }

        private enum BitPosEingang
        {
            S1 = 0,
            S2
        }

        public DatenRangieren(MainWindow window)
        {
            mainWindow = window;
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, mainWindow.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, mainWindow.S2);
            /*
            S7.SetIntAt(anInput, 0, mainWindow.n);

            S7.SetIntAt(anInput, 2, mainWindow.UGenerator);
            S7.SetIntAt(anInput, 4, mainWindow.fGenerator);
            S7.SetIntAt(anInput, 6, mainWindow.PGenerator);
            S7.SetIntAt(anInput, 8, mainWindow.cosPhiGenerator);

            S7.SetIntAt(anInput, 10, mainWindow.UNetz);
            S7.SetIntAt(anInput, 12, mainWindow.fNetz);
            S7.SetIntAt(anInput, 14, mainWindow.PNetz);
            S7.SetIntAt(anInput, 16, mainWindow.cosPhiNetz);

            S7.SetIntAt(anInput, 18, mainWindow.UDiff);
            */
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            if (!mainWindow.DebugWindowAktiv)
            {
                mainWindow.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);

                mainWindow.Y = S7.GetSint16At(anOutput, 0);
                mainWindow.Ie = S7.GetSint16At(anOutput, 2);
            }
        }
    }
}