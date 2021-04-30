using Sharp7;

namespace Schleifmaschine
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            P1 = 0, // 0.0  Meldeleuchte "Taster langsam"
            P2,     // 0.1  Meldeleuchte "Taster schnell"
            P3,     // 0.2  Meldeleuchte "Störung"
            Q1,     // 0.3  Schleifmaschine Langsam
            Q2      // 0.4  Schleifmaschine Schnell
        }

        private enum BitPosEingang
        {
            F1 = 0, // 0.0 Thermorelais langsame Drehzahl
            F2,     // 0.1 Thermorelais schnelle Drehzahl
            S0,     // 0.2 Taster ( ⓪ ) 
            S1,     // 0.3 Taster ( Ⅰ )  
            S2,     // 0.4 Taster ( Ⅱ )  
            S3,     // 0.5 Not-Halt
            S4      // 0.6 Störung quittieren
        }

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _viewModel.Schleifmaschine.F1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F2, _viewModel.Schleifmaschine.F2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S0, _viewModel.Schleifmaschine.S0);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Schleifmaschine.S1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Schleifmaschine.S2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Schleifmaschine.S3);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S4, _viewModel.Schleifmaschine.S4);
        }
        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _viewModel.Schleifmaschine.P1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Schleifmaschine.P2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.Schleifmaschine.P3 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3);
            _viewModel.Schleifmaschine.Q1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.Schleifmaschine.Q2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
        }
        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    }
}