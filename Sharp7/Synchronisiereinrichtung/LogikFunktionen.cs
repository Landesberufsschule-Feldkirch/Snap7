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


        public readonly double Y_n_Faktor = 0.001;
        public  double n_BremsFaktor = 0.92;

        public SynchronisierungAuswahl AuswahlSynchronisierung;
        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                Drehzahl += Y * Y_n_Faktor;
                Drehzahl *= n_BremsFaktor;

                SpannungGenerator = Drehzahl * S7Analog.S7_Analog_2_Double(Ie, 10) / 20;
                FrequenzGenerator = Drehzahl / 30;

                n = S7Analog.S7_Analog_2_Short(Drehzahl, 5000);
                UGenerator = S7Analog.S7_Analog_2_Short(SpannungGenerator, 1000);
                fGenerator = S7Analog.S7_Analog_2_Short(FrequenzGenerator, 100);
                UDiff = S7Analog.S7_Analog_2_Short(SpannungDifferenz, 1000);
                ph = S7Analog.S7_Analog_2_Short(Phasenlage, 1);






                Thread.Sleep(10);
            }
        }



    }
}
