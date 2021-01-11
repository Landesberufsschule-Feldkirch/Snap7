using Xunit;

namespace PlcDatenTypen.Tests
{
    public class ZeitDauer
    {
        [Theory]
        [InlineData("1234567890", 1234567890)]
        [InlineData("T#1d2h3m4s567ms", 93784567)]
        [InlineData("T#123ms", 123)]
        

        public void Test(string zahl, long ergebnis)
        {
            var zeitMs = new PlcZeitDauer(zahl);
            Assert.Equal(ergebnis, zeitMs.GetZeitDauerMs());
        }
    }
}