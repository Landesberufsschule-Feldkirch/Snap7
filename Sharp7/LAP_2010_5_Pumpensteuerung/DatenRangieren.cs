using Sharp7;

namespace LAP_2010_5_Pumpensteuerung
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private readonly LAP_2010_5_Pumpensteuerung.ViewModel.PumpensteuerungViewModel viewModel;
        private enum BitPosAusgang
        {
            K1 = 0, // Motor Pumpe
            H1,     // "Pumpe Ein"
            H2      // "Störung"
        }

        private enum BitPosEingang
        {
            S1 = 0, // Wahlschalter Hand
            S2,     // Wahlschalter Automatik
            S3,     // Störung quittieren
            F5,     // Thermorelais
            S7,     // Schwimmerschalter oben
            S8      // Schwimmerschalter unten
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Nicht verwendete Parameter entfernen", Justification = "<Ausstehend>")]
        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, viewModel.pumpensteuerung.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, viewModel.pumpensteuerung.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S3, viewModel.pumpensteuerung.S3);
            S7.SetBitAt(digInput, (int)BitPosEingang.F5, viewModel.pumpensteuerung.F5);
            S7.SetBitAt(digInput, (int)BitPosEingang.S7, viewModel.pumpensteuerung.S7);
            S7.SetBitAt(digInput, (int)BitPosEingang.S8, viewModel.pumpensteuerung.S8);

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Nicht verwendete Parameter entfernen", Justification = "<Ausstehend>")]
        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            if (!mainWindow.DebugWindowAktiv)
            {
                viewModel.pumpensteuerung.K1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K1);
                viewModel.pumpensteuerung.H1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.H1);
                viewModel.pumpensteuerung.H2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.H2);
            }
        }

        public DatenRangieren(MainWindow mw, LAP_2010_5_Pumpensteuerung.ViewModel.PumpensteuerungViewModel vm)
        {
            mainWindow = mw;
            viewModel = vm;
        }
    }
}