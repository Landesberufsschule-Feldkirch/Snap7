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
        private readonly double KurveGeschwindigkeit = 0.002;

        private double KurvePosition;

        private static int AnzahlFahrzeuge = 0;
        private static int AnzahlPersonen = 0;

        private readonly BezierCurve KurveOben;
        private readonly BezierCurve KurveUnten;

        private readonly Punkt Fahrzeug = new Punkt(100, 50);
        private readonly Punkt Person = new Punkt(25, 15);

        private readonly Punkt FahrzeugOben = new Punkt(10, 10);
        private readonly Punkt PersonOben = new Punkt(500, 10);
        private readonly Punkt FahrzeugUntenLinks = new Punkt(110, 500);
        private readonly Punkt FahrzeugUntenRechts = new Punkt(810, 500);
        private readonly Punkt PersonUnten = new Punkt(250, 650);

        private readonly Punkt FahrspurOben = new Punkt(350, 250);
        private readonly Punkt FahrspurUnten = new Punkt(350, 400);

        private readonly double Y_Position_B1 = 300;
        private readonly double Y_Position_B2 = 330;

        public readonly Punkt ParkenOben;
        public readonly Punkt ParkenUnten;
        public readonly Punkt EingangOben;
        public readonly Punkt EingangUnten;

        public FahrzeugPerson(Rolle rolle)
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
                Y_oben = FahrzeugOben.Y;
                X_oben = FahrzeugOben.X + AnzahlFahrzeuge * Fahrzeug.X;
                if (AnzahlFahrzeuge < 4)
                {
                    X_unten = FahrzeugUntenLinks.X;
                    Y_unten = FahrzeugUntenLinks.Y + AnzahlFahrzeuge * Fahrzeug.Y;

                    KontrollPunktUnten1 = new Punkt(FahrspurUnten.X, FahrspurUnten.Y + 100);
                    KontrollPunktUnten2 = new Punkt(X_unten + 100, Y_unten);
                }
                else
                {
                    X_unten = FahrzeugUntenRechts.X;
                    Y_unten = FahrzeugUntenRechts.Y + (AnzahlFahrzeuge - 4) * Fahrzeug.Y;

                    KontrollPunktUnten1 = new Punkt(FahrspurUnten.X, FahrspurUnten.Y + 100);
                    KontrollPunktUnten2 = new Punkt(X_unten - 100, Y_unten);
                }
                AnzahlFahrzeuge++;
            }
            else
            {
                Y_oben = PersonOben.Y;
                X_oben = PersonOben.X + AnzahlPersonen * Person.X;
                X_unten = PersonUnten.X + AnzahlPersonen * Person.X;
                Y_unten = PersonUnten.Y;

                KontrollPunktUnten1 = new Punkt(FahrspurUnten.X, FahrspurUnten.Y + 100);
                KontrollPunktUnten2 = new Punkt(X_unten, Y_unten - 100);

                AnzahlPersonen++;
            }

            KontrollPunktOben1 = new Punkt(X_oben, Y_oben + 100);
            KontrollPunktOben2 = new Punkt(FahrspurOben.X, FahrspurOben.Y - 100);

            ParkenOben = new Punkt(X_oben, Y_oben);
            ParkenUnten = new Punkt(X_unten, Y_unten);

            EingangOben = new Punkt(FahrspurOben.X, FahrspurOben.Y);
            EingangUnten = new Punkt(FahrspurUnten.X, FahrspurUnten.Y);

            KurveOben = new BezierCurve(ParkenOben, KontrollPunktOben1, KontrollPunktOben2, EingangOben);
            KurveUnten = new BezierCurve(EingangUnten, KontrollPunktUnten1, KontrollPunktUnten2, ParkenUnten);

            DraussenParken();
        }

        public void Losfahren()
        {
            if (Bewegung == FahrenRichtung.ObenGeparkt) Bewegung = FahrenRichtung.AbwaertsKurveOben;
            if (Bewegung == FahrenRichtung.UntenGeparkt) Bewegung = FahrenRichtung.AufwaertsKurveUnten;
        }

        public void DraussenParken() { Bewegung = FahrenRichtung.ObenGeparkt; }
        public void DrinnenParken() { Bewegung = FahrenRichtung.UntenGeparkt; }

        private bool LichtschrankeUnterbrochen(double Pos)
        {
            if (AktuellePosition.Y < Pos) return false;
            if (FP_Rolle == Rolle.Fahrzeug && AktuellePosition.Y > Pos + Fahrzeug.Y) return false;
            if (FP_Rolle == Rolle.Person && AktuellePosition.Y > Pos + Person.Y) return false;
            return true;
        }

        public (bool b1, bool b2, bool park) Bewegen()
        {
            bool AllesInParkPosition = false;

            switch (Bewegung)
            {
                case FahrenRichtung.ObenGeparkt:
                    AllesInParkPosition = true;
                    AktuellePosition = ParkenOben;
                    KurvePosition = 0;
                    break;

                case FahrenRichtung.AbwaertsKurveOben:
                    AktuellePosition = KurveOben.PunktBestimmen(KurvePosition);
                    KurvePosition += KurveGeschwindigkeit;
                    if (KurvePosition >= 1) Bewegung = FahrenRichtung.AbwaertsSenkrecht;
                    break;

                case FahrenRichtung.AbwaertsSenkrecht:
                    if (AktuellePosition.Y < FahrspurUnten.Y) AktuellePosition.Y += xy_Bewegung;
                    else Bewegung = FahrenRichtung.AbwaertsKurveUnten;
                    KurvePosition = 0;
                    break;

                case FahrenRichtung.AbwaertsKurveUnten:
                    AktuellePosition = KurveUnten.PunktBestimmen(KurvePosition);
                    KurvePosition += KurveGeschwindigkeit;
                    if (KurvePosition >= 1) Bewegung = FahrenRichtung.UntenGeparkt;
                    break;

                case FahrenRichtung.UntenGeparkt:
                    AllesInParkPosition = true;
                    AktuellePosition = ParkenUnten;
                    KurvePosition = 1;
                    break;

                case FahrenRichtung.AufwaertsKurveUnten:
                    AktuellePosition = KurveUnten.PunktBestimmen(KurvePosition);
                    KurvePosition -= KurveGeschwindigkeit;
                    if (KurvePosition <= 0) Bewegung = FahrenRichtung.AufwaertsSenkrecht;
                    break;

                case FahrenRichtung.AufwaertsSenkrecht:
                    if (AktuellePosition.Y > FahrspurOben.Y) AktuellePosition.Y -= xy_Bewegung;
                    else Bewegung = FahrenRichtung.AufwaertsKurveOben;
                    KurvePosition = 1;
                    break;

                case FahrenRichtung.AufwaertsKurveOben:
                    AktuellePosition = KurveOben.PunktBestimmen(KurvePosition);
                    KurvePosition -= KurveGeschwindigkeit;
                    if (KurvePosition <= 0) Bewegung = FahrenRichtung.ObenGeparkt;
                    break;

                default:
                    break;
            }

            return (LichtschrankeUnterbrochen(Y_Position_B1), LichtschrankeUnterbrochen(Y_Position_B2), AllesInParkPosition);
        }
    }
}