using System;
using Xunit;

namespace Utilities.Tests;

public class Winkel
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(180, Math.PI)]
    [InlineData(360, 2 * Math.PI)]

    public void WinkelDeg2RadTest(double deg, double rad)
    {
        Assert.Equal(rad, Utilities.Winkel.Deg2Rad(deg), 3);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(Math.PI, 180)]
    [InlineData(2 * Math.PI, 360)]

    public void WinkelRad2DegTest(double rad, double deg)
    {
        Assert.Equal(deg, Utilities.Winkel.Rad2Deg(rad), 3);
    }
}