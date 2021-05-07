using Kommunikation;
using PlcDatenTypen;
using Sharp7;

namespace LAP_2018_2_Abfuellanlage
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            K1 = 0,
            K2,
            P1,
            P2,
            Q1
        }

        private enum BitPosEingang
        {
            B1 = 0,
            F1,
            S1,
            S2,
            S3,
            S4
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Abfuellanlage.B1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _viewModel.Abfuellanlage.F1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Abfuellanlage.S1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Abfuellanlage.S2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Abfuellanlage.S3);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S4, _viewModel.Abfuellanlage.S4);

                _plc.SetIntAt(datenstruktur.AnalogInput, 0, Simatic.Analog_2_Int16(_viewModel.Abfuellanlage.Pegel, 1));
            }


            _viewModel.Abfuellanlage.K1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K1);
            _viewModel.Abfuellanlage.K2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K2);
            _viewModel.Abfuellanlage.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Abfuellanlage.P2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.Abfuellanlage.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}