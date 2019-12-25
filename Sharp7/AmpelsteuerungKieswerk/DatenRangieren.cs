using Sharp7;
using System;

namespace AmpelsteuerungKieswerk
{
    public class DatenRangieren
    {
        private enum BitPosAusgang
        {
            P1 = 0,
            P2,
            P3,
            P4,
            P5,
            P6
        }

        private enum BitPosEingang
        {
            B1 = 0,
            B2,
            B3,
            B4
        }

        private bool B1, B2, B3, B4;

        private AmpelZustand AmpelLinks = AmpelZustand.Rot;
        private AmpelZustand AmpelRechts = AmpelZustand.Rot;

        public event EventHandler<AmpelZustandEventArgs> AmpelChangedEvent;

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, B3);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, B4);
        }
        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            var p1_links_gruen = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
            var p2_links_gelb = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
            var p3_links_rot = S7.GetBitAt(digOutput, (int)BitPosAusgang.P3);
            var p4_rechts_gruen = S7.GetBitAt(digOutput, (int)BitPosAusgang.P4);
            var p5_rechts_gelb = S7.GetBitAt(digOutput, (int)BitPosAusgang.P5);
            var p6_rechts_rot = S7.GetBitAt(digOutput, (int)BitPosAusgang.P6);

            var ampelLinks = GetAmpelZustand(p1_links_gruen, p2_links_gelb, p3_links_rot);
            var ampelRechts = GetAmpelZustand(p4_rechts_gruen, p5_rechts_gelb, p6_rechts_rot);

            if (ampelLinks != AmpelLinks || ampelRechts != AmpelRechts)
            {
                OnAmpelChanged(new AmpelZustandEventArgs(ampelLinks, ampelRechts));

                AmpelRechts = ampelRechts;
                AmpelLinks = ampelLinks;
            }
        }

        public DatenRangieren(MainWindow window)
        {
            window.SensorenChanged += Window_SensorenChanged;
        }

        private void Window_SensorenChanged(object sender, SensorenZustandArgs e)
        {
            B1 = e.B1;
            B2 = e.B2;
            B3 = e.B3;
            B4 = e.B4;
        }

        private void OnAmpelChanged(AmpelZustandEventArgs e)
        {
            AmpelChangedEvent?.Invoke(this, e);
        }

        private AmpelZustand GetAmpelZustand(bool gruen, bool gelb, bool rot)
        {
            if (rot && gelb) return AmpelZustand.RotUndGelb;
            if (rot) return AmpelZustand.Rot;
            if (gelb) return AmpelZustand.Gelb;
            if (gruen) return AmpelZustand.Gruen;
            return AmpelZustand.Aus;
        }
    }
}