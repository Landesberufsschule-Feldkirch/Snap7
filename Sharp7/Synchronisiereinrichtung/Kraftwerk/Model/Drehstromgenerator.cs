namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    public class Drehstromgenerator
    {
        readonly MagnetischerKreis Magnetisierung = new MagnetischerKreis(0.5);

        double n;
        readonly double SpannungsFaktor;
        readonly double DrehzahlFaktor;

        const double Y_n_Faktor = 0.28;
        const double n_BremsFaktor = 0.991;


        public Drehstromgenerator(double spannungsFaktor, double drehzahlFaktor)
        {
            SpannungsFaktor = spannungsFaktor;
            DrehzahlFaktor = drehzahlFaktor;
        }

        public void MaschineAntreiben(double Y)
        {
            n += Y * Y_n_Faktor;
            n *= n_BremsFaktor;
        }
        public double Drehzahl()
        {
            return n;
        }

        public double Spannung(double strom)
        {
            return n * Magnetisierung.Magnetisierungskennlinie(strom) * SpannungsFaktor;
        }

        public double Frequenz()
        {
            return n * DrehzahlFaktor;
        }
    }
}
