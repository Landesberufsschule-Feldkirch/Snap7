﻿using Utilities;

namespace Tiefgarage.Model
{
    public class FahrzeugPerson
    {
        public enum FahrenRichtung
        {
            ObenGeparkt = 0,
            AbwaertsKurveOben,
            AbwaertsSenkrecht,
            AbwaertsKurveUnten,

            UntenGeparkt,

            AufwaertsKurveUnten,
            AufwaertsSenkrecht,
            AufwaertsKurveOben
        }

        public enum Rolle
        {
            Fahrzeug = 0,
            Person
        }

        public Rolle FpRolle { get; set; }
        public FahrenRichtung Bewegung { get; set; } = FahrenRichtung.ObenGeparkt;
        public Punkt AktuellePosition { get; set; } = new Punkt(0, 0);

        private readonly double xy_Bewegung = 1;
        private readonly double kurveGeschwindigkeit = 0.002;

        private double _kurvePosition;

        private readonly BezierCurve _kurveOben;
        private readonly BezierCurve _kurveUnten;

        private readonly Punkt _fahrzeug = new Punkt(100, 50);
        private readonly Punkt _fahrzeugOben = new Punkt(10, 10);
        private readonly Punkt _fahrzeugUntenLinks = new Punkt(110, 500);
        private readonly Punkt _fahrzeugUntenRechts = new Punkt(810, 500);

        private readonly Punkt _person = new Punkt(25, 15);
        private readonly Punkt _personOben = new Punkt(500, 10);
        private readonly Punkt _personUnten = new Punkt(250, 650);

        private readonly Punkt _fahrspurOben = new Punkt(350, 250);
        private readonly Punkt _fahrspurUnten = new Punkt(350, 400);

        private readonly double yPosition_B1 = 300;
        private readonly double yPosition_B2 = 330;

        public readonly Punkt ParkenOben;
        public readonly Punkt ParkenUnten;
        public readonly Punkt EingangOben;
        public readonly Punkt EingangUnten;

        public FahrzeugPerson(Rolle rolle, int wievieltesFahrzeugPerson)
        {
            double xOben;
            double yOben;
            double xUnten;
            double yUnten;

            Punkt kontrollPunktUnten1;
            Punkt kontrollPunktUnten2;

            FpRolle = rolle;

            if (FpRolle == Rolle.Fahrzeug)
            {
                yOben = _fahrzeugOben.Y;
                xOben = _fahrzeugOben.X + wievieltesFahrzeugPerson * _fahrzeug.X;
                if (wievieltesFahrzeugPerson < 4)
                {
                    xUnten = _fahrzeugUntenLinks.X;
                    yUnten = _fahrzeugUntenLinks.Y + wievieltesFahrzeugPerson * _fahrzeug.Y;

                    kontrollPunktUnten1 = new Punkt(_fahrspurUnten.X, _fahrspurUnten.Y + 100);
                    kontrollPunktUnten2 = new Punkt(xUnten + 100, yUnten);
                }
                else
                {
                    xUnten = _fahrzeugUntenRechts.X;
                    yUnten = _fahrzeugUntenRechts.Y + (wievieltesFahrzeugPerson - 4) * _fahrzeug.Y;

                    kontrollPunktUnten1 = new Punkt(_fahrspurUnten.X, _fahrspurUnten.Y + 100);
                    kontrollPunktUnten2 = new Punkt(xUnten - 100, yUnten);
                }
            }
            else
            {
                yOben = _personOben.Y;
                xOben = _personOben.X + wievieltesFahrzeugPerson * _person.X;
                xUnten = _personUnten.X + wievieltesFahrzeugPerson * _person.X;
                yUnten = _personUnten.Y;

                kontrollPunktUnten1 = new Punkt(_fahrspurUnten.X, _fahrspurUnten.Y + 100);
                kontrollPunktUnten2 = new Punkt(xUnten, yUnten - 100);
            }

            var kontrollPunktOben1 = new Punkt(xOben, yOben + 100);
            var kontrollPunktOben2 = new Punkt(_fahrspurOben.X, _fahrspurOben.Y - 100);

            ParkenOben = new Punkt(xOben, yOben);
            ParkenUnten = new Punkt(xUnten, yUnten);

            EingangOben = new Punkt(_fahrspurOben.X, _fahrspurOben.Y);
            EingangUnten = new Punkt(_fahrspurUnten.X, _fahrspurUnten.Y);

            _kurveOben = new BezierCurve(ParkenOben, kontrollPunktOben1, kontrollPunktOben2, EingangOben);
            _kurveUnten = new BezierCurve(EingangUnten, kontrollPunktUnten1, kontrollPunktUnten2, ParkenUnten);

            DraussenParken();
        }

