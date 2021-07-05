using Kommunikation;

namespace Heizungsregler
{
    public class DatenRangieren
    {
        private readonly Heizungsregler.ViewModel.ViewModel _viewModel;
        private readonly MainWindow _mainWindow;
        private IPlc _plc;

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
        
        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                /*
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, viewModel.Kraftwerk.KraftwerkStarten);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, viewModel.Kraftwerk.KraftwerkStoppen);

                _plc.SetIntAt(anInput, 0, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Generator_n, 10000));
                _plc.SetIntAt(anInput, 2, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Generator_U, 1000));
                _plc.SetIntAt(anInput, 4, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Generator_f, 100));
                _plc.SetIntAt(anInput, 6, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Generator_P, 10000));
                _plc.SetIntAt(anInput, 8, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Generator_CosPhi, 1));

                _plc.SetIntAt(anInput, 10, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Netz_U, 1000));
                _plc.SetIntAt(anInput, 12, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Netz_f, 100));
                _plc.SetIntAt(anInput, 14, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Netz_P, 10000));
                _plc.SetIntAt(anInput, 16, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.Netz_CosPhi, 1));

                _plc.SetIntAt(anInput, 18, Simatic.Simatic_Analog_2_Int16(viewModel.Kraftwerk.SpannungsdifferenzGeneratorNetz, 1000));
                */
            }

            if (_mainWindow.WohnHaus == null) return;

            _mainWindow.WohnHaus.HeizungsPumpe = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _mainWindow.WohnHaus.BrennerEin = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
            

            if (_mainWindow.WohnHaus.DreiwegeVentil == null) return;

            _mainWindow.WohnHaus.DreiwegeVentil.VentilOeffnen(_plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K1));
            _mainWindow.WohnHaus.DreiwegeVentil.VentilSchliessen(_plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K2));
        }

        public DatenRangieren(MainWindow mw, Heizungsregler.ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _viewModel = vm;
        }
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}