using Utilities;

namespace Synchronisiereinrichtung
{
    public class Kraftwerk
    {


        public bool MaschineTot;

        #region Sollwerte zur Bedienung

        public MainWindow.SynchronisierungAuswahl AuswahlSynchronisierung;
        public double Spannung;
        public double Frequenz;
        public double Leistung;
        public double Leistungsfaktor;

        #endregion

        #region Werte für die SPS

        public short Gen_Ie;

        public short Gen_n;
        public short Gen_U;
        public short Gen_f;
        public short Gen_P;
        public short Gen_cosPhi;

        public short Netz_U;
        public short Netz_f;
        public short Netz_P;
        public short Netz_cosPhi;

        public short UDiff;
        public short ph;

        #endregion

        Drehstromgenerator generator = new Drehstromgenerator(1 / 2, 1 / 30);

        #region Variablen zum berechnen
        public double Y;
        public double Generator_n;
        public double Generator_f;
        public double Generator_U;
        public double Generator_P;
        public double Generator_cosPhi;
        public double Generator_Winkel;
        public Punkt Generator_Momentanspannung;

        public double Netz_Winkel;
        public Punkt Netz_Momentanspannung;


        public double SpannungsUnterschiedSynchronisieren;
        public double FrequenzDifferenz;
        public double Phasenlage;

        public double Zeitdauer;

        #endregion

        public Kraftwerk()
        {
            // leerer Konstruktor
            MaschineTot = false;
        }

        public void KraftwerkTask()
        {
            generator.MaschineAntreiben(Y);
            Generator_n = generator.Drehzahl();
            Generator_U = generator.Spannung(S7Analog.S7_Analog_2_Double(Gen_Ie, 10));
            Generator_f = generator.Frequenz();

            Netz_Winkel = DrehstromZeiger.WinkelBerechnen(Zeitdauer, Frequenz, Netz_Winkel);
            Generator_Winkel = DrehstromZeiger.WinkelBerechnen(Zeitdauer, Generator_f, Generator_Winkel);

            Netz_Momentanspannung = DrehstromZeiger.GetSpannung(Netz_Winkel, Spannung);
            Generator_Momentanspannung = DrehstromZeiger.GetSpannung(Generator_Winkel, Generator_U);

            FrequenzDifferenz = Frequenz - Generator_f;
            Zeiger SpannungsDiff = new Zeiger(Generator_Momentanspannung, Netz_Momentanspannung);

            SpannungsUnterschiedSynchronisieren = SpannungsDiff.Laenge();
        }



        public void WertebereicheUmrechnen()
        {
            // Sollwerte --> SPS
            Netz_U = S7Analog.S7_Analog_2_Short(Spannung, 1000);
            Netz_f = S7Analog.S7_Analog_2_Short(Frequenz, 100);
            Netz_P = S7Analog.S7_Analog_2_Short(Leistung, 1000);
            Netz_cosPhi = S7Analog.S7_Analog_2_Short(Leistungsfaktor, 1);

            // Modell --> SPS
            Gen_n = S7Analog.S7_Analog_2_Short(Generator_n, 5000);
            Gen_U = S7Analog.S7_Analog_2_Short(Generator_U, 1000);
            Gen_f = S7Analog.S7_Analog_2_Short(Generator_f, 100);
            Gen_P = S7Analog.S7_Analog_2_Short(Generator_P, 1000);
            UDiff = S7Analog.S7_Analog_2_Short(SpannungsUnterschiedSynchronisieren, 1000);
            ph = S7Analog.S7_Analog_2_Short(Phasenlage, 1);

        }

        public void Synchronisieren()
        {
            var SpannungDifferenz = Spannung - Generator_U;

            switch (AuswahlSynchronisierung)
            {
                case MainWindow.SynchronisierungAuswahl.U_f:
                    if (FrequenzDifferenz > 2) MaschineTot = true;
                    if (SpannungDifferenz > 25) MaschineTot = true;
                    break;

                case MainWindow.SynchronisierungAuswahl.U_f_Phase:
                    if (FrequenzDifferenz > 1) MaschineTot = true;
                    if (SpannungDifferenz > 10) MaschineTot = true;
                    break;

                case MainWindow.SynchronisierungAuswahl.U_f_Phase_Leistung:
                    if (FrequenzDifferenz > 0.9) MaschineTot = true;
                    if (SpannungDifferenz > 10) MaschineTot = true;
                    break;

                case MainWindow.SynchronisierungAuswahl.U_f_Phase_Leistungsfaktor:
                    if (FrequenzDifferenz > 0.8) MaschineTot = true;
                    if (SpannungDifferenz > 10) MaschineTot = true;
                    break;

                default:

                    break;
            }

        }
    }
}
