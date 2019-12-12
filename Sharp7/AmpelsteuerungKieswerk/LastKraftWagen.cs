using System.Windows.Controls;

namespace AmpelsteuerungKieswerk
{

    public class LKW
    {
        static int AnzahlLKW;
        private int LKW_ID;
        public Button btnLKW { get; set; }
        public double x_aktuell { get; set; }
        public double y_aktuell { get; set; }
        public double y_Parkposition { get; set; }

        private LKW_Position Position = LKW_Position.LinksGeparkt;

        private readonly double x_ParkpositionLinks = 10;
        private readonly double x_ParkpositionRechts = 1300;

        private readonly double y_ParkpositionOben = 10;
        private readonly double BreiteLKW = 100;
        private readonly double HoeheLWK = 80;

        private readonly double x_SenkrechtLinks = 150;
        private readonly double x_SenkrechtRechts = 1150;

        private readonly double y_Fahrspur = 200;
        private readonly double xy_Bewegung = 0.005;
        private enum LKW_Position
        {
            LinksGeparkt = 0,
            LR_LinksWaagrecht,
            LR_LinksSenkrecht,
            LR_NachRechts,
            LR_RechtsSenkrecht,
            LR_RechtsWaagrecht,
            RechtsGeparkt,
            RL_RechtsWaagrecht,
            RL_RechtsSenkrecht,
            RL_NachLinks,
            RL_LinksSenkrecht,
            RL_LinksWaagrecht
        }

        public LKW(Button btn)
        {
            this.LKW_ID = AnzahlLKW;
            AnzahlLKW++;

            this.btnLKW = btn;
            this.y_Parkposition = y_ParkpositionOben + this.LKW_ID * (10 + HoeheLWK);
        }

        public void AnzeigeAktualisieren(bool FensterAktiv)
        {
            if (FensterAktiv)
            {
                btnLKW.SetValue(Canvas.LeftProperty, x_aktuell);
                btnLKW.SetValue(Canvas.TopProperty, y_aktuell);
            }
        }


        public void linksParken()
        {
            x_aktuell = x_ParkpositionLinks;
            y_aktuell = y_Parkposition;
        }
        public void rechtsParken()
        {
            x_aktuell = x_ParkpositionRechts;
            y_aktuell = y_Parkposition;
        }

        public void Losfahren()
        {
            if (Position == LKW_Position.LinksGeparkt) Position = LKW_Position.LR_LinksWaagrecht;
            if (Position == LKW_Position.RechtsGeparkt) Position = LKW_Position.RL_RechtsWaagrecht;
        }

        public void LastwagenFahren()
        {

            switch (Position)
            {
                case LKW_Position.LinksGeparkt:
                    x_aktuell = x_ParkpositionLinks;
                    y_aktuell = y_Parkposition;
                    break;
                case LKW_Position.LR_LinksWaagrecht:
                    if (x_aktuell < x_SenkrechtLinks) x_aktuell += xy_Bewegung;
                    else Position = LKW_Position.LR_LinksSenkrecht;
                    break;
                case LKW_Position.LR_LinksSenkrecht:
                    if (y_aktuell > y_Fahrspur + 1) y_aktuell -= xy_Bewegung;
                    else if (y_aktuell < y_Fahrspur - 1) y_aktuell += xy_Bewegung;
                    else Position = LKW_Position.LR_NachRechts;
                    break;
                case LKW_Position.LR_NachRechts:
                    if (x_aktuell < x_SenkrechtRechts) x_aktuell += xy_Bewegung;
                    else Position = LKW_Position.LR_RechtsSenkrecht;
                    break;
                case LKW_Position.LR_RechtsSenkrecht:
                    if (y_aktuell > y_Parkposition + 1) y_aktuell -= xy_Bewegung;
                    else if (y_aktuell < y_Parkposition - 1) y_aktuell += xy_Bewegung;
                    else Position = LKW_Position.LR_RechtsWaagrecht;
                    break;
                case LKW_Position.LR_RechtsWaagrecht:
                    if (x_aktuell < x_ParkpositionRechts) x_aktuell += xy_Bewegung;
                    else Position = LKW_Position.RechtsGeparkt;
                    break;

                case LKW_Position.RechtsGeparkt:
                    x_aktuell = x_ParkpositionRechts;
                    y_aktuell = y_Parkposition;
                    break;

                case LKW_Position.RL_RechtsWaagrecht:
                    if (x_aktuell > x_ParkpositionRechts) x_aktuell -= xy_Bewegung;
                    else Position = LKW_Position.RL_RechtsSenkrecht;
                    break;
                case LKW_Position.RL_RechtsSenkrecht:
                    if (y_aktuell > y_Fahrspur + 1) y_aktuell -= xy_Bewegung;
                    else if (y_aktuell < y_Fahrspur - 1) y_aktuell += xy_Bewegung;
                    else Position = LKW_Position.RL_NachLinks;
                    break;
                case LKW_Position.RL_NachLinks:
                    if (x_aktuell < x_SenkrechtLinks) x_aktuell -= xy_Bewegung;
                    else Position = LKW_Position.RL_LinksSenkrecht;
                    break;
                case LKW_Position.RL_LinksSenkrecht:
                    if (y_aktuell > y_Parkposition + 1) y_aktuell -= xy_Bewegung;
                    else if (y_aktuell < y_Parkposition - 1) y_aktuell += xy_Bewegung;
                    else Position = LKW_Position.RL_LinksWaagrecht;
                    break;
                case LKW_Position.RL_LinksWaagrecht:
                    if (x_aktuell > x_ParkpositionLinks) x_aktuell -= xy_Bewegung;
                    else Position = LKW_Position.LinksGeparkt;
                    break;

                default: break;
                    
            }
        }
    }
}