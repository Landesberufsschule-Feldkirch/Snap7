using Sharp7;
using System.Threading.Tasks;

namespace LAP_2018_Abfuellanlage
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

        public bool S1 = false;
        public bool S2 = false;
        public bool S3 = false;
        public bool P1 = false;
        public bool P2 = false;
        public bool P3 = false;

        int S1_Zaehler = 0;
        int S2_Zaehler = 0;
        int S3_Zaehler = 0;

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
            while (FensterAktiv)
            {

   

                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.B1, B1);
                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.B2, B2);
                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.B3, B3);
                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.F1, F1);
                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.F2, F2);
                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.S1, S1);
                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.S2, S2);
                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.S3, S3);

                if ((Client != null))
                {
                    Client.DBWrite((int)Datenbausteine.Input, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigInput);
                    Client.DBRead((int)Datenbausteine.Output, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigOutput);
                }

                M1 = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.M1);
                M2 = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.M2);
                P1 = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P1);
                P2 = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P2);
                P3 = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P3);



                Task.Delay(100);
            }
        }

    }
}
