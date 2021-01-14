using System;

namespace AmpelsteuerungKieswerk.Model
{
    using Utilities;

    public class LastKraftWagen
    {
        public enum LkwRichtungen
        {
            NachRechts = 0,
            NachLinks
        }

        public enum LkwPositionen
        {
            LinksGeparkt = 0,
            LrLinkeKurve,
            LrWaagrecht,
            LrRechtKurve,
            RechtsGeparkt,
            RlRechteKurve,
            RlWaagrecht,
            RlLinkeKurve
        }

        public LkwPositionen LkwPosition { get; set; }
        public LkwRichtungen LkwRichtung { get; set; }
        public Rechteck Lkw { get; set; }
        public int Id { get; set; }

        private Rechteck.RichtungX _richtungX;
        private Rechteck.RichtungY _richtungY;

        private readonly Punkt _parkPosLinks;
        private readonly Punkt _parkPosRechts;
        private readonly BezierCurve _linkeKurve;
        private readonly BezierCurve _rechteKurve;
        private double _kurvePosition;

        private readonly Punkt _parkpositionLinks = new(10, 10);
        private readonly Punkt _parkpositionRechts = new(1340, 10);
        private readonly Punkt _endeLinkeKurve = new(250, 200);
        private readonly Punkt _anfangRechteKurve = new(1100, 200);
        private readonly double _breiteLkw = 100;
        private readonly double _hoeheLkw = 80;
        private readonly Punkt _positionB1 = new(275, 0);
        private readonly Punkt _positionB2 = new(350, 0);
        private readonly Punkt _positionB3 = new(1000, 0);
        private readonly Punkt _positionB4 = new(1075, 0);
        private const double XyBewegung = 1;
        private const double KurveGeschwindigkeit = 0.002;

        public LastKraftWagen(int id)
        {
            Id = id;
            LkwRichtung = LkwRichtungen.NachRechts;
            LkwPosition = LkwPositionen.LinksGeparkt;
            _richtungX = Rechteck.RichtungX.Steht;
            _richtungY = Rechteck.RichtungY.Steht;

            var yLkwParkposition = _parkpositionLinks.Y + id * (10 + _hoeheLkw);

            _parkPosLinks = new Punkt(_parkpositionLinks.X, yLkwParkposition);
            var kontrollPunktLinks1 = new Punkt(_parkpositionLinks.X + 100, yLkwParkposition);
            var kontrollPunktLinks2 = new Punkt(_endeLinkeKurve.X - 100, _endeLinkeKurve.Y);

            _parkPosRechts = new Punkt(_parkpositionRechts.X, yLkwParkposition);
            var kontrollPunktRechts1 = new Punkt(_anfangRechteKurve.X + 100, _anfangRechteKurve.Y);
            var kontrollPunktRechts2 = new Punkt(_parkpositionRechts.X - 100, yLkwParkposition);

            var wegPosLinks = new Punkt(_endeLinkeKurve.X, _endeLinkeKurve.Y);
            var wegPosRechts = new Punkt(_anfangRechteKurve.X, _anfangRechteKurve.Y);

            _linkeKurve = new BezierCurve(_parkPosLinks, kontrollPunktLinks1, kontrollPunktLinks2, wegPosLinks);
            _rechteKurve = new BezierCurve(wegPosRechts, kontrollPunktRechts1, kontrollPunktRechts2, _parkPosRechts);

            Lkw = new Rechteck(_parkpositionLinks, _breiteLkw, _hoeheLkw);
        }

        public (Rechteck.RichtungX, Rechteck.RichtungY) GetRichtung() => (_richtungX, _richtungY);

        public void Losfahren()
        {
            if (LkwPosition == LkwPositionen.LinksGeparkt) LkwPosition = LkwPositionen.LrLinkeKurve;
            if (LkwPosition == LkwPositionen.RechtsGeparkt) LkwPosition = LkwPositionen.RlRechteKurve;
        }

        private bool LichtschrankeUnterbrochen(double xPos)
        {
            if (Lkw.GetRechts() < xPos) return false;
            return Lkw.GetLinks() <= xPos;
        }

        public (bool b1, bool b2, bool b3, bool b4) LastwagenFahren(bool stop)
        {
            _richtungX = Rechteck.RichtungX.Steht;
            _richtungY = Rechteck.RichtungY.Steht;

            switch (LkwPosition)
            {
                case LkwPositionen.LinksGeparkt:
                    Lkw.SetPosition(_parkPosLinks);
                    _kurvePosition = 0;
                    LkwRichtung = LkwRichtungen.NachRechts;
                    break;

                case LkwPositionen.LrLinkeKurve:
                    _richtungX = Rechteck.RichtungX.NachRechts;
                    Lkw.SetPosition(_linkeKurve.PunktBestimmen(_kurvePosition));
                    if (!stop) _kurvePosition += KurveGeschwindigkeit;
                    if (_kurvePosition >= 1) LkwPosition = LkwPositionen.LrWaagrecht;
                    break;

                case LkwPositionen.LrWaagrecht:
                    _richtungX = Rechteck.RichtungX.NachRechts;
                    _kurvePosition = 0;
                    if (!stop)
                    {
                        if (Lkw.GetLinks() < _anfangRechteKurve.X) Lkw.SetWaagrechtSchieben(XyBewegung);
                        else LkwPosition = LkwPositionen.LrRechtKurve;
                    }
                    break;

                case LkwPositionen.LrRechtKurve:
                    _richtungX = Rechteck.RichtungX.NachRechts;
                    Lkw.SetPosition(_rechteKurve.PunktBestimmen(_kurvePosition));
                    if (!stop) _kurvePosition += KurveGeschwindigkeit;
                    if (_kurvePosition >= 1) LkwPosition = LkwPositionen.RechtsGeparkt;
                    break;

                case LkwPositionen.RechtsGeparkt:
                    Lkw.SetPosition(_parkPosRechts);
                    LkwRichtung = LkwRichtungen.NachLinks;
                    _kurvePosition = 1;
                    break;

                case LkwPositionen.RlRechteKurve:
                    _richtungX = Rechteck.RichtungX.NachLinks;
                    Lkw.SetPosition(_rechteKurve.PunktBestimmen(_kurvePosition));
                    if (!stop) _kurvePosition -= KurveGeschwindigkeit;
                    if (_kurvePosition <= 0) LkwPosition = LkwPositionen.RlWaagrecht;
                    break;

                case LkwPositionen.RlWaagrecht:
                    _richtungX = Rechteck.RichtungX.NachLinks;
                    _kurvePosition = 1;
                    if (!stop)
                    {
                        if (Lkw.GetLinks() > _endeLinkeKurve.X) Lkw.SetWaagrechtSchieben(-XyBewegung);
                        else LkwPosition = LkwPositionen.RlLinkeKurve;
                    }
                    break;

                case LkwPositionen.RlLinkeKurve:
                    _richtungX = Rechteck.RichtungX.NachLinks;
                    Lkw.SetPosition(_linkeKurve.PunktBestimmen(_kurvePosition));
                    if (!stop) _kurvePosition -= KurveGeschwindigkeit;
                    if (_kurvePosition <= 0) LkwPosition = LkwPositionen.LinksGeparkt;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(LkwPosition));
            }

            return (LichtschrankeUnterbrochen(_positionB1.X), LichtschrankeUnterbrochen(_positionB2.X), LichtschrankeUnterbrochen(_positionB3.X), LichtschrankeUnterbrochen(_positionB4.X));
        }
    }
}