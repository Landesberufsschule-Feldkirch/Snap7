using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Shapes;
using Sharp7;

namespace BehälterSteuerung
{
    public partial class MainWindow
    {
        public class Behaelter
        {
            private readonly int BitPositionSchwimmerschalterOben;
            private readonly int BitPositionSchwimmerschalterUnten;
            private readonly int BitPositionVentilOben;

            public double Pegel { get; set; }
            public double InternerPegel { get; set; }

            public bool SchwimmerschalterOben;
            public bool SchwimmerschalterUnten;
            public bool VentilOben;
            public bool VentilUnten { get; set; }

            private readonly Image ImgVentilObenEin;
            private readonly Image ImgVentilObenAus;
            private readonly Image ImgVentilUntenEin;
            private readonly Image ImgVentilUntenAus;

            private readonly Rectangle RctZuleitung;
            private readonly Rectangle RctAbleitung;
            private readonly Rectangle RctBehaelterFuellstand;

            private readonly Label LblSchwimmerschalterOben;
            private readonly Label LblSchwimmerschalterUnten;

            private readonly Button BtnVentilUntenEin;
            private readonly Button BtnVentilUntenAus;

            private readonly double SinkGeschwindigkeit = 0.005;
            private readonly double FuellGeschwindigkeit = 0.2 * 0.005;
            private readonly double HoeheFuellBalken = 200.0;

            public Behaelter(double NeuerPegel,
                                Image VentilObenEin, Image VentilObenAus, Image VentilUntenEin, Image VentilUntenAus,
                                Rectangle Zuleitung, Rectangle Ableitung, Rectangle BehalterFuellstand,
                                Label SchwimmerschalterOben, Label SchwimmerschalterUnten,
                                Button VentUntenEin, Button VentUntenAus,
                                int BitPosSchwimmerschalterOben, int BitPosSchwimmerschalterUnten, int BitPosVentilOben)
            {
                Pegel = NeuerPegel;

                ImgVentilObenEin = VentilObenEin;
                ImgVentilObenAus = VentilObenAus;
                ImgVentilUntenEin = VentilUntenEin;
                ImgVentilUntenAus = VentilUntenAus;

                RctZuleitung = Zuleitung;
                RctAbleitung = Ableitung;
                RctBehaelterFuellstand = BehalterFuellstand;

                LblSchwimmerschalterOben = SchwimmerschalterOben;
                LblSchwimmerschalterUnten = SchwimmerschalterUnten;
            
                BtnVentilUntenEin = VentUntenEin;
                BtnVentilUntenAus = VentUntenAus;

                BitPositionSchwimmerschalterOben = BitPosSchwimmerschalterOben;
                BitPositionSchwimmerschalterUnten = BitPosSchwimmerschalterUnten;
                BitPositionVentilOben = BitPosVentilOben;
            }
            public void PegelUeberwachen()
            {
                if (InternerPegel > 0)
                {
                    VentilUnten = true;
                    InternerPegel -= SinkGeschwindigkeit;
                    Pegel = InternerPegel;
                }
                else
                {
                    if (VentilOben) Pegel += FuellGeschwindigkeit;
                    if (VentilUnten) Pegel -= SinkGeschwindigkeit;
                }

                if (Pegel > 1) Pegel = 1;
                if (Pegel < 0) Pegel = 0;

                if (Pegel > 0.95) SchwimmerschalterOben = true; else SchwimmerschalterOben = false;
                if (Pegel > 0.05) SchwimmerschalterUnten = true; else SchwimmerschalterUnten = false;
            }
            public void BehaelterAnzeigen(bool FensterAktiv)
            {
                if (FensterAktiv)
                {
                    if (SchwimmerschalterOben) LblSchwimmerschalterOben.Background = System.Windows.Media.Brushes.Red; else LblSchwimmerschalterOben.Background = System.Windows.Media.Brushes.LawnGreen;
                    if (SchwimmerschalterUnten) LblSchwimmerschalterUnten.Background = System.Windows.Media.Brushes.Red; else LblSchwimmerschalterUnten.Background = System.Windows.Media.Brushes.LawnGreen;

                    if (VentilOben)
                    {
                        ImgVentilObenEin.Visibility = System.Windows.Visibility.Visible;
                        ImgVentilObenAus.Visibility = System.Windows.Visibility.Hidden;
                        RctZuleitung.Fill = System.Windows.Media.Brushes.Blue;
                    }
                    else
                    {
                        ImgVentilObenEin.Visibility = System.Windows.Visibility.Hidden;
                        ImgVentilObenAus.Visibility = System.Windows.Visibility.Visible;
                        RctZuleitung.Fill = System.Windows.Media.Brushes.LightBlue;
                    }

                    if (VentilUnten)
                    {
                        ImgVentilUntenEin.Visibility = System.Windows.Visibility.Visible;
                        ImgVentilUntenAus.Visibility = System.Windows.Visibility.Hidden;
                        BtnVentilUntenEin.Visibility = System.Windows.Visibility.Visible;
                        BtnVentilUntenAus.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        ImgVentilUntenEin.Visibility = System.Windows.Visibility.Hidden;
                        ImgVentilUntenAus.Visibility = System.Windows.Visibility.Visible;
                        BtnVentilUntenEin.Visibility = System.Windows.Visibility.Hidden;
                        BtnVentilUntenAus.Visibility = System.Windows.Visibility.Visible;
                    }

                    if (VentilUnten && (Pegel > 0.1)) RctAbleitung.Fill = System.Windows.Media.Brushes.Blue; else RctAbleitung.Fill = System.Windows.Media.Brushes.LightBlue;

                    RctBehaelterFuellstand.Margin = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - Pegel), 0, 0);
                }

            }

