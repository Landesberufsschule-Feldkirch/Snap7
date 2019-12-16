namespace AmpelsteuerungKieswerk
{
    public partial class LKW
    {
        private LKW_Positionen LKW_Position = LKW_Positionen.LinksGeparkt;

        private readonly double x_ParkpositionLinks = 10;
        private readonly double x_ParkpositionRechts = 1300;

        private readonly double y_ParkpositionOben = 10;
        private readonly double BreiteLKW = 100;
        private readonly double HoeheLWK = 80;

        private readonly double x_SenkrechtLinks = 150;
        private readonly double x_SenkrechtRechts = 1150;

        private readonly double y_Fahrspur = 200;
        private readonly double xy_Bewegung = 1;

        private readonly double x_B1 = 275;
        private readonly double x_B2 = 350;
        private readonly double x_B3 = 1000;
        private readonly double x_B4 = 1075;

        public (bool b1, bool b2, bool b3, bool b4) LastwagenFahren()
        {
            switch (LKW_Position)
            {
                case LKW_Positionen.LinksGeparkt:
                    X_LKW_Aktuell = x_ParkpositionLinks;
                    Y_LKW_Aktuell = Y_LKW_Parkposition;
                    LKW_Richtung = LKW_Richtungen.NachRechts;
                    break;
                case LKW_Positionen.LR_LinksWaagrecht:
                    if (X_LKW_Aktuell < x_SenkrechtLinks) X_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.LR_LinksSenkrecht;
                    break;
                case LKW_Positionen.LR_LinksSenkrecht:
                    if (Y_LKW_Aktuell > y_Fahrspur + 1) Y_LKW_Aktuell -= xy_Bewegung;
                    else if (Y_LKW_Aktuell < y_Fahrspur - 1) Y_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.LR_NachRechts;
                    break;
                case LKW_Positionen.LR_NachRechts:
                    if (X_LKW_Aktuell < x_SenkrechtRechts) X_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.LR_RechtsSenkrecht;
                    break;
                case LKW_Positionen.LR_RechtsSenkrecht:
                    if (Y_LKW_Aktuell > Y_LKW_Parkposition + 1) Y_LKW_Aktuell -= xy_Bewegung;
                    else if (Y_LKW_Aktuell < Y_LKW_Parkposition - 1) Y_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.LR_RechtsWaagrecht;
                    break;
                case LKW_Positionen.LR_RechtsWaagrecht:
                    if (X_LKW_Aktuell < x_ParkpositionRechts) X_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.RechtsGeparkt;
                    break;

                case LKW_Positionen.RechtsGeparkt:
                    X_LKW_Aktuell = x_ParkpositionRechts;
                    Y_LKW_Aktuell = Y_LKW_Parkposition;
                    LKW_Richtung = LKW_Richtungen.NachLinks;
                    break;

                case LKW_Positionen.RL_RechtsWaagrecht:
                    if (X_LKW_Aktuell > x_SenkrechtRechts) X_LKW_Aktuell -= xy_Bewegung;
                    else LKW_Position = LKW_Positionen.RL_RechtsSenkrecht;
                    break;
                case LKW_Positionen.RL_RechtsSenkrecht:
                    if (Y_LKW_Aktuell > y_Fahrspur + 1) Y_LKW_Aktuell -= xy_Bewegung;
                    else if (Y_LKW_Aktuell < y_Fahrspur - 1) Y_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.RL_NachLinks;
                    break;
                case LKW_Positionen.RL_NachLinks:
                    if (X_LKW_Aktuell > x_SenkrechtLinks) X_LKW_Aktuell -= xy_Bewegung;
                    else LKW_Position = LKW_Positionen.RL_LinksSenkrecht;
                    break;
                case LKW_Positionen.RL_LinksSenkrecht:
                    if (Y_LKW_Aktuell > Y_LKW_Parkposition + 1) Y_LKW_Aktuell -= xy_Bewegung;
                    else if (Y_LKW_Aktuell < Y_LKW_Parkposition - 1) Y_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.RL_LinksWaagrecht;
                    break;
                case LKW_Positionen.RL_LinksWaagrecht:
                    if (X_LKW_Aktuell > x_ParkpositionLinks) X_LKW_Aktuell -= xy_Bewegung;
                    else LKW_Position = LKW_Positionen.LinksGeparkt;
                    break;

                default: break;
            }

            return (LichtschrankeUnterbrochen(x_B1), LichtschrankeUnterbrochen(x_B2), LichtschrankeUnterbrochen(x_B3), LichtschrankeUnterbrochen(x_B4));
        }
    }
}