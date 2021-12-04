using Xunit;

namespace Utilities.Tests;

public class Rechteck
{

    [Theory]
    [InlineData(0, 0, 1, 1, 0, 1, 0, 1)]
    [InlineData(1, 1, 10, 10, 1, 11, 1, 11)]

    public void TestKonstruktor(double links, double oben, double breite, double hoehe, double xl, double xr, double yo, double yu)
    {
        var punkt = new Utilities.Punkt(links, oben);
        var rechteck = new Utilities.Rechteck(new Utilities.Punkt(links, oben), breite, hoehe);

        Assert.Equal(xl, rechteck.GetLinks());
        Assert.Equal(xr, rechteck.GetRechts());
        Assert.Equal(yo, rechteck.GetOben());
        Assert.Equal(yu, rechteck.GetUnten());
        Assert.Equal(punkt.X, rechteck.GetPosition().X);
        Assert.Equal(punkt.Y, rechteck.GetPosition().Y);
    }


    [Theory]
    [InlineData(0, 0, 1, 1, 10, 10)]

    public void TestSetPosition(double links, double oben, double breite, double hoehe, double x1, double y1)
    {
        var punkt = new Utilities.Punkt(x1, y1);
        var rechteck = new Utilities.Rechteck(new Utilities.Punkt(links, oben), breite, hoehe);

        Assert.Equal(links, rechteck.GetLinks());
        Assert.Equal(oben, rechteck.GetOben());

        rechteck.SetPosition(punkt);

        Assert.Equal(x1, rechteck.GetLinks());
        Assert.Equal(y1, rechteck.GetOben());
    }



    [Theory]
    [InlineData(0, 0, 1, 1, 10)]
    [InlineData(0, 0, 1, 1, -10)]

    public void TestSetWaagrecht(double links, double oben, double breite, double hoehe, double x)
    {
        var rechteck = new Utilities.Rechteck(new Utilities.Punkt(links, oben), breite, hoehe);

        Assert.Equal(links, rechteck.GetLinks());
        Assert.Equal(oben, rechteck.GetOben());

        rechteck.SetWaagrechtSchieben(x);

        Assert.Equal(links + x, rechteck.GetLinks());
        Assert.Equal(oben, rechteck.GetOben());
    }


    [Theory]
    [InlineData(0, 0, 1, 1, 10)]
    [InlineData(0, 0, 1, 1, -10)]

    public void TestSetSenkrecht(double links, double oben, double breite, double hoehe, double y)
    {
        var rechteck = new Utilities.Rechteck(new Utilities.Punkt(links, oben), breite, hoehe);

        Assert.Equal(links, rechteck.GetLinks());
        Assert.Equal(oben, rechteck.GetOben());

        rechteck.SetSenkrechtSchieben(y);

        Assert.Equal(links, rechteck.GetLinks());
        Assert.Equal(oben + y, rechteck.GetOben());
    }


    [Theory]
    [InlineData(true, 0, 0, 1, 1, 0, 0, 1, 1)]
    [InlineData(true, 1, 1, 1, 1, 1, 1, 1, 1)]
    [InlineData(false, 0, 0, 2, 2, 2, 2, 2, 2)]
    [InlineData(false, 0, 0, 2, 2, 3, 3, 2, 2)]

    public void TestKollision(bool exp, double r1X, double r1Y, double r1B, double r1H, double r2X, double r2Y, double r2B, double r2H)
    {
        var r1 = new Utilities.Rechteck(new Utilities.Punkt(r1X, r1Y), r1B, r1H);
        var r2 = new Utilities.Rechteck(new Utilities.Punkt(r2X, r2Y), r2B, r2H);

        Assert.Equal(exp, Utilities.Rechteck.Kollision(r1, r2));
    }

}