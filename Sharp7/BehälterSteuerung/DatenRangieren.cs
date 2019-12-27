using Sharp7;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BehälterSteuerung
{
    public class DatenRangieren
    {
        bool Leuchte_P1 = false;
      
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
                foreach (Behaelter beh in gAlleBehaelter) beh.BehalterDatenRangieren(ref DigInput, ref DigOutput);

                if ((Client != null) && TaskAktiv)
                {
                    Client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigInput);
                    Client.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigOutput);
                }

                Leuchte_P1 = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P1);

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
