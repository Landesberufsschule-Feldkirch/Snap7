using System;

namespace PaternosterLager.Model
{
    public static class PositionBestimmen
    {
        private const double obenBahn = 50;
        private const double linksBahn = 150;
        private const double breiteBahn = 200;
        private const double hoeheBahn = 670;// gesamte Höhe: 850

        public static (double x, double y) ZapfenPositionBerechnen(double pos, double breiteZapfen)
        {
            double x;
            double y;
            double bogenX;
            double bogenY;
            double posAnteil;
            double bogenwinkel;
            double bogenRadius = (breiteBahn + breiteZapfen) / 2;
            double rundungBogen = Math.PI * bogenRadius;
            double segmentRechtsSenkrecht = hoeheBahn / 2;
            double segmentMitBogenOben = segmentRechtsSenkrecht + rundungBogen;
            double segmentMitLinksSenkrecht = segmentMitBogenOben + hoeheBahn;
            double segmentMitBogenUnten = segmentMitLinksSenkrecht + rundungBogen;

            if (pos < segmentRechtsSenkrecht)
            {
                // rechts rauf
                posAnteil = pos;
                x = linksBahn + breiteBahn;
                y = obenBahn + bogenRadius + segmentRechtsSenkrecht - posAnteil;
            }
            else if (pos < segmentMitBogenOben)
            {
                // obere Rundung
                posAnteil = pos - segmentRechtsSenkrecht;
                bogenwinkel = posAnteil / bogenRadius; // Winkel in rad
                bogenX = bogenRadius * Math.Cos(bogenwinkel);
                bogenY = bogenRadius * Math.Sin(bogenwinkel);
                x = linksBahn + breiteBahn / 2 - breiteZapfen / 2 + bogenX;
                y = obenBahn + breiteBahn / 2 - breiteZapfen - bogenY;
            }
            else if (pos < segmentMitLinksSenkrecht)
            {
                // links runter
                posAnteil = pos - segmentMitBogenOben;
                x = linksBahn - breiteZapfen;
                y = obenBahn + bogenRadius + posAnteil;
            }
            else if (pos < segmentMitBogenUnten)
            {
                // untere Rundung
                posAnteil = pos - segmentMitLinksSenkrecht;
                bogenwinkel = posAnteil / bogenRadius; // Winkel in rad
                bogenX = bogenRadius * Math.Cos(bogenwinkel);
                bogenY = bogenRadius * Math.Sin(bogenwinkel);
                x = linksBahn + breiteBahn / 2 - breiteZapfen / 2 - bogenX;
                y = obenBahn + hoeheBahn + bogenRadius + breiteZapfen + bogenY;
            }
            else
            {
                // rechts rauf
                posAnteil = pos - segmentMitBogenUnten;
                x = linksBahn + breiteBahn;
                y = obenBahn + bogenRadius + hoeheBahn - posAnteil;
            }

            return (x, y);
        }

        public static (double x, double y, double phi) KettengliedPositionBerechnen(double posAnfang, double posEnde, double posOffset, double breiteZapfen, double breiteKettenglied, double hoeheKettenglied)
        {
            double x;
            double xAnfang;
            double xEnde;            
            double y;
            double yAnfang;
            double yEnde;
            double phi;


            (xAnfang, yAnfang) = ZapfenPositionBerechnen(posAnfang, breiteZapfen);
            (xEnde, yEnde) = ZapfenPositionBerechnen(posEnde, breiteZapfen);

            if (xAnfang == xEnde)
            {
                x = xAnfang - posOffset;
                y = yAnfang - posOffset;
                phi = 0;
            }
            else
            {

                x = xAnfang - posOffset;
                y = yAnfang - posOffset;
                phi = 45;
            }


            return (x, y, phi);
        }
    }
}
