using Sharp7;

namespace WordClock
{
    public class DatenRangieren
    {
        private readonly Logikfunktionen logikfunktionen;

        private enum BytePosition
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
            Zeiten zeiten = logikfunktionen.getZeit();

            S7.SetWordAt(digInput, (int)BytePosition.Byte_0, zeiten.DatumJahr);
            S7.SetByteAt(digInput, (int)BytePosition.Byte_2, zeiten.DatumMonat);
            S7.SetByteAt(digInput, (int)BytePosition.Byte_3, zeiten.DatumTag);
            S7.SetByteAt(digInput, (int)BytePosition.Byte_4, zeiten.DatumWochentag);
            S7.SetByteAt(digInput, (int)BytePosition.Byte_5, zeiten.Stunde);
            S7.SetByteAt(digInput, (int)BytePosition.Byte_6, zeiten.Minute);
            S7.SetByteAt(digInput, (int)BytePosition.Byte_7, zeiten.Sekunde);
            S7.SetByteAt(digInput, (int)BytePosition.Byte_8, zeiten.Nanosekunde);
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