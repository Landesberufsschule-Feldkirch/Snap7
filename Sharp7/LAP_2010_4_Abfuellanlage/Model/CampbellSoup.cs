using Utilities;

namespace LAP_2010_4_Abfuellanlage.Model
{
    public class CampbellSoup
    {
        private enum BewegungSchritt
        {
            Oberhalb,
            Vereinzeln,
            Fahren,
            Runtergefallen,
            Fertig
        }

        public Rechteck Position { get; set; }
        public int ID { get; set; }
        public bool Sichtbar { get; set; }

        private readonly double BewegungIncrement = 0.5;
        private readonly double DoseBreite = 40;
        private readonly double DoseHoehe = 80;
        private BewegungSchritt bewegungSchritt;
        private readonly Punkt StartPosition;
        private readonly Punkt VereinzelnerVentil = new Punkt(105, 385);
        private readonly Punkt FoerderbandLinks = new Punkt(92, 525);
        private readonly Punkt FoerderbandRechts = new Punkt(640, 525);
        private readonly Punkt SensorB1Links = new Punkt(400, 525);
        private readonly Punkt SensorB1Rechts = new Punkt(450, 525);
        private readonly Punkt Boden = new Punkt(640, 700);
        private Utilities.Rechteck.RichtungX richtungX;
        private Utilities.Rechteck.RichtungY richtungY;


        public CampbellSoup(int id)
        {
            ID = id;
            Sichtbar = true;
            bewegungSchritt = BewegungSchritt.Oberhalb;
            richtungX = Utilities.Rechteck.RichtungX.steht;
            richtungY = Utilities.Rechteck.RichtungY.steht;
            StartPosition = new Punkt(FoerderbandLinks.X, VereinzelnerVentil.Y - ID * DoseHoehe);
            Position = new Rechteck(StartPosition.Clone(), DoseBreite, DoseHoehe);
        }


        public (Utilities.Rechteck.RichtungX, Utilities.Rechteck.RichtungY) GetRichtung() => (richtungX, richtungY);

        public void DosenVereinzeln()
        {
            if (bewegungSchritt == BewegungSchritt.Oberhalb) bewegungSchritt = BewegungSchritt.Vereinzeln;
        }

        public (bool, int) DosenBewegen(bool M1, int AnzahlDosen, int aktuelleDose, bool stop)
        {
            double y_Neu;
            richtungX = Utilities.Rechteck.RichtungX.steht;
            richtungY = Utilities.Rechteck.RichtungY.steht;

            switch (bewegungSchritt)
            {
                case BewegungSchritt.Oberhalb:
                    richtungY = Utilities.Rechteck.RichtungY.nachUnten;
                    y_Neu = VereinzelnerVentil.Y - DoseHoehe * (ID - aktuelleDose);
                    if (!stop && Position.Punkt.Y < y_Neu) Position.Punkt.Y += BewegungIncrement;
                    break;

                case BewegungSchritt.Vereinzeln:
                    richtungY = Utilities.Rechteck.RichtungY.nachUnten;
                    if (!stop)
                    {
                        if (Position.Punkt.Y < FoerderbandLinks.Y) Position.Punkt.Y += BewegungIncrement;
                        else
                        {
                            bewegungSchritt = BewegungSchritt.Fahren;
                            if (aktuelleDose < AnzahlDosen - 1) aktuelleDose++;
                        }
                    }
                    break;

                case BewegungSchritt.Fahren:
                    richtungX = Utilities.Rechteck.RichtungX.nachRechts;
                    if (M1 && !stop)
                    {
                        if (Position.Punkt.X < FoerderbandRechts.X) Position.Punkt.X += BewegungIncrement;
                        else bewegungSchritt = BewegungSchritt.Runtergefallen;
                    }
                    break;

                case BewegungSchritt.Runtergefallen:
                    richtungY = Utilities.Rechteck.RichtungY.nachUnten;
                    if (!stop)
                    {
                        if (Position.Punkt.Y < Boden.Y) Position.Punkt.Y += BewegungIncrement;
                        else bewegungSchritt = BewegungSchritt.Fertig;
                    }
                    break;

                case BewegungSchritt.Fertig:
                    Sichtbar = false;
                    break;

                default:
                    bewegungSchritt = BewegungSchritt.Vereinzeln;
                    Position.Punkt.X = StartPosition.X;
                    Position.Punkt.Y = StartPosition.Y;
                    break;
            }

            if ((Position.Punkt.X > SensorB1Links.X) && (Position.Punkt.X < SensorB1Rechts.X)) return (true, aktuelleDose);
            return (false, aktuelleDose);
        }

        internal void Reset()
        {
            bewegungSchritt = BewegungSchritt.Oberhalb;
            Sichtbar = true;
            Position.Punkt.X = StartPosition.X;
            Position.Punkt.Y = StartPosition.Y;
        }
    }
}