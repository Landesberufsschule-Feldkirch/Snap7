using System;

namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    class StateHochfahren
    {
        private readonly Kraftwerk kraftWerk;

        public StateHochfahren(Kraftwerk kw)
        {
            kraftWerk = kw;
        }

        public void OnEntry()
        {
            // nichts zu tun
        }

        public void Doing()
        {
            kraftWerk.generator.MaschineAntreiben(kraftWerk.Ventil_Y);
            kraftWerk.Generator_n = kraftWerk.generator.Drehzahl();
            kraftWerk.Generator_U = kraftWerk.generator.Spannung(kraftWerk.Generator_Ie);
            kraftWerk.Generator_f = kraftWerk.generator.Frequenz();
            kraftWerk.Generator_P = 0;
            kraftWerk.Generator_Phasenverschiebung = 0;

            kraftWerk.ViAnzeige.MessgeraetAnzeigen(Math.Abs(kraftWerk.FrequenzDifferenz) < 2);
        }

        public void OnExit()
        {
            // nichts zu tun
        }
    }
}