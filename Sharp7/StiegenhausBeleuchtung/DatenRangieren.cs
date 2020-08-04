namespace StiegenhausBeleuchtung
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            P01 = 0,   // Lampen im EG
            P02,
            P03,
            P04,
            P05,
            P11,       // Lampen im OG 1
            P12,
            P13,
            P14,
            P15,
            P21,       // Lampen im OG 2
            P22,
            P23,
            P24,
            P25,
            P31,       // Lampen im OG 3
            P32,
            P33,
            P34,
            P35,
            P41,       // Lampen im OG 4
            P42,
            P43,
            P44,
            P45
        }

        private enum BitPosEingang
        {
            B01 = 0,   // Bewegungsmelder im EG
            B02,
            B03,
            B04,
            B05,
            B11,       // Bewegungsmelder im OG 1
            B12,
            B13,
            B14,
            B15,
            B21,       // Bewegungsmelder im OG 2
            B22,
            B23,
            B24,
            B25,
            B31,       // Bewegungsmelder im OG 3
            B32,
            B33,
            B34,
            B35,
            B41,       // Bewegungsmelder im OG 4
            B42,
            B43,
            B44,
            B45
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B01, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(1));
            S7.SetBitAt(digInput, (int)BitPosEingang.B02, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(2));
            S7.SetBitAt(digInput, (int)BitPosEingang.B03, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(3));
            S7.SetBitAt(digInput, (int)BitPosEingang.B04, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(4));
            S7.SetBitAt(digInput, (int)BitPosEingang.B05, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(5));

            S7.SetBitAt(digInput, (int)BitPosEingang.B11, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(11));
            S7.SetBitAt(digInput, (int)BitPosEingang.B12, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(12));
            S7.SetBitAt(digInput, (int)BitPosEingang.B13, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(13));
            S7.SetBitAt(digInput, (int)BitPosEingang.B14, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(14));
            S7.SetBitAt(digInput, (int)BitPosEingang.B15, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(15));
            
            S7.SetBitAt(digInput, (int)BitPosEingang.B21, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(21));
            S7.SetBitAt(digInput, (int)BitPosEingang.B22, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(22));
            S7.SetBitAt(digInput, (int)BitPosEingang.B23, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(23));
            S7.SetBitAt(digInput, (int)BitPosEingang.B24, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(24));
            S7.SetBitAt(digInput, (int)BitPosEingang.B25, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(25));

            S7.SetBitAt(digInput, (int)BitPosEingang.B31, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(31));
            S7.SetBitAt(digInput, (int)BitPosEingang.B32, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(32));
            S7.SetBitAt(digInput, (int)BitPosEingang.B33, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(33));
            S7.SetBitAt(digInput, (int)BitPosEingang.B34, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(34));
            S7.SetBitAt(digInput, (int)BitPosEingang.B35, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(35));

            S7.SetBitAt(digInput, (int)BitPosEingang.B41, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(41));
            S7.SetBitAt(digInput, (int)BitPosEingang.B42, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(42));
            S7.SetBitAt(digInput, (int)BitPosEingang.B43, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(43));
            S7.SetBitAt(digInput, (int)BitPosEingang.B44, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(44));
            S7.SetBitAt(digInput, (int)BitPosEingang.B45, _viewModel.stiegenhausBeleuchtung.GetBewegungsmelder(45));
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
            _viewModel.stiegenhausBeleuchtung.SetLampen(1, S7.GetBitAt(digOutput, (int)BitPosAusgang.P01));
            _viewModel.stiegenhausBeleuchtung.SetLampen(2, S7.GetBitAt(digOutput, (int)BitPosAusgang.P02));
            _viewModel.stiegenhausBeleuchtung.SetLampen(3, S7.GetBitAt(digOutput, (int)BitPosAusgang.P03));
            _viewModel.stiegenhausBeleuchtung.SetLampen(4, S7.GetBitAt(digOutput, (int)BitPosAusgang.P04));
            _viewModel.stiegenhausBeleuchtung.SetLampen(5, S7.GetBitAt(digOutput, (int)BitPosAusgang.P05));

            _viewModel.stiegenhausBeleuchtung.SetLampen(11, S7.GetBitAt(digOutput, (int)BitPosAusgang.P11));
            _viewModel.stiegenhausBeleuchtung.SetLampen(12, S7.GetBitAt(digOutput, (int)BitPosAusgang.P12));
            _viewModel.stiegenhausBeleuchtung.SetLampen(13, S7.GetBitAt(digOutput, (int)BitPosAusgang.P13));
            _viewModel.stiegenhausBeleuchtung.SetLampen(14, S7.GetBitAt(digOutput, (int)BitPosAusgang.P14));
            _viewModel.stiegenhausBeleuchtung.SetLampen(15, S7.GetBitAt(digOutput, (int)BitPosAusgang.P15));

            _viewModel.stiegenhausBeleuchtung.SetLampen(21, S7.GetBitAt(digOutput, (int)BitPosAusgang.P21));
            _viewModel.stiegenhausBeleuchtung.SetLampen(22, S7.GetBitAt(digOutput, (int)BitPosAusgang.P22));
            _viewModel.stiegenhausBeleuchtung.SetLampen(23, S7.GetBitAt(digOutput, (int)BitPosAusgang.P23));
            _viewModel.stiegenhausBeleuchtung.SetLampen(24, S7.GetBitAt(digOutput, (int)BitPosAusgang.P24));
            _viewModel.stiegenhausBeleuchtung.SetLampen(25, S7.GetBitAt(digOutput, (int)BitPosAusgang.P25));

            _viewModel.stiegenhausBeleuchtung.SetLampen(31, S7.GetBitAt(digOutput, (int)BitPosAusgang.P31));
            _viewModel.stiegenhausBeleuchtung.SetLampen(32, S7.GetBitAt(digOutput, (int)BitPosAusgang.P32));
            _viewModel.stiegenhausBeleuchtung.SetLampen(33, S7.GetBitAt(digOutput, (int)BitPosAusgang.P33));
            _viewModel.stiegenhausBeleuchtung.SetLampen(34, S7.GetBitAt(digOutput, (int)BitPosAusgang.P34));
            _viewModel.stiegenhausBeleuchtung.SetLampen(35, S7.GetBitAt(digOutput, (int)BitPosAusgang.P35));

            _viewModel.stiegenhausBeleuchtung.SetLampen(41, S7.GetBitAt(digOutput, (int)BitPosAusgang.P41));
            _viewModel.stiegenhausBeleuchtung.SetLampen(42, S7.GetBitAt(digOutput, (int)BitPosAusgang.P42));
            _viewModel.stiegenhausBeleuchtung.SetLampen(43, S7.GetBitAt(digOutput, (int)BitPosAusgang.P43));
            _viewModel.stiegenhausBeleuchtung.SetLampen(44, S7.GetBitAt(digOutput, (int)BitPosAusgang.P44));
            _viewModel.stiegenhausBeleuchtung.SetLampen(45, S7.GetBitAt(digOutput, (int)BitPosAusgang.P45));
        }

        public DatenRangieren(ViewModel.ViewModel vm)
        {
            _viewModel = vm;
        }
    }
}