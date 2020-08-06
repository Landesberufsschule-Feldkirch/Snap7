using System;

namespace LAP_2019_Foerderanlage.Model
{
    using Utilities;

    public class Wagen
    {
        public enum Richtung
        {
            stehen = 0,
            nachLinks,
            nachRechts
        }

        private bool endlageRechts;
        private bool wagenVoll;

        private Richtung wagenRichtung;
        private readonly Punkt aktuellePosition;
        private double wagenFuellstand;

        private const double wagenGeschwindigkeit = 0.3;

        private const double wagenFuellstandLeeren = 0.5;
        private const double wagenFuellstandFuellen = 0.1;
        private const double wagenFuellstandVoll = 88;

        private readonly Punkt linkerAnschlag = new Punkt(0, 0);
        private readonly Punkt rechterAnschlag = new Punkt(125, 0);

        public Wagen()
        {
            wagenVoll = false;
            endlageRechts = false;
            wagenRichtung = Richtung.stehen;
            wagenFuellstand = 0;

            aktuellePosition = new Punkt(0, 0);
        }

        public void WagenTask()
        {
            switch (wagenRichtung)
            {
                case Richtung.nachLinks:
                    if (aktuellePosition.X > linkerAnschlag.X) aktuellePosition.X -= wagenGeschwindigkeit;
                    if (aktuellePosition.X <= linkerAnschlag.X)
                    {
                        aktuellePosition.X = linkerAnschlag.X;
                        wagenRichtung = Richtung.stehen;
                    }
                    break;

                case Richtung.nachRechts:
                    if (aktuellePosition.X < rechterAnschlag.X) aktuellePosition.X += wagenGeschwindigkeit;
                    if (aktuellePosition.X >= rechterAnschlag.X)
                    {
                        aktuellePosition.X = rechterAnschlag.X;
                        wagenRichtung = Richtung.stehen;
                    }
                    break;
            }

            // Wagen bewegen
            endlageRechts = Math.Abs(aktuellePosition.X - rechterAnschlag.X) < 0.01;
            wagenVoll = Math.Abs(wagenFuellstand - wagenFuellstandVoll) < 0.01;
            if ((Math.Abs(aktuellePosition.X - linkerAnschlag.X) < 0.01) && (wagenFuellstand > 0)) wagenFuellstand -= wagenFuellstandLeeren;
        }

        internal void Fuellen()
        {
            wagenFuellstand += wagenFuellstandFuellen;
            if (wagenFuellstand > wagenFuellstandVoll) wagenFuellstand = wagenFuellstandVoll;
        }

        internal void NachRechts()
        {
            wagenRichtung = Richtung.nachRechts;
        }

        internal void NachLinks()
        {
            wagenRichtung = Richtung.nachLinks;
        }

        public bool IstWagenVoll() => wagenVoll;

        public bool IstWagenRechts() => endlageRechts;

        internal Punkt GetPosition() => aktuellePosition;

        internal double GetFuellstand() => wagenFuellstand;
    }
}