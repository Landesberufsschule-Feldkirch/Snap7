using System.Windows;
using System.Windows.Controls;

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
        public static int AnzahlFlaschen = 0;

        private readonly int ID;
        private readonly Image imgFlasche;
        private bool Sichtbar = true;
        private double X_Start { get; set; }
        public double Y_Start { get; set; }
        private BewegungSchritt bewegungSchritt;
        private double x_Aktuell;
        private double y_Aktuell;


        public Flaschen()
        {
            ID = AnzahlFlaschen;
            AnzahlFlaschen++;
            bewegungSchritt = BewegungSchritt.Oberhalb;
        }

        public Flaschen(int NeueID, Image Flasche)
        {
            ID = NeueID;
            imgFlasche = Flasche;

        }



        public void FlaschenParken()
        {
            x_Aktuell = x_StartPosition;
            y_Aktuell = y_VereinzlerVentil - ID * y_FlachenHoehe;
        }

        public void FlascheVereinzeln()
        {
            if (bewegungSchritt == BewegungSchritt.Oberhalb) bewegungSchritt = BewegungSchritt.Vereinzeln;
        }

        public int GetAktuelleFlasche()
        {
            return AktuelleFlasche;
        }

        public void AnzeigeAktualisieren()
        {
            /*
            if (FensterAktiv)
            {
                if (Sichtbar)
                {
                    imgFlasche.SetValue(Canvas.LeftProperty, x_Aktuell);
                    imgFlasche.SetValue(Canvas.TopProperty, y_Aktuell);
                }
                else
                {
                    imgFlasche.Visibility = Visibility.Collapsed;
                }
            }
            */
        }
    }
}