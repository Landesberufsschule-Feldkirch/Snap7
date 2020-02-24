namespace Synchronisiereinrichtung.kraftwerk.Model
{
    public class Drehstromgenerator
    {
        private readonly MagnetischerKreis Magnetisierung = new MagnetischerKreis(0.5);

        private double n;
        private double P;
        private double cosPhi;
        private readonly double SpannungsFaktor;
        private readonly double DrehzahlFaktor;

        public double SynchVentil;
        public double SynchErregerstrom;

        private const double Y_n_Faktor = 0.28;
        private const double n_BremsFaktor = 0.991;

        private double Y_LeistungsFaktor = 15;
        private double Y_LeistungsBremse = 0.9;

        private double LeistungsfaktorFaktor = 1;

        public Drehstromgenerator(double spannungsFaktor, double drehzahlFaktor)
        {
            SpannungsFaktor = spannungsFaktor;
            DrehzahlFaktor = drehzahlFaktor;
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

        public double Drehzahl()
        {
            return n;
        }

        public void SynchronisiertFrequenz(double frequenz)
        {
            n = 30 * frequenz;
        }

        public double Spannung(double strom)
        {
            return n * Magnetisierung.Magnetisierungskennlinie(strom) * SpannungsFaktor;
        }

        public double Frequenz()
        {
            return n * DrehzahlFaktor;
        }

        public void SynchronisierungVentil(double Y)
        {
            SynchVentil = Y;
        }
        public void SynchronisierungErregerstrom(double Ie)
        {
            SynchErregerstrom = Ie;
        }

        public double Leistung()
        {
            return P;
        }
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

        public double CosPhi()
        {
            return cosPhi;
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