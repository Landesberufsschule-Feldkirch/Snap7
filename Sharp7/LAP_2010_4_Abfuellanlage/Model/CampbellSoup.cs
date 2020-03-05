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
        private BewegungSchritt bewegungSchritt;

        private readonly int ID;
        private readonly Punkt StartPosition;

        private readonly double BewegungIncrement = 0.5;
        private readonly double HoeheDose = 80;

        private readonly Punkt VereinzelnerVentil = new Punkt(105, 385);
        private readonly Punkt FoerderbandLinks = new Punkt(92, 525);
        private readonly Punkt FoerderbandRechts = new Punkt(640, 525);
        private readonly Punkt SensorB1Links = new Punkt(410, 525);
        private readonly Punkt SensorB1Rechts = new Punkt(450, 525);
        private readonly Punkt Boden = new Punkt(640, 700);

        public Punkt AktuellePosition { get; set; }
        public bool Sichtbar { get; set; }

        public CampbellSoup(int id)
        {
            ID = id;
            bewegungSchritt = BewegungSchritt.Oberhalb;
            Sichtbar = true;

            AktuellePosition = new Punkt(FoerderbandLinks.X, VereinzelnerVentil.Y - ID * HoeheDose);
            StartPosition = AktuellePosition;
        }


        public void DosenVereinzeln()
        {
            if (bewegungSchritt == BewegungSchritt.Oberhalb) bewegungSchritt = BewegungSchritt.Vereinzeln;
        }

        public (bool, int) DosenBewegen(bool M1, int AnzahlDosen, int aktuelleDose)
        {
            double y_Neu;

            switch (bewegungSchritt)
            {
                case BewegungSchritt.Oberhalb:
                    y_Neu = VereinzelnerVentil.Y - HoeheDose * (ID - aktuelleDose);
                    if (AktuellePosition.Y < y_Neu) AktuellePosition.Y += BewegungIncrement;
                    break;

                case BewegungSchritt.Vereinzeln:
                    if (AktuellePosition.Y < FoerderbandLinks.Y) AktuellePosition.Y += BewegungIncrement;
                    else
                    {
                        bewegungSchritt = BewegungSchritt.Fahren;
                        if (aktuelleDose < AnzahlDosen - 1) aktuelleDose++;
                    }
                    break;

                case BewegungSchritt.Fahren:
                    if (M1)
                    {
                        if (AktuellePosition.X < FoerderbandRechts.X) AktuellePosition.X += BewegungIncrement;
                        else bewegungSchritt = BewegungSchritt.Runtergefallen;
                    }
                    break;

                case BewegungSchritt.Runtergefallen:
                    if (AktuellePosition.Y < Boden.Y) AktuellePosition.Y += BewegungIncrement;
                    else bewegungSchritt = BewegungSchritt.Fertig;
                    break;

                case BewegungSchritt.Fertig:
                    Sichtbar = false;
                    break;

                default:
                    bewegungSchritt = BewegungSchritt.Vereinzeln;
                    AktuellePosition = StartPosition;
                    break;
            }

            if ((AktuellePosition.X > SensorB1Links.X) && (AktuellePosition.X < SensorB1Rechts.X)) return (true, aktuelleDose);
            return (false, aktuelleDose);
        }

    }
}