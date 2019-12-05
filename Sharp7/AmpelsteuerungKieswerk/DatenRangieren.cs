using Sharp7;
using System.Threading.Tasks;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow
    {
        bool Leuchte_P1 = false;

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
            Q1 = 0,
            Q3,
            Q5,
            Q7,
            P1
        }
        enum BitPosEingang
        {
            B1 = 0,
            B2,
            B3,
            B4,
            B5,
            B6,
            B7,
            B8
        }

        public void DatenRangieren_Task()
        {
            while (TaskAktiv && FensterAktiv)
            {
                foreach (LKW lkw in gAlleLKW) lkw.LKWDatenRangieren(ref DigInput, ref DigOutput);

                if ((Client != null) && TaskAktiv)
                {
                    Client.DBWrite((int)Datenbausteine.Input, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigInput);
                    Client.DBRead((int)Datenbausteine.Output, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigOutput);
                }

               Leuchte_P1 = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P1);

                Task.Delay(100);
            }
        }

    }
}
