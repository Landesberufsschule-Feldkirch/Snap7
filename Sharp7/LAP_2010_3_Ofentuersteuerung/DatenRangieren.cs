using Sharp7;

namespace LAP_2010_3_Ofentuersteuerung
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel viewModel;

        private enum BitPosAusgang
        {
            P1 = 0, // Meldeleuchte "Schliessen"
            Q1,     // Motor LL (Öffnen)
            Q2      // Motor RL (Schliessen)
        }

        private enum BitPosEingang
        {
            B1 = 0, // Endschalter Offen
            B2,     // Endschalter Geschlossen
            B3,     // Lichtschranke
            S1,     // Taster "Halt"
            S2,     // Taster "Öffnen"
            S3      // Taster "Schliessen"
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, viewModel.ofentuerSteuerung.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, viewModel.ofentuerSteuerung.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.B3, viewModel.ofentuerSteuerung.B3);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, viewModel.ofentuerSteuerung.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, viewModel.ofentuerSteuerung.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, viewModel.ofentuerSteuerung.S3);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            viewModel.ofentuerSteuerung.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
            viewModel.ofentuerSteuerung.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
            viewModel.ofentuerSteuerung.Q2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q2);
        }

        public DatenRangieren(ViewModel.ViewModel vm)
        {
            viewModel = vm;
        }
    }
}