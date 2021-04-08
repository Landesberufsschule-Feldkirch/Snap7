using PlcDatenTypen;
using Sharp7;

namespace LAP_2018_1_Silosteuerung
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _foerderanlageViewModel;

        private enum BitPosAusgang
        {
            P1 = 0, // Anlage Ein
            P2,     // Sammelstörung
            Q1,     // Motorschütz Förderband M1 
            Q2,     // Freigabe FU M2 (Schneckenförderer)
            Y1      // Mangnetventil
        }

        private enum BitPosEingang
        {
            B1 = 0, // Wagen vorhanden
            B2,     // Wagen voll
            F1,     // Motorschutzschalter Förderband
            F2,     // Motorschutzschalter Schneckenförderer
            S0,     // Taster Anlage Aus
            S1,     // Taster Anlage Ein
            S2,     // Not-Halt
            S3      // Störungen quittieren
        }

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _foerderanlageViewModel.Silosteuerung.B1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _foerderanlageViewModel.Silosteuerung.B2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _foerderanlageViewModel.Silosteuerung.F1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F2, _foerderanlageViewModel.Silosteuerung.F2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S0, _foerderanlageViewModel.Silosteuerung.S0);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _foerderanlageViewModel.Silosteuerung.S1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _foerderanlageViewModel.Silosteuerung.S2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _foerderanlageViewModel.Silosteuerung.S3);

            S7.SetSint_16_At(datenstruktur.AnalogInput, 0, Simatic.Analog_2_Int16(_foerderanlageViewModel.Silosteuerung.Silo.GetFuellstand(), 1));
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _foerderanlageViewModel.Silosteuerung.P1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _foerderanlageViewModel.Silosteuerung.P2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _foerderanlageViewModel.Silosteuerung.Q1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _foerderanlageViewModel.Silosteuerung.Q2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
            _foerderanlageViewModel.Silosteuerung.Y1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Y1);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _foerderanlageViewModel = vm;
    }
}