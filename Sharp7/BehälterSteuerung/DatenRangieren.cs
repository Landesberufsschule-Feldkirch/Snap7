using Sharp7;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BehälterSteuerung
{
    public partial class MainWindow
    {
        bool Leuchte_P1 = false;
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

        //private int InByte(BitPosEingang Pos) { return ((int)Pos) / 8; }
        //private int InBit(BitPosEingang Pos) { return ((int)Pos) % 8; }
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
