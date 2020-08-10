using Sharp7;

namespace LAP_2010_3_Ofentuersteuerung
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

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
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, _viewModel.OfentuerSteuerung.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, _viewModel.OfentuerSteuerung.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.B3, _viewModel.OfentuerSteuerung.B3);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, _viewModel.OfentuerSteuerung.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, _viewModel.OfentuerSteuerung.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, _viewModel.OfentuerSteuerung.S3);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            _viewModel.OfentuerSteuerung.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
            _viewModel.OfentuerSteuerung.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
            _viewModel.OfentuerSteuerung.Q2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q2);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    }
}