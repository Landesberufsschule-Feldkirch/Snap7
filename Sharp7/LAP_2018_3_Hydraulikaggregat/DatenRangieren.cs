using Sharp7;

namespace LAP_2018_3_Hydraulikaggregat
{
    public class DatenRangieren
    {
        private readonly MainWindow _mainWindow;
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            P1 = 0, // Meldeleuchte Störung Motor
            P2,     // Meldeleuchte Überdruck
            P3,     // Meldeleuchte Druck erreicht
            P4,     // Meldeleuchte Ölstand min.
            Q1,     // Netzschütz
            Q2,     // Sternschütz
            Q3      // Dreieckschütz
        }

        private enum BitPosEingang
        {
            B1 = 0, // Sensor Ölstand
            B2,     // Sensor Druck erreicht
            B3,     // Sensor Überdruck
            F1,     // Motorstörung
            S1,     // Taster Start
            S2,     // Taster Stop
            S3      // Taster Quittieren
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, _viewModel.hydraulikaggregat.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, _viewModel.hydraulikaggregat.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.B3, _viewModel.hydraulikaggregat.B3);
            S7.SetBitAt(digInput, (int)BitPosEingang.F1, _viewModel.hydraulikaggregat.F1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, _viewModel.hydraulikaggregat.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, _viewModel.hydraulikaggregat.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, _viewModel.hydraulikaggregat.S3);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            _viewModel.hydraulikaggregat.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
            _viewModel.hydraulikaggregat.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
            _viewModel.hydraulikaggregat.P3 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P3);
            _viewModel.hydraulikaggregat.P4 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P4);

            if (!_mainWindow.DebugWindowAktiv)
            {
                _viewModel.hydraulikaggregat.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
                _viewModel.hydraulikaggregat.Q2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q2);
                _viewModel.hydraulikaggregat.Q3 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q3);
            }
        }

        public DatenRangieren(MainWindow mw, ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _viewModel = vm;
        }
    }
}