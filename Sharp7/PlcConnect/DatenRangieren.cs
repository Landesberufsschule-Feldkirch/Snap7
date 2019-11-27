using Sharp7;
using System.Threading.Tasks;

namespace PlcConnect
{
    public partial class MainWindow
    {
        public bool S1;
        public bool S2;
        public bool M1;
        public bool P1;

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
            P1
        }
        enum BitPosEingang
        {
            S1 = 0,
            S2
        }

        public void DatenRangieren_Task()
        {
            while (TaskAktiv && FensterAktiv)
            {
                AnzeigeAktualisieren();

                if ((Client != null) && TaskAktiv)
                {
                    S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.S1, ButtonStart.IsPressed);
                    S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.S2, ButtonStop.IsPressed);

                    Client.DBWrite(DB_DigInput, Startbyte_0, AnzahlByte_1, DigInput);
                    Client.DBRead(DB_DigOutput, Startbyte_0, AnzahlByte_1, DigOutput);

                    M1 = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.M1);
                    P1 = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P1);
                }

                Task.Delay(100);
            }
        }

    }
}
