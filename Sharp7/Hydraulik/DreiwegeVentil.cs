using System.Threading;

namespace Hydraulik
{
    public class DreiwegeVentil
    {

        private double _positionProzent;
        private readonly double _posMinProzent;
        private readonly double _posMaxProzent;
        private readonly double _deltaMillisekunden;
        private const int Schrittweite = 10;

        private bool _ventilOeffnen;
        private bool _ventilSchliessen;

        public DreiwegeVentil(double minPosProzent, double maxPosProzent, double laufzeitSekunden)
        {
            _positionProzent = 0;
            _posMinProzent = minPosProzent;
            _posMaxProzent = maxPosProzent;
            _deltaMillisekunden = (_posMaxProzent - _posMinProzent) * laufzeitSekunden / (1000 * Schrittweite);

            System.Threading.Tasks.Task.Run(DreiwegeVentilTask);
        }

        private void DreiwegeVentilTask()
        {
            while (true)
            {
                if (_ventilOeffnen)
                {
                    _positionProzent += _deltaMillisekunden;
                }
                else
                {
                    if (_ventilSchliessen)
                    {
                        _positionProzent -= _deltaMillisekunden;
                    }
                }


                VentilPositionLimitieren();

                Thread.Sleep(Schrittweite);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        public void VentilOeffnen(bool wert) => _ventilOeffnen = wert;
        public void VentilSchliessen(bool wert) => _ventilSchliessen = wert;
        public double GetPosition() => _positionProzent;

        private void VentilPositionLimitieren()
        {
            if (_positionProzent > _posMaxProzent) _positionProzent = _posMaxProzent;
            if (_positionProzent < _posMinProzent) _positionProzent = _posMinProzent;
        }
    }
}