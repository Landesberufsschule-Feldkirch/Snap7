using System;
using Xunit;

namespace Hydraulik.Tests
{
    public class Thermostat
    {
        [Theory]
        [InlineData(50, 60)]
        [InlineData(-50, 60)]
        public void KonstruktorOk(double min, double max)
        {
            var thermostat = new Hydraulik.Thermostat(min, max);

            Assert.Equal(min, thermostat.GetTemperaturMin());
            Assert.Equal(max, thermostat.GetTemperaturMax());
        }


        [Theory]
        [InlineData(60, 50)]
        [InlineData(60, 60)]
        public void KonstruktorFails(double min, double max)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Hydraulik.Thermostat(min, max));
        }

        [Theory]
        [InlineData(false, 50, 60, 0, 0)]
        [InlineData(true, 50, 60, 0, 70)]
        [InlineData(false, 50, 60, 70, 40)]
        public void HystereseTesten(bool exp, double min, double max, double temp1, double temp2)
        {
            var thermostat = new Hydraulik.Thermostat(min, max);

            thermostat.SetTemperatur(temp1);
            thermostat.SetTemperatur(temp2);

            Assert.Equal(exp, thermostat.GetTemperaturErreicht());
        }
    }
}