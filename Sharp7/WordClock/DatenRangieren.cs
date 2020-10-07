﻿namespace WordClock
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

        private enum BytePosition
        {
            Byte0 = 0,
            Byte2 = 2,
            Byte3,
            Byte4,
            Byte5,
            Byte6,
            Byte7,
            Byte8
        }

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetUint_16_At(datenstruktur.DigInput, (int)BytePosition.Byte0, _viewModel.Zeiten.DatumJahr);
            S7.SetUInt_8_At(datenstruktur.DigInput, (int)BytePosition.Byte2, _viewModel.Zeiten.DatumMonat);
            S7.SetUInt_8_At(datenstruktur.DigInput, (int)BytePosition.Byte3, _viewModel.Zeiten.DatumTag);
            S7.SetUInt_8_At(datenstruktur.DigInput, (int)BytePosition.Byte4, _viewModel.Zeiten.DatumWochentag);
            S7.SetUInt_8_At(datenstruktur.DigInput, (int)BytePosition.Byte5, _viewModel.Zeiten.Stunde);
            S7.SetUInt_8_At(datenstruktur.DigInput, (int)BytePosition.Byte6, _viewModel.Zeiten.Minute);
            S7.SetUInt_8_At(datenstruktur.DigInput, (int)BytePosition.Byte7, _viewModel.Zeiten.Sekunde);
            S7.SetUInt_8_At(datenstruktur.DigInput, (int)BytePosition.Byte8, 0);
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            //
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    }
}