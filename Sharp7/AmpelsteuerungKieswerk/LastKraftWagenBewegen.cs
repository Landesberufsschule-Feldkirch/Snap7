namespace AmpelsteuerungKieswerk
{
    public partial class LKW
    {
        private LKW_Positionen LKW_Position = LKW_Positionen.LinksGeparkt;

        private readonly double BreiteLKW = 100;
        private readonly double HoeheLWK = 80;
        private readonly double x_B1 = 275;
        private readonly double x_B2 = 350;
        private readonly double x_B3 = 1000;
        private readonly double x_B4 = 1075;
        private readonly double xy_Bewegung = 1;
        private readonly double KurveGeschwindigkeit = 0.002;

        private double KurvePosition;

        public (bool b1, bool b2, bool b3, bool b4) LastwagenFahren()
        {
            switch (LKW_Position)
            {
                case LKW_Positionen.LinksGeparkt:
                    PosAktuell = ParkPosLinks;
                    KurvePosition = 0;
                    LKW_Richtung = LKW_Richtungen.NachRechts;
                    break;
                case LKW_Positionen.LR_LinkeKurve:
                    PosAktuell = LinkeKurve.PunktBestimmen(KurvePosition);
                    KurvePosition += KurveGeschwindigkeit;
                    if (KurvePosition >= 1) LKW_Position = LKW_Positionen.LR_Waagrecht;
                    break;
                case LKW_Positionen.LR_Waagrecht:
                    if (PosAktuell.X < x_AnfangRechteKurve) PosAktuell.X += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.LR_RechtKurve;
                    KurvePosition = 0;
                    break;
                case LKW_Positionen.LR_RechtKurve:
                    PosAktuell = RechteKurve.PunktBestimmen(KurvePosition);

                    KurvePosition += KurveGeschwindigkeit;
                    if (KurvePosition >= 1) LKW_Position = LKW_Positionen.RechtsGeparkt;
                    break;

                case LKW_Positionen.RechtsGeparkt:
                    PosAktuell = ParkPosRechts;
                    LKW_Richtung = LKW_Richtungen.NachLinks;
                    KurvePosition = 1;
                    break;


                case LKW_Positionen.RL_RechteKurve:
                    PosAktuell = RechteKurve.PunktBestimmen(KurvePosition);
                    KurvePosition -= KurveGeschwindigkeit;
                    if (KurvePosition <= 0) LKW_Position = LKW_Positionen.RL_Waagrecht;
                    break;
                case LKW_Positionen.RL_Waagrecht:
                    if (PosAktuell.X > x_EndeLinkeKurve) PosAktuell.X -= xy_Bewegung;
                    else LKW_Position = LKW_Positionen.RL_LinkeKurve;
                    KurvePosition = 1;
                    break;
                case LKW_Positionen.RL_LinkeKurve:
                    PosAktuell = LinkeKurve.PunktBestimmen(KurvePosition);
                    KurvePosition -= KurveGeschwindigkeit;
                    if (KurvePosition <= 0) LKW_Position = LKW_Positionen.LinksGeparkt;
                    break;

                default: break;
            }

            return (LichtschrankeUnterbrochen(x_B1), LichtschrankeUnterbrochen(x_B2), LichtschrankeUnterbrochen(x_B3), LichtschrankeUnterbrochen(x_B4));
        }
    }
}