namespace PaternosterLager.Model;

using System;
using Utilities;

public enum Zeichenbereich
{
    Rechts = 0,
    Oben,
    Links,
    Unten
}

public static class PositionBestimmen
{
    private static readonly Punkt ZentrumOben = new(200, 150);
    private static readonly Punkt ZentrumUnten = new(200, 150 + 680);
    private const double RadiusUmlenkung = 100;

    public static double GetGesamtLaenge(double breiteBolzen) => 2 * 680 + breiteBolzen + Math.PI * (2 * RadiusUmlenkung + breiteBolzen);

    public static (double x, double y, Zeichenbereich zeichenBereich) ZapfenPositionBerechnen(double pos, double breiteBolzen)
    {
        double x;
        double y;
        Zeichenbereich zeichenBereich;
        var senkrechterTeil = ZentrumUnten.Y - ZentrumOben.Y;
        double positionRest;
        double bogenwinkel;
        var bogenRadius = RadiusUmlenkung + breiteBolzen / 2;
        var rundungBogen = Math.PI * bogenRadius;
        var segmentRechtsSenkrecht = senkrechterTeil / 2;
        var segmentMitBogenOben = segmentRechtsSenkrecht + rundungBogen;
        var segmentMitLinksSenkrecht = segmentMitBogenOben + senkrechterTeil;
        var segmentMitBogenUnten = segmentMitLinksSenkrecht + rundungBogen;

        if (pos > GetGesamtLaenge(breiteBolzen)) pos -= GetGesamtLaenge(breiteBolzen);

        if (pos < segmentRechtsSenkrecht)
        {
            // rechts rauf
            zeichenBereich = Zeichenbereich.Rechts;
            positionRest = pos;
            x = ZentrumOben.X + bogenRadius;
            y = ZentrumOben.Y + segmentRechtsSenkrecht - positionRest;
        }
        else if (pos < segmentMitBogenOben)
        {
            // obere Rundung
            zeichenBereich = Zeichenbereich.Oben;
            positionRest = pos - segmentRechtsSenkrecht;
            bogenwinkel = positionRest / bogenRadius; // Winkel in rad
            x = ZentrumOben.X + bogenRadius * Math.Cos(bogenwinkel);
            y = ZentrumOben.Y - bogenRadius * Math.Sin(bogenwinkel);
        }
        else if (pos < segmentMitLinksSenkrecht)
        {
            // links runter
            zeichenBereich = Zeichenbereich.Links;
            positionRest = pos - segmentMitBogenOben;
            x = ZentrumOben.X - bogenRadius;
            y = ZentrumOben.Y + positionRest;
        }
        else if (pos < segmentMitBogenUnten)
        {
            // untere Rundung
            zeichenBereich = Zeichenbereich.Unten;
            positionRest = pos - segmentMitLinksSenkrecht;
            bogenwinkel = Math.PI - positionRest / bogenRadius; // Winkel in rad
            x = ZentrumUnten.X + bogenRadius * Math.Cos(bogenwinkel);
            y = ZentrumUnten.Y + bogenRadius * Math.Sin(bogenwinkel);
        }
        else
        {
            // rechts rauf
            zeichenBereich = Zeichenbereich.Rechts;
            positionRest = pos - segmentMitBogenUnten;
            x = ZentrumUnten.X + RadiusUmlenkung + breiteBolzen / 2;
            y = ZentrumUnten.Y - positionRest;
        }

        return (x, y, zeichenBereich);
    }

    public static (double x, double y, double phi, Zeichenbereich zeichenBereich) KettengliedPositionBerechnen(double posZapfen, double breiteZapfen, double breiteKettenglied, double hoeheKettenglied)
    {
        Zeichenbereich zeichenBereichVorher;
        Zeichenbereich zeichenBereich;
        double x;
        double xVorher;
        double xDavor;
        double y;
        double yVorher;
        double yDavor;
        double phi;

        (xVorher, yVorher, zeichenBereichVorher) = ZapfenPositionBerechnen(posZapfen - hoeheKettenglied + breiteKettenglied, breiteZapfen);
        (xDavor, yDavor, _) = ZapfenPositionBerechnen(posZapfen - hoeheKettenglied, breiteZapfen);
        (x, y, zeichenBereich) = ZapfenPositionBerechnen(posZapfen, breiteZapfen);

        if (Math.Abs(x - xDavor) < 0.1) return (x, y, 0, zeichenBereich);

        if (zeichenBereichVorher == Zeichenbereich.Rechts) { return (x, y, 0, zeichenBereich); }

        switch (zeichenBereich)
        {
            case Zeichenbereich.Links:
                return (x, y, 0, zeichenBereich);
            case Zeichenbereich.Oben:
                phi = 270 + Winkel.Rad2Deg(Math.Atan((y - yDavor) / (x - xDavor)));
                return (x, y, phi, zeichenBereichVorher);
            case Zeichenbereich.Rechts:
                break;
            case Zeichenbereich.Unten:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(zeichenBereich));
        }

        phi = 270 + Winkel.Rad2Deg(Math.Atan((y - yVorher) / (x - xVorher)));
        return (xVorher, yVorher, phi, zeichenBereich);
    }
}