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

        private readonly double bewegungIncrement = 0.5;
        private readonly double doseBreite = 40;
        private readonly double doseHoehe = 80;
        private BewegungSchritt bewegungSchritt;
        private readonly Punkt startPosition;
        private readonly Punkt vereinzelnerVentil = new Punkt(105, 385);
        private readonly Punkt foerderbandLinks = new Punkt(92, 525);
        private readonly Punkt foerderbandRechts = new Punkt(640, 525);
        private readonly Punkt sensorB1Links = new Punkt(400, 525);
        private readonly Punkt sensorB1Rechts = new Punkt(450, 525);
        private readonly Punkt boden = new Punkt(640, 700);
        private Utilities.Rechteck.RichtungX richtungX;
        private Utilities.Rechteck.RichtungY richtungY;


        public CampbellSoup(int id)
        {
            ID = id;
            Sichtbar = true;
            bewegungSchritt = BewegungSchritt.Oberhalb;
            richtungX = Utilities.Rechteck.RichtungX.steht;
            richtungY = Utilities.Rechteck.RichtungY.steht;
            startPosition = new Punkt(foerderbandLinks.X, vereinzelnerVentil.Y - ID * doseHoehe);
            Position = new Rechteck(startPosition.Clone(), doseBreite, doseHoehe);
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
                    y_Neu = vereinzelnerVentil.Y - doseHoehe * (ID - aktuelleDose);
                    if (!stop && Position.Punkt.Y < y_Neu) Position.Punkt.Y += bewegungIncrement;
                    break;

                case BewegungSchritt.Vereinzeln:
                    richtungY = Utilities.Rechteck.RichtungY.nachUnten;
                    if (!stop)
                    {
                        if (Position.Punkt.Y < foerderbandLinks.Y) Position.Punkt.Y += bewegungIncrement;
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
                        if (Position.Punkt.X < foerderbandRechts.X) Position.Punkt.X += bewegungIncrement;
                        else bewegungSchritt = BewegungSchritt.Runtergefallen;
                    }
                    break;

                case BewegungSchritt.Runtergefallen:
                    richtungY = Utilities.Rechteck.RichtungY.nachUnten;
                    if (!stop)
                    {
                        if (Position.Punkt.Y < boden.Y) Position.Punkt.Y += bewegungIncrement;
                        else bewegungSchritt = BewegungSchritt.Fertig;
                    }
                    break;

                case BewegungSchritt.Fertig:
                    Sichtbar = false;
                    break;

                default:
                    bewegungSchritt = BewegungSchritt.Vereinzeln;
                    Position.Punkt.X = startPosition.X;
                    Position.Punkt.Y = startPosition.Y;
                    break;
            }

            if ((Position.Punkt.X > sensorB1Links.X) && (Position.Punkt.X < sensorB1Rechts.X)) return (true, aktuelleDose);
            return (false, aktuelleDose);
        }

        internal void Reset()
        {
            bewegungSchritt = BewegungSchritt.Oberhalb;
            Sichtbar = true;
            Position.Punkt.X = startPosition.X;
            Position.Punkt.Y = startPosition.Y;
        }
    }
}