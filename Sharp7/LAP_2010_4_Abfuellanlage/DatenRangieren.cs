using Sharp7;

namespace LAP_2010_4_Abfuellanlage
{
    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private readonly ViewModel.ViewModel abfuellanlageViewModel;

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
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, abfuellanlageViewModel.abfuellAnlage.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, abfuellanlageViewModel.abfuellAnlage.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, abfuellanlageViewModel.abfuellAnlage.S1);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, abfuellanlageViewModel.abfuellAnlage.S2);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            if (!mainWindow.DebugWindowAktiv)
            {
                abfuellanlageViewModel.abfuellAnlage.K1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K1);
                abfuellanlageViewModel.abfuellAnlage.K2 = S7.GetBitAt(digOutput, (int)BitPosAusgang.K2);
                abfuellanlageViewModel.abfuellAnlage.P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
                abfuellanlageViewModel.abfuellAnlage.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
            }
        }

        public DatenRangieren(MainWindow mw, LAP_2010_4_Abfuellanlage.ViewModel.ViewModel vm)
        {
            mainWindow = mw;
            abfuellanlageViewModel = vm;
        }
    }
}