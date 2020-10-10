using Utilities;

namespace LAP_2018_2_Abfuellanlage.Model
{
    public class Flaschen
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
        public int Id { get; set; }

        private BewegungSchritt _bewegungSchritt;
        private readonly Punkt _startPosition;
        private const double BewegungIncrement = 0.5;
        private const double FlascheBreite = 40;
        private const double FlascheHoehe = 80;
        private readonly Punkt _vereinzelnerVentil = new Punkt(105, 385);
        private readonly Punkt _foerderbandLinks = new Punkt(105, 525);
        private readonly Punkt _foerderbandRechts = new Punkt(640, 525);
        private readonly Punkt _sensorB1Links = new Punkt(418, 525);
        private readonly Punkt _sensorB1Rechts = new Punkt(450, 525);
        private readonly Punkt _boden = new Punkt(640, 700);
        private Rechteck.RichtungX _richtungX;
        private Rechteck.RichtungY _richtungY;

        public Flaschen(int id)
        {
            Id = id;
            _bewegungSchritt = BewegungSchritt.Oberhalb;
            Sichtbar = true;

            _startPosition = new Punkt(_foerderbandLinks.X, _vereinzelnerVentil.Y - Id * FlascheHoehe);
            Position = new Rechteck(_startPosition.Clone(), FlascheBreite, FlascheHoehe);
        }

        public (Rechteck.RichtungX, Rechteck.RichtungY) GetRichtung() => (_richtungX, _richtungY);

        public void FlascheVereinzeln()
        {
            if (_bewegungSchritt == BewegungSchritt.Oberhalb) _bewegungSchritt = BewegungSchritt.Vereinzeln;
        }

        public (bool, int) FlascheBewegen(bool q1, int anzahlFlaschen, int aktuelleFlasche, bool stop)
        {
            _richtungX = Rechteck.RichtungX.Steht;
            _richtungY = Rechteck.RichtungY.Steht;

            switch (_bewegungSchritt)
            {
                case BewegungSchritt.Oberhalb:
                    _richtungY = Rechteck.RichtungY.NachUnten;
                    var yNeu = _vereinzelnerVentil.Y - FlascheHoehe * (Id - aktuelleFlasche);
                    if (!stop && Position.Punkt.Y < yNeu) Position.Punkt.Y += BewegungIncrement;
                    break;

                case BewegungSchritt.Vereinzeln:
                    _richtungY = Rechteck.RichtungY.NachUnten;
                    if (!stop)
                    {
                        if (Position.Punkt.Y < _foerderbandLinks.Y) Position.Punkt.Y += BewegungIncrement;
                        else
                        {
                            _bewegungSchritt = BewegungSchritt.Fahren;
                            if (aktuelleFlasche < anzahlFlaschen - 1) aktuelleFlasche++;
                        }
                    }
                    break;

                case BewegungSchritt.Fahren:
                    _richtungX = Rechteck.RichtungX.NachRechts;
                    if (!stop && q1)
                    {
                        if (Position.Punkt.X < _foerderbandRechts.X) Position.Punkt.X += BewegungIncrement;
                        else _bewegungSchritt = BewegungSchritt.Runtergefallen;
                    }
                    break;

                case BewegungSchritt.Runtergefallen:
                    _richtungY = Rechteck.RichtungY.NachUnten;
                    if (!stop)
                    {
                        if (Position.Punkt.Y < _boden.Y) Position.Punkt.Y += BewegungIncrement;
                        else _bewegungSchritt = BewegungSchritt.Fertig;
                    }
                    break;

                case BewegungSchritt.Fertig:
                    Sichtbar = false;
                    break;

                default:
                    _bewegungSchritt = BewegungSchritt.Oberhalb;
                    Position.Punkt.X = _startPosition.X;
                    Position.Punkt.Y = _startPosition.Y;
                    break;
            }

            if (Position.Punkt.X > _sensorB1Links.X && Position.Punkt.X < _sensorB1Rechts.X) return (true, aktuelleFlasche);
            return (false, aktuelleFlasche);
        }

        internal void Reset()
        {
            _bewegungSchritt = BewegungSchritt.Oberhalb;
            Sichtbar = true;
            Position.Punkt.X = _startPosition.X;
            Position.Punkt.Y = _startPosition.Y;
        }
    }
}