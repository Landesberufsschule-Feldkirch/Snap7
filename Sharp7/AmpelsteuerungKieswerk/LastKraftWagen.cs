using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AmpelsteuerungKieswerk
{
    public partial class LKW
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
        private readonly int ID;
        private LKW_Richtungen LKW_Richtung = LKW_Richtungen.NachRechts;
        private LKW_Richtungen LKW_RichtungAlt = LKW_Richtungen.NachRechts;
        private Image ImgLKW;
        private double X_LKW_Aktuell;
        private double Y_LKW_Aktuell;
        private double Y_LKW_Parkposition;
               
        public LKW(Image img)
        {
            ID = AnzahlLKW;
            AnzahlLKW++;

            ImgLKW = img;
            Y_LKW_Parkposition = y_ParkpositionOben + ID * (10 + HoeheLWK);
        }

        public void LastwagenAnzeigen(bool fensterAktiv, Button btn)
        {
            if (fensterAktiv)
            {
                btn.SetValue(Canvas.LeftProperty, X_LKW_Aktuell);
                btn.SetValue(Canvas.TopProperty, Y_LKW_Aktuell);

                if (LKW_RichtungAlt != LKW_Richtung)
                {
                    if (LKW_Richtung == LKW_Richtungen.NachRechts)
                    {
                        ImgLKW.RenderTransformOrigin = new Point(0.5, 0.5);
                        ScaleTransform flipTrans = new ScaleTransform { ScaleX = 1 };
                        ImgLKW.RenderTransform = flipTrans;
                    }
                    if (LKW_Richtung == LKW_Richtungen.NachLinks)
                    {
                        ImgLKW.RenderTransformOrigin = new Point(0.5, 0.5);
                        ScaleTransform flipTrans = new ScaleTransform { ScaleX = -1 };
                        ImgLKW.RenderTransform = flipTrans;
                    }
                }

                LKW_RichtungAlt = LKW_Richtung;
            }
        }

        public void LinksParken() { LKW_Position = LKW_Positionen.LinksGeparkt; }
        public void RechtsParken() { LKW_Position = LKW_Positionen.RechtsGeparkt; }

        public void Losfahren()
        {
            if (LKW_Position == LKW_Positionen.LinksGeparkt) LKW_Position = LKW_Positionen.LR_LinksWaagrecht;
            if (LKW_Position == LKW_Positionen.RechtsGeparkt) LKW_Position = LKW_Positionen.RL_RechtsWaagrecht;
        }

        bool LichtschrankeUnterbrochen(double xPos)
        {
            if (X_LKW_Aktuell + BreiteLKW < xPos) return false;
            if (X_LKW_Aktuell > xPos) return false;
            return true;
        }

    }
}