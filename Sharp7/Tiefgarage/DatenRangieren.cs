﻿using Sharp7;
using System.Threading.Tasks;

namespace Tiefgarage
{
    public partial class MainWindow
    {
        public bool Pegel_B1;
        public bool Pegel_B2;
        public bool Pegel_B1_Alt;
        public bool Pegel_B2_Alt;

        public int AnzahlFahrzeuge = 0;
        public int AnzahlPersonen = 0;
        public int AnzahlFahrzeuge_Alt = -1;
        public int AnzahlPersonen_Alt = -1;

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
        }
        enum BitPosEingang
        {
            B1 = 0,
            B2
        }

        public void DatenRangieren_Task()
        {
            while (TaskAktiv && FensterAktiv)
            {
                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.B1, Pegel_B1);
                S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.B2, Pegel_B2);

                if ((Client != null) && TaskAktiv)
                {
                    Client.DBWrite((int)Datenbausteine.Input, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigInput);
                    Client.DBRead((int)Datenbausteine.Output, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigOutput);
                }

                AnzahlFahrzeuge = DigOutput[0];
                AnzahlPersonen = DigOutput[1];

                Task.Delay(100);
            }
        }

    }
}
