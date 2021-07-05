using Kommunikation;

namespace LaborGetriebemotor
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            Q1 = 0,     // 0.0  Getriebemotor Schnell Rechtslauf
            Q2 = 1,     // 0.1  Getriebemotor Linkslauf
            Q3 = 2,     // 0.2  Getriebemotor Langsam Rechtslauf
            P1 = 4,     // 0.4  Meldeleuchte weiß
            P2 = 5,     // 0.5  Meldeleuchte grün
            P3 = 6      // 0.6  Meldeleuchte rot
        }

        private enum BitPosEingang
        {
            S1 = 0,     // 0.0  Taster ( ① ) → Schliesser
            S2 = 1,     // 0.1  Taster ( ⓪ ) → Öffner
            S4 = 2,     // 0.2  Taster ( STOP ) → Öffner 
            S3 = 3,     // 0.3  Taster ( Ⅰ ) → Schliesser 
            S5 = 4,     // 0.4  Taster ( Ⅱ ) → Schliesser 
            S7 = 5,     // 0.5  Taster (STOP) → Öffner
            S6 = 6,     // 0.6  Taster (←) → Schliesser
            S8 = 7,     // 0.7  Taster (→) → Schliesser
            S91 = 8,    // 1.0  Not-Halt → Schliesser 
            S92 = 9,    // 1.1  Not-Halt → Öffner
            B1 = 10,    // 1.2  Lichtschranke 0°
            B2 = 11     // 1.3  Lichtschranke 45° CCW 
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Getriebemotor.B1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.Getriebemotor.B2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Getriebemotor.S1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Getriebemotor.S2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Getriebemotor.S3);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S4, _viewModel.Getriebemotor.S4);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S5, _viewModel.Getriebemotor.S5);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S6, _viewModel.Getriebemotor.S6);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S7, _viewModel.Getriebemotor.S7);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S8, _viewModel.Getriebemotor.S8);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S91, _viewModel.Getriebemotor.S91);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S92, _viewModel.Getriebemotor.S92);
            }

            _viewModel.Getriebemotor.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Getriebemotor.P2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.Getriebemotor.P3 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3);
            _viewModel.Getriebemotor.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.Getriebemotor.Q2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
            _viewModel.Getriebemotor.Q3 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q3);
        }
        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}