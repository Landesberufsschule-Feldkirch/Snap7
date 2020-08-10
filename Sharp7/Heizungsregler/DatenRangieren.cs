﻿namespace Heizungsregler
{
    public class DatenRangieren
    {
        private readonly Heizungsregler.ViewModel.ViewModel _viewModel;
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

        public DatenRangieren(MainWindow mw, Heizungsregler.ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _viewModel = vm;
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            /*
            S7.SetBitAt(digInput, (int)BitPosEingang.S1, viewModel.Kraftwerk.KraftwerkStarten);
            S7.SetBitAt(digInput, (int)BitPosEingang.S2, viewModel.Kraftwerk.KraftwerkStoppen);

            S7.SetSint_16_At(anInput, 0, S7Analog.S7_Analog_2_Int16(viewModel.Kraftwerk.Generator_n, 10000));
            S7.SetSint_16_At(anInput, 2, S7Analog.S7_Analog_2_Int16(viewModel.Kraftwerk.Generator_U, 1000));
            S7.SetSint_16_At(anInput, 4, S7Analog.S7_Analog_2_Int16(viewModel.Kraftwerk.Generator_f, 100));
            S7.SetSint_16_At(anInput, 6, S7Analog.S7_Analog_2_Int16(viewModel.Kraftwerk.Generator_P, 10000));
            S7.SetSint_16_At(anInput, 8, S7Analog.S7_Analog_2_Int16(viewModel.Kraftwerk.Generator_CosPhi, 1));

            S7.SetSint_16_At(anInput, 10, S7Analog.S7_Analog_2_Int16(viewModel.Kraftwerk.Netz_U, 1000));
            S7.SetSint_16_At(anInput, 12, S7Analog.S7_Analog_2_Int16(viewModel.Kraftwerk.Netz_f, 100));
            S7.SetSint_16_At(anInput, 14, S7Analog.S7_Analog_2_Int16(viewModel.Kraftwerk.Netz_P, 10000));
            S7.SetSint_16_At(anInput, 16, S7Analog.S7_Analog_2_Int16(viewModel.Kraftwerk.Netz_CosPhi, 1));

            S7.SetSint_16_At(anInput, 18, S7Analog.S7_Analog_2_Int16(viewModel.Kraftwerk.SpannungsdifferenzGeneratorNetz, 1000));
            */
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            /*
            if (!mainWindow.DebugWindowAktiv)
            {
                viewModel.Kraftwerk.Q1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.Q1);
                viewModel.Kraftwerk.Ventil_Y = viewModel.Kraftwerk.generator.VentilRampe.GetWert(S7Analog.S7_Analog_2_Double(S7.GetSint_16_At(anOutput, 0), 100));
                viewModel.Kraftwerk.Generator_Ie = viewModel.Kraftwerk.generator.ErregerstromRampe.GetWert(S7Analog.S7_Analog_2_Double(S7.GetSint_16_At(anOutput, 2), 10));
            }
            */
        }
    }
}