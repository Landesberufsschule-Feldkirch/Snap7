namespace WordClock
{
    public class Zeiten
    {
        public ushort DatumJahr { get; set; }
        public byte DatumMonat { get; set; }
        public byte DatumTag { get; set; }
        public byte DatumWochentag { get; set; }
        public byte Stunde { get; set; }
        public byte Minute { get; set; }
        public byte Sekunde { get; set; }
        public byte Nanosekunde { get; set; }

        public Zeiten(ushort datumJahr, byte datumMonat, byte datumTag, byte datumWochentag, byte stunde, byte minute, byte sekunde, byte nanosekunde)
        {
            DatumJahr = datumJahr;
            DatumMonat = datumMonat;
            DatumTag = datumTag;
            DatumWochentag = datumWochentag;
            Stunde = stunde;
            Minute = minute;
            Sekunde = sekunde;
            Nanosekunde = nanosekunde;
        }
    }
}