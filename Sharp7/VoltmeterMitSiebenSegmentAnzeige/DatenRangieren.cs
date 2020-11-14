namespace VoltmeterMitSiebenSegmentAnzeige
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

        private enum BytePosition
        {
            Byte0 = 0,
            Byte1,
            Byte2,
            Byte3,
            Byte4
        }

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
           //
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _viewModel.Voltmeter.AlleVoltmeter[0] = S7.GetUint8At(datenstruktur.DigOutput, (int)BytePosition.Byte0);
            _viewModel.Voltmeter.AlleVoltmeter[1] = S7.GetUint8At(datenstruktur.DigOutput, (int)BytePosition.Byte1);
            _viewModel.Voltmeter.AlleVoltmeter[2] = S7.GetUint8At(datenstruktur.DigOutput, (int)BytePosition.Byte2);
            _viewModel.Voltmeter.AlleVoltmeter[3] = S7.GetUint8At(datenstruktur.DigOutput, (int)BytePosition.Byte3);
            _viewModel.Voltmeter.AlleVoltmeter[4] = S7.GetUint8At(datenstruktur.DigOutput, (int)BytePosition.Byte4);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    }
}