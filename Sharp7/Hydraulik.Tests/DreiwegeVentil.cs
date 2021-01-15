using Xunit;

namespace Hydraulik.Tests
{
    public class DreiwegeVentil
    {

        [Theory]
        [InlineData(0, 100, 10)]
        [InlineData(-100, 100, 10)]
        public void KonstruktorTesten(double min, double max, double laufzeit)
        {
            var ventil = new Hydraulik.DreiwegeVentil(min, max, laufzeit);
            Assert.Equal(0, ventil.GetPosition());
        }
    }
}