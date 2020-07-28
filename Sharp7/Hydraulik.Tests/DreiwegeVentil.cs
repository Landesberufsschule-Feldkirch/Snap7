using Xunit;

namespace Hydraulik.Tests
{
    public class DreiwegeVentil
    {
        [Theory]
        [InlineData(0, 0, 100, 10, Hydraulik.DreiwegeVentil.Richtung.Stop, 0)]
        [InlineData(-100, -100, 100, 10, Hydraulik.DreiwegeVentil.Richtung.Stop, 0)]
        [InlineData(50, 0, 100, 10, Hydraulik.DreiwegeVentil.Richtung.Oeffnen, 5)]
        [InlineData(100, 0, 100, 10, Hydraulik.DreiwegeVentil.Richtung.Oeffnen, 10)]
        public void Test(double exp, double min, double max, double laufzeit, Hydraulik.DreiwegeVentil.Richtung richtung, double dauer)
        {
            var dreiwegeVentil = new Hydraulik.DreiwegeVentil(min, max, laufzeit);
            dreiwegeVentil.SetNeuePosition(richtung, dauer);
            Assert.Equal(exp, dreiwegeVentil.GetPosition());
        }
    }
}