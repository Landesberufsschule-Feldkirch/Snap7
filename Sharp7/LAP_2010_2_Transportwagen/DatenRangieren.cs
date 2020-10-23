﻿using Sharp7;

namespace LAP_2010_2_Transportwagen
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _transportwagenViewModel;

        private enum BitPosAusgang
        {
            P1 = 0, // Störung
            Q1,     // Motor LL
            Q2      // Motor RL
        }

        private enum BitPosEingang
        {
            B1 = 0, // Endschalter Links
            B2,     // Endschalter Rechts
            F1,     // Thermorelais
            S1,     // Taster "Start"
            S2,     // NotHalt
            S3      // Taster Reset
        }

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _transportwagenViewModel.Transportwagen.B1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _transportwagenViewModel.Transportwagen.B2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _transportwagenViewModel.Transportwagen.F1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _transportwagenViewModel.Transportwagen.S1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _transportwagenViewModel.Transportwagen.S2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _transportwagenViewModel.Transportwagen.S3);
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _transportwagenViewModel.Transportwagen.P1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _transportwagenViewModel.Transportwagen.Q1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _transportwagenViewModel.Transportwagen.Q2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _transportwagenViewModel = vm;
    }
}