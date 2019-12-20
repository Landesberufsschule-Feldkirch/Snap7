using System.Threading;
using Utilities;


namespace Synchronisiereinrichtung
{
    public partial class MainWindow
    {
        public enum SynchronisierungAuswahl
        {
            U_f = 0,
            U_f_Phase,
            U_f_Phase_Leistung
        }


        public readonly double Y_n_Faktor = 0.01;
        public readonly double n_BremsFaktor = 0.92;

        public SynchronisierungAuswahl AuswahlSynchronisierung;
        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {

                Drehzahl += Y * Y_n_Faktor;
                Drehzahl *= n_BremsFaktor;

                SpannungGenerator = Drehzahl * S7Analog.S7_Analog_2_Double(Ie, 10) / 10;
                FrequenzGenerator = Drehzahl / 30;

                n = S7Analog.S7_Analog_2_Short(Drehzahl, 5000);
                UGenerator = S7Analog.S7_Analog_2_Short(SpannungGenerator, 1000);
                fGenerator = S7Analog.S7_Analog_2_Short(FrequenzGenerator, 100);
                UDiff = S7Analog.S7_Analog_2_Short(SpannungDifferenz, 1000);
                ph = S7Analog.S7_Analog_2_Short(Phasenlage, 1);

                this.Dispatcher.Invoke(() =>
                               {
                                   UNetz = (short)SldNetzSpannung.Value;
                                   fNetz = (short)SldNetzFrequenz.Value;
                                   PNetz = (short)SldNetzLeistung.Value;

                                   SpannungNetz = S7Analog.S7_Analog_2_Double(UNetz, 1000);
                                   FrequenzNetz = S7Analog.S7_Analog_2_Double(fNetz, 100);
                                   LeistungNetz = S7Analog.S7_Analog_2_Double(PNetz, 1000);

                                   if (DebugWindowAktiv)
                                   {
                                       Q1 = secondWindow.Q1;
                                       Y = (int)secondWindow.Sld_Y.Value;
                                       Ie = (int)secondWindow.Sld_Ie.Value;
                                   }
                               });

                Thread.Sleep(100);
            }
        }



    }
}
