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

        public Rechteck Dose { get; set; }
        public int Id { get; set; }
        public bool Sichtbar { get; set; }

        private const double BewegungIncrement = 0.5;
        private const double DoseBreite = 40;
        private const double DoseHoehe = 80;
        private BewegungSchritt _bewegungSchritt;
        private readonly Punkt _startPosition;
        private readonly Punkt _vereinzelnerVentil = new(105, 375);
        private readonly Punkt _foerderbandLinks = new(82, 515);
        private readonly Punkt _foerderbandRechts = new(630, 515);
        private readonly Punkt _sensorB2Links = new(395, 515);
        private readonly Punkt _sensorB2Rechts = new(440, 515);
        private readonly Punkt _boden = new(640, 690);

        public BlechDosen(int id)
        {
            Id = id;
            Sichtbar = true;
            _bewegungSchritt = BewegungSchritt.Oberhalb;
            _startPosition = new Punkt(_foerderbandLinks.X, _vereinzelnerVentil.Y - Id * DoseHoehe);
            Dose = new Rechteck(_startPosition.Clone(), DoseBreite, DoseHoehe);
        }
        public void DosenVereinzeln()
        {
            if (_bewegungSchritt == BewegungSchritt.Oberhalb) _bewegungSchritt = BewegungSchritt.Vereinzeln;
        }
        public (bool lichtschranke, int aktuelleDose) DosenBewegen(bool m1, int anzahlDosen, int aktuelleDose)
        {
            switch (_bewegungSchritt)
            {
                case BewegungSchritt.Oberhalb:
                    var yNeu = _vereinzelnerVentil.Y - DoseHoehe * (Id - aktuelleDose);
                    if (Dose.GetOben() < yNeu) Dose.SetSenkrechtSchieben(BewegungIncrement);
                    break;

                case BewegungSchritt.Vereinzeln:
                    if (Dose.GetOben() < _foerderbandLinks.Y) Dose.SetSenkrechtSchieben(BewegungIncrement);
                    else
                    {
                        _bewegungSchritt = BewegungSchritt.Fahren;
                        if (aktuelleDose < anzahlDosen - 1) aktuelleDose++;
                    }
                    break;

                case BewegungSchritt.Fahren:
                    if (m1)
                    {
                        if (Dose.GetLinks() < _foerderbandRechts.X) Dose.SetWaagrechtSchieben(BewegungIncrement);
                        else _bewegungSchritt = BewegungSchritt.Runtergefallen;
                    }
                    break;

                case BewegungSchritt.Runtergefallen:
                    if (Dose.GetOben() < _boden.Y) Dose.SetSenkrechtSchieben(BewegungIncrement);
                    else _bewegungSchritt = BewegungSchritt.Fertig;
                    break;

                case BewegungSchritt.Fertig:
                    Sichtbar = false;
                    break;

                default:
                    _bewegungSchritt = BewegungSchritt.Vereinzeln;
                    Dose.SetPosition(_startPosition);
                    break;
            }

            if (Dose.GetLinks() > _sensorB2Links.X && Dose.GetLinks() < _sensorB2Rechts.X) return (true, aktuelleDose);
            return (false, aktuelleDose);
        }
        internal void Reset()
        {
            _bewegungSchritt = BewegungSchritt.Oberhalb;
            Sichtbar = true;
            Dose.SetPosition(_startPosition);
        }
    }
}