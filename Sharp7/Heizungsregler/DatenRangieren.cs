using Sharp7;

namespace Heizungsregler
{
    public class DatenRangieren
    {
        private readonly Heizungsregler.ViewModel.ViewModel _viewModel;
        private readonly MainWindow _mainWindow;

        private enum BitPosAusgang
        {
            K1, // Ventil öffnen
            K2, // Ventil schliessen
            Q1, // Schütz Kesselpumpe
            Q2 // Öl-/Gasbrenner
        }

        private enum BitPosEingang
        {
            S1 = 0, // Kraftwerk Starten
            S2      // Kraftwerk Stoppen
        }



        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            /*
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, viewModel.Kraftwerk.KraftwerkStarten);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, viewModel.Kraftwerk.KraftwerkStoppen);

            S7.SetIntAt(anInput, 0, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Generator_n, 10000));
            S7.SetIntAt(anInput, 2, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Generator_U, 1000));
            S7.SetIntAt(anInput, 4, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Generator_f, 100));
            S7.SetIntAt(anInput, 6, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Generator_P, 10000));
            S7.SetIntAt(anInput, 8, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Generator_CosPhi, 1));

            S7.SetIntAt(anInput, 10, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Netz_U, 1000));
            S7.SetIntAt(anInput, 12, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Netz_f, 100));
            S7.SetIntAt(anInput, 14, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Netz_P, 10000));
            S7.SetIntAt(anInput, 16, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Netz_CosPhi, 1));

            S7.SetIntAt(anInput, 18, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.SpannungsdifferenzGeneratorNetz, 1000));
            */
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            if (_mainWindow.WohnHaus == null) return;

            _mainWindow.WohnHaus.HeizungsPumpe = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _mainWindow.WohnHaus.BrennerEin = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);



            if (_mainWindow.WohnHaus.DreiwegeVentil == null) return;

            _mainWindow.WohnHaus.DreiwegeVentil.VentilOeffnen(S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K1));
            _mainWindow.WohnHaus.DreiwegeVentil.VentilSchliessen(S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K2));
        }

        public DatenRangieren(MainWindow mw, Heizungsregler.ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _viewModel = vm;
        }

    }
}