using Sharp7;

namespace LAP_2010_2_Transportwagen
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _transportwagenViewModel;

        private enum BitPosAusgang
        {
            P1 = 0, // Störung
            Q1,     // Motor LL
            Q2      // Motor RL
        }

        private enum BitPosEingang
        {
            B1 = 0, // Endschalter Links
            B2,     // Endschalter Rechts
            F1,     // Thermorelais
            S1,     // Taster "Start"
            S2,     // NotHalt
            S3      // Taster Reset
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, _transportwagenViewModel.transportwagen.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, _transportwagenViewModel.transportwagen.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.F1, _transportwagenViewModel.transportwagen.F1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, _transportwagenViewModel.transportwagen.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, _transportwagenViewModel.transportwagen.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, _transportwagenViewModel.transportwagen.S3);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            _transportwagenViewModel.transportwagen.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
            _transportwagenViewModel.transportwagen.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
            _transportwagenViewModel.transportwagen.Q2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q2);
        }

        public DatenRangieren(ViewModel.ViewModel vm)
        {
            _transportwagenViewModel = vm;
        }
    }
}