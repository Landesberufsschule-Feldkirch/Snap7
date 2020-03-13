namespace StiegenhausBeleuchtung
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly ViewModel.StiegenhausBeleuchtungViewModel viewModel;

        private enum BitPosAusgang
        {
            P_01 = 0,   // Lampen im EG
            P_02,
            P_03,
            P_11,       // Lampen im OG 1
            P_12,
            P_13,
            P_21,       // Lampen im OG 2
            P_22,
            P_23,
            P_31,       // Lampen im OG 3
            P_32,
            P_33,
            P_41,       // Lampen im OG 4
            P_42,
            P_43
        }

        private enum BitPosEingang
        {
            B_01 = 0,   // Bewegungsmelder im EG
            B_02,
            B_03,
            B_11,       // Bewegungsmelder im OG 1
            B_12,
            B_13,
            B_21,       // Bewegungsmelder im OG 2
            B_22,
            B_23,
            B_31,       // Bewegungsmelder im OG 3
            B_32,
            B_33,
            B_41,       // Bewegungsmelder im OG 4
            B_42,
            B_43
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B_01, viewModel.stiegenhausBeleuchtung.B_01);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_01, viewModel.stiegenhausBeleuchtung.B_01);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_01, viewModel.stiegenhausBeleuchtung.B_01);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_11, viewModel.stiegenhausBeleuchtung.B_11);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_11, viewModel.stiegenhausBeleuchtung.B_11);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_11, viewModel.stiegenhausBeleuchtung.B_11);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_21, viewModel.stiegenhausBeleuchtung.B_21);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_21, viewModel.stiegenhausBeleuchtung.B_21);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_21, viewModel.stiegenhausBeleuchtung.B_21);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_31, viewModel.stiegenhausBeleuchtung.B_31);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_31, viewModel.stiegenhausBeleuchtung.B_31);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_31, viewModel.stiegenhausBeleuchtung.B_31);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_41, viewModel.stiegenhausBeleuchtung.B_41);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_41, viewModel.stiegenhausBeleuchtung.B_41);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_41, viewModel.stiegenhausBeleuchtung.B_41);
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            viewModel.stiegenhausBeleuchtung.P_01 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_01);
            viewModel.stiegenhausBeleuchtung.P_02 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_02);
            viewModel.stiegenhausBeleuchtung.P_03 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_03);
            viewModel.stiegenhausBeleuchtung.P_11 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_11);
            viewModel.stiegenhausBeleuchtung.P_12 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_12);
            viewModel.stiegenhausBeleuchtung.P_13 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_13);
            viewModel.stiegenhausBeleuchtung.P_21 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_21);
            viewModel.stiegenhausBeleuchtung.P_22 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_22);
            viewModel.stiegenhausBeleuchtung.P_23 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_23);
            viewModel.stiegenhausBeleuchtung.P_31 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_31);
            viewModel.stiegenhausBeleuchtung.P_32 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_32);
            viewModel.stiegenhausBeleuchtung.P_33 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_33);
            viewModel.stiegenhausBeleuchtung.P_41 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_41);
            viewModel.stiegenhausBeleuchtung.P_42 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_42);
            viewModel.stiegenhausBeleuchtung.P_43 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_43);
        }

        public DatenRangieren(ViewModel.StiegenhausBeleuchtungViewModel vm)
        {
            viewModel = vm;
        }
    }
}