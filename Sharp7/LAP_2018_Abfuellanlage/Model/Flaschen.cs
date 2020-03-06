using Utilities;

namespace LAP_2018_Abfuellanlage.Model
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
        private readonly Punkt StartPosition;
        private readonly double BewegungIncrement = 0.5;
        private readonly double FlascheBreite = 40;
        private readonly double FlascheHoehe = 80;
        private readonly Punkt VereinzelnerVentil = new Punkt(105, 385);
        private readonly Punkt FoerderbandLinks = new Punkt(105, 525);
        private readonly Punkt FoerderbandRechts = new Punkt(640, 525);
        private readonly Punkt SensorB1Links = new Punkt(418, 525);
        private readonly Punkt SensorB1Rechts = new Punkt(450, 525);
        private readonly Punkt Boden = new Punkt(640, 700);
        private Utilities.Rechteck.RichtungX richtungX;
        private Utilities.Rechteck.RichtungY richtungY;



        public Flaschen(int id)
        {
            ID = id;
            bewegungSchritt = BewegungSchritt.Oberhalb;
            Sichtbar = true;

            StartPosition = new Punkt(FoerderbandLinks.X, VereinzelnerVentil.Y - ID * FlascheHoehe);
            Position = new Rechteck(StartPosition.Clone(), FlascheBreite, FlascheHoehe);
        }

        public (Utilities.Rechteck.RichtungX, Utilities.Rechteck.RichtungY) GetRichtung() { return (richtungX, richtungY); }

        public void FlascheVereinzeln()
        {
            if (bewegungSchritt == BewegungSchritt.Oberhalb) bewegungSchritt = BewegungSchritt.Vereinzeln;
        }

        public (bool, int) FlascheBewegen(bool M1, int AnzahlFlaschen, int aktuelleFlasche, bool stop)
        {
            double y_Neu;
            richtungX = Utilities.Rechteck.RichtungX.steht;
            richtungY = Utilities.Rechteck.RichtungY.steht;

            switch (bewegungSchritt)
            {
                case BewegungSchritt.Oberhalb:
                    richtungY = Utilities.Rechteck.RichtungY.nachUnten;
                    y_Neu = VereinzelnerVentil.Y - FlascheHoehe * (ID - aktuelleFlasche);
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
                            if (aktuelleFlasche < AnzahlFlaschen - 1) aktuelleFlasche++;
                        }
                    }
                    break;

                case BewegungSchritt.Fahren:
                    richtungX = Utilities.Rechteck.RichtungX.nachRechts;
                    if (!stop && M1)
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
                    bewegungSchritt = BewegungSchritt.Oberhalb;
                    Position.Punkt.X = StartPosition.X;
                    Position.Punkt.Y = StartPosition.Y;
                    break;
            }

            if ((Position.Punkt.X > SensorB1Links.X) && (Position.Punkt.X < SensorB1Rechts.X)) return (true, aktuelleFlasche);
            return (false, aktuelleFlasche);
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