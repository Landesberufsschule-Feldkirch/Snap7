using Synchronisiereinrichtung.Model;
using Xunit;

namespace SynchronisiereinrichtungTests
{
    public class LogikFunktionen
    {
        [Theory]
        [InlineData(0, 0, 10, 0)]
        [InlineData(0, 100, 100, 0)]
        [InlineData(0, 100, 10, 0)]
        [InlineData(90, 25, 10, 0)]
        [InlineData(90, 125, 10, 0)]
        [InlineData(180, 50, 10, 0)]
        [InlineData(180, 500, 1, 0)]
        [InlineData(270, 75, 10, 0)]
        public void WinkelBerechnen(int exp, int time, double freq, int winkel)
        {
            var res = DrehstromZeiger.WinkelBerechnen(time, freq, winkel);
            res %= 360; // Ergebnis ist z.T. 360
            Assert.Equal(exp, res);
        }

        [Theory]
        [InlineData(230.94, 0, 0, 400)]
        [InlineData(0, 230.94, 90, 400)]
        [InlineData(-230.94, 0, 180, 400)]
        [InlineData(0, -230.94, 270, 400)]
        public void SpannungBerechnen(float exp_x, float exp_y, int winkel, double spannung)
        {
            var res = DrehstromZeiger.GetSpannung(winkel, spannung);

            Assert.Equal(exp_x, res.X, 3);
            Assert.Equal(exp_y, res.Y, 3);
        }
    }
}