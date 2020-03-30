using Sharp7;

namespace LAP_2010_4_Abfuellanlage
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private readonly ViewModel.ViewModel abfuellanlageViewModel;

        private enum BitPosAusgang
        {
            K1 = 0, // Förderband
            K2,     // Vereinzelner
            K3,     // Füllen
            H4      // Behälter leer
        }

        private enum BitPosEingang
        {
            S1 = 0, // Taster Reset
            S2,     // Taster Start
            S7,     // Füllstand
            S8      // Position Dose
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, abfuellanlageViewModel.abfuellAnlage.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, abfuellanlageViewModel.abfuellAnlage.S2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S7, abfuellanlageViewModel.abfuellAnlage.S7);
            S7.SetBitAt(digInput, (int)BitPosEingang.S8, abfuellanlageViewModel.abfuellAnlage.S8);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            if (!mainWindow.DebugWindowAktiv)
            {
                abfuellanlageViewModel.abfuellAnlage.K1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K1);
                abfuellanlageViewModel.abfuellAnlage.K2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K2);
                abfuellanlageViewModel.abfuellAnlage.K3 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K3);
                abfuellanlageViewModel.abfuellAnlage.H4 = S7.GetBitAt(digOutput, (int)BitPosAusgang.H4);
            }
        }

        public DatenRangieren(MainWindow mw, LAP_2010_4_Abfuellanlage.ViewModel.ViewModel vm)
        {
            mainWindow = mw;
            abfuellanlageViewModel = vm;
        }
    }
}