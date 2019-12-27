using Sharp7;
using System.Threading;

namespace Tiefgarage
{
    public class DatenRangieren
    {
        public bool B1;
        public bool B2;

        public int AnzahlFahrzeuge = 0;
        public int AnzahlPersonen = 0;

     
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
