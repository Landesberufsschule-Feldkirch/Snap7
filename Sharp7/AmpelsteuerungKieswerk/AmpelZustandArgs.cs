using System;

namespace AmpelsteuerungKieswerk
{
    public enum AmpelZustand
    {
        Rot,
        RotUndGelb,
        Gelb,
        Gruen,
        Aus
    }

    public class AmpelZustandEventArgs : EventArgs
    {
        public AmpelZustand AmpelZustandLinks { get; set; }
        public AmpelZustand AmpelZustandRechts { get; set; }

        public AmpelZustandEventArgs(AmpelZustand ampelZustandLinks, AmpelZustand ampelZustandRechts)
        {
            AmpelZustandLinks = ampelZustandLinks;
            AmpelZustandRechts = ampelZustandRechts;
        }
    }
}