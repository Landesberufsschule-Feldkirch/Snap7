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

        private static int AktuelleFlasche = 0;
        private static int AnzahlFlaschen = 0;

        private readonly int ID;
        private bool Sichtbar = true;

        private BewegungSchritt bewegungSchritt;

        private Punkt AkuellePosition;

       // private double x_Aktuell;
       // private double y_Aktuell;

        private double X_Start { get; set; }
        public double Y_Start { get; set; }

        public Flaschen()
        {
            ID = AnzahlFlaschen;
            AnzahlFlaschen++;
            bewegungSchritt = BewegungSchritt.Oberhalb;

            AkuellePosition = new Punkt(x_StartPosition, y_VereinzlerVentil - ID * y_FlachenHoehe);
        }


        public void FlascheVereinzeln()
        {
            if (bewegungSchritt == BewegungSchritt.Oberhalb) bewegungSchritt = BewegungSchritt.Vereinzeln;
        }

        public int GetAktuelleFlasche() { return AktuelleFlasche; }
        public Punkt AktuellePosition() { return AkuellePosition; }
        internal bool FlascheSichtbar() { return Sichtbar; }
    }
}