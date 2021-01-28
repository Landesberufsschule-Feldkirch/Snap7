using Xunit;

namespace PlcDatenTypen.Tests
{
    public class Uint
    {
        [Theory]
        [InlineData("12345", 12345)]
        [InlineData("2#0001_0010_0011_0100", 4660)]
        [InlineData("8#12345", 5349)]
        [InlineData("16#1234", 4660)]

        public void EinlesenTest(string zahl, uint ergebnis)
        {
            var plc = new PlcDatenTypen.Uint(zahl);
            Assert.Equal(ergebnis, plc.GetDec());
        }

        
        [Theory]
        [InlineData("1", 0)]
        [InlineData("4", 2)]

        public void Bin_Test(string zahl, int position)
        {
            var plc = new PlcDatenTypen.Uint(zahl);
            Assert.True(plc.GetBitGesetzt(position));
        }
        
        [Theory]
        [InlineData("256", "uuups - zu große Zahl!")]
        [InlineData("1", "2#0000_0001")]
        [InlineData("105", "2#0110_1001")]

        public void Bin_8Bit_Test(string zahl, string ergebnis)
        {
            var plc = new PlcDatenTypen.Uint(zahl);
            Assert.Equal(ergebnis, plc.GetBin8Bit());
        }

        [Theory]
        [InlineData("65536", "uuups - zu große Zahl!")]
        [InlineData("2", "2#0000_0000_0000_0010")]
        [InlineData("26985", "2#0110_1001_0110_1001")]

        public void Bin_16Bit_Test(string zahl, string ergebnis)
        {
            var plc = new PlcDatenTypen.Uint(zahl);
            Assert.Equal(ergebnis, plc.GetBin16Bit());
        }

        [Theory]
        [InlineData("4294967296", "uuups - zu große Zahl!")]
        [InlineData("3", "2#0000_0000_0000_0000_0000_0000_0000_0011")]
        [InlineData("4660", "2#0000_0000_0000_0000_0001_0010_0011_0100")]

        public void Bin_32Bit_Test(string zahl, string ergebnis)
        {
            var plc = new PlcDatenTypen.Uint(zahl);
            Assert.Equal(ergebnis, plc.GetBin32Bit());
        }



    }
}