namespace LAP_2018_1_Silosteuerung
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private readonly ViewModel.ViewModel foerderanlageViewModel;

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
            S8,     // Wagen voll
            S9,     // Handbetrieb Förderband RL
            S10,    // Handbetrieb Förderband LL
            S11,    // Handbetrieb Schneckenförderer
            S12     // Handbetrieb Materialschieber
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            /*
            S7.SetBitAt(digInput, (int)BitPosEingang.S0, foerderanlageViewModel.foerderanlage.S0);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, foerderanlageViewModel.foerderanlage.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, foerderanlageViewModel.foerderanlage.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.F4, foerderanlageViewModel.foerderanlage.F4);
            S7.SetBitAt(digInput, (int)BitPosEingang.S5, foerderanlageViewModel.foerderanlage.S5);
            S7.SetBitAt(digInput, (int)BitPosEingang.S6, foerderanlageViewModel.foerderanlage.S6);
            S7.SetBitAt(digInput, (int)BitPosEingang.S7, foerderanlageViewModel.foerderanlage.S7);
            S7.SetBitAt(digInput, (int)BitPosEingang.S8, foerderanlageViewModel.foerderanlage.S8);
            S7.SetBitAt(digInput, (int)BitPosEingang.S9, foerderanlageViewModel.foerderanlage.S8);
            S7.SetBitAt(digInput, (int)BitPosEingang.S10, foerderanlageViewModel.foerderanlage.S10);
            S7.SetBitAt(digInput, (int)BitPosEingang.S11, foerderanlageViewModel.foerderanlage.S11);
            S7.SetBitAt(digInput, (int)BitPosEingang.S12, foerderanlageViewModel.foerderanlage.S12);

            S7.SetSint_16_At(anInput, 0, S7Analog.S7_Analog_2_Int16(foerderanlageViewModel.foerderanlage.Silo.GetFuellstand(), 1));
            */
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            /*
            if (!mainWindow.DebugWindowAktiv)
            {
                foerderanlageViewModel.foerderanlage.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
                foerderanlageViewModel.foerderanlage.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
                foerderanlageViewModel.foerderanlage.Q3_RL = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q3_RL);
                foerderanlageViewModel.foerderanlage.Q4_LL = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q4_LL);
                foerderanlageViewModel.foerderanlage.XFU = S7.GetBitAt(digOutput, (int)BitPosAusgang.XFU);
            }
            */
        }

        public DatenRangieren(MainWindow mw, ViewModel.ViewModel vm)
        {
            mainWindow = mw;
            foerderanlageViewModel = vm;
        }
    }
}