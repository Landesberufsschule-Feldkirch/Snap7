using Kommunikation;
using PlcDatenTypen;

namespace Synchronisiereinrichtung
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private readonly MainWindow _mainWindow;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            Q1 = 0
        }

        private enum BitPosEingang
        {
            S1 = 0, // Kraftwerk Starten
            S2      // Kraftwerk Stoppen
        }

        public DatenRangieren(MainWindow mw, ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _viewModel = vm;
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Kraftwerk.KraftwerkStarten);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Kraftwerk.KraftwerkStoppen);

                _plc.SetIntAt(datenstruktur.AnalogInput, 0, Simatic.Analog_2_Int16(_viewModel.Kraftwerk.GeneratorN, 10000));
                _plc.SetIntAt(datenstruktur.AnalogInput, 2, Simatic.Analog_2_Int16(_viewModel.Kraftwerk.GeneratorU, 1000));
                _plc.SetIntAt(datenstruktur.AnalogInput, 4, Simatic.Analog_2_Int16(_viewModel.Kraftwerk.GeneratorF, 100));
                _plc.SetIntAt(datenstruktur.AnalogInput, 6, Simatic.Analog_2_Int16(_viewModel.Kraftwerk.GeneratorP, 10000));
                _plc.SetIntAt(datenstruktur.AnalogInput, 8, Simatic.Analog_2_Int16(_viewModel.Kraftwerk.GeneratorCosPhi, 1));

                _plc.SetIntAt(datenstruktur.AnalogInput, 10, Simatic.Analog_2_Int16(_viewModel.Kraftwerk.NetzU, 1000));
                _plc.SetIntAt(datenstruktur.AnalogInput, 12, Simatic.Analog_2_Int16(_viewModel.Kraftwerk.NetzF, 100));
                _plc.SetIntAt(datenstruktur.AnalogInput, 14, Simatic.Analog_2_Int16(_viewModel.Kraftwerk.NetzP, 10000));
                _plc.SetIntAt(datenstruktur.AnalogInput, 16, Simatic.Analog_2_Int16(_viewModel.Kraftwerk.NetzCosPhi, 1));

                _plc.SetIntAt(datenstruktur.AnalogInput, 18, Simatic.Analog_2_Int16(_viewModel.Kraftwerk.SpannungsdifferenzGeneratorNetz, 1000));
            }

            if (_mainWindow.DebugWindowAktiv) return;

            _viewModel.Kraftwerk.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.Kraftwerk.VentilY = _viewModel.Kraftwerk.Generator.VentilRampe.GetWert(Simatic.Analog_2_Double(_plc.GetIntAt(datenstruktur.AnalogOutput, 0), 100));
            _viewModel.Kraftwerk.GeneratorIe = _viewModel.Kraftwerk.Generator.ErregerstromRampe.GetWert(Simatic.Analog_2_Double(_plc.GetIntAt(datenstruktur.AnalogOutput, 2), 10));
        }
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}