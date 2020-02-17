using Sharp7;
using System;
using AmpelsteuerungKieswerk.Model;


namespace AmpelsteuerungKieswerk
{

    public class DatenRangieren
    {
        private bool B1, B2, B3, B4;

        private AmpelZustand AmpelLinks = AmpelZustand.Rot;
        private AmpelZustand AmpelRechts = AmpelZustand.Rot;

        public event EventHandler<AmpelZustandEventArgs> AmpelChangedEvent;

        private enum BitPosAusgang
        {
            P1 = 0, // Ampel links rot
            P2,     // Ampel links gelb
            P3,     // Ampel links grün
            P4,     // Ampel rechts rot
            P5,     // Ampel rechts gelb
            P6      // Ampel rechts grün
        }

        private enum BitPosEingang
        {
            B1 = 0,
            B2,
            B3,
            B4
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            S7.SetBitAt(digInput, (int)BitPosEingang.B1, B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.B3, B3);
            S7.SetBitAt(digInput, (int)BitPosEingang.B4, B4);
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            var p1_links_rot = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
            var p2_links_gelb = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
            var p3_links_gruen = S7.GetBitAt(digOutput, (int)BitPosAusgang.P3);
            var p4_rechts_rot = S7.GetBitAt(digOutput, (int)BitPosAusgang.P4);
            var p5_rechts_gelb = S7.GetBitAt(digOutput, (int)BitPosAusgang.P5);
            var p6_rechts_gruen = S7.GetBitAt(digOutput, (int)BitPosAusgang.P6);

            var ampelLinks = GetAmpelZustand(p1_links_rot, p2_links_gelb, p3_links_gruen);
            var ampelRechts = GetAmpelZustand(p4_rechts_rot, p5_rechts_gelb, p6_rechts_gruen);

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

            AmpelChangedEvent += window.DatenRangieren_AmpelChangedEvent;
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

        private AmpelZustand GetAmpelZustand(bool rot, bool gelb, bool gruen)
        {
            if (rot && gelb) return AmpelZustand.RotUndGelb;
            if (rot) return AmpelZustand.Rot;
            if (gelb) return AmpelZustand.Gelb;
            if (gruen) return AmpelZustand.Gruen;
            return AmpelZustand.Aus;
        }
    }
}