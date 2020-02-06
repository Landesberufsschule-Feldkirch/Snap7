namespace LAP_2018_Abfuellanlage.Model
{
    public partial class Flaschen
    {
        private readonly double x_StartPosition = 105;
        private readonly double BewegungIncrement = 0.005;
        private readonly double x_B1_Links = 418;
        private readonly double x_B1_Rechts = 450;
        private readonly double x_Foerderband_Rechts = 640;
        private readonly double y_VereinzlerVentil = 385;
        private readonly double y_FlachenHoehe = 80;
        private readonly double y_Foerderband = 525;
        private readonly double y_Boden = 700;

        public bool FlascheBewegen(bool M1)
        {
            double y_Neu;

            switch (bewegungSchritt)
            {
                case BewegungSchritt.Oberhalb:
                    y_Neu = y_VereinzlerVentil - (ID - AktuelleFlasche) * y_FlachenHoehe;
                    if (AkuellePosition.Y < y_Neu) AkuellePosition.Y += BewegungIncrement;
                    break;

                case BewegungSchritt.Vereinzeln:
                    if (AkuellePosition.Y < y_Foerderband) AkuellePosition.Y += BewegungIncrement;
                    else
                    {
                        bewegungSchritt = BewegungSchritt.Fahren;
                        if (AktuelleFlasche < AnzahlFlaschen - 1) AktuelleFlasche++;
                    }
                    break;

                case BewegungSchritt.Fahren:
                    if (M1)
                    {
                        if (AkuellePosition.X < x_Foerderband_Rechts) AkuellePosition.X += BewegungIncrement;
                        else bewegungSchritt = BewegungSchritt.Runtergefallen;
                    }
                    break;

                case BewegungSchritt.Runtergefallen:
                    if (AkuellePosition.Y < y_Boden) AkuellePosition.Y += BewegungIncrement;
                    else bewegungSchritt = BewegungSchritt.Fertig;
                    break;

                case BewegungSchritt.Fertig:
                    Sichtbar = false;
                    break;

                default:
                    bewegungSchritt = BewegungSchritt.Vereinzeln;
                    AkuellePosition.X = X_Start;
                    AkuellePosition.Y = Y_Start;
                    break;
            }

            if ((AkuellePosition.X > x_B1_Links) && (AkuellePosition.X < x_B1_Rechts)) return true;
            return false;
        }
    }
}