using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LAP_2018_Abfuellanlage
{
    public partial class MainWindow : Window
    {
        public partial class Flaschen
        {
            enum BewegungSchritt
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
            private Image imgFlasche;
            bool Sichtbar = true;
            private double X_Start { get; set; }
            public double Y_Start { get; set; }
            private BewegungSchritt bewegungSchritt;
            private double x_Aktuell;
            private double y_Aktuell;

            public Flaschen(int NeueID, Image Flasche)
            {
                ID = NeueID;
                imgFlasche = Flasche;
                bewegungSchritt = BewegungSchritt.Oberhalb;
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
            public int GetAktuelleFlasche() { return AktuelleFlasche; }

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