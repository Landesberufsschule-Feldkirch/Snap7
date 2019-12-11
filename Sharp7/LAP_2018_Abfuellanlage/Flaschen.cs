using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LAP_2018_Abfuellanlage
{
    enum BewegungSchritt
    {
        Oberhalb,
        Vereinzeln,
        Fahren,
        Runtergefallen,
        Fertig
    }
    public partial class MainWindow : Window
    {


        public class Flaschen
        {
            public static int AktuelleFlasche = 0;
            public static int AnzahlFlaschen = 0;
            int ID;
            public Image imgFlasche;
            bool Sichtbar = true;
            public double x_Start { get; set; }
            public double y_Start { get; set; }
            private BewegungSchritt bewegungSchritt;

            private double x_Aktuell;
            private double y_Aktuell;



            private readonly double x_StartPosition = 105;
            private readonly double BewegungIncrement = 0.005;
            private readonly double x_B1_Links = 418;
            private readonly double x_B1_Rechts = 450;
            private readonly double x_Foerderband_Rechts = 640;
            private readonly double y_VereinzlerVentil = 385;
            private readonly double y_FlachenHoehe = 80;
            private readonly double y_Foerderband = 525;
            private readonly double y_Boden = 700;

            public Flaschen(int ID, Image imgFlasche)
            {
                this.ID = ID;
                this.imgFlasche = imgFlasche;
                this.bewegungSchritt = BewegungSchritt.Oberhalb;
                AnzahlFlaschen++;
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

            public int getAktuelleFlasche() { return AktuelleFlasche; }
            // public BewegungSchritt GetStatus() { return bewegungSchritt; }
            public bool FlascheBewegen(bool M1)
            {
                double y_Neu;


                switch (bewegungSchritt)
                {
                    case BewegungSchritt.Oberhalb:
                        y_Neu = y_VereinzlerVentil - (ID - AktuelleFlasche) * y_FlachenHoehe;
                        if (y_Aktuell < y_Neu) y_Aktuell += BewegungIncrement;
                        break;


                    case BewegungSchritt.Vereinzeln:
                        if (y_Aktuell < y_Foerderband) y_Aktuell += BewegungIncrement;
                        else
                        {
                            bewegungSchritt = BewegungSchritt.Fahren;
                            if (AktuelleFlasche < AnzahlFlaschen - 1) AktuelleFlasche++;
                        }
                        break;

                    case BewegungSchritt.Fahren:
                        if (M1)
                        {
                            if (x_Aktuell < x_Foerderband_Rechts) x_Aktuell += BewegungIncrement;
                            else bewegungSchritt = BewegungSchritt.Runtergefallen;
                        }
                        break;

                    case BewegungSchritt.Runtergefallen:
                        if (y_Aktuell < y_Boden) y_Aktuell += BewegungIncrement;
                        else bewegungSchritt = BewegungSchritt.Fertig;
                        break;

                    case BewegungSchritt.Fertig:
                        Sichtbar = false;
                        break;

                    default:
                        bewegungSchritt = BewegungSchritt.Vereinzeln;
                        x_Aktuell = x_Start;
                        y_Aktuell = y_Start;
                        break;
                }

                if ((x_Aktuell > x_B1_Links) && (x_Aktuell < x_B1_Rechts)) return true; else return false;


            }

            public void AnzeigeAktualisieren(bool FensterAktiv)
            {
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

            }
        }

        public ObservableCollection<Flaschen> gAlleFlaschen = new ObservableCollection<Flaschen>();

        public void AlleFlaschenInitialisieren()
        {

            gAlleFlaschen.Add(new Flaschen(0, imgFlasche_1));
            gAlleFlaschen.Add(new Flaschen(1, imgFlasche_2));
            gAlleFlaschen.Add(new Flaschen(2, imgFlasche_3));
            gAlleFlaschen.Add(new Flaschen(3, imgFlasche_4));
            gAlleFlaschen.Add(new Flaschen(4, imgFlasche_5));
            gAlleFlaschen.Add(new Flaschen(5, imgFlasche_6));

        }

        public void AlleFlaschenParken()
        {
            foreach (Flaschen flasche in gAlleFlaschen) { flasche.FlaschenParken(); }
        }
    }
}