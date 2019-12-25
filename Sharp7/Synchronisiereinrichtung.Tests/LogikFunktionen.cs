using Xunit;

namespace SynchronisiereinrichtungTests
{
    public class LogikFunktionen
    {
        [Theory]
        [InlineData(0, 0, 10, 0)]
        [InlineData(180, 50, 10, 0)]
        [InlineData(90, 25, 10, 0)]
        [InlineData(270, 75, 10, 0)]
        [InlineData(0, 100, 100, 0)]
        [InlineData(180, 500, 1, 0)]
        [InlineData(90, 125, 10, 0)]
        [InlineData(0, 100, 10, 0)]
        public void WinkelBerechnen(int exp, int time, double freq, int winkel)
        {
            var res = Synchronisiereinrichtung.RestKlasse.WinkelBerechnen(time, freq, winkel);
            Assert.Equal(exp, res);
        }

        /*

        [Theory]
        [InlineData(0,0,400)]
        [InlineData(400,90,400)]
        [InlineData(0,180,400)]
        [InlineData(-400,270,400)]
        public void Spannung(double exp, int winkel, double spannung)
        {
            var res = Synchronisiereinrichtung.RestKlasse.GetSpannung(winkel, spannung);

            Assert.Equal(exp, res,3);
        }*/
    }
}