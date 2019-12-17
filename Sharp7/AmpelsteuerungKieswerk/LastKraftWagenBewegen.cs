using System;

namespace AmpelsteuerungKieswerk
{
    public partial class LKW
    {
        private LKW_Positionen LKW_Position = LKW_Positionen.LinksGeparkt;

        private readonly double BreiteLKW = 100;
        private readonly double HoeheLWK = 80;

        private readonly double x_ParkpositionLinks = 10;
        private readonly double x_EndeLinkeKurve = 250;
        private readonly double x_B1 = 275;
        private readonly double x_B2 = 350;
        private readonly double x_B3 = 1000;
        private readonly double x_B4 = 1075;
        private readonly double x_AnfangRechteKurve = 1100;
        private readonly double x_ParkpositionRechts = 1340;

        private readonly double y_ParkpositionOben = 10;
        private readonly double y_Fahrspur = 200;

        private readonly double xy_Bewegung = 1;
        private readonly double KurveGeschwindigkeit = 0.002;

        private double KurvePosition;

        public (bool b1, bool b2, bool b3, bool b4) LastwagenFahren()
        {
            (double x, double y) KurveDatenpunkte;

            switch (LKW_Position)
            {
                case LKW_Positionen.LinksGeparkt:
                    X_LKW_Aktuell = x_ParkpositionLinks;
                    Y_LKW_Aktuell = Y_LKW_Parkposition;
                    KurvePosition = 0;
                    LKW_Richtung = LKW_Richtungen.NachRechts;
                    break;
                case LKW_Positionen.LR_LinkeKurve:
                    KurveDatenpunkte = LinkeKurve.PunktBestimmen(KurvePosition);
                    X_LKW_Aktuell = KurveDatenpunkte.x;
                    Y_LKW_Aktuell = KurveDatenpunkte.y;
                    KurvePosition += KurveGeschwindigkeit;
                    if (KurvePosition >= 1) LKW_Position = LKW_Positionen.LR_Waagrecht;
                    break;
                case LKW_Positionen.LR_Waagrecht:
                    if (X_LKW_Aktuell < x_AnfangRechteKurve) X_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.LR_RechtKurve;
                    KurvePosition = 0;
                    break;
                case LKW_Positionen.LR_RechtKurve:
                    KurveDatenpunkte = RechteKurve.PunktBestimmen(KurvePosition);
                    X_LKW_Aktuell = KurveDatenpunkte.x;
                    Y_LKW_Aktuell = KurveDatenpunkte.y;
                    KurvePosition += KurveGeschwindigkeit;
                    if (KurvePosition >= 1) LKW_Position = LKW_Positionen.RechtsGeparkt;
                    break;

                case LKW_Positionen.RechtsGeparkt:
                    X_LKW_Aktuell = x_ParkpositionRechts;
                    Y_LKW_Aktuell = Y_LKW_Parkposition;
                    LKW_Richtung = LKW_Richtungen.NachLinks;
                    KurvePosition = 1;
                    break;


                case LKW_Positionen.RL_RechteKurve:
                    KurveDatenpunkte = RechteKurve.PunktBestimmen(KurvePosition);
                    X_LKW_Aktuell = KurveDatenpunkte.x;
                    Y_LKW_Aktuell = KurveDatenpunkte.y;
                    KurvePosition -= KurveGeschwindigkeit;
                    if (KurvePosition <= 0) LKW_Position = LKW_Positionen.RL_Waagrecht;
                    break;
                case LKW_Positionen.RL_Waagrecht:
                    if (X_LKW_Aktuell > x_EndeLinkeKurve) X_LKW_Aktuell -= xy_Bewegung;
                    else LKW_Position = LKW_Positionen.RL_LinkeKurve;
                    KurvePosition = 1;
                    break;
                case LKW_Positionen.RL_LinkeKurve:
                    KurveDatenpunkte = LinkeKurve.PunktBestimmen(KurvePosition);
                    X_LKW_Aktuell = KurveDatenpunkte.x;
                    Y_LKW_Aktuell = KurveDatenpunkte.y;
                    KurvePosition -= KurveGeschwindigkeit;
                    if (KurvePosition <= 0) LKW_Position = LKW_Positionen.LinksGeparkt;
                    break;

                default: break;
            }

            return (LichtschrankeUnterbrochen(x_B1), LichtschrankeUnterbrochen(x_B2), LichtschrankeUnterbrochen(x_B3), LichtschrankeUnterbrochen(x_B4));
        }
    }
}