namespace AmpelsteuerungKieswerk.Model
{
    using Utilities;

    public class LastKraftWagen
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
        public Rechteck Position { get; set; }
        public int ID { get; set; }

        private Utilities.Rechteck.RichtungX richtungX;
        private Utilities.Rechteck.RichtungY richtungY;

        private readonly Punkt parkPosLinks;
        private readonly Punkt parkPosRechts;
        private readonly BezierCurve linkeKurve;
        private readonly BezierCurve rechteKurve;
        private double kurvePosition;

        private readonly Punkt parkpositionLinks = new Punkt(10, 10);
        private readonly Punkt parkpositionRechts = new Punkt(1340, 10);
        private readonly Punkt endeLinkeKurve = new Punkt(250, 200);
        private readonly Punkt anfangRechteKurve = new Punkt(1100, 200);
        private readonly Punkt groesseLkw = new Punkt(100, 80);
        private readonly Punkt positionB1 = new Punkt(275, 0);
        private readonly Punkt positionB2 = new Punkt(350, 0);
        private readonly Punkt positionB3 = new Punkt(1000, 0);
        private readonly Punkt positionB4 = new Punkt(1075, 0);
        private readonly double xy_Bewegung = 1;
        private readonly double kurveGeschwindigkeit = 0.002;

        public LastKraftWagen(int id)
        {
            ID = id;
            LKW_Richtung = LkwRichtungen.NachRechts;
            LKW_Position = LkwPositionen.LinksGeparkt;
            richtungX = Utilities.Rechteck.RichtungX.Steht;
            richtungY = Utilities.Rechteck.RichtungY.Steht;

            var Y_LKW_Parkposition = parkpositionLinks.Y + id * (10 + groesseLkw.Y);

            parkPosLinks = new Punkt(parkpositionLinks.X, Y_LKW_Parkposition);
            var KontrollPunktLinks1 = new Punkt(parkpositionLinks.X + 100, Y_LKW_Parkposition);
            var KontrollPunktLinks2 = new Punkt(endeLinkeKurve.X - 100, endeLinkeKurve.Y);

            parkPosRechts = new Punkt(parkpositionRechts.X, Y_LKW_Parkposition);
            var KontrollPunktRechts1 = new Punkt(anfangRechteKurve.X + 100, anfangRechteKurve.Y);
            var KontrollPunktRechts2 = new Punkt(parkpositionRechts.X - 100, Y_LKW_Parkposition);

            var WegPosLinks = new Punkt(endeLinkeKurve.X, endeLinkeKurve.Y);
            var WegPosRechts = new Punkt(anfangRechteKurve.X, anfangRechteKurve.Y);

            linkeKurve = new BezierCurve(parkPosLinks, KontrollPunktLinks1, KontrollPunktLinks2, WegPosLinks);
            rechteKurve = new BezierCurve(WegPosRechts, KontrollPunktRechts1, KontrollPunktRechts2, parkPosRechts);

            Position = new Rechteck(parkpositionLinks, groesseLkw.X, groesseLkw.Y);
        }

        public (Utilities.Rechteck.RichtungX, Utilities.Rechteck.RichtungY) GetRichtung() => (richtungX, richtungY);

        public void Losfahren()
        {
            if (LKW_Position == LkwPositionen.LinksGeparkt) LKW_Position = LkwPositionen.LR_LinkeKurve;
            if (LKW_Position == LkwPositionen.RechtsGeparkt) LKW_Position = LkwPositionen.RL_RechteKurve;
        }

        private bool LichtschrankeUnterbrochen(double xPos)
        {
            if (Position.Punkt.X + groesseLkw.X < xPos) return false;
            if (Position.Punkt.X > xPos) return false;
            return true;
        }

        public (bool b1, bool b2, bool b3, bool b4) LastwagenFahren(bool stop)
        {
            richtungX = Utilities.Rechteck.RichtungX.Steht;
            richtungY = Utilities.Rechteck.RichtungY.Steht;

            switch (LKW_Position)
            {
                case LkwPositionen.LinksGeparkt:
                    Position.Punkt = parkPosLinks;
                    kurvePosition = 0;
                    LKW_Richtung = LkwRichtungen.NachRechts;
                    break;

                case LkwPositionen.LR_LinkeKurve:
                    richtungX = Utilities.Rechteck.RichtungX.NachRechts;
                    Position.Punkt = linkeKurve.PunktBestimmen(kurvePosition);
                    if (!stop) kurvePosition += kurveGeschwindigkeit;
                    if (kurvePosition >= 1) LKW_Position = LkwPositionen.LR_Waagrecht;
                    break;

                case LkwPositionen.LR_Waagrecht:
                    richtungX = Utilities.Rechteck.RichtungX.NachRechts;
                    kurvePosition = 0;
                    if (!stop)
                    {
                        if (Position.Punkt.X < anfangRechteKurve.X) Position.Punkt.X += xy_Bewegung;
                        else LKW_Position = LkwPositionen.LR_RechtKurve;
                    }
                    break;

                case LkwPositionen.LR_RechtKurve:
                    richtungX = Utilities.Rechteck.RichtungX.NachRechts;
                    Position.Punkt = rechteKurve.PunktBestimmen(kurvePosition);
                    if (!stop) kurvePosition += kurveGeschwindigkeit;
                    if (kurvePosition >= 1) LKW_Position = LkwPositionen.RechtsGeparkt;
                    break;

                case LkwPositionen.RechtsGeparkt:
                    Position.Punkt = parkPosRechts;
                    LKW_Richtung = LkwRichtungen.NachLinks;
                    kurvePosition = 1;
                    break;

                case LkwPositionen.RL_RechteKurve:
                    richtungX = Utilities.Rechteck.RichtungX.NachLinks;
                    Position.Punkt = rechteKurve.PunktBestimmen(kurvePosition);
                    if (!stop) kurvePosition -= kurveGeschwindigkeit;
                    if (kurvePosition <= 0) LKW_Position = LkwPositionen.RL_Waagrecht;
                    break;

                case LkwPositionen.RL_Waagrecht:
                    richtungX = Utilities.Rechteck.RichtungX.NachLinks;
                    kurvePosition = 1;
                    if (!stop)
                    {
                        if (Position.Punkt.X > endeLinkeKurve.X) Position.Punkt.X -= xy_Bewegung;
                        else LKW_Position = LkwPositionen.RL_LinkeKurve;
                    }
                    break;

                case LkwPositionen.RL_LinkeKurve:
                    richtungX = Utilities.Rechteck.RichtungX.NachLinks;
                    Position.Punkt = linkeKurve.PunktBestimmen(kurvePosition);
                    if (!stop) kurvePosition -= kurveGeschwindigkeit;
                    if (kurvePosition <= 0) LKW_Position = LkwPositionen.LinksGeparkt;
                    break;

                default: break;
            }

            return (LichtschrankeUnterbrochen(positionB1.X), LichtschrankeUnterbrochen(positionB2.X), LichtschrankeUnterbrochen(positionB3.X), LichtschrankeUnterbrochen(positionB4.X));
        }
    }
}