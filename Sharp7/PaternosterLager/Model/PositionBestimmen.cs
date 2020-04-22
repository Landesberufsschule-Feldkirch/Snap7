﻿using System;
using Utilities;

namespace PaternosterLager.Model
{
    public enum Zeichenbereich
    {
        rechts = 0,
        oben,
        links,
        unten
    }

    public static class PositionBestimmen
    {
        private static readonly Punkt zentrumOben = new Punkt(200, 150);
        private static readonly Punkt zentrumUnten = new Punkt(200, 150 + 680);
        private const double radiusUmlenkung = 100;


        public static double GetGesamtLaenge(double breiteBolzen) => 2 * 680 + breiteBolzen + Math.PI * (2 * radiusUmlenkung + breiteBolzen);

        public static (double x, double y, Zeichenbereich zeichenBereich) ZapfenPositionBerechnen(double pos, double breiteBolzen)
        {
            double x;
            double y;
            Zeichenbereich zeichenBereich;
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
                zeichenBereich = Zeichenbereich.rechts;
                positionRest = pos;
                x = zentrumOben.X + bogenRadius;
                y = zentrumOben.Y + segmentRechtsSenkrecht - positionRest;
            }
            else if (pos < segmentMitBogenOben)
            {
                // obere Rundung
                zeichenBereich = Zeichenbereich.oben;
                positionRest = pos - segmentRechtsSenkrecht;
                bogenwinkel = positionRest / bogenRadius; // Winkel in rad
                x = zentrumOben.X + bogenRadius * Math.Cos(bogenwinkel);
                y = zentrumOben.Y - bogenRadius * Math.Sin(bogenwinkel);
            }
            else if (pos < segmentMitLinksSenkrecht)
            {
                // links runter
                zeichenBereich = Zeichenbereich.links;
                positionRest = pos - segmentMitBogenOben;
                x = zentrumOben.X - bogenRadius;
                y = zentrumOben.Y + positionRest;
            }
            else if (pos < segmentMitBogenUnten)
            {
                // untere Rundung
                zeichenBereich = Zeichenbereich.unten;
                positionRest = pos - segmentMitLinksSenkrecht;
                bogenwinkel = Math.PI - positionRest / bogenRadius; // Winkel in rad
                x = zentrumUnten.X + bogenRadius * Math.Cos(bogenwinkel);
                y = zentrumUnten.Y + bogenRadius * Math.Sin(bogenwinkel);
            }
            else
            {
                // rechts rauf
                zeichenBereich = Zeichenbereich.rechts;
                positionRest = pos - segmentMitBogenUnten;
                x = zentrumUnten.X + radiusUmlenkung + breiteBolzen / 2;
                y = zentrumUnten.Y - positionRest;
            }

            return (x, y, zeichenBereich);
        }

        public static (double x, double y, double phi, Zeichenbereich zeichenBereich) KettengliedPositionBerechnen(double posZapfen, double breiteZapfen, double breiteKettenglied, double hoeheKettenglied)
        {

            Zeichenbereich zeichenBereichVorher;
            Zeichenbereich zeichenBereichDavor;
            Zeichenbereich zeichenBereich;
            Zeichenbereich zeichenBereichDanach;
            Zeichenbereich zeichenBereichNochDanach;
            double x;
            double xVorher;
            double xDavor;
            double xDanach;
            double xNochDanach;
            double y;
            double yVorher;
            double yDavor;
            double yDanach;
            double yNochDanach;
            double phi;

            (xVorher, yVorher, zeichenBereichVorher) = ZapfenPositionBerechnen(posZapfen - hoeheKettenglied, breiteZapfen);
            (xDavor, yDavor, zeichenBereichDavor) = ZapfenPositionBerechnen(posZapfen - hoeheKettenglied, breiteZapfen);
            (x, y, zeichenBereich) = ZapfenPositionBerechnen(posZapfen, breiteZapfen);
            (xDanach, yDanach, zeichenBereichDanach) = ZapfenPositionBerechnen(posZapfen + hoeheKettenglied, breiteZapfen);
            (xNochDanach, yNochDanach, zeichenBereichNochDanach) = ZapfenPositionBerechnen(posZapfen + 2 * hoeheKettenglied, breiteZapfen);



            if (x != xDavor)
            {
                if (zeichenBereich == Zeichenbereich.rechts || zeichenBereich == Zeichenbereich.oben)
                {
                    phi = 270 + Utilities.Winkel.Rad2Deg(Math.Atan((y - yDavor) / (x - xDavor)));
                    return (x, y, phi, zeichenBereich);
                }
                else
                {
                    if (zeichenBereich == Zeichenbereich.links)
                    {
                        phi = 0;
                        return (x, y, phi, zeichenBereich);
                    }
                    //phi = 270 + Utilities.Winkel.Rad2Deg(Math.Atan((y - yDanach) / (x - xDanach)));
                    phi = 270 + Utilities.Winkel.Rad2Deg(Math.Atan((y - yDavor) / (x - xDavor)));
                    return (xVorher, yVorher, phi, zeichenBereich);
                }
            }

            phi = 0;
            return (x, y, phi, zeichenBereich);
        }
    }
}