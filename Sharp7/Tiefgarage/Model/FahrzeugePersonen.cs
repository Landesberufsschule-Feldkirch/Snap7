using Utilities;

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

        public Rolle FP_Rolle { get; set; }
        public FahrenRichtung Bewegung { get; set; } = FahrenRichtung.ObenGeparkt;
        public Punkt AktuellePosition { get; set; } = new Punkt(0, 0);

        private readonly double xy_Bewegung = 1;
        private readonly double kurveGeschwindigkeit = 0.002;

        private double kurvePosition;

        private readonly BezierCurve kurveOben;
        private readonly BezierCurve kurveUnten;

        private readonly Punkt fahrzeug = new Punkt(100, 50);
        private readonly Punkt fahrzeugOben = new Punkt(10, 10);
        private readonly Punkt fahrzeugUntenLinks = new Punkt(110, 500);
        private readonly Punkt fahrzeugUntenRechts = new Punkt(810, 500);

        private readonly Punkt person = new Punkt(25, 15);
        private readonly Punkt personOben = new Punkt(500, 10);
        private readonly Punkt personUnten = new Punkt(250, 650);

        private readonly Punkt fahrspurOben = new Punkt(350, 250);
        private readonly Punkt fahrspurUnten = new Punkt(350, 400);

        private readonly double yPosition_B1 = 300;
        private readonly double yPosition_B2 = 330;

        public readonly Punkt parkenOben;
        public readonly Punkt parkenUnten;
        public readonly Punkt eingangOben;
        public readonly Punkt eingangUnten;

        public FahrzeugPerson(Rolle rolle, int wievieltesFahrzeugPerson)
        {
            double X_oben;
            double Y_oben;
            double X_unten;
            double Y_unten;

            Punkt KontrollPunktOben1;
            Punkt KontrollPunktOben2;
            Punkt KontrollPunktUnten1;
            Punkt KontrollPunktUnten2;

            FP_Rolle = rolle;

            if (FP_Rolle == Rolle.Fahrzeug)
            {
                Y_oben = fahrzeugOben.Y;
                X_oben = fahrzeugOben.X + wievieltesFahrzeugPerson * fahrzeug.X;
                if (wievieltesFahrzeugPerson < 4)
                {
                    X_unten = fahrzeugUntenLinks.X;
                    Y_unten = fahrzeugUntenLinks.Y + wievieltesFahrzeugPerson * fahrzeug.Y;

                    KontrollPunktUnten1 = new Punkt(fahrspurUnten.X, fahrspurUnten.Y + 100);
                    KontrollPunktUnten2 = new Punkt(X_unten + 100, Y_unten);
                }
                else
                {
                    X_unten = fahrzeugUntenRechts.X;
                    Y_unten = fahrzeugUntenRechts.Y + (wievieltesFahrzeugPerson - 4) * fahrzeug.Y;

                    KontrollPunktUnten1 = new Punkt(fahrspurUnten.X, fahrspurUnten.Y + 100);
                    KontrollPunktUnten2 = new Punkt(X_unten - 100, Y_unten);
                }
            }
            else
            {
                Y_oben = personOben.Y;
                X_oben = personOben.X + wievieltesFahrzeugPerson * person.X;
                X_unten = personUnten.X + wievieltesFahrzeugPerson * person.X;
                Y_unten = personUnten.Y;

                KontrollPunktUnten1 = new Punkt(fahrspurUnten.X, fahrspurUnten.Y + 100);
                KontrollPunktUnten2 = new Punkt(X_unten, Y_unten - 100);
            }

            KontrollPunktOben1 = new Punkt(X_oben, Y_oben + 100);
            KontrollPunktOben2 = new Punkt(fahrspurOben.X, fahrspurOben.Y - 100);

            parkenOben = new Punkt(X_oben, Y_oben);
            parkenUnten = new Punkt(X_unten, Y_unten);

            eingangOben = new Punkt(fahrspurOben.X, fahrspurOben.Y);
            eingangUnten = new Punkt(fahrspurUnten.X, fahrspurUnten.Y);

            kurveOben = new BezierCurve(parkenOben, KontrollPunktOben1, KontrollPunktOben2, eingangOben);
            kurveUnten = new BezierCurve(eingangUnten, KontrollPunktUnten1, KontrollPunktUnten2, parkenUnten);

            DraussenParken();
        }

        public void Losfahren()
        {
            if (Bewegung == FahrenRichtung.ObenGeparkt) Bewegung = FahrenRichtung.AbwaertsKurveOben;
            if (Bewegung == FahrenRichtung.UntenGeparkt) Bewegung = FahrenRichtung.AufwaertsKurveUnten;
        }

        public void DraussenParken() => Bewegung = FahrenRichtung.ObenGeparkt;
        public void DrinnenParken() => Bewegung = FahrenRichtung.UntenGeparkt;

        private bool LichtschrankeUnterbrochen(double Pos)
        {
            if (AktuellePosition.Y < Pos) return false;
            if (FP_Rolle == Rolle.Fahrzeug && AktuellePosition.Y > Pos + fahrzeug.Y) return false;
            if (FP_Rolle == Rolle.Person && AktuellePosition.Y > Pos + person.Y) return false;
            return true;
        }

        public (bool b1, bool b2, bool park) Bewegen()
        {
            bool AllesInParkPosition = false;

            switch (Bewegung)
            {
                case FahrenRichtung.ObenGeparkt:
                    AllesInParkPosition = true;
                    AktuellePosition = parkenOben;
                    kurvePosition = 0;
                    break;

                case FahrenRichtung.AbwaertsKurveOben:
                    AktuellePosition = kurveOben.PunktBestimmen(kurvePosition);
                    kurvePosition += kurveGeschwindigkeit;
                    if (kurvePosition >= 1) Bewegung = FahrenRichtung.AbwaertsSenkrecht;
                    break;

                case FahrenRichtung.AbwaertsSenkrecht:
                    if (AktuellePosition.Y < fahrspurUnten.Y) AktuellePosition.Y += xy_Bewegung;
                    else Bewegung = FahrenRichtung.AbwaertsKurveUnten;
                    kurvePosition = 0;
                    break;

                case FahrenRichtung.AbwaertsKurveUnten:
                    AktuellePosition = kurveUnten.PunktBestimmen(kurvePosition);
                    kurvePosition += kurveGeschwindigkeit;
                    if (kurvePosition >= 1) Bewegung = FahrenRichtung.UntenGeparkt;
                    break;

                case FahrenRichtung.UntenGeparkt:
                    AllesInParkPosition = true;
                    AktuellePosition = parkenUnten;
                    kurvePosition = 1;
                    break;

                case FahrenRichtung.AufwaertsKurveUnten:
                    AktuellePosition = kurveUnten.PunktBestimmen(kurvePosition);
                    kurvePosition -= kurveGeschwindigkeit;
                    if (kurvePosition <= 0) Bewegung = FahrenRichtung.AufwaertsSenkrecht;
                    break;

                case FahrenRichtung.AufwaertsSenkrecht:
                    if (AktuellePosition.Y > fahrspurOben.Y) AktuellePosition.Y -= xy_Bewegung;
                    else Bewegung = FahrenRichtung.AufwaertsKurveOben;
                    kurvePosition = 1;
                    break;

                case FahrenRichtung.AufwaertsKurveOben:
                    AktuellePosition = kurveOben.PunktBestimmen(kurvePosition);
                    kurvePosition -= kurveGeschwindigkeit;
                    if (kurvePosition <= 0) Bewegung = FahrenRichtung.ObenGeparkt;
                    break;

                default:
                    break;
            }

            return (LichtschrankeUnterbrochen(yPosition_B1), LichtschrankeUnterbrochen(yPosition_B2), AllesInParkPosition);
        }
    }
}