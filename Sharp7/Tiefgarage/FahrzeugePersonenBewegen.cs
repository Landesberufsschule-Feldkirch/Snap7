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
            Tuple<double, double> KurveDatenpunkte;
      
            switch (Bewegung)
            {
                case FahrenRichtung.ObenGeparkt:
                    X_aktuell = X_oben;
                    Y_aktuell = Y_oben;
                    KurvePosition = 0;
                    break;

                case FahrenRichtung.AbwaertsKurveOben:
                    KurveDatenpunkte = KurveOben.PunktBestimmen(KurvePosition);
                    X_aktuell = KurveDatenpunkte.Item1;
                    Y_aktuell = KurveDatenpunkte.Item2;
                    KurvePosition += KurveGeschwindigkeit;
                    if (KurvePosition >= 1) Bewegung = FahrenRichtung.AbwaertsSenkrecht;
                    break;

                case FahrenRichtung.AbwaertsSenkrecht:
                    if (Y_aktuell < Y_Fahrspur_unten) Y_aktuell += xy_Bewegung;
                    else Bewegung = FahrenRichtung.AbwaertsKurveUnten;
                    KurvePosition = 0;
                    break;

                case FahrenRichtung.AbwaertsKurveUnten:
                    KurveDatenpunkte = KurveUnten.PunktBestimmen(KurvePosition);
                    X_aktuell = KurveDatenpunkte.Item1;
                    Y_aktuell = KurveDatenpunkte.Item2;
                    KurvePosition += KurveGeschwindigkeit;
                    if (KurvePosition >= 1) Bewegung = FahrenRichtung.UntenGeparkt;
                    break;

                case FahrenRichtung.UntenGeparkt:
                    X_aktuell = X_unten;
                    Y_aktuell = Y_unten;
                    KurvePosition = 1;
                    break;


                case FahrenRichtung.AufwaertsKurveUnten:
                    KurveDatenpunkte = KurveUnten.PunktBestimmen(KurvePosition);
                    X_aktuell = KurveDatenpunkte.Item1;
                    Y_aktuell = KurveDatenpunkte.Item2;
                    KurvePosition -= KurveGeschwindigkeit;
                    if (KurvePosition <= 0) Bewegung = FahrenRichtung.AufwaertsSenkrecht;
                    break;

                case FahrenRichtung.AufwaertsSenkrecht:
                    if (Y_aktuell > Y_Fahrspur_oben) Y_aktuell -= xy_Bewegung;
                    else Bewegung = FahrenRichtung.AufwaertsKurveOben;
                    KurvePosition = 1;
                    break;

                case FahrenRichtung.AufwaertsKurveOben:
                    KurveDatenpunkte = KurveOben.PunktBestimmen(KurvePosition);
                    X_aktuell = KurveDatenpunkte.Item1;
                    Y_aktuell = KurveDatenpunkte.Item2;
                    KurvePosition -= KurveGeschwindigkeit;
                    if (KurvePosition <= 0) Bewegung = FahrenRichtung.ObenGeparkt;
                    break;


                default:
                    break;
            }
            
            return new Tuple<bool, bool>(LichtschrankeUnterbrochen(Y_Position_B1), LichtschrankeUnterbrochen(Y_Position_B2) );
        }
    }
}