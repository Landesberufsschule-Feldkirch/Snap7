using Sharp7;
using System.Threading;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow
    {
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
            Byte_1 = 1
        }
        enum BitPosAusgang
        {
            P1 = 0,
            P2,
            P3,
            P4,
            P5,
            P6
        }
        enum BitPosEingang
        {
            B1 = 0,
            B2,
            B3,
            B4
        }

        public void DatenRangieren_Task()
        {
            while (TaskAktiv && FensterAktiv)
            {
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.B1), InBit(BitPosEingang.B1), B1);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.B1), InBit(BitPosEingang.B2), B2);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.B1), InBit(BitPosEingang.B1), B3);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.B1), InBit(BitPosEingang.B2), B4);

                if ((Client != null) && TaskAktiv)
                {
                    Client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigInput);
                    Client.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigOutput);
                }

                P1_links_gruen = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P1);
                P2_links_gelb = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P2);
                P3_links_rot = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P3);
                P4_rechts_gruen = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P4);
                P5_rechts_gelb = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P5);
                P6_rechts_rot = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P6);

                Thread.Sleep(100);
            }
        }

        private int InByte(BitPosEingang Pos) { return ((int)Pos) / 8; }
        private int InBit(BitPosEingang Pos) { return ((int)Pos) % 8; }
        //private int OutByte(BitPosAusgang Pos) { return ((int)Pos) / 8; }
        //private int OutBit(BitPosAusgang Pos) { return ((int)Pos) % 8; }

        void EinAusgabeFelderInitialisieren()
        {
            foreach (byte b in DigInput) DigInput[b] = 0;
            foreach (byte b in DigOutput) DigOutput[b] = 0;
            foreach (byte b in AnalogOutput) AnalogOutput[b] = 0;
            foreach (byte b in AnalogInput) AnalogInput[b] = 0;
        }
    }
}
