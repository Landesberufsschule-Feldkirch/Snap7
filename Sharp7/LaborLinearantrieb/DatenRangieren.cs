using Sharp7;

namespace LaborLinearantrieb
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            Q1 = 0, // 0.0  Linearantrieb Rechtslauf
            Q2,     // 0.1  Linearantrieb Linkslauf
            P1,     // 0.2  Meldeleuchte im Taster S1/S2 (weiß) 
            P2,     // 0.3  Meldeleuchte weiß
            P3,     // 0.4  Meldeleuchte rot
            P4      // 0.5  Meldeleuchte grün
        }

        private enum BitPosEingang
        {
            B1 = 0, // 0.0  Linearantrieb Endlage links → Öffner
            B2,     // 0.1  Linearantrieb Endlage rechts → Öffner
            S2,     // 0.2  Taster ( ⓪ ) → Öffner 
            S1,     // 0.3  Taster ( ① ) → Schliesser 
            S4,     // 0.4  Taster ( Ⅱ ) → Schliesser 
            S3,     // 0.5  Taster ( Ⅰ ) → Schliesser
            S6,     // 0.6  Taster ( ↓ ) → Schliesser
            S5,     // 0.7  Taster ( ↑ ) → Schliesser 
            S8,     // 1.0  Taster ( － ) → Schliesser 
            S7,     // 1.1  Taster ( + ) → Schliesser 
            S9,     // 1.2  Taster ( STOP ) → Öffner 
            S10,    // 1.3  Not-Halt → Öffner
            S11     // 1.4  Not-Halt → Schliesser 
        }

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Linearantrieb.B1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.Linearantrieb.B2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Linearantrieb.S1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Linearantrieb.S2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Linearantrieb.S3);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S4, _viewModel.Linearantrieb.S4);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S5, _viewModel.Linearantrieb.S5);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S6, _viewModel.Linearantrieb.S6);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S7, _viewModel.Linearantrieb.S7);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S8, _viewModel.Linearantrieb.S8);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S9, _viewModel.Linearantrieb.S9);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S10, _viewModel.Linearantrieb.S10);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S11, _viewModel.Linearantrieb.S11);
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _viewModel.Linearantrieb.P1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Linearantrieb.P2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.Linearantrieb.P3 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3);
            _viewModel.Linearantrieb.P4 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P4);
            _viewModel.Linearantrieb.Q1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.Linearantrieb.Q2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    }
}