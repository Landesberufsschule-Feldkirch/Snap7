using Kommunikation;

namespace VoltmeterMitSiebenSegmentAnzeige
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BytePosition
        {
            Byte0 = 0,
            Byte1,
            Byte2,
            Byte3,
            Byte4,
            Byte5
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                //
            }


            _viewModel.Voltmeter.AlleVoltmeter[0] = _plc.GetUsIntAt(datenstruktur.DigOutput, (int)BytePosition.Byte0);    // Farbe
            _viewModel.Voltmeter.AlleVoltmeter[1] = _plc.GetUsIntAt(datenstruktur.DigOutput, (int)BytePosition.Byte1);    // 7-Segment Anzeige ganz rechts
            _viewModel.Voltmeter.AlleVoltmeter[2] = _plc.GetUsIntAt(datenstruktur.DigOutput, (int)BytePosition.Byte2);
            _viewModel.Voltmeter.AlleVoltmeter[3] = _plc.GetUsIntAt(datenstruktur.DigOutput, (int)BytePosition.Byte3);
            _viewModel.Voltmeter.AlleVoltmeter[4] = _plc.GetUsIntAt(datenstruktur.DigOutput, (int)BytePosition.Byte4);
            _viewModel.Voltmeter.AlleVoltmeter[5] = _plc.GetUsIntAt(datenstruktur.DigOutput, (int)BytePosition.Byte5);    // 7-Segment Anzeige ganz rechts
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}