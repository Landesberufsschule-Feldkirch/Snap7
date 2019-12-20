using System.Threading;
using System.Windows.Controls;
using Utilities;


namespace Synchronisiereinrichtung
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

                        switch (AuswahlSynchronisierung)
                        {
                            case SynchronisierungAuswahl.U_f:
                                TxtLeistungGenerator.Visibility = System.Windows.Visibility.Hidden;
                                TxtLeistungNetz.Visibility = System.Windows.Visibility.Hidden;
                                LblLeistung.Visibility = System.Windows.Visibility.Hidden;
                                SldNetzLeistung.Visibility = System.Windows.Visibility.Hidden;
                                Grid.SetRowSpan(RctAuswahl, 13);
                                break;
                            case SynchronisierungAuswahl.U_f_Phase:
                                TxtLeistungGenerator.Visibility = System.Windows.Visibility.Hidden;
                                TxtLeistungNetz.Visibility = System.Windows.Visibility.Hidden;
                                LblLeistung.Visibility = System.Windows.Visibility.Hidden;
                                SldNetzLeistung.Visibility = System.Windows.Visibility.Hidden;
                                Grid.SetRowSpan(RctAuswahl, 13);
                                break;
                            case SynchronisierungAuswahl.U_f_Phase_Leistung:
                                TxtLeistungGenerator.Visibility = System.Windows.Visibility.Visible;
                                TxtLeistungNetz.Visibility = System.Windows.Visibility.Visible;
                                LblLeistung.Visibility = System.Windows.Visibility.Visible;
                                SldNetzLeistung.Visibility = System.Windows.Visibility.Visible;
                                Grid.SetRowSpan(RctAuswahl, 15);
                                break;
                            default:
                                break;

                        }

                        TxtVentilOeffnung.Text = System.Math.Round(S7Analog.S7_Analog_2_Double(Y, 100), 1).ToString() + "%";
                        TxtErregerstrom.Text = System.Math.Round(S7Analog.S7_Analog_2_Double(Ie, 10), 1).ToString() + "A";
                        BildEinblenden(Q1, ImgSchalterEin, ImgSchalterAus);

                        TxtDrehzahl.Text = "n = " + System.Math.Round(Drehzahl, 1).ToString() + "RPM";

                        TxtSpannungGenerator.Text = "U = " + System.Math.Round(SpannungGenerator, 1).ToString() + "V";
                        TxtFrequenzGenerator.Text = "f = " + System.Math.Round(FrequenzGenerator, 1).ToString() + "Hz";
                        TxtLeistungGenerator.Text = "P = " + System.Math.Round(LeistungGenerator, 1).ToString() + "W";

                        TxtSpannungNetz.Text = "U = " + System.Math.Round(SpannungNetz, 1).ToString() + "V";
                        TxtFrequenzNetz.Text = "f = " + System.Math.Round(FrequenzNetz, 1).ToString() + "Hz";
                        TxtLeistungNetz.Text = "P = " + System.Math.Round(LeistungNetz, 1).ToString() + "W";

                        // Eingaben lesen und übernehmen
                        UNetz = (short)SldNetzSpannung.Value;
                        fNetz = (short)SldNetzFrequenz.Value;
                        PNetz = (short)SldNetzLeistung.Value;

                        SpannungNetz = S7Analog.S7_Analog_2_Double(UNetz, 1000);
                        FrequenzNetz = S7Analog.S7_Analog_2_Double(fNetz, 100);
                        LeistungNetz = S7Analog.S7_Analog_2_Double(PNetz, 10000);

                        if (DebugWindowAktiv)
                        {
                            Q1 = secondWindow.Q1;
                            Y = (int)secondWindow.Sld_Y.Value;
                            Ie = (int)secondWindow.Sld_Ie.Value;
                        }
                    }
                });
                Thread.Sleep(10);
            }
        }

        public void BildEinblenden(bool Bedingung, Image imgEin, Image imgAus)
        {
            if (Bedingung)
            {
                imgEin.Visibility = System.Windows.Visibility.Visible;
                imgAus.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                imgEin.Visibility = System.Windows.Visibility.Hidden;
                imgAus.Visibility = System.Windows.Visibility.Visible;
            }
        }

    }
}
