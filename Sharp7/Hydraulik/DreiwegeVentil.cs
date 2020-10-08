using System;
using System.Threading;

namespace Hydraulik
{
    public class DreiwegeVentil
    {
        public enum Richtung
        {
            Stop,
            Oeffnen,
            Schliessen
        }

        private Richtung _richtung;
        private double _positionProzent;
        private readonly double _posMinProzent;
        private readonly double _posMaxProzent;
        private readonly double _deltaMillisekunden;
        private const int Schrittweite = 10;

        public DreiwegeVentil(double minPosProzent, double maxPosProzent, double laufzeitSekunden)
        {
            _positionProzent = minPosProzent;
            _posMinProzent = minPosProzent;
            _posMaxProzent = maxPosProzent;
            _deltaMillisekunden = (_posMaxProzent - _posMinProzent) * laufzeitSekunden / 1000 * Schrittweite;

            System.Threading.Tasks.Task.Run(DreiwegeVentilTask);
        }

        private void DreiwegeVentilTask()
        {
            while (true)
            {
                switch (_richtung)
                {
                    case Richtung.Oeffnen:
                        _positionProzent += _deltaMillisekunden;
                        break;

                    case Richtung.Schliessen:
                        _positionProzent -= _deltaMillisekunden;
                        break;
                    case Richtung.Stop:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(_richtung));
                }

                LimitsTesten();

                Thread.Sleep(Schrittweite);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        public void SetNeuePosition(Richtung ri, double dauer)
        {
            switch (ri)
            {
                case Richtung.Oeffnen:
                    _positionProzent += _deltaMillisekunden * dauer;
                    break;

                case Richtung.Schliessen:
                    _positionProzent -= _deltaMillisekunden * dauer;
                    break;
                case Richtung.Stop:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ri), ri, null);
            }

            LimitsTesten();
        }

        public void SetRichtung(Richtung ri) => _richtung = ri;

        public double GetPosition() => _positionProzent;

        private void LimitsTesten()
        {
            if (_positionProzent > _posMaxProzent) _positionProzent = _posMaxProzent;
            if (_positionProzent < _posMinProzent) _positionProzent = _posMinProzent;
        }
    }
}