namespace Hydraulik
{
    public class Thermostat
    {
        private bool TemperaturErreicht;
        private readonly double TemperaturMin;
        private readonly double TemperaturMax;

        public Thermostat(double min, double max)
        {
            TemperaturErreicht = false;
            TemperaturMin = min;
            TemperaturMax = max;
        }

        public void SetTemperatur(double temp)
        {
            if (temp < TemperaturMin) TemperaturErreicht = false;
            if (temp > TemperaturMax) TemperaturErreicht = true;
        }

        public bool GetTemperaturErreicht() => TemperaturErreicht;
    }
}