using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LAP_2018_Abfuellanlage
{
    public partial class MainWindow : Window
    {

        public class Flaschen
        {
            enum BewegungSchritt
            {
                Vereinzeln,
                Fahren,
                Runtergefallen,
                Fertig
            }

            public Image imgFlasche;
            bool Sichtbar = true;
            public double x_Start { get; set; }
            public double y_Start { get; set; }

            private double x_Aktuell;
            private double y_Aktuell;

            private BewegungSchritt bewegungSchritt;

            private readonly double BewegungIncrement = 0.01;
            private readonly double x_Foerderband_Rechts = 630;
            private readonly double y_Foerderband = 525;
            private readonly double y_Boden = 700;

            public Flaschen(Image imgFlasche, double x_Start, double y_Start)
            {
                this.imgFlasche = imgFlasche;
                this.x_Start = x_Start;
                this.y_Start = y_Start;
                this.bewegungSchritt = BewegungSchritt.Vereinzeln;
            }

            public void FlaschenParken()
            {
                x_Aktuell = x_Start;
                y_Aktuell = y_Start;
            }

            public void FlascheBewegen()
            {
                switch (bewegungSchritt)
                {
                    case BewegungSchritt.Vereinzeln:
                        if (y_Aktuell < y_Foerderband) y_Aktuell += BewegungIncrement;
                        else bewegungSchritt = BewegungSchritt.Fahren;
                        break;

                    case BewegungSchritt.Fahren:
                        if (x_Aktuell < x_Foerderband_Rechts) x_Aktuell += BewegungIncrement;
                        else bewegungSchritt = BewegungSchritt.Runtergefallen;
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
            gAlleFlaschen.Add(new Flaschen(imgFlasche_1, 102, 10));
            gAlleFlaschen.Add(new Flaschen(imgFlasche_2, 102, 90));
            gAlleFlaschen.Add(new Flaschen(imgFlasche_3, 102, 170));
            gAlleFlaschen.Add(new Flaschen(imgFlasche_4, 102, 250));
            gAlleFlaschen.Add(new Flaschen(imgFlasche_5, 102, 330));
            gAlleFlaschen.Add(new Flaschen(imgFlasche_6, 102, 410));
        }

        public void AlleFlaschenParken()
        {
            foreach (Flaschen flasche in gAlleFlaschen) { flasche.FlaschenParken(); }
        }
    }
}