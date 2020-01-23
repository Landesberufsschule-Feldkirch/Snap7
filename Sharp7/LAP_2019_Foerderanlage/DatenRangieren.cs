using Sharp7;

namespace LAP_2019_Foerderanlage
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;

        private enum BitPosAusgang
        {
            P1 = 0, // Anlage Ein
            P2,     //Sammelstörung
            Q3_RL,  //Förderband Rechtslauf
            Q4_LL,  // Förderband Linkslauf
            XFU     // Freigabe FU (Schneckenförderer)
        }

        private enum BitPosEingang
        {
            S0 = 0, // Anlage Aus
            S1,     // Anlage Ein
            S2,     // Not-Halt
            F4,     // Störung Motorschutzschalter
            S4,
            S5,
            S6,
            S7,     // Wagen Position rechts
            S8      // Wagen voll
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.S0, mainWindow.S0);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, mainWindow.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, mainWindow.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.F4, mainWindow.F4);
            S7.SetBitAt(digInput, (int)BitPosEingang.S4, mainWindow.S4);
            S7.SetBitAt(digInput, (int)BitPosEingang.S5, mainWindow.S5);
            S7.SetBitAt(digInput, (int)BitPosEingang.S6, mainWindow.S6);
            S7.SetBitAt(digInput, (int)BitPosEingang.S7, mainWindow.S7);
            S7.SetBitAt(digInput, (int)BitPosEingang.S8, mainWindow.S8);

            S7.SetIntAt(anInput, 0, mainWindow.MaterialsiloPegel);
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            mainWindow.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
            mainWindow.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
            mainWindow.Q3_RL = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q3_RL);
            mainWindow.Q4_LL = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q4_LL);
            mainWindow.XFU = S7.GetBitAt(digOutput, (int)BitPosAusgang.XFU);

            mainWindow.FuSpeed = S7.GetSint16At(anOutput, 0);
        }

        public DatenRangieren(MainWindow window)
        {
            mainWindow = window;
        }
    }
}