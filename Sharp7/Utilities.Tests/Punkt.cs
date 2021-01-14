using Xunit;

namespace Utilities.Tests
{
    public class Punkt
    {

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]

        public void ZeigerPunktXy(double x, double y)
        {
            var p = new Utilities.Punkt(x, y);

            Assert.Equal(x,p.X, 3);
            Assert.Equal(y, p.Y, 3);
        }

        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(14.1421356237, 45, 10, 10)]

        public void ZeigerPunktRad(double rad, double winkel, double x, double y)
        {
            var p = new Utilities.Punkt(rad, winkel, 0);

            Assert.Equal(x, p.X, 3);
            Assert.Equal(y, p.Y, 3);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]

        public void ZeigerPunktClone(double x, double y)
        {
            var p1 = new Utilities.Punkt(x, y);
            var p2 = p1.Clone();

            Assert.NotSame(p1, p2);

            Assert.Equal(x, p2.X, 3);
            Assert.Equal(y, p2.Y, 3);
        }
    }
}