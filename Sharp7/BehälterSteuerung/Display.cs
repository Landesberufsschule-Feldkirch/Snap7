using System.Windows.Controls;
using System.Windows.Shapes;

namespace BehälterSteuerung
{
    public partial class MainWindow
    {
        public void btn_Sichtbarkeit(bool Bedingung, Button btn_Ein, Button btn_Aus)
        {
            if (Bedingung)
            {
                btn_Ein.Visibility = System.Windows.Visibility.Visible;
                btn_Aus.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                btn_Ein.Visibility = System.Windows.Visibility.Hidden;
                btn_Aus.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public void img_Sichtbarkeit(bool Bedingung, Image img_Ein, Image img_Aus, Rectangle rct_Leitung)
        {
            if (Bedingung)
            {
                img_Ein.Visibility = System.Windows.Visibility.Visible;
                img_Aus.Visibility = System.Windows.Visibility.Hidden;
                rct_Leitung.Fill = System.Windows.Media.Brushes.Blue;
            }
            else
            {
                img_Ein.Visibility = System.Windows.Visibility.Hidden;
                img_Aus.Visibility = System.Windows.Visibility.Visible;
                rct_Leitung.Fill = System.Windows.Media.Brushes.LightBlue;
            }
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

                btn_Sichtbarkeit(Ventil_Q2, btn_Q2_Ein, btn_Q2_Aus);
                btn_Sichtbarkeit(Ventil_Q4, btn_Q4_Ein, btn_Q4_Aus);
                btn_Sichtbarkeit(Ventil_Q6, btn_Q6_Ein, btn_Q6_Aus);

                img_Sichtbarkeit(Ventil_Q1, img_Q1_Ein, img_Q1_Aus, rct_Zuleitung_1b);
                img_Sichtbarkeit(Ventil_Q3, img_Q3_Ein, img_Q3_Aus, rct_Zuleitung_2b);
                img_Sichtbarkeit(Ventil_Q5, img_Q5_Ein, img_Q5_Aus, rct_Zuleitung_3b);

                if (Leuchte_P1) circ_Stoerung.Fill = System.Windows.Media.Brushes.Red;
                else circ_Stoerung.Fill = System.Windows.Media.Brushes.LightGray;

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
