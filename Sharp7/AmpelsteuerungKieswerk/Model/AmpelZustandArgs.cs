using System;

namespace AmpelsteuerungKieswerk.Model
{
    public enum AmpelZustand
    {
        Rot = 0,
        RotUndGelb = 1,
        Gelb = 2,
        Gruen = 3,
        Aus = 4
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