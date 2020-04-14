namespace Synchronisiereinrichtung.kraftwerk.Model
{
    public class Drehstromgenerator
    {

        public double SynchVentil { get; set; }
        public double SynchErregerstrom { get; set; }
        public Utilities.Rampen VentilRampe { get; set; }
        public Utilities.Rampen ErregerstromRampe { get; set; }

        private readonly MagnetischerKreis Magnetisierung = new MagnetischerKreis(0.5);

        private double n;
        private double P;
        private double cosPhi;
        private readonly double SpannungsFaktor;
        private readonly double DrehzahlFaktor;

        private const double Y_n_Faktor = 0.28;
        private const double n_BremsFaktor = 0.991;

        private readonly double Y_LeistungsFaktor = 15;
        private readonly double Y_LeistungsBremse = 0.9;

        private double LeistungsfaktorFaktor = 1;

        public Drehstromgenerator(double spannungsFaktor, double drehzahlFaktor)
        {
            SpannungsFaktor = spannungsFaktor;
            DrehzahlFaktor = drehzahlFaktor;

            VentilRampe = new Utilities.Rampen(0, 100, 0.05);
            ErregerstromRampe = new Utilities.Rampen(0, 10, 0.01);
        }

        internal void Reset()
        {
            n = 0;
            P = 0;
            LeistungsfaktorFaktor = 1;
            SynchErregerstrom = 0;
            SynchVentil = 0;
        }

        public void MaschineAntreiben(double Y)
        {
            n += Y * Y_n_Faktor;
            n *= n_BremsFaktor;
        }

        public double Drehzahl() => n;
        public double Leistung() => P;
        public double CosPhi() => cosPhi;
        public double Frequenz() => n * DrehzahlFaktor;
        public double Spannung(double strom) => n * Magnetisierung.Magnetisierungskennlinie(strom) * SpannungsFaktor;

        public void SynchronisiertFrequenz(double frequenz) { n = 30 * frequenz; }
        public void SynchronisierungVentil(double Y) { SynchVentil = Y; }
        public void SynchronisierungErregerstrom(double Ie) { SynchErregerstrom = Ie; }

        public void MaschineLeistungFahren(double Y)
        {
            if (Y < SynchVentil)
            {
                P = 0;
            }
            else
            {
                P += Y_LeistungsFaktor * (Y - SynchVentil);
                P *= Y_LeistungsBremse;
            }
        }

        public void PhasenSchieberbetrieb(double Ie)
        {
            if (Ie == SynchErregerstrom)
            {
                cosPhi = 1;
            }
            else
            {
                if (Ie > SynchErregerstrom)
                {
                    // übererregt -> kapazitiv
                    cosPhi = 90 - LeistungsfaktorFaktor * (Ie - SynchErregerstrom);

                }
                else
                {
                    // untererregt ->induktiv
                }
            }
        }
    }
}