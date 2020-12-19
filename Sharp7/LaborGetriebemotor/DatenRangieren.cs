using Sharp7;

namespace LaborGetriebemotor
{
    public class DatenRangieren
    {
        private readonly LaborGetriebemotor.ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            Q1 = 0, // 0.0  Getriebemotor Schnell Rechtslauf
            Q2,     // 0.1  Getriebemotor Linkslauf
            Q3,     // 0.2  Getriebemotor Langsam Rechtslauf
            P1 = 4, // 0.4  Meldeleuchte weiß
            P2,     // 0.5  Meldeleuchte grün
            P3      // 0.6  Meldeleuchte rot
        }

        private enum BitPosEingang
        {

            S1 = 0, // 0.0  Taster ( ① ) → Schliesser
            S2,     // 0.1  Taster ( ⓪ ) → Öffner
            S4,     // 0.2  Taster ( STOP ) → Öffner 
            S3,     // 0.3  Taster ( Ⅰ ) → Schliesser 
            S5,     // 0.4  Taster ( Ⅱ ) → Schliesser 
            S7,     // 0.5  Taster (STOP) → Öffner
            S6,     // 0.6  Taster (←) → Schliesser
            S8,     // 0.7  Taster (→) → Schliesser
            S91,    // 1.0  Not-Halt → Schliesser 
            S92,    // 1.1  Not-Halt → Öffner
            B1,     // 1.2  Lichtschranke 0° 
            B2      // 1.3  Lichtschranke 45° CCW 
        }

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Getriebemotor.B1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.Getriebemotor.B2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Getriebemotor.S1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Getriebemotor.S2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Getriebemotor.S3);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S4, _viewModel.Getriebemotor.S4);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S5, _viewModel.Getriebemotor.S5);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S6, _viewModel.Getriebemotor.S6);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S7, _viewModel.Getriebemotor.S7);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S8, _viewModel.Getriebemotor.S8);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S91, _viewModel.Getriebemotor.S91);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S92, _viewModel.Getriebemotor.S92);
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _viewModel.Getriebemotor.P1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Getriebemotor.P2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.Getriebemotor.P3 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3);
            _viewModel.Getriebemotor.Q1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.Getriebemotor.Q2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
            _viewModel.Getriebemotor.Q3 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q3);
        }

        public DatenRangieren(LaborGetriebemotor.ViewModel.ViewModel vm) => _viewModel = vm;
    }
}