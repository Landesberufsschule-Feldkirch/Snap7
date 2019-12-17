using Sharp7;
using System.Threading;

namespace Tiefgarage
{
    public partial class MainWindow
    {
        public bool B1;
        public bool B2;

        public int AnzahlFahrzeuge = 0;
        public int AnzahlPersonen = 0;

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
            Byte_2
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
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.B1), InBit(BitPosEingang.B1), B1);
                S7.SetBitAt(ref DigInput, InByte(BitPosEingang.B1), InBit(BitPosEingang.B2), B2);

                if ((Client != null) && TaskAktiv)
                {
                    Client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigInput);
                    Client.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_2, DigOutput);
                }

                AnzahlFahrzeuge = DigOutput[0];
                AnzahlPersonen = DigOutput[1];

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
