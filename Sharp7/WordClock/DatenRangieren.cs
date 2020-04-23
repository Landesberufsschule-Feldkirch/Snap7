namespace WordClock
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel viewModel;

        private enum BytePosition
        {
            Byte_0 = 0,
            Byte_1,
            Byte_2,
            Byte_3,
            Byte_4,
            Byte_5,
            Byte_6,
            Byte_7,
            Byte_8
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetUint_16_At(digInput, (int)BytePosition.Byte_0, viewModel.Zeiten.DatumJahr);
            S7.SetUInt_8_At(digInput, (int)BytePosition.Byte_2, viewModel.Zeiten.DatumMonat);
            S7.SetUInt_8_At(digInput, (int)BytePosition.Byte_3, viewModel.Zeiten.DatumTag);
            S7.SetUInt_8_At(digInput, (int)BytePosition.Byte_4, viewModel.Zeiten.DatumWochentag);
            S7.SetUInt_8_At(digInput, (int)BytePosition.Byte_5, viewModel.Zeiten.Stunde);
            S7.SetUInt_8_At(digInput, (int)BytePosition.Byte_6, viewModel.Zeiten.Minute);
            S7.SetUInt_8_At(digInput, (int)BytePosition.Byte_7, viewModel.Zeiten.Sekunde);
            S7.SetUInt_8_At(digInput, (int)BytePosition.Byte_8, 0);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            //
        }

        public DatenRangieren(ViewModel.ViewModel vm)
        {
            viewModel = vm;
        }
    }
}