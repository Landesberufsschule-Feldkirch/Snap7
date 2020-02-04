﻿namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    class StateMaschineTot
    {
        private readonly Kraftwerk kraftWerk;

        public StateMaschineTot(Kraftwerk kw)
        {
            kraftWerk = kw;
        }

        public void OnEntry()
        {
            kraftWerk.ViAnzeige.MaschineTot(true);
        }

        public void Doing()
        {
            // wird über den "Reset" Knopf weitergeschaltet
        }

        public void OnExit()
        {
            // nichts zu tun
        }
    }
}