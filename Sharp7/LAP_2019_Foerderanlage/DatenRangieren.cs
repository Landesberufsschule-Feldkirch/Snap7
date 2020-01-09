using Sharp7;

namespace LAP_2019_Foerderanlage
{
    public class DatenRangieren
    {
        readonly MainWindow mainWindow;

        enum BitPosAusgang
        {
            P1 = 0, // Anlage Ein
            P2,     //Sammelstörung
            Q3_RL,  //Förderband Rechtslauf
            Q4_LL,  // Förderband Linkslauf
            XFU     // Freigabe FU (Schneckenförderer)
        }
        enum BitPosEingang
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
            //
        }
        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            //
        }

        public DatenRangieren(MainWindow window)
        {
            mainWindow = window;
        }

    }
}
