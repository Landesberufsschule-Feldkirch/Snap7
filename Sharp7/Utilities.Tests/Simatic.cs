using Xunit;

namespace Utilities.Tests
{
    public class Simatic
    {

        [Theory]
        [InlineData(0, 100, 0)]
        [InlineData(10, 100, 2765)]
        [InlineData(-10, 100, -2765)]
        [InlineData(1000, 100, 27648)]
        [InlineData(-1000, 100, -27648)]

        public void Analog2Int16Test(double analog, double scale, int siemens)
        {
            Assert.Equal(siemens, Utilities.Simatic.Simatic_Analog_2_Int16(analog, scale));
        }

        [Theory]
        [InlineData(0, 100, 0)]
        [InlineData(10, 100, 2765)]
        [InlineData(-10, 100, -2765)]
        [InlineData(1000, 100, 27648)]
        [InlineData(-1000, 100, -27648)]

        public void Analog2Int32Test(double analog, double scale, int siemens)
        {
            Assert.Equal(siemens, Utilities.Simatic.Simatic_Analog_2_Int32(analog, scale));
        }

        [Theory]
        [InlineData(0, 100, 0)]
        [InlineData(27648, 100, 100)]
        [InlineData(-27648, 100, -100)]

        public void Analog2DoubleTest(int analog, double scale, int siemens)
        {
            Assert.Equal(siemens, Utilities.Simatic.Simatic_Analog_2_Double(analog, scale), 3);
        }

        [Theory]
        [InlineData(0, -100, 100, 0)]
        [InlineData(100, -100, 100, 100)]
        [InlineData(-100, -100, 100, -100)]
        [InlineData(200, -100, 100, 100)]
        [InlineData(-200, -100, 100, -100)]

        public void ClampTest(int wert, double min, double max, double exp)
        {
            Assert.Equal(exp, Utilities.Simatic.Clamp(wert, min, max), 3);
        }





        // ACHTUNG: Siemens verwendet Big Endian!!

        [Theory]
        [InlineData(0,  0)]
        [InlineData(256, 1)]
        [InlineData(3840, 15)]
        [InlineData(65535, 255)]

        public void GetLowByteTest(uint wert,byte exp)
        {
            Assert.Equal(exp, Utilities.Simatic.Simatic_Digital_GetLowByte(wert));
        }


        [Theory]
        [InlineData(0, 0)]
        [InlineData(256, 0)]
        [InlineData(3840, 0)]
        [InlineData(65535, 255)]

        public void GetHighByteTest(uint wert, byte exp)
        {
            Assert.Equal(exp, Utilities.Simatic.Simatic_Digital_GetHighByte(wert));
        }


        [Theory]
        [InlineData(0, 0,0)]
        [InlineData(1, 0, 256)]
        [InlineData(0, 1, 1)]

        public void GetCombineByteTest(byte low, byte high, uint exp)
        {
            Assert.Equal(exp, Utilities.Simatic.Simatic_Digital_CombineTwoByte(low, high));
        }

    }
}