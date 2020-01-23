using Sharp7;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace BehälterSteuerung
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

        public void BehalterDatenRangierenInput(byte[] BufferInput)
        {
            S7.SetBitAt(BufferInput, BitPositionSchwimmerschalterOben, SchwimmerschalterOben);
            S7.SetBitAt(BufferInput, BitPositionSchwimmerschalterUnten, SchwimmerschalterUnten);
        }

        public void BehalterDatenRangierenOutput(byte[] BufferOutput)
        {
            VentilOben = S7.GetBitAt(BufferOutput, BitPositionVentilOben);
        }

        public void AutomatikmodusStarten(double Startpegel)
        {
            this.InternerPegel = Startpegel;
        }
    }
}