using Xunit;

namespace Hydraulik.Tests
{
    public class Thermostat
    {
        [Theory]
        [InlineData(false, 50, 60, 0, 0)]
        [InlineData(true, 50, 60, 0, 70)]
        [InlineData(false, 50, 60, 70, 40)]
        public void Test(bool exp, double min, double max, double temp1, double temp2)
        {
            var thermostat = new Hydraulik.Thermostat(min, max);

            thermostat.SetTemperatur(temp1);
            thermostat.SetTemperatur(temp2);

            Assert.Equal(exp, thermostat.GetTemperaturErreicht());
        }
    }
}