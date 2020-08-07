using Sharp7;
using Utilities;

namespace LAP_2019_Foerderanlage
{
    public class DatenRangieren
    {
        private readonly MainWindow _mainWindow;
        private readonly ViewModel.ViewModel _foerderanlageViewModel;

        private enum BitPosAusgang
        {
            K1 = 0, // Materialschieber Silo
            P1,     // Anlage Ein
            P2,     //Sammelstörung
            Q1,  //Förderband Rechtslauf
            Q2,  // Förderband Linkslauf
            T1      // Freigabe FU (Schneckenförderer)
        }

        private enum BitPosEingang
        {
            B1 = 0, // Wagen Position rechts
            B2,     // Wagen voll
            F1,     // Störung Motorschutzschalter
            S0,     // Anlage Aus
            S1,     // Anlage Ein
            S2,     // Not-Halt
            S3,     // Schalter Automatikbetrieb
            S4,     // Schalter Handbetrieb
            S5,     // Handbetrieb Förderband RL
            S6,     // Handbetrieb Förderband LL
            S7,     // Handbetrieb Schneckenförderer
            S8      // Handbetrieb Materialschieber
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, _foerderanlageViewModel.foerderanlage.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, _foerderanlageViewModel.foerderanlage.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.F1, _foerderanlageViewModel.foerderanlage.F1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S0, _foerderanlageViewModel.foerderanlage.S0);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, _foerderanlageViewModel.foerderanlage.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, _foerderanlageViewModel.foerderanlage.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, _foerderanlageViewModel.foerderanlage.S3);
            S7.SetBitAt(digInput, (int)BitPosEingang.S4, _foerderanlageViewModel.foerderanlage.S4);
            S7.SetBitAt(digInput, (int)BitPosEingang.S5, _foerderanlageViewModel.foerderanlage.S5);
            S7.SetBitAt(digInput, (int)BitPosEingang.S6, _foerderanlageViewModel.foerderanlage.S6);
            S7.SetBitAt(digInput, (int)BitPosEingang.S7, _foerderanlageViewModel.foerderanlage.S7);
            S7.SetBitAt(digInput, (int)BitPosEingang.S8, _foerderanlageViewModel.foerderanlage.S8);

            S7.SetSint_16_At(anInput, 0, S7Analog.S7_Analog_2_Int16(_foerderanlageViewModel.foerderanlage.Silo.GetFuellstand(), 1));
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            if (!_mainWindow.DebugWindowAktiv)
            {
                _foerderanlageViewModel.foerderanlage.K1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K1);
                _foerderanlageViewModel.foerderanlage.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
                _foerderanlageViewModel.foerderanlage.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
                _foerderanlageViewModel.foerderanlage.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
                _foerderanlageViewModel.foerderanlage.Q2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q2);
                _foerderanlageViewModel.foerderanlage.T1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.T1);
            }
        }

        public DatenRangieren(MainWindow mw, ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _foerderanlageViewModel = vm;
        }
    }
}