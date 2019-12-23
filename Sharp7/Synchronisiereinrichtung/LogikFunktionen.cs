using System;
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
            U_f_Phase_Leistung,
            U_f_Phase_Leistungsfaktor
        }

        public SynchronisierungAuswahl AuswahlSynchronisierung { get; set; }

        public void Logikfunktionen_Task()
        {
            const double Y_n_Faktor = 0.001;
            const double n_BremsFaktor = 0.991;
            const double Zeitdauer = 10;

            double WinkelGenerator = 0;
            double WinkelNetz = 0;


            Punkt MomentanSpannungGenerator;
            Punkt MomentanSpannungNetz;


            while (FensterAktiv)
            {
                Drehzahl += Y * Y_n_Faktor;
                Drehzahl *= n_BremsFaktor;

                SpannungGenerator = Drehzahl * MagnetischerKreis.Magnetisierungskennlinie(S7Analog.S7_Analog_2_Double(Ie, 10)) / 2;
                FrequenzGenerator = Drehzahl / 30;

                n = S7Analog.S7_Analog_2_Short(Drehzahl, 5000);
                UGenerator = S7Analog.S7_Analog_2_Short(SpannungGenerator, 1000);
                fGenerator = S7Analog.S7_Analog_2_Short(FrequenzGenerator, 100);
                UDiff = S7Analog.S7_Analog_2_Short(SpannungsUnterschiedSynchronisieren, 1000);
                ph = S7Analog.S7_Analog_2_Short(Phasenlage, 1);

                WinkelNetz = RestKlasse.WinkelBerechnen(Zeitdauer, FrequenzNetz, WinkelNetz);
                WinkelGenerator = RestKlasse.WinkelBerechnen(Zeitdauer, FrequenzGenerator, WinkelGenerator);

                MomentanSpannungNetz = RestKlasse.GetSpannung(WinkelNetz, SpannungNetz);
                MomentanSpannungGenerator = RestKlasse.GetSpannung(WinkelGenerator, SpannungGenerator);

                SpannungDifferenz = SpannungNetz - SpannungGenerator;
                FrequenzDifferenz = FrequenzNetz - FrequenzGenerator;
                Zeiger SpannungsDiff = new Zeiger(MomentanSpannungGenerator, MomentanSpannungNetz);
                SpannungsUnterschiedSynchronisieren = SpannungsDiff.Laenge();

                if (Q1alt != Q1)
                {
                    Q1alt = Q1;
                    if (FrequenzDifferenz > 2) MaschineTot = true;
                    if (SpannungDifferenz > 10) MaschineTot = true;
                }
                Thread.Sleep((int)Zeitdauer);
            }
        }

    }

    public static class RestKlasse
    {

        public static double WinkelBerechnen(double zeit, double freqenz, double winkel)
        {
            double PeriodenDauer = 1000 / freqenz; // in ms
            winkel += 360 * zeit / PeriodenDauer;
            if (winkel > 360) winkel -= 360;
            return winkel;
        }

        public static Punkt GetSpannung(double winkel, double spannung)
        {

            var strangSpannung = spannung / Math.Sqrt(3);
            return new Punkt(strangSpannung, winkel, 0);
        }



        public static (double diffF, double diffV) GetDifferenz(double vNetz, double vGenerator, double fNetz, double fGen)
        {
            var diffV = vNetz - vGenerator;
            var diffF = fNetz - fGen;

            return (diffF, diffV);
        }




    }
    public class MagnetischerKreis
    {

        public static double Magnetisierungskennlinie(double strom)
        {
            return 1 - Math.Exp(-strom);
        }

    }

}
