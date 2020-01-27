using Sharp7;
using Synchronisiereinrichtung.Kraftwerk.ViewModel;
using Utilities;

namespace Synchronisiereinrichtung
{
    public class DatenRangieren
    {
        private readonly KraftwerkViewModel viewModel;
        private readonly MainWindow mainWindow;
        private enum BitPosAusgang
        {
            Q1 = 0
        }

        private enum BitPosEingang
        {
            S1 = 0, // Kraftwerk Starten
            S2      // Kraftwerk Stoppen
        }

        public DatenRangieren(MainWindow mw, KraftwerkViewModel vm)
        {
            mainWindow = mw;
            viewModel = vm;
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {

            S7.SetBitAt(digInput, (int)BitPosEingang.S1, viewModel.Kraftwerk.KraftwerkStarten);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, viewModel.Kraftwerk.KraftwerkStoppen);

            S7.SetIntAt(anInput, 0, S7Analog.S7_Analog_2_Short(viewModel.Kraftwerk.Generator_n, 10000));
            S7.SetIntAt(anInput, 2, S7Analog.S7_Analog_2_Short(viewModel.Kraftwerk.Generator_U, 1000));
            S7.SetIntAt(anInput, 4, S7Analog.S7_Analog_2_Short(viewModel.Kraftwerk.Generator_f, 100));
            S7.SetIntAt(anInput, 6, S7Analog.S7_Analog_2_Short(viewModel.Kraftwerk.Generator_P, 1000));
            S7.SetIntAt(anInput, 8, S7Analog.S7_Analog_2_Short(viewModel.Kraftwerk.Generator_CosPhi, 1));

            S7.SetIntAt(anInput, 10, S7Analog.S7_Analog_2_Short(viewModel.Kraftwerk.Netz_U, 1000));
            S7.SetIntAt(anInput, 12, S7Analog.S7_Analog_2_Short(viewModel.Kraftwerk.Netz_f, 100));
            S7.SetIntAt(anInput, 14, S7Analog.S7_Analog_2_Short(viewModel.Kraftwerk.Netz_P, 1000));
            S7.SetIntAt(anInput, 16, S7Analog.S7_Analog_2_Short(viewModel.Kraftwerk.Netz_CosPhi, 1));

            S7.SetIntAt(anInput, 18, S7Analog.S7_Analog_2_Short(viewModel.Kraftwerk.SpannungsdifferenzGeneratorNetz, 1000));
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            if (!mainWindow.DebugWindowAktiv)
            {
                viewModel.Kraftwerk.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);

                viewModel.Kraftwerk.Ventil_Y = S7.GetSint16At(anOutput, 0);
                viewModel.Kraftwerk.Generator_Ie = S7.GetSint16At(anOutput, 2);
            }
        }
    }
}