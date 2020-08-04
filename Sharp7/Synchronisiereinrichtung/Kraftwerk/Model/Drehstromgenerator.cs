using System;

namespace Synchronisiereinrichtung.kraftwerk.Model
{
    public class Drehstromgenerator
    {
        public double SynchVentil { get; set; }
        public double SynchErregerstrom { get; set; }
        public Utilities.Rampen VentilRampe { get; set; }
        public Utilities.Rampen ErregerstromRampe { get; set; }

        private readonly MagnetischerKreis _magnetisierung = new MagnetischerKreis(0.5);

        private double _n;
        private double _p;
        private double _cosPhi;
        private readonly double _spannungsFaktor;
        private readonly double _drehzahlFaktor;

        private const double YnFaktor = 0.28;
        private const double NBremsFaktor = 0.991;

        private const double YLeistungsFaktor = 15;
        private readonly double _yLeistungsBremse = 0.9;

        private double _leistungsfaktorFaktor = 1;

        public Drehstromgenerator(double spannungsFaktor, double drehzahlFaktor)
        {
            _spannungsFaktor = spannungsFaktor;
            _drehzahlFaktor = drehzahlFaktor;

            VentilRampe = new Utilities.Rampen(0, 100, 0.05);
            ErregerstromRampe = new Utilities.Rampen(0, 10, 0.01);
        }

        internal void Reset()
        {
            _n = 0;
            _p = 0;
            _leistungsfaktorFaktor = 1;
            SynchErregerstrom = 0;
            SynchVentil = 0;
        }

        public void MaschineAntreiben(double y)
        {
            _n += y * YnFaktor;
            _n *= NBremsFaktor;
        }

        public double GetDrehzahl() => _n;

        public double GetLeistung() => _p;

        public double GetCosPhi() => _cosPhi;

        public double GetFrequenz() => _n * _drehzahlFaktor;

        public double GetSpannung(double strom) => _n * _magnetisierung.Magnetisierungskennlinie(strom) * _spannungsFaktor;

        public void SetSynchronisiertFrequenz(double frequenz) => _n = 30 * frequenz;

        public void SetSynchronisierungVentil(double y) => SynchVentil = y;

        public void SetSynchronisierungErregerstrom(double ie) => SynchErregerstrom = ie;

        public void MaschineLeistungFahren(double y)
        {
            if (y < SynchVentil)
            {
                _p = 0;
            }
            else
            {
                _p += YLeistungsFaktor * (y - SynchVentil);
                _p *= _yLeistungsBremse;
            }
        }

        public void PhasenSchieberbetrieb(double ie)
        {
            if (Math.Abs(ie - SynchErregerstrom) < 0.001)
            {
                _cosPhi = 1;
            }
            else
            {
                if (ie > SynchErregerstrom)
                {
                    // übererregt -> kapazitiv
                    _cosPhi = 90 - _leistungsfaktorFaktor * (ie - SynchErregerstrom);
                }
                else
                {
                    // untererregt ->induktiv
                }
            }
        }
    }
}