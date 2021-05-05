using Kommunikation;
using Sharp7;

namespace LAP_2010_4_Abfuellanlage
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _abfuellanlageViewModel;
        private IPlc _plc;

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

        public void Rangieren(Kommunikation.Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _abfuellanlageViewModel.AbfuellAnlage.B1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _abfuellanlageViewModel.AbfuellAnlage.B2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _abfuellanlageViewModel.AbfuellAnlage.S1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _abfuellanlageViewModel.AbfuellAnlage.S2);
            }

            _abfuellanlageViewModel.AbfuellAnlage.K1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K1);
            _abfuellanlageViewModel.AbfuellAnlage.K2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K2);
            _abfuellanlageViewModel.AbfuellAnlage.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _abfuellanlageViewModel.AbfuellAnlage.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
        }

        public DatenRangieren(LAP_2010_4_Abfuellanlage.ViewModel.ViewModel vm) => _abfuellanlageViewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}