﻿using Xunit;

namespace Utilities.Tests
{
    public class Rechteck
    {
        [Theory]
        [InlineData(true, 0, 0, 1, 1, 0, 0, 1, 1)]
        [InlineData(true, 1, 1, 1, 1, 1, 1, 1, 1)]
        [InlineData(false, 0, 0, 2, 2, 2, 2, 2, 2)]
        [InlineData(false, 0, 0, 2, 2, 3, 3, 2, 2)]
        public void Test(bool exp, double r1X, double r1Y, double r1B, double r1H, double r2X, double r2Y, double r2B, double r2H)
        {
            var r1 = new Utilities.Rechteck(new Punkt(r1X, r1Y), r1B, r1H);
            var r2 = new Utilities.Rechteck(new Punkt(r2X, r2Y), r2B, r2H);

            Assert.Equal(exp, Utilities.Rechteck.Kollision(r1, r2));
        }
    }
}