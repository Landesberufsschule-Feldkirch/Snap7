using Utilities;

namespace LAP_2010_4_Abfuellanlage.Model
{
    public class BlechDosen
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
        public int Id { get; set; }
        public bool Sichtbar { get; set; }

        private const double BewegungIncrement = 0.5;
        private const double DoseBreite = 40;
        private const double DoseHoehe = 80;
        private BewegungSchritt _bewegungSchritt;
        private readonly Punkt _startPosition;
        private readonly Punkt _vereinzelnerVentil = new Punkt(105, 375);
        private readonly Punkt _foerderbandLinks = new Punkt(82, 515);
        private readonly Punkt _foerderbandRechts = new Punkt(630, 515);
        private readonly Punkt _sensorB2Links = new Punkt(395, 515);
        private readonly Punkt _sensorB2Rechts = new Punkt(440, 515);
        private readonly Punkt _boden = new Punkt(640, 690);
        private Rechteck.RichtungX _richtungX;
        private Rechteck.RichtungY _richtungY;

        public BlechDosen(int id)
        {
            Id = id;
            Sichtbar = true;
            _bewegungSchritt = BewegungSchritt.Oberhalb;
            _richtungX = Rechteck.RichtungX.Steht;
            _richtungY = Rechteck.RichtungY.Steht;
            _startPosition = new Punkt(_foerderbandLinks.X, _vereinzelnerVentil.Y - Id * DoseHoehe);
            Position = new Rechteck(_startPosition.Clone(), DoseBreite, DoseHoehe);
        }

        public (Rechteck.RichtungX, Rechteck.RichtungY) GetRichtung() => (_richtungX, _richtungY);

        public void DosenVereinzeln()
        {
            if (_bewegungSchritt == BewegungSchritt.Oberhalb) _bewegungSchritt = BewegungSchritt.Vereinzeln;
        }

        public (bool lichtschranke, int aktuelleDose) DosenBewegen(bool m1, int anzahlDosen, int aktuelleDose, bool stop)
        {
            _richtungX = Rechteck.RichtungX.Steht;
            _richtungY = Rechteck.RichtungY.Steht;

            switch (_bewegungSchritt)
            {
                case BewegungSchritt.Oberhalb:
                    _richtungY = Rechteck.RichtungY.NachUnten;
                    var yNeu = _vereinzelnerVentil.Y - DoseHoehe * (Id - aktuelleDose);
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
                            if (aktuelleDose < anzahlDosen - 1) aktuelleDose++;
                        }
                    }
                    break;

                case BewegungSchritt.Fahren:
                    _richtungX = Rechteck.RichtungX.NachRechts;
                    if (m1 && !stop)
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
                    _bewegungSchritt = BewegungSchritt.Vereinzeln;
                    Position.Punkt.X = _startPosition.X;
                    Position.Punkt.Y = _startPosition.Y;
                    break;
            }

            if (Position.Punkt.X > _sensorB2Links.X && Position.Punkt.X < _sensorB2Rechts.X) return (true, aktuelleDose);
            return (false, aktuelleDose);
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