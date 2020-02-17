namespace AmpelsteuerungKieswerk.Model
{
    using Utilities;
    public class LKW
    {
        public enum LkwRichtungen
        {
            NachRechts = 0,
            NachLinks
        }

        public enum LkwPositionen
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

        public LkwPositionen LKW_Position { get; set; }
        public LkwRichtungen LKW_Richtung { get; set; }
        public Punkt AktuellePosition { get; set; }

        private readonly Punkt ParkPosLinks;
        private readonly Punkt ParkPosRechts;
        private readonly BezierCurve LinkeKurve;
        private readonly BezierCurve RechteKurve;
        private double KurvePosition;

        private readonly Punkt ParkpositionLinks = new Punkt(10, 10);
        private readonly Punkt ParkpositionRechts = new Punkt(1340, 10);
        private readonly Punkt EndeLinkeKurve = new Punkt(250, 200);
        private readonly Punkt AnfangRechteKurve = new Punkt(1100, 200);
        private readonly Punkt GroesseLkw = new Punkt(100, 80);
        private readonly Punkt PositionB1 = new Punkt(275, 0);
        private readonly Punkt PositionB2 = new Punkt(350, 0);
        private readonly Punkt PositionB3 = new Punkt(1000, 0);
        private readonly Punkt PositionB4 = new Punkt(1075, 0);
        private readonly double xy_Bewegung = 1;
        private readonly double KurveGeschwindigkeit = 0.002;

        public LKW(int id)
        {
            LKW_Richtung = LkwRichtungen.NachRechts;
            LKW_Position = LkwPositionen.LinksGeparkt;

            var Y_LKW_Parkposition = ParkpositionLinks.Y + id * (10 + GroesseLkw.Y);

            ParkPosLinks = new Punkt(ParkpositionLinks.X, Y_LKW_Parkposition);
            Punkt KontrollPunktLinks1 = new Punkt(ParkpositionLinks.X + 100, Y_LKW_Parkposition);
            Punkt KontrollPunktLinks2 = new Punkt(EndeLinkeKurve.X - 100, EndeLinkeKurve.Y);

            ParkPosRechts = new Punkt(ParkpositionRechts.X, Y_LKW_Parkposition);
            Punkt KontrollPunktRechts1 = new Punkt(AnfangRechteKurve.X + 100, AnfangRechteKurve.Y);
            Punkt KontrollPunktRechts2 = new Punkt(ParkpositionRechts.X - 100, Y_LKW_Parkposition);

            var WegPosLinks = new Punkt(EndeLinkeKurve.X, EndeLinkeKurve.Y);
            var WegPosRechts = new Punkt(AnfangRechteKurve.X, AnfangRechteKurve.Y);

            LinkeKurve = new BezierCurve(ParkPosLinks, KontrollPunktLinks1, KontrollPunktLinks2, WegPosLinks);
            RechteKurve = new BezierCurve(WegPosRechts, KontrollPunktRechts1, KontrollPunktRechts2, ParkPosRechts);

            AktuellePosition = ParkPosLinks;
        }

        public void Losfahren()
        {
            if (LKW_Position == LkwPositionen.LinksGeparkt) LKW_Position = LkwPositionen.LR_LinkeKurve;
            if (LKW_Position == LkwPositionen.RechtsGeparkt) LKW_Position = LkwPositionen.RL_RechteKurve;
        }

        private bool LichtschrankeUnterbrochen(double xPos)
        {
            if (AktuellePosition.X + GroesseLkw.X < xPos) return false;
            if (AktuellePosition.X > xPos) return false;
            return true;
        }

        public (bool b1, bool b2, bool b3, bool b4) LastwagenFahren()
        {
            switch (LKW_Position)
            {
                case LkwPositionen.LinksGeparkt:
                    AktuellePosition = ParkPosLinks;
                    KurvePosition = 0;
                    LKW_Richtung = LkwRichtungen.NachRechts;
                    break;

                case LkwPositionen.LR_LinkeKurve:
                    AktuellePosition = LinkeKurve.PunktBestimmen(KurvePosition);
                    KurvePosition += KurveGeschwindigkeit;
                    if (KurvePosition >= 1) LKW_Position = LkwPositionen.LR_Waagrecht;
                    break;

                case LkwPositionen.LR_Waagrecht:
                    if (AktuellePosition.X < AnfangRechteKurve.X) AktuellePosition.X += xy_Bewegung;
                    else LKW_Position = LkwPositionen.LR_RechtKurve;
                    KurvePosition = 0;
                    break;

                case LkwPositionen.LR_RechtKurve:
                    AktuellePosition = RechteKurve.PunktBestimmen(KurvePosition);
                    KurvePosition += KurveGeschwindigkeit;
                    if (KurvePosition >= 1) LKW_Position = LkwPositionen.RechtsGeparkt;
                    break;

                case LkwPositionen.RechtsGeparkt:
                    AktuellePosition = ParkPosRechts;
                    LKW_Richtung = LkwRichtungen.NachLinks;
                    KurvePosition = 1;
                    break;

                case LkwPositionen.RL_RechteKurve:
                    AktuellePosition = RechteKurve.PunktBestimmen(KurvePosition);
                    KurvePosition -= KurveGeschwindigkeit;
                    if (KurvePosition <= 0) LKW_Position = LkwPositionen.RL_Waagrecht;
                    break;

                case LkwPositionen.RL_Waagrecht:
                    if (AktuellePosition.X > EndeLinkeKurve.X) AktuellePosition.X -= xy_Bewegung;
                    else LKW_Position = LkwPositionen.RL_LinkeKurve;
                    KurvePosition = 1;
                    break;

                case LkwPositionen.RL_LinkeKurve:
                    AktuellePosition = LinkeKurve.PunktBestimmen(KurvePosition);
                    KurvePosition -= KurveGeschwindigkeit;
                    if (KurvePosition <= 0) LKW_Position = LkwPositionen.LinksGeparkt;
                    break;

                default: break;
            }

            return (LichtschrankeUnterbrochen(PositionB1.X), LichtschrankeUnterbrochen(PositionB2.X), LichtschrankeUnterbrochen(PositionB3.X), LichtschrankeUnterbrochen(PositionB4.X));
        }
    }
}