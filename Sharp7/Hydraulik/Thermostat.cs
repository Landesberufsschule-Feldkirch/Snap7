namespace Hydraulik
{
    public class Thermostat
    {
        private bool _temperaturErreicht;
        private readonly double _temperaturMin;
        private readonly double _temperaturMax;

        public Thermostat(double min, double max)
        {
            _temperaturErreicht = false;
            _temperaturMin = min;
            _temperaturMax = max;
        }

        public void SetTemperatur(double temp)
        {
            if (temp < _temperaturMin) _temperaturErreicht = false;
            if (temp > _temperaturMax) _temperaturErreicht = true;
        }

        public bool GetTemperaturErreicht() => _temperaturErreicht;
    }
}