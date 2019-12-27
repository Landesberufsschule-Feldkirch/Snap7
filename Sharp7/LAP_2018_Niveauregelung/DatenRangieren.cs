using Sharp7;
using System.Threading;

namespace LAP_2018_Niveauregelung
{
    public class DatenRangieren
    {
        public bool B1 = false;
        public bool B2 = false;
        public bool B3 = false;
        public bool F1 = true;
        public bool F2 = true;
        public bool M1 = false;
        public bool M2 = false;
        public bool Y1 = false;

        public bool S1 = false;
        public bool S2 = false;
        public bool S3 = false;
        public bool P1 = false;
        public bool P2 = false;
        public bool P3 = false;


       
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
            M2,
            P1,
            P2,
            P3
        }
        enum BitPosEingang
        {
            B1 = 0,
            B2,
            B3,
            F1,
            F2,
            S1,
            S2,
            S3
        }

        public void DatenRangieren_Task()
        {
            while (TaskAktiv && FensterAktiv)
            {
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.B1), InBit(BitPosEingang.B1), B1);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.B2), InBit(BitPosEingang.B2), B2);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.B3), InBit(BitPosEingang.B3), B3);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.F1), InBit(BitPosEingang.F1), F1);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.F2), InBit(BitPosEingang.F2), F2);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.S1), InBit(BitPosEingang.S1), S1);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.S2), InBit(BitPosEingang.S2), S2);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.S3), InBit(BitPosEingang.S3), S3);

                if ((Client != null))
                {
                    Client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigInput);
                    Client.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigOutput);
                }

                M1 = S7.GetBitAt(DigOutput, OutByte(BitPosAusgang.M1), OutBit(BitPosAusgang.M1));
                M2 = S7.GetBitAt(DigOutput, OutByte(BitPosAusgang.M1), OutBit(BitPosAusgang.M2));
                P1 = S7.GetBitAt(DigOutput, OutByte(BitPosAusgang.P1), OutBit(BitPosAusgang.P1));
                P2 = S7.GetBitAt(DigOutput, OutByte(BitPosAusgang.P2), OutBit(BitPosAusgang.P2));
                P3 = S7.GetBitAt(DigOutput, OutByte(BitPosAusgang.P3), OutBit(BitPosAusgang.P3));

                Thread.Sleep(100);
            }
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            // Daten lesen        
        }
        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            // es werden keine Werte von der SPS geschrieben
        }

        public DatenRangieren(Logikfunktionen logikfunktionen)
        {
            this.logikfunktionen = logikfunktionen;
        }
    }
}
