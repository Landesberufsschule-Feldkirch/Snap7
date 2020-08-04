﻿namespace Synchronisiereinrichtung.kraftwerk.Model
{
    using System;

    public class MagnetischerKreis
    {
        private readonly double _kennlinie;

        public MagnetischerKreis(double kennlinie) => this._kennlinie = kennlinie;

        public double Magnetisierungskennlinie(double strom) => 1 - Math.Exp(-_kennlinie * strom);
    }
}