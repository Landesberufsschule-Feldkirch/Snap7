using Xunit;

namespace Hydraulik.Tests
{
    public class Heizkurve
    {
        [Theory]
        [InlineData(0, 0, 0, 100, 20, 0, 20)]
        [InlineData(0, 20, 0, 100, 20, 0, 40)]
        [InlineData(1, 20, 0, 100, 20, 0, 62.28)]

        public void Test(double neigung, double niveau, double vorlaufMin, double vorlaufMax, double raumtempteratur, double witterungstemperatur, double vorlaufSolltemperatur)
        {
            var heizkurve = new Hydraulik.Heizkurve(neigung, niveau, vorlaufMin, vorlaufMax);

            Assert.Equal(vorlaufSolltemperatur, heizkurve.GetVorlaufSollTemperatur(raumtempteratur, witterungstemperatur), 1);
        }
    }
}