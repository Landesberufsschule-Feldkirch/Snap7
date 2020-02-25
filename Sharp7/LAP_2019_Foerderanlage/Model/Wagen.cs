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

        private bool EndlageRechts;
        private bool WagenVoll;

        private Richtung WagenRichtung;
        private readonly Punkt AktuellePosition;
        private double WagenFuellstand;

        private const double WagenGeschwindigkeit = 3;

        private const double WagenFuellstandLeeren = 5;
        private const double WagenFuellstandFuellen = 1;
        private const double WagenFuellstandVoll = 88;

        readonly Punkt LinkerAnschlag = new Punkt(0, 0);
        readonly Punkt RechterAnschlag = new Punkt(125, 0);

        public Wagen()
        {
            WagenVoll = false;
            EndlageRechts = false;
            WagenRichtung = Richtung.stehen;
            WagenFuellstand = 0;

            AktuellePosition = new Punkt(0, 0);
        }

        public void WagenTask()
        {
            switch (WagenRichtung)
            {
                case Richtung.nachLinks:
                    if (AktuellePosition.X > LinkerAnschlag.X) AktuellePosition.X -= WagenGeschwindigkeit;
                    if (AktuellePosition.X <= LinkerAnschlag.X)
                    {
                        AktuellePosition.X = LinkerAnschlag.X;
                        WagenRichtung = Richtung.stehen;
                    }
                    break;

                case Richtung.nachRechts:
                    if (AktuellePosition.X < RechterAnschlag.X) AktuellePosition.X += WagenGeschwindigkeit;
                    if (AktuellePosition.X >= RechterAnschlag.X)
                    {
                        AktuellePosition.X = RechterAnschlag.X;
                        WagenRichtung = Richtung.stehen;
                    }
                    break;

                case Richtung.stehen:
                default:
                    break;
            }

            // Wagen bewegen
            if (AktuellePosition.X == RechterAnschlag.X) EndlageRechts = true; else EndlageRechts = false;
            if (WagenFuellstand == WagenFuellstandVoll) WagenVoll = true; else WagenVoll = false;
            if ((AktuellePosition.X == LinkerAnschlag.X) && (WagenFuellstand > 0)) WagenFuellstand -= WagenFuellstandLeeren;
        }

        internal void NachRechts() { WagenRichtung = Richtung.nachRechts; }
        internal void NachLinks() { WagenRichtung = Richtung.nachLinks; }
        public bool IstWagenVoll() { return WagenVoll; }
        public bool IstWagenRechts() { return EndlageRechts; }
        internal Punkt GetPosition() { return AktuellePosition; }
        internal double GetFuellstand() { return WagenFuellstand; }
    }
}