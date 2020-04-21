using System;
using Utilities;

namespace PaternosterLager.Model
{



    public static class PositionBestimmen
    {
        private static readonly Punkt zentrumOben = new Punkt(200, 150);
        private static readonly Punkt zentrumUnten = new Punkt(200, 820);
        private const double radiusUmlenkung = 100;


        public static double GetGesamtLaenge(double breiteBolzen) => 2 * 670 + breiteBolzen + Math.PI * (2 * radiusUmlenkung + breiteBolzen);

        public static (double x, double y, bool rechtsOben) ZapfenPositionBerechnen(double pos, double breiteBolzen)
        {
            double x;
            double y;
            bool rechtsOben;
            double senkrechterTeil = zentrumUnten.Y - zentrumOben.Y;
            double positionRest;
            double bogenwinkel;
            double bogenRadius = radiusUmlenkung + breiteBolzen / 2;
            double rundungBogen = Math.PI * bogenRadius;
            double segmentRechtsSenkrecht = senkrechterTeil / 2;
            double segmentMitBogenOben = segmentRechtsSenkrecht + rundungBogen;
            double segmentMitLinksSenkrecht = segmentMitBogenOben + senkrechterTeil;
            double segmentMitBogenUnten = segmentMitLinksSenkrecht + rundungBogen;

            if (pos > GetGesamtLaenge(breiteBolzen)) pos -= GetGesamtLaenge(breiteBolzen);

            if (pos < segmentRechtsSenkrecht)
            {
                // rechts rauf
                rechtsOben = true;
                positionRest = pos;
                x = zentrumOben.X + bogenRadius;
                y = zentrumOben.Y + segmentRechtsSenkrecht - positionRest;
            }
            else if (pos < segmentMitBogenOben)
            {
                // obere Rundung
                rechtsOben = true;
                positionRest = pos - segmentRechtsSenkrecht;
                bogenwinkel = positionRest / bogenRadius; // Winkel in rad
                x = zentrumOben.X + bogenRadius * Math.Cos(bogenwinkel);
                y = zentrumOben.Y - bogenRadius * Math.Sin(bogenwinkel);
            }
            else if (pos < segmentMitLinksSenkrecht)
            {
                // links runter
                rechtsOben = false;
                positionRest = pos - segmentMitBogenOben;
                x = zentrumOben.X - bogenRadius;
                y = zentrumOben.Y + positionRest;
            }
            else if (pos < segmentMitBogenUnten)
            {
                // untere Rundung
                rechtsOben = false;
                positionRest = pos - segmentMitLinksSenkrecht;
                bogenwinkel = Math.PI - positionRest / bogenRadius; // Winkel in rad
                x = zentrumUnten.X + bogenRadius * Math.Cos(bogenwinkel);
                y = zentrumUnten.Y + bogenRadius * Math.Sin(bogenwinkel) + 1.5 * breiteBolzen;
            }
            else
            {
                // rechts rauf
                rechtsOben = true;
                positionRest = pos - segmentMitBogenUnten;
                x = zentrumUnten.X + radiusUmlenkung + breiteBolzen / 2;
                y = zentrumUnten.Y - positionRest;
            }

            return (x, y, rechtsOben);
        }

        public static (double x, double y, double phi, bool rechtsOben) KettengliedPositionBerechnen(double posAnfang, double posEnde, double breiteZapfen, double breiteKettenglied, double hoeheKettenglied)
        {

            bool rechtsOben;
            double x;
            double xAnfang;
            double xEnde;
            double y;
            double yAnfang;
            double yEnde;
            double phi;
            bool rechtsObenAnfang;
            bool rechtsObenEnde;


            (xAnfang, yAnfang, rechtsObenAnfang) = ZapfenPositionBerechnen(posAnfang, breiteZapfen);
            (xEnde, yEnde, rechtsObenEnde) = ZapfenPositionBerechnen(posEnde, breiteZapfen);

            x = xAnfang;
            y = yAnfang;

            if (xAnfang == xEnde)
            {
                phi = 0;
            }
            else
            {

                if (rechtsObenAnfang)
                {

                    phi = 270 + Utilities.Winkel.Rad2Deg(Math.Atan((yEnde - yAnfang) / (xEnde - xAnfang)));
                }
                else
                {
                    phi = 270 + Utilities.Winkel.Rad2Deg(Math.Atan((yAnfang - yEnde) / (xAnfang - xEnde)));

                }


            }

            return (x, y, phi, rechtsObenAnfang);
        }
    }
}
