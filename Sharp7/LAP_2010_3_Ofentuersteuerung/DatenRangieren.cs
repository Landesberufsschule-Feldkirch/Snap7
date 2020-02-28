using Sharp7;

namespace LAP_2010_3_Ofentuersteuerung
{
    public class DatenRangieren
    {
        private readonly ViewModel.OfensteuerungViewModel ofensteuerungViewModel;

        private enum BitPosAusgang
        {
            K1 = 0, // Motor LL (Öffnen)
            K2,     // Motor RL (Schliessen)
            H3      // Meldeleuchte "Schliessen"
        }

        private enum BitPosEingang
        {
            S1 = 0, // Taster "Halt" 
            S2,     // Taster "Öffnen" 
            S3,     // Taster "Schliessen" 
            S7,     // Endschalter Offen 
            S8,     // Endschalter Geschlossen 
            S9      // Lichtschranke
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, ofensteuerungViewModel.ofentuerSteuerung.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, ofensteuerungViewModel.ofentuerSteuerung.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, ofensteuerungViewModel.ofentuerSteuerung.S3);
            S7.SetBitAt(digInput, (int)BitPosEingang.S7, ofensteuerungViewModel.ofentuerSteuerung.S7);
            S7.SetBitAt(digInput, (int)BitPosEingang.S8, ofensteuerungViewModel.ofentuerSteuerung.S8);
            S7.SetBitAt(digInput, (int)BitPosEingang.S9, ofensteuerungViewModel.ofentuerSteuerung.S9);
            
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            ofensteuerungViewModel.ofentuerSteuerung.K1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K1);
            ofensteuerungViewModel.ofentuerSteuerung.K2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K2);
            ofensteuerungViewModel.ofentuerSteuerung.H3 = S7.GetBitAt(digOutput, (int)BitPosAusgang.H3);            
        }

        public DatenRangieren(ViewModel.OfensteuerungViewModel vm)
        {
            ofensteuerungViewModel = vm;
        }
    }
}