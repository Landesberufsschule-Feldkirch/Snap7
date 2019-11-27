using System;
using System.Threading.Tasks;
using Sharp7;

namespace LAP_2018_Niveauregelung
{
    public partial class MainWindow
    {
        public bool B1 = false;
        public bool B2 = false;
        public bool B3 = false;
        public bool F1 = true;
        public bool F2 = true;
        public bool M1 = false;
        public bool M2 = false;
        public bool Y1 = false;

        enum Datenbausteine
        {
            Input = 1,
            Output = 2
        }
        enum BytePosition
        {
            Byte_0 = 0
        }
        enum AnzahlByte
        {
            Byte_1 = 1
        }
        enum BitPosAusgang
        {
            M1 = 0,
            M2
        }
        enum BitPosEingang
        {
            B1 = 0,
            B2,
            B3,
            F1,
            F2
        }

        public void DatenRangieren_Task()
        {
            while (FensterAktiv)
            {
                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.B1, B1);
                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.B2, B2);
                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.B3, B3);
                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.F1, F1);
                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.F2, F2);

                if ((Client != null))
                {
                    Client.DBWrite((int)Datenbausteine.Input, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigInput);
                    Client.DBRead((int)Datenbausteine.Output, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigOutput);
                }

                M1 = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.M1);
                M2 = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.M2);

                Task.Delay(100);
            }
        }

    }
}
