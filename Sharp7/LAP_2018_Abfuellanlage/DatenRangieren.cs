﻿using Sharp7;
using System.Threading.Tasks;

namespace LAP_2018_Abfuellanlage
{
    public partial class MainWindow
    {
        public bool B1;
        public bool F5 = true;
        public bool M1;
        public bool M2;

        public bool S1;
        public bool S2;
        public bool S3;
        public bool S4;

        public bool P1;
        public bool P2;

        public bool K1;
        public bool K2;

        public byte PegelByte;
        public ushort PegelInt;

        int S1_Zaehler = 0;
        int S2_Zaehler = 0;
        int S3_Zaehler = 0;
        int S4_Zaehler = 0;

        enum Datenbausteine
        {
            DigIn = 1,
            DigOut,
            AnIn,
            AnOut
        }
        enum BytePosition
        {
            Byte_0 = 0
        }
        enum AnzahlByte
        {
            Byte_1 = 1,
            Byte_2,
            Byte_3,
            Byte_4
        }
        enum BitPosAusgang
        {
            M1 = 0,
            K1,
            K2,
            P1,
            P2
        }
        enum BitPosEingang
        {
            B1 = 0,
            F5,
            S1,
            S2,
            S3,
            S4
        }

        public void DatenRangieren_Task()
        {
            while (FensterAktiv)
            {
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.B1), (int)BitPosEingang.B1, B1);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.F5), (int)BitPosEingang.F5, F5);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.B1), (int)BitPosEingang.S1, S1);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.B1), (int)BitPosEingang.S2, S2);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.B1), (int)BitPosEingang.S3, S3);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.B1), (int)BitPosEingang.S4, S4);

                S7.SetByteAt( AnalogInput, 0, PegelByte);
                S7.SetWordAt(AnalogInput, 2, PegelInt);

                if ((Client != null))
                {
                    Client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigInput);
                    Client.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigOutput);
                    Client.DBWrite((int)Datenbausteine.AnIn, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_4, AnalogInput);
                }
               
                M1 = S7.GetBitAt(DigOutput, OutByte(BitPosAusgang.M1), OutBit(BitPosAusgang.M1));
                K1 = S7.GetBitAt(DigOutput, OutByte(BitPosAusgang.K1), OutBit(BitPosAusgang.K1));
                K2 = S7.GetBitAt(DigOutput, OutByte(BitPosAusgang.K2), OutBit(BitPosAusgang.K2));
                P1 = S7.GetBitAt(DigOutput, OutByte(BitPosAusgang.P1), OutBit(BitPosAusgang.P1));
                P2 = S7.GetBitAt(DigOutput, OutByte(BitPosAusgang.P2), OutBit(BitPosAusgang.P2));
                
                Task.Delay(100);
            }
        }

        private int InByte(BitPosEingang Pos) { return ((int)Pos) / 8; }
        private int InBit(BitPosEingang Pos) { return ((int)Pos) % 8; }
        private int OutByte(BitPosAusgang Pos) { return ((int)Pos) / 8; }
        private int OutBit(BitPosAusgang Pos) { return ((int)Pos) % 8; }

    }
}
