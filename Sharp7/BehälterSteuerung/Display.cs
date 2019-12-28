using System.Threading;

namespace BehälterSteuerung
{
    public partial class MainWindow
    {
        public void Display_Task()
        {
            while (FensterAktiv)
            {
                this.Dispatcher.Invoke(() =>
                {
                    if (FensterAktiv)
                    {
                        foreach (Behaelter beh in gAlleBehaelter) beh.BehaelterAnzeigen(FensterAktiv);
                        AbleitungenAnzeigen(FensterAktiv);
                    }
                });

                Thread.Sleep(10);
            }
        }



        public void AbleitungenAnzeigen(bool FensterAktiv)
        {
            bool AbleitungenVoll = false;

            foreach (Behaelter beh in gAlleBehaelter)
            {
                if ((beh.Pegel > 0.1) && beh.VentilUnten) AbleitungenVoll = true;
            }

            if (FensterAktiv)
            {
                if (AbleitungenVoll)
                {
                    rct_Ableitung_1b.Fill = System.Windows.Media.Brushes.Blue;
                    rct_Ableitung_2b.Fill = System.Windows.Media.Brushes.Blue;
                    rct_Ableitung_3b.Fill = System.Windows.Media.Brushes.Blue;
                    rct_Ableitung_4b.Fill = System.Windows.Media.Brushes.Blue;
                    rct_Ableitung.Fill = System.Windows.Media.Brushes.Blue;
                }
                else
                {
                    rct_Ableitung_1b.Fill = System.Windows.Media.Brushes.LightBlue;
                    rct_Ableitung_2b.Fill = System.Windows.Media.Brushes.LightBlue;
                    rct_Ableitung_3b.Fill = System.Windows.Media.Brushes.LightBlue;
                    rct_Ableitung_4b.Fill = System.Windows.Media.Brushes.LightBlue;
                    rct_Ableitung.Fill = System.Windows.Media.Brushes.LightBlue;
                }

                if (Leuchte_P1) circ_Stoerung.Fill = System.Windows.Media.Brushes.Red;
                else circ_Stoerung.Fill = System.Windows.Media.Brushes.LightGray;
            }

        }

        public void AutomatikKnoepfeDeaktivieren()
        {
            this.Dispatcher.Invoke(() =>
            {
                btn_Automatik_1234.IsEnabled = false;
                btn_Automatik_1324.IsEnabled = false;
                btn_Automatik_1432.IsEnabled = false;
                btn_Automatik_4321.IsEnabled = false;
            });
        }

        public void AutomatikKnoepfeAktivieren()
        {
            this.Dispatcher.Invoke(() =>
            {
                btn_Automatik_1234.IsEnabled = true;
                btn_Automatik_1324.IsEnabled = true;
                btn_Automatik_1432.IsEnabled = true;
                btn_Automatik_4321.IsEnabled = true;
            });
        }

    }
}
