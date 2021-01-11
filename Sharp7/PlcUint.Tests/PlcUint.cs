using Xunit;


namespace PlcDatenTypen.Tests
{
    public class PlcUint
    {
        [Theory]
        [InlineData("12345", 12345)]
        [InlineData("2#0001_0010_0011_0100", 4660)]
        [InlineData("8#12345", 5349)]
        [InlineData("16#1234", 4660)]

        public void EinlesenTest(string zahl, uint ergebnis)
        {
            var plc = new PlcDatenTypen.PlcUint(zahl);
            Assert.Equal(ergebnis, plc.GetDec());
        }

        [Theory]
        [InlineData("1", "2#0000_0000_0000_0001")]
        [InlineData("4660", "2#0001_0010_0011_0100")]
        
        public void BinTest(string zahl, string ergebnis)
        {
            var plc = new PlcDatenTypen.PlcUint(zahl);
            Assert.Equal(ergebnis, plc.GetBinFormatiert());
        }

    }
}