using Sharp7;

namespace LaborLinearachse
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            Q1 = 0, // 0.0  Linearachse Rechtslauf
            Q2,     // 0.1  Linearachse Linkslauf
            P1,     // 0.2  Meldeleuchte im Taster S1/S2 (weiß) 
            P2,     // 0.3  Meldeleuchte weiß
            P3,     // 0.4  Meldeleuchte rot
            P4      // 0.5  Meldeleuchte grün
        }

        private enum BitPosEingang
        {
            B1 = 0, // 0.0  Linearachse Endlage links → Öffner
            B2,     // 0.1  Linearachse Endlage rechts → Öffner
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
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Linearachse.B1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.Linearachse.B2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Linearachse.S1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Linearachse.S2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Linearachse.S3);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S4, _viewModel.Linearachse.S4);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S5, _viewModel.Linearachse.S5);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S6, _viewModel.Linearachse.S6);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S7, _viewModel.Linearachse.S7);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S8, _viewModel.Linearachse.S8);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S9, _viewModel.Linearachse.S9);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S10, _viewModel.Linearachse.S10);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S11, _viewModel.Linearachse.S11);
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _viewModel.Linearachse.P1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Linearachse.P2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.Linearachse.P3 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3);
            _viewModel.Linearachse.P4 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P4);
            _viewModel.Linearachse.Q1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.Linearachse.Q2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    }
}