using Sharp7;

namespace LAP_2010_2_Transportwagen
{
    public class DatenRangieren
    {
        private readonly ViewModel.TransportwagenViewModel transportwagenViewModel;

        private enum BitPosAusgang
        {
            K1 = 0, // Motor LL 
            K2,     // Motor RL 
            H3      // Störung
        }

        private enum BitPosEingang
        {
            S1 = 0, // Taster "Start" 
            S2,     // NotHalt 
            S3,     // Reset
            F3,     // Thermorelais 
            S7,     // Endschalter Links 
            S8      // Endschalter Rechts 
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, transportwagenViewModel.transportwagen.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, transportwagenViewModel.transportwagen.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, transportwagenViewModel.transportwagen.S3);
            S7.SetBitAt(digInput, (int)BitPosEingang.F3, transportwagenViewModel.transportwagen.F3);
            S7.SetBitAt(digInput, (int)BitPosEingang.S7, transportwagenViewModel.transportwagen.S7);
            S7.SetBitAt(digInput, (int)BitPosEingang.S8, transportwagenViewModel.transportwagen.S8);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            transportwagenViewModel.transportwagen.K1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K1);
            transportwagenViewModel.transportwagen.K2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K2);
            transportwagenViewModel.transportwagen.H3 = S7.GetBitAt(digOutput, (int)BitPosAusgang.H3);
        }

        public DatenRangieren(ViewModel.TransportwagenViewModel vm)
        {
            transportwagenViewModel = vm;
        }
    }
}