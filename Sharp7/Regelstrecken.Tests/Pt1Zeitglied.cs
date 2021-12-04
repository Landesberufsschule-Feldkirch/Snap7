using Xunit;

namespace Regelstrecken.Tests;

public class Pt1Tests
{
    [Theory]
    [InlineData(1, 1, 0, 100, 0)]

    public void TestKonstruktor(int zeitKonstanteMs, double faktor, double limitMin, double limitMax, double aktuellerWert)
    {
        var pt1 = new Regelstrecken.Pt1Zeitglied(zeitKonstanteMs, faktor, limitMin, limitMax);
        Assert.Equal(aktuellerWert, pt1.GetAktuellerWert());
    }
}