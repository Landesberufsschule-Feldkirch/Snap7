using System;

namespace Tiefgarage
{
    public partial class FahrzeugPerson
    {
        private readonly double xy_Bewegung = 1;
        private readonly double KurveGeschwindigkeit = 0.002;

        private double KurvePosition;

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
        public Tuple<bool, bool> Bewegen()
        {
            switch (Bewegung)
            {
                case FahrenRichtung.ObenGeparkt:
                    PosAktuell = ParkenOben;
                    KurvePosition = 0;
                    break;

                case FahrenRichtung.AbwaertsKurveOben:
                    PosAktuell = KurveOben.PunktBestimmen(KurvePosition);
                    KurvePosition += KurveGeschwindigkeit;
                    if (KurvePosition >= 1) Bewegung = FahrenRichtung.AbwaertsSenkrecht;
                    break;

                case FahrenRichtung.AbwaertsSenkrecht:
                    if (PosAktuell.Y < Y_Fahrspur_unten) PosAktuell.Y += xy_Bewegung;
                    else Bewegung = FahrenRichtung.AbwaertsKurveUnten;
                    KurvePosition = 0;
                    break;

                case FahrenRichtung.AbwaertsKurveUnten:
                    PosAktuell = KurveUnten.PunktBestimmen(KurvePosition);
                    KurvePosition += KurveGeschwindigkeit;
                    if (KurvePosition >= 1) Bewegung = FahrenRichtung.UntenGeparkt;
                    break;

                case FahrenRichtung.UntenGeparkt:
                    PosAktuell = ParkenUnten;
                    KurvePosition = 1;
                    break;


                case FahrenRichtung.AufwaertsKurveUnten:
                    PosAktuell = KurveUnten.PunktBestimmen(KurvePosition);
                    KurvePosition -= KurveGeschwindigkeit;
                    if (KurvePosition <= 0) Bewegung = FahrenRichtung.AufwaertsSenkrecht;
                    break;

                case FahrenRichtung.AufwaertsSenkrecht:
                    if (PosAktuell.Y > Y_Fahrspur_oben) PosAktuell.Y -= xy_Bewegung;
                    else Bewegung = FahrenRichtung.AufwaertsKurveOben;
                    KurvePosition = 1;
                    break;

                case FahrenRichtung.AufwaertsKurveOben:
                    PosAktuell = KurveOben.PunktBestimmen(KurvePosition);
                    KurvePosition -= KurveGeschwindigkeit;
                    if (KurvePosition <= 0) Bewegung = FahrenRichtung.ObenGeparkt;
                    break;


                default:
                    break;
            }

            return new Tuple<bool, bool>(LichtschrankeUnterbrochen(Y_Position_B1), LichtschrankeUnterbrochen(Y_Position_B2));
        }
    }
}