using System;
using System.Threading;
using Utilities;


namespace Synchronisiereinrichtung
{
    public class Logikfunktionen
    {
        readonly MainWindow mainWindow;

        public Logikfunktionen(MainWindow window)
        {
            mainWindow = window;
        }
        public void Logikfunktionen_Task()
        {
            const double Y_n_Faktor = 0.001;
            const double n_BremsFaktor = 0.991;
            const double Zeitdauer = 10;

            double WinkelGenerator = 0;
            double WinkelNetz = 0;

            Punkt MomentanSpannungGenerator;
            Punkt MomentanSpannungNetz;


            while (mainWindow.FensterAktiv)
            {
                mainWindow.Drehzahl += mainWindow.Y * Y_n_Faktor;
                mainWindow.Drehzahl *= n_BremsFaktor;

                mainWindow.SpannungGenerator = mainWindow.Drehzahl * MagnetischerKreis.Magnetisierungskennlinie(S7Analog.S7_Analog_2_Double(mainWindow.Ie, 10)) / 2;
                mainWindow.FrequenzGenerator = mainWindow.Drehzahl / 30;

                mainWindow.n = S7Analog.S7_Analog_2_Short(mainWindow.Drehzahl, 5000);
                mainWindow.UGenerator = S7Analog.S7_Analog_2_Short(mainWindow.SpannungGenerator, 1000);
                mainWindow.fGenerator = S7Analog.S7_Analog_2_Short(mainWindow.FrequenzGenerator, 100);
                mainWindow.UDiff = S7Analog.S7_Analog_2_Short(mainWindow.SpannungsUnterschiedSynchronisieren, 1000);
                mainWindow.ph = S7Analog.S7_Analog_2_Short(mainWindow.Phasenlage, 1);

                WinkelNetz = RestKlasse.WinkelBerechnen(Zeitdauer, mainWindow.FrequenzNetz, WinkelNetz);
                WinkelGenerator = RestKlasse.WinkelBerechnen(Zeitdauer, mainWindow.FrequenzGenerator, WinkelGenerator);

                MomentanSpannungNetz = RestKlasse.GetSpannung(WinkelNetz, mainWindow.SpannungNetz);
                MomentanSpannungGenerator = RestKlasse.GetSpannung(WinkelGenerator, mainWindow.SpannungGenerator);

                mainWindow.SpannungDifferenz = mainWindow.SpannungNetz - mainWindow.SpannungGenerator;
                mainWindow.FrequenzDifferenz = mainWindow.FrequenzNetz - mainWindow.FrequenzGenerator;
                Zeiger SpannungsDiff = new Zeiger(MomentanSpannungGenerator, MomentanSpannungNetz);
                mainWindow.SpannungsUnterschiedSynchronisieren = SpannungsDiff.Laenge();

                if (mainWindow.Q1alt != mainWindow.Q1)
                {
                    mainWindow.Q1alt = mainWindow.Q1;
                    switch (mainWindow.AuswahlSynchronisierung)
                    {
                        case MainWindow.SynchronisierungAuswahl.U_f:
                            if (mainWindow.FrequenzDifferenz > 2) mainWindow.MaschineTot = true;
                            if (mainWindow.SpannungDifferenz > 25) mainWindow.MaschineTot = true;
                            break;

                        case MainWindow.SynchronisierungAuswahl.U_f_Phase:
                            if (mainWindow.FrequenzDifferenz > 1) mainWindow.MaschineTot = true;
                            if (mainWindow.SpannungDifferenz > 10) mainWindow.MaschineTot = true;
                            break;

                        case MainWindow.SynchronisierungAuswahl.U_f_Phase_Leistung:
                            if (mainWindow.FrequenzDifferenz > 0.9) mainWindow.MaschineTot = true;
                            if (mainWindow.SpannungDifferenz > 10) mainWindow.MaschineTot = true;
                            break;

                        case MainWindow.SynchronisierungAuswahl.U_f_Phase_Leistungsfaktor:
                            if (mainWindow.FrequenzDifferenz > 0.8) mainWindow.MaschineTot = true;
                            if (mainWindow.SpannungDifferenz > 10) mainWindow.MaschineTot = true;
                            break;

                        default:

                            break;
                    }
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
