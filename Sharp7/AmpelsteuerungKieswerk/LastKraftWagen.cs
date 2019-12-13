using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AmpelsteuerungKieswerk
{
    public class LKW
    {
        private enum LKW_Richtungen
        {
            NachRechts = 0,
            NachLinks
        }
        private enum LKW_Positionen
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

        static int AnzahlLKW;
        private int ID;
        private LKW_Richtungen LKW_Richtung = LKW_Richtungen.NachRechts;
        private LKW_Richtungen LKW_RichtungAlt = LKW_Richtungen.NachRechts;
        public Button btnLKW { get; set; }
        public Image imgLKW { get; set; }
        public double x_LKW_Aktuell { get; set; }
        public double y_LKW_Aktuell { get; set; }
        public double y_LKW_Parkposition { get; set; }

        private LKW_Positionen LKW_Position = LKW_Positionen.LinksGeparkt;

        private bool B1, B2, B3, B4;

        private readonly double x_ParkpositionLinks = 10;
        private readonly double x_ParkpositionRechts = 1300;

        private readonly double y_ParkpositionOben = 10;
        private readonly double BreiteLKW = 100;
        private readonly double HoeheLWK = 80;

        private readonly double x_SenkrechtLinks = 150;
        private readonly double x_SenkrechtRechts = 1150;

        private readonly double y_Fahrspur = 200;
        private readonly double xy_Bewegung = 0.01;

        private readonly double x_B1 = 275;
        private readonly double x_B2 = 350;
        private readonly double x_B3 = 1000;
        private readonly double x_B4 = 1075;

        public LKW(Button btn, Image img)
        {
            this.ID = AnzahlLKW;
            AnzahlLKW++;

            this.btnLKW = btn;
            this.imgLKW = img;
            this.y_LKW_Parkposition = y_ParkpositionOben + this.ID * (10 + HoeheLWK);
        }

        public void AnzeigeAktualisieren(bool FensterAktiv)
        {
            if (FensterAktiv)
            {
                btnLKW.SetValue(Canvas.LeftProperty, x_LKW_Aktuell);
                btnLKW.SetValue(Canvas.TopProperty, y_LKW_Aktuell);

                if (LKW_RichtungAlt != LKW_Richtung)
                {
                    if (LKW_Richtung == LKW_Richtungen.NachRechts)
                    {
                        imgLKW.RenderTransformOrigin = new Point(0.5, 0.5);
                        ScaleTransform flipTrans = new ScaleTransform();
                        flipTrans.ScaleX = 1;
                        imgLKW.RenderTransform = flipTrans;
                    }
                    if (LKW_Richtung == LKW_Richtungen.NachLinks)
                    {
                        imgLKW.RenderTransformOrigin = new Point(0.5, 0.5);
                        ScaleTransform flipTrans = new ScaleTransform();
                        flipTrans.ScaleX = -1;
                        imgLKW.RenderTransform = flipTrans;
                    }
                }
                LKW_RichtungAlt = LKW_Richtung;

            }
        }


        public void linksParken() { LKW_Position = LKW_Positionen.LinksGeparkt; }
        public void rechtsParken() { LKW_Position = LKW_Positionen.RechtsGeparkt; }

        public void Losfahren()
        {
            if (LKW_Position == LKW_Positionen.LinksGeparkt) LKW_Position = LKW_Positionen.LR_LinksWaagrecht;
            if (LKW_Position == LKW_Positionen.RechtsGeparkt) LKW_Position = LKW_Positionen.RL_RechtsWaagrecht;
        }

        public Tuple<bool, bool, bool, bool> LastwagenFahren()
        {
            switch (LKW_Position)
            {
                case LKW_Positionen.LinksGeparkt:
                    x_LKW_Aktuell = x_ParkpositionLinks;
                    y_LKW_Aktuell = y_LKW_Parkposition;
                    LKW_Richtung = LKW_Richtungen.NachRechts;
                    break;
                case LKW_Positionen.LR_LinksWaagrecht:
                    if (x_LKW_Aktuell < x_SenkrechtLinks) x_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.LR_LinksSenkrecht;
                    break;
                case LKW_Positionen.LR_LinksSenkrecht:
                    if (y_LKW_Aktuell > y_Fahrspur + 1) y_LKW_Aktuell -= xy_Bewegung;
                    else if (y_LKW_Aktuell < y_Fahrspur - 1) y_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.LR_NachRechts;
                    break;
                case LKW_Positionen.LR_NachRechts:
                    if (x_LKW_Aktuell < x_SenkrechtRechts) x_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.LR_RechtsSenkrecht;
                    break;
                case LKW_Positionen.LR_RechtsSenkrecht:
                    if (y_LKW_Aktuell > y_LKW_Parkposition + 1) y_LKW_Aktuell -= xy_Bewegung;
                    else if (y_LKW_Aktuell < y_LKW_Parkposition - 1) y_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.LR_RechtsWaagrecht;
                    break;
                case LKW_Positionen.LR_RechtsWaagrecht:
                    if (x_LKW_Aktuell < x_ParkpositionRechts) x_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.RechtsGeparkt;
                    break;

                case LKW_Positionen.RechtsGeparkt:
                    x_LKW_Aktuell = x_ParkpositionRechts;
                    y_LKW_Aktuell = y_LKW_Parkposition;
                    LKW_Richtung = LKW_Richtungen.NachLinks;
                    break;

                case LKW_Positionen.RL_RechtsWaagrecht:
                    if (x_LKW_Aktuell > x_SenkrechtRechts) x_LKW_Aktuell -= xy_Bewegung;
                    else LKW_Position = LKW_Positionen.RL_RechtsSenkrecht;
                    break;
                case LKW_Positionen.RL_RechtsSenkrecht:
                    if (y_LKW_Aktuell > y_Fahrspur + 1) y_LKW_Aktuell -= xy_Bewegung;
                    else if (y_LKW_Aktuell < y_Fahrspur - 1) y_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.RL_NachLinks;
                    break;
                case LKW_Positionen.RL_NachLinks:
                    if (x_LKW_Aktuell > x_SenkrechtLinks) x_LKW_Aktuell -= xy_Bewegung;
                    else LKW_Position = LKW_Positionen.RL_LinksSenkrecht;
                    break;
                case LKW_Positionen.RL_LinksSenkrecht:
                    if (y_LKW_Aktuell > y_LKW_Parkposition + 1) y_LKW_Aktuell -= xy_Bewegung;
                    else if (y_LKW_Aktuell < y_LKW_Parkposition - 1) y_LKW_Aktuell += xy_Bewegung;
                    else LKW_Position = LKW_Positionen.RL_LinksWaagrecht;
                    break;
                case LKW_Positionen.RL_LinksWaagrecht:
                    if (x_LKW_Aktuell > x_ParkpositionLinks) x_LKW_Aktuell -= xy_Bewegung;
                    else LKW_Position = LKW_Positionen.LinksGeparkt;
                    break;

                default: break;
            }

            B1 = LichtschrankeUnterbrochen(x_B1);
            B2 = LichtschrankeUnterbrochen(x_B2);
            B3 = LichtschrankeUnterbrochen(x_B3);
            B4 = LichtschrankeUnterbrochen(x_B4);

            return new Tuple<bool, bool, bool, bool>(B1, B2, B3, B4);
        }

        bool LichtschrankeUnterbrochen(double xPos)
        {
            if (x_LKW_Aktuell + BreiteLKW < xPos) return false;
            if (x_LKW_Aktuell > xPos) return false;
            return true;
        }

    }
}