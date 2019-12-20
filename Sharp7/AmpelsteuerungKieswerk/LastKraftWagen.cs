using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Utilities;

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
            LR_LinkeKurve,
            LR_Waagrecht,
            LR_RechtKurve,
            RechtsGeparkt,
            RL_RechteKurve,
            RL_Waagrecht,
            RL_LinkeKurve
        }

        static int AnzahlLKW;
        private readonly int ID;
        private LKW_Richtungen LKW_Richtung = LKW_Richtungen.NachRechts;
        private LKW_Richtungen LKW_RichtungAlt = LKW_Richtungen.NachRechts;
        private readonly Image ImgLKW;
        private Punkt PosAktuell;
        private readonly Punkt ParkPosLinks;
        private readonly Punkt ParkPosRechts;
        private readonly Punkt WegPosLinks;
        private readonly Punkt WegPosRechts;

        private readonly double Y_LKW_Parkposition;
        private readonly BezierCurve LinkeKurve;
        private readonly BezierCurve RechteKurve;

        private readonly double x_ParkpositionLinks = 10;
        private readonly double x_EndeLinkeKurve = 250;
        private readonly double x_AnfangRechteKurve = 1100;
        private readonly double x_ParkpositionRechts = 1340;
        private readonly double y_ParkpositionOben = 10;
        private readonly double y_Fahrspur = 200;

        public LKW(Image img)
        {
            ID = AnzahlLKW;
            AnzahlLKW++;

            ImgLKW = img;
            Y_LKW_Parkposition = y_ParkpositionOben + ID * (10 + HoeheLWK);

            ParkPosLinks = new Punkt(x_ParkpositionLinks, Y_LKW_Parkposition);
            Punkt KontrollPunktLinks1 = new Punkt(x_ParkpositionLinks + 100, Y_LKW_Parkposition);
            Punkt KontrollPunktLinks2 = new Punkt(x_EndeLinkeKurve - 100, y_Fahrspur);

            ParkPosRechts = new Punkt(x_ParkpositionRechts, Y_LKW_Parkposition);
            Punkt KontrollPunktRechts1 = new Punkt(x_AnfangRechteKurve + 100, y_Fahrspur);
            Punkt KontrollPunktRechts2 = new Punkt(x_ParkpositionRechts - 100, Y_LKW_Parkposition);

            WegPosLinks = new Punkt(x_EndeLinkeKurve, y_Fahrspur);
            WegPosRechts = new Punkt(x_AnfangRechteKurve, y_Fahrspur);

            LinkeKurve = new BezierCurve(ParkPosLinks, KontrollPunktLinks1, KontrollPunktLinks2, WegPosLinks);
            RechteKurve = new BezierCurve(WegPosRechts, KontrollPunktRechts1, KontrollPunktRechts2, ParkPosRechts);

            PosAktuell = ParkPosLinks;
        }

        public void LastwagenAnzeigen(bool fensterAktiv, Button btn)
        {
            if (!fensterAktiv) return;

            btn.SetValue(Canvas.LeftProperty, PosAktuell.X);
            btn.SetValue(Canvas.TopProperty, PosAktuell.Y);

            if (LKW_RichtungAlt == LKW_Richtung) return;

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

            LKW_RichtungAlt = LKW_Richtung;
        }

        public void LinksParken() { LKW_Position = LKW_Positionen.LinksGeparkt; }
        public void RechtsParken() { LKW_Position = LKW_Positionen.RechtsGeparkt; }

        public void Losfahren()
        {
            if (LKW_Position == LKW_Positionen.LinksGeparkt) LKW_Position = LKW_Positionen.LR_LinkeKurve;
            if (LKW_Position == LKW_Positionen.RechtsGeparkt) LKW_Position = LKW_Positionen.RL_RechteKurve;
        }

        bool LichtschrankeUnterbrochen(double xPos)
        {
            if (PosAktuell.X + BreiteLKW < xPos) return false;
            if (PosAktuell.X > xPos) return false;
            return true;
        }
    }
}