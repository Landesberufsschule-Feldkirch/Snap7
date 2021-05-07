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
            Q1 = 0, // Aufwärts
            Q2      // Abwärts
        }

        private enum BitPosEingang
        {
            B1 = 0, // Initiator Regal 0
            B2,     // Initiator irgendein Regal
            S1,     // Auf
            S2      // Ab
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                /*
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, viewModel.paternosterlager.B1);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, viewModel.paternosterlager.B2);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, viewModel.paternosterlager.S1);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, viewModel.paternosterlager.S2);

            _plc.SetUSIntAt(digInput, 1, (byte)viewModel.paternosterlager.Zeichen);
    */
            }


            if (!_mainWindow.DebugWindowAktiv)
            {
                /*
  viewModel.paternosterlager.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
  viewModel.paternosterlager.Q2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
  */
            }

            /*
            viewModel.paternosterlager.IstPos = _plc.GetUSIntAt(digOutput, 1);
            viewModel.paternosterlager.SollPos = _plc.GetUSIntAt(digOutput, 2);
            */
        }

        public DatenRangieren(MainWindow mw, ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _viewModel = vm;
        }
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}