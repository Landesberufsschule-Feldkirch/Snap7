using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tiefgarage
{
    public partial class MainWindow
    {
        public const int BitPos_B1 = 0x0001;
        public const int BitPos_B2 = 0x0002;

        public const double FahrenSchrittweite = 0.01;

        public double[] Position_X = new double[20];
        public double[] Position_Y = new double[20];

        public bool Pegel_B1 = true;
        public bool Pegel_B2 = true;


        public string[] Auto_Fahren = new string[20];

        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                if (FahrzeugPersonGeklickt >= 0)
                {

                    if (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell < 200)
                    {
                        Pegel_B1 = true;
                        Pegel_B2 = true;
                    }
                    else
                    {
                        if (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell > 300)
                        {
                            Pegel_B1 = true;
                            Pegel_B2 = true;
                        }
                        else
                        {
                            switch (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Rolle)
                            {
                                case Rolle.Auto:
                                    if ((gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell > 200) && (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell < 240))
                                    {
                                        Pegel_B1 = false;
                                        Pegel_B2 = true;
                                    }
                                    if ((gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell > 240) && (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell < 280))
                                    {
                                        Pegel_B1 = false;
                                        Pegel_B2 = false;
                                    }
                                    if ((gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell > 280) && (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell < 320))
                                    {
                                        Pegel_B1 = true;
                                        Pegel_B2 = false;
                                    }
                                    break;

                                case Rolle.Person:
                                    if ((gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell > 200) && (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell < 240))
                                    {
                                        Pegel_B1 = false;
                                        Pegel_B2 = true;
                                    }
                                    if ((gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell > 240) && (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell < 280))
                                    {
                                        Pegel_B1 = true;
                                        Pegel_B2 = true;
                                    }
                                    if ((gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell > 280) && (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell < 320))
                                    {
                                        Pegel_B1 = true;
                                        Pegel_B2 = false;
                                    }
                                    break;

                                default:
                                    Pegel_B1 = true;
                                    Pegel_B2 = true;
                                    break;
                            }
                        }
                    }




                    switch (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Bewegung)
                    {
                        case "Draussen parken":
                            gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Bewegung = "Draussen senkrecht fahren";
                            break;

                        case "Draussen senkrecht fahren":
                            if (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell < 180)
                            {
                                gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell += FahrenSchrittweite;
                            }
                            else
                            {
                                gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Bewegung = "Draussen waagrecht fahren";
                            }
                            break;

                        case "Draussen waagrecht fahren":
                            if (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell < 300)
                            {
                                gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell += FahrenSchrittweite;
                            }
                            else
                            {

                                if (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell > 310)
                                {
                                    gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell -= FahrenSchrittweite;
                                }
                                else
                                {
                                    gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Bewegung = "Hinein fahren";
                                }
                            }
                            break;

                        case "Hinein fahren":
                            if (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell < gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_drinnen)
                            {
                                gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell += FahrenSchrittweite;
                            }
                            else
                            {
                                gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Bewegung = "Drinnen Einparken";
                            }
                            break;

                        case "Drinnen Einparken":
                            if (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell > gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_drinnen + 1)
                            {
                                gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell -= FahrenSchrittweite;
                            }
                            else
                            {
                                if (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell < gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_drinnen - 1)
                                {
                                    gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell += FahrenSchrittweite;
                                }
                                else
                                {
                                    gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Bewegung = "Drinnen Geparkt";
                                    FahrzeugPersonGeklickt = -1;
                                    Pegel_B1 = true;
                                    Pegel_B2 = true;

                                    foreach (AlleFahrzeugePersonen fp in gAlleFahrzeugePersonen)
                                    {
                                        this.Dispatcher.Invoke(() =>
                                        {
                                            fp.btnAktivieren();
                                        });

                                    }
                                }
                            }
                            break;

                        case "Drinnen Geparkt":
                            gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Bewegung = "Drinnen ausparken";
                            break;

                        case "Drinnen ausparken":

                            if (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell < 300)
                            {
                                gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell += FahrenSchrittweite;
                            }
                            else
                            {

                                if (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell > 310)
                                {
                                    gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell -= FahrenSchrittweite;
                                }
                                else
                                {
                                    gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Bewegung = "Hinausfahren";
                                }
                            }
                            break;


                        case "Hinausfahren":
                            if (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell > 180)
                            {
                                gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell -= FahrenSchrittweite;
                            }
                            else
                            {
                                gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Bewegung = "Draussen auf die Seite fahren";
                            }
                            break;

                        case "Draussen auf die Seite fahren":
                            if (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell > gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_draussen + 1)
                            {
                                gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell -= FahrenSchrittweite;
                            }
                            else
                            {
                                if (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell < gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_draussen - 1)
                                {
                                    gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].x_aktuell += FahrenSchrittweite;
                                }
                                else
                                {
                                    gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Bewegung = "Draussen einparken";
                                }
                            }
                            break;


                        case "Draussen einparken":
                            if (gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell > gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_draussen)
                            {
                                gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell -= FahrenSchrittweite;
                            }
                            else
                            {
                                gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Bewegung = "Draussen Eingeparkt";
                                FahrzeugPersonGeklickt = -1;
                                Pegel_B1 = true;
                                Pegel_B2 = true;

                                foreach (AlleFahrzeugePersonen fp in gAlleFahrzeugePersonen)
                                {
                                    this.Dispatcher.Invoke(() =>
                                    {
                                        fp.btnAktivieren();
                                    });

                                }
                            }
                            break;


                        case "Draussen Eingeparkt":
                            break;

                        default:
                            break;

                    }





                }


                AnzeigeAktualisieren();

                Task.Delay(100);
            }
        }






    }
}
