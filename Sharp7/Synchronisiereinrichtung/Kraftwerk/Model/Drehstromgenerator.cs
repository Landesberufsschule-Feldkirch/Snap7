namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    public class Drehstromgenerator
    {
        private readonly MagnetischerKreis Magnetisierung = new MagnetischerKreis(0.5);

        private double n;
        private readonly double SpannungsFaktor;
        private readonly double DrehzahlFaktor;

        private const double Y_n_Faktor = 0.28;
        private const double n_BremsFaktor = 0.991;

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