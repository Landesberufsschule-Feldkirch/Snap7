using Sharp7;

namespace LAP_2010_5_Pumpensteuerung
{
    public class DatenRangieren
    {
        private readonly MainWindow _mainWindow;
        private readonly LAP_2010_5_Pumpensteuerung.ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            P1 = 0, // "Pumpe Ein"
            P2,     // "Störung"
            Q1      // Motor Pumpe
        }

        private enum BitPosEingang
        {
            B1 = 0, // Schwimmerschalter oben
            B2,     // Schwimmerschalter unten
            F1,     // Thermorelais
            S1,     // Wahlschalter Hand
            S2,     // Wahlschalter Automatik
            S3,     // Störung quittieren
        }

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Pumpensteuerung.B1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.Pumpensteuerung.B2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _viewModel.Pumpensteuerung.F1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Pumpensteuerung.S1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Pumpensteuerung.S2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Pumpensteuerung.S3);
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            if (!_mainWindow.DebugWindowAktiv)
            {
                _viewModel.Pumpensteuerung.P1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
                _viewModel.Pumpensteuerung.P2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
                _viewModel.Pumpensteuerung.Q1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            }
        }

        public DatenRangieren(MainWindow mw, LAP_2010_5_Pumpensteuerung.ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _viewModel = vm;
        }
    }
}