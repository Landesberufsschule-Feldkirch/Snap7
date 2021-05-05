using Kommunikation;

namespace WordClock
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

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

        public void Rangieren(Kommunikation.Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetUIntAt(datenstruktur.DigInput, (int)BytePosition.Byte0, _viewModel.Zeiten.DatumJahr);
                _plc.SetUsIntAt(datenstruktur.DigInput, (int)BytePosition.Byte2, _viewModel.Zeiten.DatumMonat);
                _plc.SetUsIntAt(datenstruktur.DigInput, (int)BytePosition.Byte3, _viewModel.Zeiten.DatumTag);
                _plc.SetUsIntAt(datenstruktur.DigInput, (int)BytePosition.Byte4, _viewModel.Zeiten.DatumWochentag);
                _plc.SetUsIntAt(datenstruktur.DigInput, (int)BytePosition.Byte5, _viewModel.Zeiten.Stunde);
                _plc.SetUsIntAt(datenstruktur.DigInput, (int)BytePosition.Byte6, _viewModel.Zeiten.Minute);
                _plc.SetUsIntAt(datenstruktur.DigInput, (int)BytePosition.Byte7, _viewModel.Zeiten.Sekunde);
                _plc.SetUsIntAt(datenstruktur.DigInput, (int)BytePosition.Byte8, 0);
            }


        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}