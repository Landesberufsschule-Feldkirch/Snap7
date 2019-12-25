using Sharp7;

namespace WordClock
{
    public class DatenRangieren
    {
        public ushort DatumJahr { get; set; }
        public byte DatumMonat { get; set; }
        public byte DatumTag { get; set; }
        public byte DatumWochentag { get; set; }
        public byte Stunde { get; set; }
        public byte Minute { get; set; }
        public byte Sekunde { get; set; }
        public byte Nanosekunde { get; set; }

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

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetWordAt(digInput, (int)BytePosition.Byte_0, DatumJahr);
            S7.SetByteAt(digInput, (int)BytePosition.Byte_2, DatumMonat);
            S7.SetByteAt(digInput, (int)BytePosition.Byte_3, DatumTag);
            S7.SetByteAt(digInput, (int)BytePosition.Byte_4, DatumWochentag);
            S7.SetByteAt(digInput, (int)BytePosition.Byte_5, Stunde);
            S7.SetByteAt(digInput, (int)BytePosition.Byte_6, Minute);
            S7.SetByteAt(digInput, (int)BytePosition.Byte_7, Sekunde);
            S7.SetByteAt(digInput, (int)BytePosition.Byte_8, Nanosekunde);
        }
        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            // es werden keine Werte von der SPS geschrieben
        }

        public DatenRangieren()
        {
        }
    }
}
