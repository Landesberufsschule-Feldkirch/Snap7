using Kommunikation;
using PlcDatenTypen;

namespace LAP_2018_1_Silosteuerung
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _foerderanlageViewModel;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            P1 = 0,     // Anlage Ein
            P2 = 1,     // Sammelstörung
            Q1 = 2,     // Motorschütz Förderband M1 
            Q2 = 3,     // Freigabe FU M2 (Schneckenförderer)
            Y1 = 4      // Mangnetventil

        }

        private enum BitPosEingang
        {
            B1 = 0,     // Wagen vorhanden
            B2 = 1,     // Wagen voll
            F1 = 2,     // Motorschutzschalter Förderband
            F2 = 3,     // Motorschutzschalter Schneckenförderer
            S0 = 4,     // Taster Anlage Aus
            S1 = 5,     // Taster Anlage Ein
            S2 = 6,     // Not-Halt
            S3 = 7      // Störungen quittieren

        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _foerderanlageViewModel.Silosteuerung.B1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _foerderanlageViewModel.Silosteuerung.B2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _foerderanlageViewModel.Silosteuerung.F1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F2, _foerderanlageViewModel.Silosteuerung.F2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S0, _foerderanlageViewModel.Silosteuerung.S0);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _foerderanlageViewModel.Silosteuerung.S1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _foerderanlageViewModel.Silosteuerung.S2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _foerderanlageViewModel.Silosteuerung.S3);

                _plc.SetIntAt(datenstruktur.AnalogInput, 0, Simatic.Analog_2_Int16(_foerderanlageViewModel.Silosteuerung.Silo.GetFuellstand(), 1));
            }

            _foerderanlageViewModel.Silosteuerung.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _foerderanlageViewModel.Silosteuerung.P2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _foerderanlageViewModel.Silosteuerung.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _foerderanlageViewModel.Silosteuerung.Q2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
            _foerderanlageViewModel.Silosteuerung.Y1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Y1);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _foerderanlageViewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}