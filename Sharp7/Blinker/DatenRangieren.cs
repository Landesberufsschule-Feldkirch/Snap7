﻿using Kommunikation;

namespace Blinker
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            P1 = 0
        }

        private enum BitPosEingang
        {
            S1 = 0,     // Frequenz weniger
            S2 = 1,     // f +
            S3 = 2,     // Tastverhältnis weniger
            S4 = 3,     // t +
            S5 = 4      // Reset
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Blinker.S1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Blinker.S2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Blinker.S3);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S4, _viewModel.Blinker.S4);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S5, _viewModel.Blinker.S5);
            }

            _viewModel.Blinker.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
        }
        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}