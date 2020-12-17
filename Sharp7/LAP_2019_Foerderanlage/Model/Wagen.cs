using System;

namespace LAP_2019_Foerderanlage.Model
{
    using Utilities;

    public class Wagen
    {
        public enum Richtung
        {
            Stehen = 0,
            NachLinks,
            NachRechts
        }

        private bool _endlageRechts;
        private bool _wagenVoll;

        private Richtung _wagenRichtung;
        private readonly Punkt _aktuellePosition;
        private double _wagenFuellstand;

        private const double WagenGeschwindigkeit = 0.3;

        private const double WagenFuellstandLeeren = 0.5;
        private const double WagenFuellstandFuellen = 0.1;
        private const double WagenFuellstandVoll = 88;

        private readonly Punkt _linkerAnschlag = new(0, 0);
        private readonly Punkt _rechterAnschlag = new(125, 0);

        public Wagen()
        {
            _wagenVoll = false;
            _endlageRechts = false;
            _wagenRichtung = Richtung.Stehen;
            _wagenFuellstand = 0;

            _aktuellePosition = new Punkt(0, 0);
        }

        public void WagenTask()
        {
            switch (_wagenRichtung)
            {
                case Richtung.NachLinks:
                    if (_aktuellePosition.X > _linkerAnschlag.X) _aktuellePosition.X -= WagenGeschwindigkeit;
                    if (_aktuellePosition.X <= _linkerAnschlag.X)
                    {
                        _aktuellePosition.X = _linkerAnschlag.X;
                        _wagenRichtung = Richtung.Stehen;
                    }
                    break;

                case Richtung.NachRechts:
                    if (_aktuellePosition.X < _rechterAnschlag.X) _aktuellePosition.X += WagenGeschwindigkeit;
                    if (_aktuellePosition.X >= _rechterAnschlag.X)
                    {
                        _aktuellePosition.X = _rechterAnschlag.X;
                        _wagenRichtung = Richtung.Stehen;
                    }
                    break;
                case Richtung.Stehen:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Wagen bewegen
            _endlageRechts = Math.Abs(_aktuellePosition.X - _rechterAnschlag.X) < 0.01;
            _wagenVoll = Math.Abs(_wagenFuellstand - WagenFuellstandVoll) < 0.01;
            if (Math.Abs(_aktuellePosition.X - _linkerAnschlag.X) < 0.01 && _wagenFuellstand > 0) _wagenFuellstand -= WagenFuellstandLeeren;
        }

        internal void Fuellen()
        {
            _wagenFuellstand += WagenFuellstandFuellen;
            if (_wagenFuellstand > WagenFuellstandVoll) _wagenFuellstand = WagenFuellstandVoll;
        }

        internal void NachRechts()
        {
            _wagenRichtung = Richtung.NachRechts;
        }

        internal void NachLinks()
        {
            _wagenRichtung = Richtung.NachLinks;
        }

        public bool IstWagenVoll() => _wagenVoll;

        public bool IstWagenRechts() => _endlageRechts;

        internal Punkt GetPosition() => _aktuellePosition;

        internal double GetFuellstand() => _wagenFuellstand;
    }
}