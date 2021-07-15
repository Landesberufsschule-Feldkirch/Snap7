using Kommunikation;

namespace StiegenhausBeleuchtung
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            P01 = 0,    // Lampen im EG
            P02 = 1,
            P03 = 2,
            P04 = 3,
            P05 = 4,
            P11 = 5,    // Lampen im OG 1
            P12 = 6,
            P13 = 7,
            P14 = 8,
            P15 = 9,
            P21 = 10,   // Lampen im OG 2
            P22 = 11,
            P23 = 12,
            P24 = 13,
            P25 = 14,
            P31 = 15,   // Lampen im OG 3
            P32 = 16,
            P33 = 17,
            P34 = 18,
            P35 = 19,
            P41 = 20,   // Lampen im OG 4
            P42 = 21,
            P43 = 22,
            P44 = 23,
            P45 = 24
        }

        private enum BitPosEingang
        {
            B01 = 0,   // Bewegungsmelder im EG
            B02 = 1,
            B03 = 2,
            B04 = 3,
            B05 = 4,
            B11 = 5,    // Bewegungsmelder im OG 1
            B12 = 6,
            B13 = 7,
            B14 = 8,
            B15 = 9,
            B21 = 10,   // Bewegungsmelder im OG 2
            B22 = 11,
            B23 = 12,
            B24 = 13,
            B25 = 14,
            B31 = 15,   // Bewegungsmelder im OG 3
            B32 = 16,
            B33 = 17,
            B34 = 18,
            B35 = 19,
            B41 = 20,   // Bewegungsmelder im OG 4
            B42 = 21,
            B43 = 22,
            B44 = 23,
            B45 = 24
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B01, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(1));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B02, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(2));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B03, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(3));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B04, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(4));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B05, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(5));

                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B11, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(11));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B12, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(12));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B13, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(13));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B14, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(14));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B15, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(15));

                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B21, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(21));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B22, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(22));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B23, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(23));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B24, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(24));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B25, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(25));

                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B31, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(31));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B32, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(32));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B33, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(33));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B34, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(34));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B35, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(35));

                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B41, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(41));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B42, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(42));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B43, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(43));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B44, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(44));
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B45, _viewModel.StiegenhausBeleuchtung.GetBewegungsmelder(45));
            }

            _viewModel.StiegenhausBeleuchtung.SetLampen(1, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P01));
            _viewModel.StiegenhausBeleuchtung.SetLampen(2, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P02));
            _viewModel.StiegenhausBeleuchtung.SetLampen(3, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P03));
            _viewModel.StiegenhausBeleuchtung.SetLampen(4, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P04));
            _viewModel.StiegenhausBeleuchtung.SetLampen(5, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P05));

            _viewModel.StiegenhausBeleuchtung.SetLampen(11, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P11));
            _viewModel.StiegenhausBeleuchtung.SetLampen(12, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P12));
            _viewModel.StiegenhausBeleuchtung.SetLampen(13, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P13));
            _viewModel.StiegenhausBeleuchtung.SetLampen(14, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P14));
            _viewModel.StiegenhausBeleuchtung.SetLampen(15, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P15));

            _viewModel.StiegenhausBeleuchtung.SetLampen(21, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P21));
            _viewModel.StiegenhausBeleuchtung.SetLampen(22, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P22));
            _viewModel.StiegenhausBeleuchtung.SetLampen(23, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P23));
            _viewModel.StiegenhausBeleuchtung.SetLampen(24, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P24));
            _viewModel.StiegenhausBeleuchtung.SetLampen(25, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P25));

            _viewModel.StiegenhausBeleuchtung.SetLampen(31, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P31));
            _viewModel.StiegenhausBeleuchtung.SetLampen(32, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P32));
            _viewModel.StiegenhausBeleuchtung.SetLampen(33, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P33));
            _viewModel.StiegenhausBeleuchtung.SetLampen(34, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P34));
            _viewModel.StiegenhausBeleuchtung.SetLampen(35, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P35));

            _viewModel.StiegenhausBeleuchtung.SetLampen(41, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P41));
            _viewModel.StiegenhausBeleuchtung.SetLampen(42, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P42));
            _viewModel.StiegenhausBeleuchtung.SetLampen(43, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P43));
            _viewModel.StiegenhausBeleuchtung.SetLampen(44, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P44));
            _viewModel.StiegenhausBeleuchtung.SetLampen(45, _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P45));
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}