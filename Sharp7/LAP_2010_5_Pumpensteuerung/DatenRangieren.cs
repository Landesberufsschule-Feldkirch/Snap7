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
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, viewModel.pumpensteuerung.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, viewModel.pumpensteuerung.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.F1, viewModel.pumpensteuerung.F1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, viewModel.pumpensteuerung.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, viewModel.pumpensteuerung.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, viewModel.pumpensteuerung.S3);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            if (!mainWindow.DebugWindowAktiv)
            {
                viewModel.pumpensteuerung.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
                viewModel.pumpensteuerung.P2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
                viewModel.pumpensteuerung.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
            }
        }

        public DatenRangieren(MainWindow mw, LAP_2010_5_Pumpensteuerung.ViewModel.ViewModel vm)
        {
            mainWindow = mw;
            viewModel = vm;
        }
    }
}