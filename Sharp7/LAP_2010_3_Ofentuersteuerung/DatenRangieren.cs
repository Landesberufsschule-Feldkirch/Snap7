using Kommunikation;

namespace LAP_2010_3_Ofentuersteuerung
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

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

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.OfentuerSteuerung.B1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.OfentuerSteuerung.B2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B3, _viewModel.OfentuerSteuerung.B3);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.OfentuerSteuerung.S1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.OfentuerSteuerung.S2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.OfentuerSteuerung.S3);
            }


            _viewModel.OfentuerSteuerung.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.OfentuerSteuerung.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.OfentuerSteuerung.Q2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}