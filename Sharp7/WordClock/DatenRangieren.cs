using Sharp7;
using System.Threading;

namespace WordClock
{
    public partial class MainWindow
    {

        public ushort DatumJahr;
        public byte DatumMonat;
        public byte DatumTag;
        public byte DatumWochentag;
        public byte Stunde;
        public byte Minute;
        public byte Sekunde;
        public byte Nanosekunde;

        enum Datenbausteine
        {
            DigIn = 1,
            DigOut,
            AnIn,
            AnOut
        }
        enum BytePosition
        {
            Byte_0 = 0,
            Byte_1,
            Byte_2,
            Byte_3,
            Byte_4,
            Byte_5,
            Byte_6,
            Byte_7,
            Byte_8,
            Byte_9


        }
        enum AnzahlByte
        {
            Byte_1 = 1,
            Byte_2,
            Byte_3,
            Byte_4,
            Byte_5,
            Byte_6,
            Byte_7,
            Byte_8,
            Byte_9,
            Byte_10
        }
        enum BitPosAusgang
        {
        }
        enum BitPosEingang
        {
        }

        public void DatenRangieren_Task()
        {
            while (TaskAktiv && FensterAktiv)
            {
                S7.SetWordAt(DigInput, (int)BytePosition.Byte_0, DatumJahr);
                S7.SetByteAt(DigInput, (int)BytePosition.Byte_2, DatumMonat);
                S7.SetByteAt(DigInput, (int)BytePosition.Byte_3, DatumTag);
                S7.SetByteAt(DigInput, (int)BytePosition.Byte_4, DatumWochentag);
                S7.SetByteAt(DigInput, (int)BytePosition.Byte_5, Stunde);
                S7.SetByteAt(DigInput, (int)BytePosition.Byte_6, Minute);
                S7.SetByteAt(DigInput, (int)BytePosition.Byte_7, Sekunde);
                S7.SetByteAt(DigInput, (int)BytePosition.Byte_8, Nanosekunde);

                if ((Client != null) && TaskAktiv)
                {
                    Client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_10, DigInput);
                }

                Thread.Sleep(100);
            }
        }

        private int InByte(BitPosEingang Pos) { return ((int)Pos) / 8; }
        private int InBit(BitPosEingang Pos) { return ((int)Pos) % 8; }
        private int OutByte(BitPosAusgang Pos) { return ((int)Pos) / 8; }
        private int OutBit(BitPosAusgang Pos) { return ((int)Pos) % 8; }

        void EinAusgabeFelderInitialisieren()
        {
            foreach (byte b in DigInput) DigInput[b] = 0;
            foreach (byte b in DigOutput) DigOutput[b] = 0;
            foreach (byte b in AnalogOutput) AnalogOutput[b] = 0;
            foreach (byte b in AnalogInput) AnalogInput[b] = 0;
        }
    }
}
