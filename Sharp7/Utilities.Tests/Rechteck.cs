using Xunit;

namespace Utilities.Tests
{
    public class Rechteck
    {
        [Theory]
        [InlineData(true, 0, 0, 1, 1, 0, 0, 1, 1)]
        [InlineData(true, 1, 1, 1, 1, 1, 1, 1, 1)]
        [InlineData(false, 0, 0, 2, 2, 2, 2, 2, 2)]
        [InlineData(false, 0, 0, 2, 2, 3, 3, 2, 2)]

        public void Test(bool exp, double r1x, double r1y, double r1b, double r1h, double r2x, double r2y, double r2b, double r2h)
        {
            var r1 = new Utilities.Rechteck(new Utilities.Punkt(r1x, r1y), r1b, r1h);
            var r2 = new Utilities.Rechteck(new Utilities.Punkt(r2x, r2y), r2b, r2h);

            Assert.Equal(exp, Utilities.Rechteck.Kollision(r1, r2) );
        }

    }
}
