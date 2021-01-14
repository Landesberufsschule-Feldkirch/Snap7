using Xunit;

namespace Utilities.Tests
{
    public class S7Analog
    {

        [Theory]
        [InlineData(0, 100, 0)]
        [InlineData(10, 100, 2765)]
        [InlineData(-10, 100, -2765)]
        [InlineData(1000, 100, 27648)]
        [InlineData(-1000, 100, -27648)]

        public void Analog2Int16Test(double analog, double scale, int siemens)
        {
            Assert.Equal(siemens, Utilities.S7Analog.S7_Analog_2_Int16(analog, scale));
        }

        [Theory]
        [InlineData(0, 100, 0)]
        [InlineData(10, 100, 2765)]
        [InlineData(-10, 100, -2765)]
        [InlineData(1000, 100, 27648)]
        [InlineData(-1000, 100, -27648)]

        public void Analog2Int32Test(double analog, double scale, int siemens)
        {
            Assert.Equal(siemens, Utilities.S7Analog.S7_Analog_2_Int32(analog, scale));
        }

        [Theory]
        [InlineData(0, 100, 0)]
        [InlineData(27648, 100, 100)]
        [InlineData(-27648, 100, -100)]

        public void Analog2DoubleTest(int analog, double scale, int siemens)
        {
            Assert.Equal(siemens, Utilities.S7Analog.S7_Analog_2_Double(analog, scale), 3);
        }

        [Theory]
        [InlineData(0, -100, 100, 0)]
        [InlineData(100, -100, 100, 100)]
        [InlineData(-100, -100, 100, -100)]
        [InlineData(200, -100, 100, 100)]
        [InlineData(-200, -100, 100, -100)]

        public void ClampTest(int wert, double min, double max, double exp)
        {
            Assert.Equal(exp, Utilities.S7Analog.Clamp(wert, min, max), 3);
        }
    }
}