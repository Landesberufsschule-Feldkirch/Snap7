using Sharp7;

namespace LAP_2010_4_Abfuellanlage
{
    public class DatenRangieren
    {
        private readonly MainWindow _mainWindow;
        private readonly ViewModel.ViewModel _abfuellanlageViewModel;

        private enum BitPosAusgang
        {
            K1 = 0, // Vereinzelner
            K2,     // Füllen
            P1,     // Behälter leer
            Q1      // Förderband
        }

        private enum BitPosEingang
        {
            B1 = 0, // Füllstand
            B2,     // Position Dose
            S1,     // Taster Reset
            S2      // Taster Start
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, _abfuellanlageViewModel.AbfuellAnlage.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, _abfuellanlageViewModel.AbfuellAnlage.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, _abfuellanlageViewModel.AbfuellAnlage.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, _abfuellanlageViewModel.AbfuellAnlage.S2);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            if (!_mainWindow.DebugWindowAktiv)
            {
                _abfuellanlageViewModel.AbfuellAnlage.K1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K1);
                _abfuellanlageViewModel.AbfuellAnlage.K2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K2);
                _abfuellanlageViewModel.AbfuellAnlage.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
                _abfuellanlageViewModel.AbfuellAnlage.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
            }
        }

        public DatenRangieren(MainWindow mw, LAP_2010_4_Abfuellanlage.ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _abfuellanlageViewModel = vm;
        }
    }
}