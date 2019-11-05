using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BehälterSteuerung
{
    public partial class MainWindow
    {
        public const int BitPos_Q1 = 0x0001;
        public const int BitPos_Q3 = 0x0002;
        public const int BitPos_Q5 = 0x0004;
        public const int BitPos_P1 = 0x0008;

        public const int BitPos_B1 = 0x0001;
        public const int BitPos_B2 = 0x0002;
        public const int BitPos_B3 = 0x0004;
        public const int BitPos_B4 = 0x0008;
        public const int BitPos_B5 = 0x0010;
        public const int BitPos_B6 = 0x0020;

        public const double SinkGeschwindigkeit = 0.00005;
        public const double FuellGeschwindigkeit = 3 * SinkGeschwindigkeit;

        public double PegelOffset_1;
        public double PegelOffset_2;
        public double PegelOffset_3;

        public const double HoeheFuellBalken = 200.0;

        public bool Tank_1_AutomatischEntleeren;
        public bool Tank_2_AutomatischEntleeren;
        public bool Tank_3_AutomatischEntleeren;

        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                if (AutomatikModusAktiv) AutomatikFunktionen();
                else ManuelleFunktionen();

                LimitsUeberwachen();
                AnzeigeAktualisieren();

                BitmusterSchreiben(Pegel_B1, DigInput, Startbyte_0, BitPos_B1);
                BitmusterSchreiben(Pegel_B2, DigInput, Startbyte_0, BitPos_B2);
                BitmusterSchreiben(Pegel_B3, DigInput, Startbyte_0, BitPos_B3);
                BitmusterSchreiben(Pegel_B4, DigInput, Startbyte_0, BitPos_B4);
                BitmusterSchreiben(Pegel_B5, DigInput, Startbyte_0, BitPos_B5);
                BitmusterSchreiben(Pegel_B6, DigInput, Startbyte_0, BitPos_B6);

                Task.Delay(100);
            }
        }

        public void AutomatikFunktionen()
        {

            switch (AutomatikSchrittschaltwerk)
            {

                case "Init":
                    Pegel_1 = 1;
                    Pegel_2 = 1;
                    Pegel_3 = 1;

                    if (Automatik_123_Aktiv)
                    {
                        PegelOffset_1 = 1.2;
                        PegelOffset_2 = 2.4;
                        PegelOffset_3 = 3.6;
                    }

                    if (Automatik_132_Aktiv)
                    {
                        PegelOffset_1 = 1.2;
                        PegelOffset_3 = 2.4;
                        PegelOffset_2 = 3.6;
                    }

                    if (Automatik_321_Aktiv)
                    {
                        PegelOffset_3 = 1.2;
                        PegelOffset_2 = 2.4;
                        PegelOffset_1 = 3.6;
                    }

                    Tank_1_AutomatischEntleeren = true;
                    Tank_2_AutomatischEntleeren = true;
                    Tank_3_AutomatischEntleeren = true;

                    AutomatikSchrittschaltwerk = "Entleeren";
                    break;

                case "Entleeren":
                    PegelOffset_1 -= SinkGeschwindigkeit;
                    PegelOffset_2 -= SinkGeschwindigkeit;
                    PegelOffset_3 -= SinkGeschwindigkeit;

                    if ((PegelOffset_1 < 1) && (PegelOffset_1 > 0.05)) Ventil_Q2 = true; else Ventil_Q2 = false;
                    if ((PegelOffset_2 < 1) && (PegelOffset_2 > 0.05)) Ventil_Q4 = true; else Ventil_Q4 = false;
                    if ((PegelOffset_3 < 1) && (PegelOffset_3 > 0.05)) Ventil_Q6 = true; else Ventil_Q6 = false;

                    if (Tank_1_AutomatischEntleeren) Pegel_1 = PegelOffset_1;
                    if (Tank_2_AutomatischEntleeren) Pegel_2 = PegelOffset_2;
                    if (Tank_3_AutomatischEntleeren) Pegel_3 = PegelOffset_3;

                    if (PegelOffset_1 < 0.05) Tank_1_AutomatischEntleeren = false;
                    if (PegelOffset_2 < 0.05) Tank_2_AutomatischEntleeren = false;
                    if (PegelOffset_3 < 0.05) Tank_3_AutomatischEntleeren = false;

                    if ((Tank_1_AutomatischEntleeren || Tank_2_AutomatischEntleeren || Tank_3_AutomatischEntleeren) == false)
                    {
                        AutomatikModusAktiv = false;
                        AutomatikAktivieren();
                    }
                    break;

                default:
                    AutomatikSchrittschaltwerk = "Init";
                    break;
            }

            TankFuellen();

        }

        public void TankFuellen()
        {

            if (BitmusterTesten(DigOutput, Startbyte_0, BitPos_Q1))
            {
                Pegel_1 += FuellGeschwindigkeit;
            }
            if (BitmusterTesten(DigOutput, Startbyte_0, BitPos_Q3))
            {
                Pegel_2 += FuellGeschwindigkeit;
            }
            if (BitmusterTesten(DigOutput, Startbyte_0, BitPos_Q5))
            {
                Pegel_3 += FuellGeschwindigkeit;
            }
        }
        public void ManuelleFunktionen()
        {
            if (Ventil_Q2)
            {
                Pegel_1 -= SinkGeschwindigkeit;
            }
            if (Ventil_Q4)
            {
                Pegel_2 -= SinkGeschwindigkeit;
            }
            if (Ventil_Q6)
            {
                Pegel_3 -= SinkGeschwindigkeit;
            }

            TankFuellen();

        }

        public void LimitsUeberwachen()
        {


            if (Pegel_1 > 0.95) Pegel_B1 = true; else Pegel_B1 = false;
            if (Pegel_2 > 0.95) Pegel_B3 = true; else Pegel_B3 = false;
            if (Pegel_3 > 0.95) Pegel_B5 = true; else Pegel_B5 = false;

            if (Pegel_1 > 0.05) Pegel_B2 = true; else Pegel_B2 = false;
            if (Pegel_2 > 0.05) Pegel_B4 = true; else Pegel_B4 = false;
            if (Pegel_3 > 0.05) Pegel_B6 = true; else Pegel_B6 = false;


            if (Pegel_1 < 0) Pegel_1 = 0;
            if (Pegel_1 > 1) Pegel_1 = 1;

            if (Pegel_2 < 0) Pegel_2 = 0;
            if (Pegel_2 > 1) Pegel_2 = 1;

            if (Pegel_3 < 0) Pegel_3 = 0;
            if (Pegel_3 > 1) Pegel_3 = 1;
        }

        public void AnzeigeAktualisieren()
        {
            this.Dispatcher.Invoke(() =>
            {
                if (Pegel_B1) lbl_B1.Background = System.Windows.Media.Brushes.Red; else lbl_B1.Background = System.Windows.Media.Brushes.LawnGreen;
                if (Pegel_B2) lbl_B2.Background = System.Windows.Media.Brushes.Red; else lbl_B2.Background = System.Windows.Media.Brushes.LawnGreen;
                if (Pegel_B3) lbl_B3.Background = System.Windows.Media.Brushes.Red; else lbl_B3.Background = System.Windows.Media.Brushes.LawnGreen;
                if (Pegel_B4) lbl_B4.Background = System.Windows.Media.Brushes.Red; else lbl_B4.Background = System.Windows.Media.Brushes.LawnGreen;
                if (Pegel_B5) lbl_B5.Background = System.Windows.Media.Brushes.Red; else lbl_B5.Background = System.Windows.Media.Brushes.LawnGreen;
                if (Pegel_B6) lbl_B6.Background = System.Windows.Media.Brushes.Red; else lbl_B6.Background = System.Windows.Media.Brushes.LawnGreen;

                rct_Behaelter_1_Voll.Margin = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - Pegel_1), 0, 0);
                rct_Behaelter_2_Voll.Margin = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - Pegel_2), 0, 0);
                rct_Behaelter_3_Voll.Margin = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - Pegel_3), 0, 0);

                if (Ventil_Q2)
                {
                    btn_Q2_Ein.Visibility = System.Windows.Visibility.Visible;
                    btn_Q2_Aus.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    btn_Q2_Ein.Visibility = System.Windows.Visibility.Hidden;
                    btn_Q2_Aus.Visibility = System.Windows.Visibility.Visible;
                }

                if (Ventil_Q4)
                {
                    btn_Q4_Ein.Visibility = System.Windows.Visibility.Visible;
                    btn_Q4_Aus.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    btn_Q4_Ein.Visibility = System.Windows.Visibility.Hidden;
                    btn_Q4_Aus.Visibility = System.Windows.Visibility.Visible;
                }

                if (Ventil_Q6)
                {
                    btn_Q6_Ein.Visibility = System.Windows.Visibility.Visible;
                    btn_Q6_Aus.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    btn_Q6_Ein.Visibility = System.Windows.Visibility.Hidden;
                    btn_Q6_Aus.Visibility = System.Windows.Visibility.Visible;
                }

                if (BitmusterTesten(DigOutput, Startbyte_0, BitPos_Q1))
                {
                    img_Q1_Ein.Visibility = System.Windows.Visibility.Visible;
                    img_Q1_Aus.Visibility = System.Windows.Visibility.Hidden;
                    rct_Zuleitung_1b.Fill = System.Windows.Media.Brushes.Blue;
                }
                else
                {
                    img_Q1_Ein.Visibility = System.Windows.Visibility.Hidden;
                    img_Q1_Aus.Visibility = System.Windows.Visibility.Visible;
                    rct_Zuleitung_1b.Fill = System.Windows.Media.Brushes.LightBlue;
                }

                if (BitmusterTesten(DigOutput, Startbyte_0, BitPos_Q3))
                {
                    img_Q3_Ein.Visibility = System.Windows.Visibility.Visible;
                    img_Q3_Aus.Visibility = System.Windows.Visibility.Hidden;
                    rct_Zuleitung_2b.Fill = System.Windows.Media.Brushes.Blue;
                }
                else
                {
                    img_Q3_Ein.Visibility = System.Windows.Visibility.Hidden;
                    img_Q3_Aus.Visibility = System.Windows.Visibility.Visible;
                    rct_Zuleitung_2b.Fill = System.Windows.Media.Brushes.LightBlue;
                }

                if (BitmusterTesten(DigOutput, Startbyte_0, BitPos_Q5))
                {
                    img_Q5_Ein.Visibility = System.Windows.Visibility.Visible;
                    img_Q5_Aus.Visibility = System.Windows.Visibility.Hidden;
                    rct_Zuleitung_3b.Fill = System.Windows.Media.Brushes.Blue;
                }
                else
                {
                    img_Q5_Ein.Visibility = System.Windows.Visibility.Hidden;
                    img_Q5_Aus.Visibility = System.Windows.Visibility.Visible;
                    rct_Zuleitung_3b.Fill = System.Windows.Media.Brushes.LightBlue;
                }


                if (BitmusterTesten(DigOutput, Startbyte_0, BitPos_P1))
                {
                    circ_Stoerung.Fill = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    circ_Stoerung.Fill = System.Windows.Media.Brushes.LightGray;
                }

                if (Pegel_1 > 0.1) rct_Ableitung_1a.Fill = System.Windows.Media.Brushes.Blue; else rct_Ableitung_1a.Fill = System.Windows.Media.Brushes.LightBlue;
                if (Pegel_2 > 0.1) rct_Ableitung_2a.Fill = System.Windows.Media.Brushes.Blue; else rct_Ableitung_2a.Fill = System.Windows.Media.Brushes.LightBlue;
                if (Pegel_3 > 0.1) rct_Ableitung_3a.Fill = System.Windows.Media.Brushes.Blue; else rct_Ableitung_3a.Fill = System.Windows.Media.Brushes.LightBlue;

                if (
                    ((Pegel_1 > 0.1) && Ventil_Q2)
                    ||
                    ((Pegel_2 > 0.1) && Ventil_Q4)
                    ||
                    ((Pegel_3 > 0.1) && Ventil_Q6)
                    )
                {
                    rct_Ableitung_1b.Fill = System.Windows.Media.Brushes.Blue;
                    rct_Ableitung_2b.Fill = System.Windows.Media.Brushes.Blue;
                    rct_Ableitung_3b.Fill = System.Windows.Media.Brushes.Blue;
                    rct_Ableitung.Fill = System.Windows.Media.Brushes.Blue;
                }

                else
                {
                    rct_Ableitung_1b.Fill = System.Windows.Media.Brushes.LightBlue;
                    rct_Ableitung_2b.Fill = System.Windows.Media.Brushes.LightBlue;
                    rct_Ableitung_3b.Fill = System.Windows.Media.Brushes.LightBlue;
                    rct_Ableitung.Fill = System.Windows.Media.Brushes.LightBlue;
                }

            });

        }

    }
}
