using Hydraulik;
using System;

namespace Heizungsregler.Model
{
    public class Raumheizung
    {
        private readonly Heizkurve _heizkurve;
        private double _vorlaufSolltemperatur;
        private Betriebsarten _betriebsart = Betriebsarten.Aus;
        private double _witterungsTemperatur;
        private const double RaumTemperatur = 20;
        private const double NachtAbsenkung = 10;
        private const double Neigung = 1.6;
        private const double Niveau = 10;

        private const double VorlaufMindestTemperatur = 10;
        private const double VorlaufMaximalTemperatur = 60;

        public Raumheizung() => _heizkurve = new Heizkurve(Neigung, Niveau, VorlaufMindestTemperatur, VorlaufMaximalTemperatur);

        public void RaumheizungAktualisieren()
        {
            _vorlaufSolltemperatur = _betriebsart switch
            {
                Betriebsarten.Aus => 0,
                Betriebsarten.Tag => _heizkurve.GetVorlaufSollTemperatur(RaumTemperatur, _witterungsTemperatur),
                Betriebsarten.Nacht => _heizkurve.GetVorlaufSollTemperatur(RaumTemperatur - NachtAbsenkung, _witterungsTemperatur),
                Betriebsarten.Hand => 0,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        internal double GetVorlaufSolltemperatur() => _vorlaufSolltemperatur;
        public void SetBetriebsart(Betriebsarten betriebsart) => _betriebsart = betriebsart;
        public void SetWitterungsTemperatur(in double witterungsTemperatur) =>
            _witterungsTemperatur = witterungsTemperatur;
    }
}