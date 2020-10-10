using Sharp7;
using Utilities;

namespace LAP_2018_2_Abfuellanlage
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

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

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Abfuellanlage.B1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _viewModel.Abfuellanlage.F1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Abfuellanlage.S1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Abfuellanlage.S2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Abfuellanlage.S3);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S4, _viewModel.Abfuellanlage.S4);

            S7.SetUInt_8_At(datenstruktur.AnalogInput, 0, (byte)(_viewModel.Abfuellanlage.Pegel * 100.0));
            S7.SetSint_16_At(datenstruktur.AnalogInput, 2, S7Analog.S7_Analog_2_Int16(_viewModel.Abfuellanlage.Pegel, 1));
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _viewModel.Abfuellanlage.K1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K1);
            _viewModel.Abfuellanlage.K2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.K2);
            _viewModel.Abfuellanlage.P1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Abfuellanlage.P2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.Abfuellanlage.Q1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
        }

        public DatenRangieren(ViewModel.ViewModel vm)
        {
            _viewModel = vm;
        }
    }
}