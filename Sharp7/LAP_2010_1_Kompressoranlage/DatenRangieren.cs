using Sharp7;
using Utilities;

namespace LAP_2010_1_Kompressoranlage
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private readonly ViewModel.KompressoranlageViewModel kompressoranlageViewModel;

        private enum BitPosAusgang
        {
            P1 = 0, // Anlage Ein
            P2,     //Sammelstörung
        }

        private enum BitPosEingang
        {
            S0 = 0, // Anlage Aus
            S1,     // Anlage Ein
            S2,     // Not-Halt
            F4,     // Störung Motorschutzschalter
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {/*
            S7.SetBitAt(digInput, (int)BitPosEingang.S0, foerderanlageViewModel.foerderanlage.S0);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, foerderanlageViewModel.foerderanlage.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, foerderanlageViewModel.foerderanlage.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.F4, foerderanlageViewModel.foerderanlage.F4);
            */
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            /*
           
                foerderanlageViewModel.foerderanlage.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
                foerderanlageViewModel.foerderanlage.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
                foerderanlageViewModel.foerderanlage.Q3_RL = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q3_RL);
                foerderanlageViewModel.foerderanlage.Q4_LL = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q4_LL);
           */
        }

        public DatenRangieren(MainWindow mw, ViewModel.KompressoranlageViewModel vm)
        {
            mainWindow = mw;
            kompressoranlageViewModel = vm;
        }
    }
}