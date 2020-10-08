namespace ElektronischesZahlenschloss.Model
{
    public class Zahlenschloss
    {
        public char Zeichen { get; internal set; }
        public int CodeAnzeige { get; set; }
        public bool P1 { get; set; } // Lampe rot
        public bool P2 { get; set; } // Lampe grün

        public Zahlenschloss()
        {
            Zeichen = ' ';
        }
    }
}