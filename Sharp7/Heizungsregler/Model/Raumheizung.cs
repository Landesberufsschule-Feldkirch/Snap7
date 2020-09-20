using System;
using Hydraulik;

namespace Heizungsregler.Model
{
    public class Raumheizung
    {
        private readonly Heizkurve _heizkurve;
        private double _vorlaufSolltemperatur;
        private Betriebsarten _betriebsart = Betriebsarten.Aus;
        private double _witterungsTemperatur;
        private const double _raumTemperatur = 20;
        private const double _nachtAbsenkung = 10;
        private const double Neigung = 1.6;
        private const double Niveau = 10;

        private const double VorlaufMindestTemperatur = 10;
        private const double VorlaufMaximalTemperatur = 60;

        public Raumheizung() => _heizkurve = new Heizkurve(Neigung, Niveau, VorlaufMindestTemperatur, VorlaufMaximalTemperatur);

        public void RaumheizungAktualisieren()
        {
            switch (_betriebsart)
            {
                case Betriebsarten.Aus:
                    _vorlaufSolltemperatur = 0;
                    break;
                case Betriebsarten.Tag: 
                    _vorlaufSolltemperatur = _heizkurve.GetVorlaufSollTemperatur(_raumTemperatur, _witterungsTemperatur);
                    break;
                case Betriebsarten.Nacht:
                    _vorlaufSolltemperatur = _heizkurve.GetVorlaufSollTemperatur(_raumTemperatur - _nachtAbsenkung, _witterungsTemperatur);
                    break;
                case Betriebsarten.Hand:
                    _vorlaufSolltemperatur = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal double GetVorlaufSolltemperatur() => _vorlaufSolltemperatur;
        public void SetBetriebsart(Betriebsarten betriebsart) => _betriebsart = betriebsart;
        public void SetWitterungsTemperatur(in double witterungsTemperatur) =>
            _witterungsTemperatur = witterungsTemperatur;
    }
}