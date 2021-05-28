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

        public Rechteck Flasche { get; set; }
        public bool Sichtbar { get; set; }
        public int Id { get; set; }

        private BewegungSchritt _bewegungSchritt;
        private readonly Punkt _startPosition;
        private const double BewegungIncrement = 0.5;
        private const double FlascheBreite = 40;
        private const double FlascheHoehe = 80;
        private readonly Punkt _vereinzelnerVentil = new(105, 385);
        private readonly Punkt _foerderbandLinks = new(105, 525);
        private readonly Punkt _foerderbandRechts = new(640, 525);
        private readonly Punkt _sensorB1Links = new(415, 525);
        private readonly Punkt _sensorB1Rechts = new(450, 525);
        private readonly Punkt _boden = new(640, 700);

        public Flaschen(int id)
        {
            Id = id;
            _bewegungSchritt = BewegungSchritt.Oberhalb;
            Sichtbar = true;

            _startPosition = new Punkt(_foerderbandLinks.X, _vereinzelnerVentil.Y - Id * FlascheHoehe);
            Flasche = new Rechteck(_startPosition.Clone(), FlascheBreite, FlascheHoehe);
        }
        public void FlascheVereinzeln()
        {
            if (_bewegungSchritt == BewegungSchritt.Oberhalb) _bewegungSchritt = BewegungSchritt.Vereinzeln;
        }
        public (bool lichtschranke, int aktuelleFlasche) FlascheBewegen(bool q1, int anzahlFlaschen, int aktuelleFlasche)
        {

            switch (_bewegungSchritt)
            {
                case BewegungSchritt.Oberhalb:
                    var yNeu = _vereinzelnerVentil.Y - FlascheHoehe * (Id - aktuelleFlasche);
                    if (Flasche.GetOben() < yNeu) Flasche.SetSenkrechtSchieben(BewegungIncrement);
                    break;

                case BewegungSchritt.Vereinzeln:
                    if (Flasche.GetOben() < _foerderbandLinks.Y) Flasche.SetSenkrechtSchieben(BewegungIncrement);
                    else
                    {
                        _bewegungSchritt = BewegungSchritt.Fahren;
                        if (aktuelleFlasche < anzahlFlaschen - 1) aktuelleFlasche++;
                    }
                    break;

                case BewegungSchritt.Fahren:
                    if (q1)
                    {
                        if (Flasche.GetLinks() < _foerderbandRechts.X) Flasche.SetWaagrechtSchieben(BewegungIncrement);
                        else _bewegungSchritt = BewegungSchritt.Runtergefallen;
                    }
                    break;

                case BewegungSchritt.Runtergefallen:
                    if (Flasche.GetOben() < _boden.Y) Flasche.SetSenkrechtSchieben(BewegungIncrement);
                    else _bewegungSchritt = BewegungSchritt.Fertig;
                    break;

                case BewegungSchritt.Fertig:
                    Sichtbar = false;
                    break;

                default:
                    _bewegungSchritt = BewegungSchritt.Oberhalb;
                    Flasche.SetPosition(_startPosition);
                    break;
            }

            if (Flasche.GetLinks() > _sensorB1Links.X && Flasche.GetLinks() < _sensorB1Rechts.X) return (true, aktuelleFlasche);
            return (false, aktuelleFlasche);
        }
        internal void Reset()
        {
            _bewegungSchritt = BewegungSchritt.Oberhalb;
            Sichtbar = true;
            Flasche.SetPosition(_startPosition);
        }
    }
}