using Sharp7;
using System;

namespace AmpelsteuerungKieswerk
{
    public class DatenRangieren
    {
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
            P1 = 0,
            P2,
            P3,
            P4,
            P5,
            P6
        }
        enum BitPosEingang
        {
            B1 = 0,
            B2,
            B3,
            B4
        }

        public DatenRangieren(MainWindow window)
        {
            Array.Clear(DigInput, 0, DigInput.Length);
            Array.Clear(DigOutput, 0, DigOutput.Length);

            window.SensorenChanged += Window_SensorenChanged;
        }

        private void Window_SensorenChanged(object sender, SensorenZustandArgs e)
        {
            B1 = e.B1;
            B2 = e.B2;
            B3 = e.B3;
            B4 = e.B4;
        }
        private bool B1, B2, B3, B4;

        private readonly byte[] DigOutput = new byte[1024];
        private byte[] DigInput = new byte[1024];

        private AmpelZustand AmpelLinks = AmpelZustand.Rot;
        private AmpelZustand AmpelRechts = AmpelZustand.Rot;

        public void Task(S7Client client)
        {
            S7.SetBitAt(DigInput, InByte(BitPosEingang.B1), InBit(BitPosEingang.B1), B1);
            S7.SetBitAt(DigInput, InByte(BitPosEingang.B1), InBit(BitPosEingang.B2), B2);
            S7.SetBitAt(DigInput, InByte(BitPosEingang.B1), InBit(BitPosEingang.B1), B3);
            S7.SetBitAt(DigInput, InByte(BitPosEingang.B1), InBit(BitPosEingang.B2), B4);

            client?.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigInput);
            client?.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigOutput);

            var p1_links_gruen = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P1);
            var p2_links_gelb = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P2);
            var p3_links_rot = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P3);
            var p4_rechts_gruen = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P4);
            var p5_rechts_gelb = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P5);
            var p6_rechts_rot = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.P6);

            var ampelLinks = GetAmpelZustand(p1_links_gruen, p2_links_gelb, p3_links_rot);
            var ampelRechts = GetAmpelZustand(p4_rechts_gruen, p5_rechts_gelb, p6_rechts_rot);

            if (ampelLinks != AmpelLinks || ampelRechts != AmpelRechts)
            {
                OnAmpelChanged(new AmpelZustandEventArgs(ampelLinks, ampelRechts));

                AmpelRechts = ampelRechts;
                AmpelLinks = ampelLinks;
            }
        }

        public event EventHandler<AmpelZustandEventArgs> AmpelChangedEvent;

        private void OnAmpelChanged(AmpelZustandEventArgs e)
        {
            AmpelChangedEvent?.Invoke(this, e);
        }

        private int InByte(BitPosEingang Pos) { return ((int)Pos) / 8; }
        private int InBit(BitPosEingang Pos) { return ((int)Pos) % 8; }
        //private int OutByte(BitPosAusgang Pos) { return ((int)Pos) / 8; }
        //private int OutBit(BitPosAusgang Pos) { return ((int)Pos) % 8; }

        private AmpelZustand GetAmpelZustand(bool grün, bool gelb, bool rot)
        {
            if (rot) return AmpelZustand.Rot;
            if (gelb) return AmpelZustand.Gelb;
            return AmpelZustand.Grün;
        }
    }
}