            public void BehalterDatenRangieren(ref byte[] BufferInput, ref byte[] BufferOutput)
            {
                S7.SetBitAt(ref BufferInput, (BitPositionSchwimmerschalterOben / 8), (BitPositionSchwimmerschalterOben % 8), SchwimmerschalterOben);
                S7.SetBitAt(ref BufferInput, (BitPositionSchwimmerschalterUnten / 8), (BitPositionSchwimmerschalterUnten % 8), SchwimmerschalterUnten);

                VentilOben = S7.GetBitAt(BufferOutput, (BitPositionVentilOben / 8), (BitPositionVentilOben % 8));
            }
            public void AutomatikmodusStarten(double Startpegel)
            {
                this.InternerPegel = Startpegel;
            }
        }

        public ObservableCollection<Behaelter> gAlleBehaelter = new ObservableCollection<Behaelter>();
        public void AlleBehaelterInitialisieren()
        {
            gAlleBehaelter.Add(new Behaelter(0.9, img_Q1_Ein, img_Q1_Aus, img_Q2_Ein, img_Q2_Aus, rct_Ableitung_1a, rct_Ableitung_1a, rct_Behaelter_1_Voll, lbl_B1, lbl_B2, btn_Q2_Ein, btn_Q2_Aus, (int)BitPosEingang.B1, (int)BitPosEingang.B2, (int)BitPosAusgang.Q1));
            gAlleBehaelter.Add(new Behaelter(0.7, img_Q3_Ein, img_Q3_Aus, img_Q4_Ein, img_Q4_Aus, rct_Ableitung_2a, rct_Ableitung_2a, rct_Behaelter_2_Voll, lbl_B3, lbl_B4, btn_Q4_Ein, btn_Q4_Aus, (int)BitPosEingang.B3, (int)BitPosEingang.B4, (int)BitPosAusgang.Q3));
            gAlleBehaelter.Add(new Behaelter(0.5, img_Q5_Ein, img_Q5_Aus, img_Q6_Ein, img_Q6_Aus, rct_Ableitung_3a, rct_Ableitung_3a, rct_Behaelter_3_Voll, lbl_B5, lbl_B6, btn_Q6_Ein, btn_Q6_Aus, (int)BitPosEingang.B5, (int)BitPosEingang.B6, (int)BitPosAusgang.Q5));
            gAlleBehaelter.Add(new Behaelter(0.3, img_Q7_Ein, img_Q7_Aus, img_Q8_Ein, img_Q8_Aus, rct_Ableitung_4a, rct_Ableitung_4a, rct_Behaelter_4_Voll, lbl_B7, lbl_B8, btn_Q8_Ein, btn_Q8_Aus, (int)BitPosEingang.B7, (int)BitPosEingang.B8, (int)BitPosAusgang.Q7));
        }

    }
}