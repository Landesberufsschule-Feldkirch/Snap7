using Kommunikation;

namespace AutomatischesLagersystem
{
    public class DatenRangieren
    {
        private readonly MainWindow _mainWindow;
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            K1 = 0, // Aufwärts
            K2 = 1  // Abwärts
        }

        private enum BitPosEingang
        {
            B1 = 0, // Initiator Regal 0
            B2 = 1,     // Initiator irgendein Regal
            S1 = 2,     // Auf
            S2 = 3     // Ab
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.AutomatischesLagersystem.B1);
            }

            if (!_mainWindow.DebugWindowAktiv)
            {
                _viewModel.AutomatischesLagersystem.K1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K1);
            }
            _viewModel.AutomatischesLagersystem.K2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K2);
        }
        public DatenRangieren(MainWindow mw, ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _viewModel = vm;
        }
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}