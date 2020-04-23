using Utilities;

namespace LAP_2018_2_Abfuellanlage.Model
{
    public partial class Flaschen
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
        public bool Sichtbar { get; set; }
        public int ID { get; set; }

        private BewegungSchritt bewegungSchritt;
        private readonly Punkt startPosition;
        private readonly double bewegungIncrement = 0.5;
        private readonly double flascheBreite = 40;
        private readonly double flascheHoehe = 80;
        private readonly Punkt vereinzelnerVentil = new Punkt(105, 385);
        private readonly Punkt foerderbandLinks = new Punkt(105, 525);
        private readonly Punkt foerderbandRechts = new Punkt(640, 525);
        private readonly Punkt sensorB1Links = new Punkt(418, 525);
        private readonly Punkt sensorB1Rechts = new Punkt(450, 525);
        private readonly Punkt boden = new Punkt(640, 700);
        private Utilities.Rechteck.RichtungX richtungX;
        private Utilities.Rechteck.RichtungY richtungY;

        public Flaschen(int id)
        {
            ID = id;
            bewegungSchritt = BewegungSchritt.Oberhalb;
            Sichtbar = true;

            startPosition = new Punkt(foerderbandLinks.X, vereinzelnerVentil.Y - ID * flascheHoehe);
            Position = new Rechteck(startPosition.Clone(), flascheBreite, flascheHoehe);
        }

        public (Utilities.Rechteck.RichtungX, Utilities.Rechteck.RichtungY) GetRichtung() => (richtungX, richtungY);

        public void FlascheVereinzeln()
        {
            if (bewegungSchritt == BewegungSchritt.Oberhalb) bewegungSchritt = BewegungSchritt.Vereinzeln;
        }

        public (bool, int) FlascheBewegen(bool Q1, int AnzahlFlaschen, int aktuelleFlasche, bool stop)
        {
            double y_Neu;
            richtungX = Utilities.Rechteck.RichtungX.steht;
            richtungY = Utilities.Rechteck.RichtungY.steht;

            switch (bewegungSchritt)
            {
                case BewegungSchritt.Oberhalb:
                    richtungY = Utilities.Rechteck.RichtungY.nachUnten;
                    y_Neu = vereinzelnerVentil.Y - flascheHoehe * (ID - aktuelleFlasche);
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
                            if (aktuelleFlasche < AnzahlFlaschen - 1) aktuelleFlasche++;
                        }
                    }
                    break;

                case BewegungSchritt.Fahren:
                    richtungX = Utilities.Rechteck.RichtungX.nachRechts;
                    if (!stop && Q1)
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
                    bewegungSchritt = BewegungSchritt.Oberhalb;
                    Position.Punkt.X = startPosition.X;
                    Position.Punkt.Y = startPosition.Y;
                    break;
            }

            if ((Position.Punkt.X > sensorB1Links.X) && (Position.Punkt.X < sensorB1Rechts.X)) return (true, aktuelleFlasche);
            return (false, aktuelleFlasche);
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