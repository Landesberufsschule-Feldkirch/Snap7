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
            S7.SetBitAt(digInput, (int)BitPosEingang.B_01, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(1));
            S7.SetBitAt(digInput, (int)BitPosEingang.B_02, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(2));
            S7.SetBitAt(digInput, (int)BitPosEingang.B_03, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(3));
            S7.SetBitAt(digInput, (int)BitPosEingang.B_11, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(11));
            S7.SetBitAt(digInput, (int)BitPosEingang.B_12, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(12));
            S7.SetBitAt(digInput, (int)BitPosEingang.B_13, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(13));
            S7.SetBitAt(digInput, (int)BitPosEingang.B_21, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(21));
            S7.SetBitAt(digInput, (int)BitPosEingang.B_22, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(22));
            S7.SetBitAt(digInput, (int)BitPosEingang.B_23, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(23));
            S7.SetBitAt(digInput, (int)BitPosEingang.B_31, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(31));
            S7.SetBitAt(digInput, (int)BitPosEingang.B_32, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(32));
            S7.SetBitAt(digInput, (int)BitPosEingang.B_33, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(33));
            S7.SetBitAt(digInput, (int)BitPosEingang.B_41, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(41));
            S7.SetBitAt(digInput, (int)BitPosEingang.B_42, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(42));
            S7.SetBitAt(digInput, (int)BitPosEingang.B_43, viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(43));
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            viewModel.stiegenhausBeleuchtung.SetLampen(1, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_01));
            viewModel.stiegenhausBeleuchtung.SetLampen(2, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_02));
            viewModel.stiegenhausBeleuchtung.SetLampen(3, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_03));
            viewModel.stiegenhausBeleuchtung.SetLampen(11, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_11));
            viewModel.stiegenhausBeleuchtung.SetLampen(12, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_12));
            viewModel.stiegenhausBeleuchtung.SetLampen(13, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_13));
            viewModel.stiegenhausBeleuchtung.SetLampen(21, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_21));
            viewModel.stiegenhausBeleuchtung.SetLampen(22, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_22));
            viewModel.stiegenhausBeleuchtung.SetLampen(23, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_23));
            viewModel.stiegenhausBeleuchtung.SetLampen(31, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_31));
            viewModel.stiegenhausBeleuchtung.SetLampen(32, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_32));
            viewModel.stiegenhausBeleuchtung.SetLampen(33, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_33));
            viewModel.stiegenhausBeleuchtung.SetLampen(41, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_41));
            viewModel.stiegenhausBeleuchtung.SetLampen(42, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_42));
            viewModel.stiegenhausBeleuchtung.SetLampen(43, S7.GetBitAt(digOutput, (int)BitPosAusgang.P_43));
        }

        public DatenRangieren(ViewModel.StiegenhausBeleuchtungViewModel vm)
        {
            viewModel = vm;
        }
    }
}