        public void Losfahren()
        {
            if (Bewegung == FahrenRichtung.ObenGeparkt) Bewegung = FahrenRichtung.AbwaertsKurveOben;
            if (Bewegung == FahrenRichtung.UntenGeparkt) Bewegung = FahrenRichtung.AufwaertsKurveUnten;
        }

        public void DraussenParken() => Bewegung = FahrenRichtung.ObenGeparkt;

        public void DrinnenParken() => Bewegung = FahrenRichtung.UntenGeparkt;

        private bool LichtschrankeUnterbrochen(double pos)
        {
            if (AktuellePosition.Y < pos) return false;
            if (FpRolle == Rolle.Fahrzeug && AktuellePosition.Y > pos + _fahrzeug.Y) return false;
            if (FpRolle == Rolle.Person && AktuellePosition.Y > pos + _person.Y) return false;
            return true;
        }

        public (bool b1, bool b2, bool park) Bewegen()
        {
            var allesInParkPosition = false;

            switch (Bewegung)
            {
                case FahrenRichtung.ObenGeparkt:
                    allesInParkPosition = true;
                    AktuellePosition = ParkenOben;
                    _kurvePosition = 0;
                    break;

                case FahrenRichtung.AbwaertsKurveOben:
                    AktuellePosition = _kurveOben.PunktBestimmen(_kurvePosition);
                    _kurvePosition += kurveGeschwindigkeit;
                    if (_kurvePosition >= 1) Bewegung = FahrenRichtung.AbwaertsSenkrecht;
                    break;

                case FahrenRichtung.AbwaertsSenkrecht:
                    if (AktuellePosition.Y < _fahrspurUnten.Y) AktuellePosition.Y += xy_Bewegung;
                    else Bewegung = FahrenRichtung.AbwaertsKurveUnten;
                    _kurvePosition = 0;
                    break;

                case FahrenRichtung.AbwaertsKurveUnten:
                    AktuellePosition = _kurveUnten.PunktBestimmen(_kurvePosition);
                    _kurvePosition += kurveGeschwindigkeit;
                    if (_kurvePosition >= 1) Bewegung = FahrenRichtung.UntenGeparkt;
                    break;

                case FahrenRichtung.UntenGeparkt:
                    allesInParkPosition = true;
                    AktuellePosition = ParkenUnten;
                    _kurvePosition = 1;
                    break;

                case FahrenRichtung.AufwaertsKurveUnten:
                    AktuellePosition = _kurveUnten.PunktBestimmen(_kurvePosition);
                    _kurvePosition -= kurveGeschwindigkeit;
                    if (_kurvePosition <= 0) Bewegung = FahrenRichtung.AufwaertsSenkrecht;
                    break;

                case FahrenRichtung.AufwaertsSenkrecht:
                    if (AktuellePosition.Y > _fahrspurOben.Y) AktuellePosition.Y -= xy_Bewegung;
                    else Bewegung = FahrenRichtung.AufwaertsKurveOben;
                    _kurvePosition = 1;
                    break;

                case FahrenRichtung.AufwaertsKurveOben:
                    AktuellePosition = _kurveOben.PunktBestimmen(_kurvePosition);
                    _kurvePosition -= kurveGeschwindigkeit;
                    if (_kurvePosition <= 0) Bewegung = FahrenRichtung.ObenGeparkt;
                    break;
            }

            return (LichtschrankeUnterbrochen(yPosition_B1), LichtschrankeUnterbrochen(yPosition_B2), allesInParkPosition);
        }
    }
}