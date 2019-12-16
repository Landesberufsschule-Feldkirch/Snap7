using Sharp7;
using System.Diagnostics;
using System.Threading;
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
                
                if ((Client != null) && TaskAktiv)
                {
                    S7.SetBitAt(ref DigInput, InByte(BitPosEingang.S1), InBit(BitPosEingang.S1), ButtonStart.IsPressed);
                    S7.SetBitAt(ref DigInput, InByte(BitPosEingang.S2), InBit(BitPosEingang.S2), ButtonStop.IsPressed);

                    if ((Client != null))
                    {
                        Client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigInput);
                        Client.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigOutput);
                    }

                    M1 = S7.GetBitAt(DigOutput, OutByte(BitPosAusgang.M1), OutBit(BitPosAusgang.M1));
                    P1 = S7.GetBitAt(DigOutput, OutByte(BitPosAusgang.P1), OutBit(BitPosAusgang.P1));
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
