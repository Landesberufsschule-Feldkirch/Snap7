using Kommunikation;
using PlcDatenTypen;

namespace LAP_2019_Foerderanlage
{
    public class DatenRangieren
    {
        private readonly MainWindow _mainWindow;
        private readonly ViewModel.ViewModel _foerderanlageViewModel;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            K1 = 0,     // Materialschieber Silo
            P1 = 1,     // Anlage Ein
            P2 = 2,     //Sammelstörung
            Q1 = 3,     //Förderband Rechtslauf
            Q2 = 4,     // Förderband Linkslauf
            T1 = 5       // Freigabe FU (Schneckenförderer)

        }

        private enum BitPosEingang
        {
            B1 = 0,     // Wagen Position rechts
            B2 = 1,     // Wagen voll
            F1 = 2,     // Störung Motorschutzschalter
            S0 = 3,     // Anlage Aus
            S1 = 4,     // Anlage Ein
            S2 = 5,     // Not-Halt
            S3 = 6,     // Schalter Automatikbetrieb
            S4 = 7,     // Schalter Handbetrieb
            S5 = 8,     // Handbetrieb Förderband RL
            S6 = 9,     // Handbetrieb Förderband LL
            S7 = 10,    // Handbetrieb Schneckenförderer
            S8 = 11      // Handbetrieb Materialschieber

        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _foerderanlageViewModel.Foerderanlage.B1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _foerderanlageViewModel.Foerderanlage.B2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _foerderanlageViewModel.Foerderanlage.F1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S0, _foerderanlageViewModel.Foerderanlage.S0);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _foerderanlageViewModel.Foerderanlage.S1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _foerderanlageViewModel.Foerderanlage.S2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _foerderanlageViewModel.Foerderanlage.S3);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S4, _foerderanlageViewModel.Foerderanlage.S4);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S5, _foerderanlageViewModel.Foerderanlage.S5);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S6, _foerderanlageViewModel.Foerderanlage.S6);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S7, _foerderanlageViewModel.Foerderanlage.S7);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S8, _foerderanlageViewModel.Foerderanlage.S8);

                _plc.SetIntAt(datenstruktur.AnalogInput, 0, Simatic.Analog_2_Int16(_foerderanlageViewModel.Foerderanlage.Silo.GetFuellstand(), 1));
            }

            if (_mainWindow.DebugWindowAktiv) return;

            _foerderanlageViewModel.Foerderanlage.K1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K1);
            _foerderanlageViewModel.Foerderanlage.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _foerderanlageViewModel.Foerderanlage.P2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _foerderanlageViewModel.Foerderanlage.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _foerderanlageViewModel.Foerderanlage.Q2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
            _foerderanlageViewModel.Foerderanlage.T1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.T1);
        }

        public DatenRangieren(MainWindow mw, ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _foerderanlageViewModel = vm;
        }
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}