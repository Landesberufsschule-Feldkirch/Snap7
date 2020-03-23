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


        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B_01, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[1]);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_02, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[2]);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_03, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[3]);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_11, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[11]);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_12, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[12]);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_13, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[13]);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_21, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[21]);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_22, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[22]);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_23, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[23]);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_31, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[31]);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_32, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[32]);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_33, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[33]);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_41, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[41]);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_42, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[42]);
            S7.SetBitAt(digInput, (int)BitPosEingang.B_43, viewModel.stiegenhausBeleuchtung.AlleBewegungsmelder[43]);
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            viewModel.stiegenhausBeleuchtung.AlleLampen[1] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_01);
            viewModel.stiegenhausBeleuchtung.AlleLampen[2] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_02);
            viewModel.stiegenhausBeleuchtung.AlleLampen[3] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_03);
            viewModel.stiegenhausBeleuchtung.AlleLampen[11] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_11);
            viewModel.stiegenhausBeleuchtung.AlleLampen[12] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_12);
            viewModel.stiegenhausBeleuchtung.AlleLampen[13] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_13);
            viewModel.stiegenhausBeleuchtung.AlleLampen[21] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_21);
            viewModel.stiegenhausBeleuchtung.AlleLampen[22] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_22);
            viewModel.stiegenhausBeleuchtung.AlleLampen[23] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_23);
            viewModel.stiegenhausBeleuchtung.AlleLampen[31] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_31);
            viewModel.stiegenhausBeleuchtung.AlleLampen[32] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_32);
            viewModel.stiegenhausBeleuchtung.AlleLampen[33] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_33);
            viewModel.stiegenhausBeleuchtung.AlleLampen[41] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_41);
            viewModel.stiegenhausBeleuchtung.AlleLampen[42] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_42);
            viewModel.stiegenhausBeleuchtung.AlleLampen[43] = S7.GetBitAt(digOutput, (int)BitPosAusgang.P_43);
        }

        public DatenRangieren(ViewModel.StiegenhausBeleuchtungViewModel vm)
        {
            viewModel = vm;
        }
    }
}