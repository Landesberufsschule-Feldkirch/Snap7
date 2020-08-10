using Sharp7;

namespace LAP_2010_5_Pumpensteuerung
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private readonly LAP_2010_5_Pumpensteuerung.ViewModel.ViewModel viewModel;

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

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, viewModel.Pumpensteuerung.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, viewModel.Pumpensteuerung.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.F1, viewModel.Pumpensteuerung.F1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, viewModel.Pumpensteuerung.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, viewModel.Pumpensteuerung.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, viewModel.Pumpensteuerung.S3);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            if (!mainWindow.DebugWindowAktiv)
            {
                viewModel.Pumpensteuerung.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
                viewModel.Pumpensteuerung.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
                viewModel.Pumpensteuerung.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
            }
        }

        public DatenRangieren(MainWindow mw, LAP_2010_5_Pumpensteuerung.ViewModel.ViewModel vm)
        {
            mainWindow = mw;
            viewModel = vm;
        }
    }
}