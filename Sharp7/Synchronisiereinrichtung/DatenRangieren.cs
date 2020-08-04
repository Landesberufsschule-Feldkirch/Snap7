using Sharp7;
using Synchronisiereinrichtung.kraftwerk.ViewModel;
using Utilities;

namespace Synchronisiereinrichtung
{
    public class DatenRangieren
    {
        private readonly ViewModel _viewModel;
        private readonly MainWindow _mainWindow;

        private enum BitPosAusgang
        {
            Q1 = 0
        }

        private enum BitPosEingang
        {
            S1 = 0, // Kraftwerk Starten
            S2      // Kraftwerk Stoppen
        }

        public DatenRangieren(MainWindow mw, ViewModel vm)
        {
            _mainWindow = mw;
            _viewModel = vm;
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, _viewModel.Kraftwerk.KraftwerkStarten);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, _viewModel.Kraftwerk.KraftwerkStoppen);

            S7.SetSint_16_At(anInput, 0, S7Analog.S7_Analog_2_Int16(_viewModel.Kraftwerk.GeneratorN, 10000));
            S7.SetSint_16_At(anInput, 2, S7Analog.S7_Analog_2_Int16(_viewModel.Kraftwerk.GeneratorU, 1000));
            S7.SetSint_16_At(anInput, 4, S7Analog.S7_Analog_2_Int16(_viewModel.Kraftwerk.GeneratorF, 100));
            S7.SetSint_16_At(anInput, 6, S7Analog.S7_Analog_2_Int16(_viewModel.Kraftwerk.GeneratorP, 10000));
            S7.SetSint_16_At(anInput, 8, S7Analog.S7_Analog_2_Int16(_viewModel.Kraftwerk.GeneratorCosPhi, 1));

            S7.SetSint_16_At(anInput, 10, S7Analog.S7_Analog_2_Int16(_viewModel.Kraftwerk.NetzU, 1000));
            S7.SetSint_16_At(anInput, 12, S7Analog.S7_Analog_2_Int16(_viewModel.Kraftwerk.NetzF, 100));
            S7.SetSint_16_At(anInput, 14, S7Analog.S7_Analog_2_Int16(_viewModel.Kraftwerk.NetzP, 10000));
            S7.SetSint_16_At(anInput, 16, S7Analog.S7_Analog_2_Int16(_viewModel.Kraftwerk.NetzCosPhi, 1));

            S7.SetSint_16_At(anInput, 18, S7Analog.S7_Analog_2_Int16(_viewModel.Kraftwerk.SpannungsdifferenzGeneratorNetz, 1000));
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            if (!_mainWindow.DebugWindowAktiv)
            {
                _viewModel.Kraftwerk.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
                _viewModel.Kraftwerk.VentilY = _viewModel.Kraftwerk.Generator.VentilRampe.GetWert(S7Analog.S7_Analog_2_Double(S7.GetSint_16_At(anOutput, 0), 100));
                _viewModel.Kraftwerk.GeneratorIe = _viewModel.Kraftwerk.Generator.ErregerstromRampe.GetWert(S7Analog.S7_Analog_2_Double(S7.GetSint_16_At(anOutput, 2), 10));
            }
        }
    }
}