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
                        TxtVentilOeffnung.Text = S7Analog.S7_Analog_2_Double(Y, 100, 1).ToString() + "%";
                        TxtErregerstrom.Text = S7Analog.S7_Analog_2_Double(Ie, 10, 1).ToString() + "A";
                        BildEinblenden(Q1, ImgSchalterEin, ImgSchalterAus);

                        TxtDrehzahl.Text = "n = " + System.Math.Round(Drehzahl, 1).ToString() + "RPM";

                        TxtSpannungGenerator.Text = "U = " + System.Math.Round(SpannungGenerator, 1).ToString() + "V";
                        TxtFrequenzGenerator.Text = "f = " + System.Math.Round(FrequenzGenerator, 1).ToString() + "Hz";

                        TxtSpannungNetz.Text = "U = " + System.Math.Round(SpannungNetz, 1).ToString() + "V";
                        TxtFrequenzNetz.Text = "f = " + System.Math.Round(FrequenzNetz, 1).ToString() + "Hz";



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
